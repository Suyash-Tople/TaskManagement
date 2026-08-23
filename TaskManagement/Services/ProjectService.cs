using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.DTOs;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Enums;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Object>> GetProjectsAsync()
        {
            return await _context.Projects
                .Select(x => new
                {
                    x.ProjectId,
                    x.Title,
                    x.Description,
                    x.CreatedDate,
                    x.IsActive
                }).ToListAsync();
        }

        public async Task<Object> GetProjectByIdAsync(int projectId)
        {
            var project = await _context.Projects
                .Select(x => new
                {
                    x.ProjectId,
                    x.Title,
                    x.Description,
                    x.CreatedDate,
                    x.IsActive,
                    CreatedBy = new
                    {
                        x.CreatedBy.Name,
                        x.CreatedBy.Role
                    },
                    TaskItem = new
                    {
                        TaskCount = x.TaskItems.Count(),
                        PendingTask = x.TaskItems.Where(x => x.Status == ProjectTaskStatus.Todo).Count(),
                        InProgress = x.TaskItems.Where(x => x.Status == ProjectTaskStatus.InProgress).Count(),
                        Done = x.TaskItems.Where(x => x.Status == ProjectTaskStatus.Done).Count()
                    }
                })
                .FirstOrDefaultAsync(x => x.ProjectId == projectId);

            if(project == null)
            {
                throw new NotFoundException("Project not found");
            }
            return project;
        }

        public async Task<Object> CreateProjectAsync(CreateProjectDto dto)
        {
            var creator = await _context.Users.FirstOrDefaultAsync(x => x.UserId == dto.CreatedById && x.Role == UserRole.Manager);
            if(creator == null)
            {
                throw new NotFoundException("Creator user does not exist or role is not manager.");
            }

            if (!creator.IsActive)
            {
                throw new BusinessException("Inactive Manager cannot create a new project.");
            }

            var duplicate = await _context.Projects.AnyAsync(x => x.Title == dto.Title);
            if (duplicate)
            {
                throw new DuplicateException("A project with this title already exists.");
            }

            var alreadyHasProject = await _context.Projects.AnyAsync(x => x.CreatedById == dto.CreatedById && x.IsActive == true);
            if(alreadyHasProject)
            {
                throw new DuplicateException("This Manager already has a project");
            }

            var project = new Project
            {
                Title = dto.Title,
                Description = dto.Description,
                CreatedById = dto.CreatedById,
                CreatedDate = DateTime.UtcNow,
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Object> UpdateProjectAsync(int projectId, int managerId, ProjectDto dto)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
            {
                throw new NotFoundException("Project not found");
            }

            if (!(project.CreatedById == managerId))
            {
                throw new BusinessException("Project can only be updated by creator or project manager");
            }

            var duplicate = await _context.Projects.AnyAsync(x => x.Title == dto.Title && x.ProjectId != projectId);
            if(duplicate)
            {
                throw new DuplicateException("Another Project with this title already exists");
            }

            project.Title = dto.Title;
            project.Description = dto.Description;

            await _context.SaveChangesAsync();
            return project;
        }

        public async Task DeleteProjectAsync(int projectId, int managerId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if(project == null)
            {
                throw new NotFoundException("Project not found");
            }
            if(!(project.CreatedById == managerId))
            {
                throw new BusinessException("Project can only be updated by creator or project manager");
            }
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }

        public async Task<Object> ChangeProjectManager(int projectId, int currentManagerId, int newManagerId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            if(project == null)
            {
                throw new NotFoundException("Project does not exists.");
            }
            if(project.CreatedById != currentManagerId)
            {
                throw new BusinessException("Project Manager can only changed by current project manager or creator.");
            }
            var newManagerAlreadyHasActiveProject = await _context.Projects.AnyAsync(x => x.CreatedById == newManagerId && x.IsActive == true);
            if (newManagerAlreadyHasActiveProject)
            {
                throw new BusinessException("The selected new manager already has an active project.");
            }

            var userExists = await _context.Users.FindAsync(newManagerId);
            if (userExists == null)
            {
                throw new NotFoundException("Assinged user is does not exists");
            }

            if(userExists.Role != UserRole.Manager || userExists.IsActive == false)
            {
                throw new BusinessException("Either User role is not manager or user is not active");
            }

            project.CreatedById = newManagerId;
            project.IsActive = true;
            await _context.SaveChangesAsync();
            return new
            {
                Title = project.Title,
                Name = userExists.Name,
                Role = userExists.Role,
            };
        }
    }
}
