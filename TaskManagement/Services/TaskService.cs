using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.DTOs;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Enums;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Object> GetTasksByProjectId(int projectId)
        {
            var project = await _context.Projects.Where(p => p.ProjectId == projectId)
                .Select(p => new
                {
                    p.ProjectId,
                    p.Title,
                    CreatedBy = p.CreatedBy.Name,
                    Tasks = p.TaskItems.Select(t => new
                    {
                        t.TaskId,
                        t.Title,
                        t.Descripton,
                        t.Status,
                        t.Priority,
                        t.CreatedBy.Name,
                        t.DueDate
                    }).ToList()
                }).FirstOrDefaultAsync();
            if (project == null)
            {
                throw new NotFoundException($"There is no task assinged for ProjectId: {projectId}");
            }
            return project;
        }

        public async Task<Object> CreateTask(TaskItemDto dto)
        {
            var userActive = await _context.Users.Where(x => x.UserId == dto.CreatedById).Select(x => x.IsActive).FirstOrDefaultAsync();
            if (!userActive)
            {
                throw new BusinessException("Either User does not exists or is inactive");
            }

            var userTasks = await _context.TaskItems.Where(x => x.CreatedById == dto.CreatedById).ToListAsync();
            var activeTasks = userTasks.Where(x => x.Status == ProjectTaskStatus.Todo || x.Status == ProjectTaskStatus.InProgress).ToList();
            int taskCount = activeTasks.Count();
            if (taskCount >= 3)
            {
                throw new BusinessException("New Task cannot be assinged to user having 3 task undone.");
            }

            var hasTaskInAnotherProject = activeTasks.Any(x => x.ProjectId != dto.ProjectId);
            if (hasTaskInAnotherProject)
            {
                throw new BusinessException(
                    "User cannot be assigned to another project while having active tasks in a different project.");
            }

            if (userTasks.Any(x => x.Title == dto.Title && x.CreatedById == dto.CreatedById && x.ProjectId == dto.ProjectId))
            {
                throw new DuplicateException("Same title exists for the user");
            }

            var taskItem = new TaskItem
            {
                Title = dto.Title,
                Descripton = dto.Descripton,
                CreatedById = dto.CreatedById,
                ProjectId = dto.ProjectId,
                Status = dto.Status,
                DueDate = dto.DueDate,
                Priority = dto.Priority
            };
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();
            return new { TaskItemId = taskItem.TaskId };
        }

        public async Task<Object> UpdateTask(int taskId, TaskItemDto dto)
        {
            var userActive = await _context.Users.Where(x => x.UserId == dto.CreatedById).Select(x => x.IsActive).FirstOrDefaultAsync();
            if (!userActive)
            {
                throw new BusinessException("Either User does not exists or is inactive");
            }

            var taskItem = await _context.TaskItems.FindAsync(taskId);
            if (taskItem == null)
            {
                throw new NotFoundException($"TaskItem with taskID {taskId} does not exists");
            }

            // 3. Get other active tasks of the user
            var activeTasks = await _context.TaskItems
                .Where(x =>
                    x.CreatedById == dto.CreatedById &&
                    x.TaskId != taskId &&
                    (x.Status == ProjectTaskStatus.Todo ||
                     x.Status == ProjectTaskStatus.InProgress))
                .ToListAsync();

            // 4. Maximum 3 active tasks
            if (activeTasks.Count >= 3)
            {
                throw new BusinessException(
                    "User cannot have more than 3 unfinished tasks.");
            }

            // 5. User cannot work on another project
            var hasTaskInAnotherProject = activeTasks
                .Any(x => x.ProjectId != dto.ProjectId);

            if (hasTaskInAnotherProject)
            {
                throw new BusinessException(
                    "User cannot be assigned to another project while having active tasks in a different project.");
            }

            taskItem.Title = dto.Title;
            taskItem.Descripton = dto.Descripton;
            taskItem.CreatedById = dto.CreatedById;
            taskItem.Status = dto.Status;
            taskItem.DueDate = dto.DueDate;
            taskItem.Priority = dto.Priority;
            taskItem.ProjectId = dto.ProjectId;
            await _context.SaveChangesAsync();
            return new { TaskItemId = taskItem.TaskId };
        }

        public async Task DeleteTask(int taskId)
        {
            var taskItem = await _context.TaskItems.FindAsync(taskId);
            if (taskItem == null)
            {
                throw new NotFoundException($"TaskItem with taskID {taskId} does not exists");
            }
            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task<Object> UpdateTaskStatus(int taskId, ProjectTaskStatus status)
        {
            var taskExists = await _context.TaskItems.Where(x => x.TaskId == taskId).FirstOrDefaultAsync();
            if(taskExists ==  null)
            {
                throw new NotFoundException($"No task found for task id {taskId}.");
            }
            taskExists.Status = status;
            await _context.SaveChangesAsync();
            return new { updatedStatus = taskExists.Status };
        }
    }
}
