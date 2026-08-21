using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.DTOs;
using TaskManagement.Exceptions;
using TaskManagement.Models;
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
                .Include(x => x.CreatedBy)
                .Include(x => x.TaskItems)
                .Select(x => new
                {
                    x.ProjectId,
                    x.Title,
                    x.Description,
                    x.CreatedDate,

                    CreatedBy = new
                    {
                        x.CreatedBy.UserId,
                        x.CreatedBy.Name
                    },

                    TaskCount = x.TaskItems.Count()
                }).ToListAsync();
        }

        public async Task<Object> GetProjectByIdAsync(int id)
        {
            var project = await _context.Projects
                .Include(x => x.CreatedBy)
                .Include(x => x.TaskItems)
                .FirstOrDefaultAsync(x => x.ProjectId == id);

            if(project == null)
            {
                throw new NotFoundException("Project not found");
            }
            return project;
        }

        public async Task<Object> CreateProjectAsync(CreateProjectDto dto)
        {
            var creator = await _context.Users.FirstOrDefaultAsync(x => x.UserId == dto.CreatedById);
            if(creator == null)
            {
                throw new NotFoundException("Creator user does not exist");
            }

            if (!creator.IsActive)
            {
                throw new BusinessException("Inactive user cannot create a new project");
            }

            var duplicate = await _context.Projects.AnyAsync(x => x.Title == dto.Title);
            if (duplicate)
            {
                throw new DuplicateException("A project with this title already exists");
            }

            var alreadyHasProject = await _context.Projects.AnyAsync(x => x.CreatedById == dto.CreatedById);
            if(alreadyHasProject)
            {
                throw new DuplicateException("This user already has a project");
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

        public async Task<Object> UpdateProjectAsync(int id, UpdateProjectDto dto)
        {
            var project = await _context.Projects.FindAsync(id);
            if(project == null)
            {
                throw new NotFoundException("Project not found");
            }

            var duplicate = await _context.Projects.AnyAsync(x => x.Title == dto.Title && x.ProjectId != id);
            if(duplicate)
            {
                throw new DuplicateException("Another Project with this title already exists");
            }

            project.Title = dto.Title;
            project.Description = dto.Description;

            await _context.SaveChangesAsync();
            return project;
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if(project == null)
            {
                throw new NotFoundException("Project not found");
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}
