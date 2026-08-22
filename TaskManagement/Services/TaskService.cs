using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Object>> GetAllTaskItems()
        {
            return await _context.TaskItems
                    .ToListAsync();
        }

        public async Task<Object> GetTasksByProjectId(int projectId)
        {
            var taskItems = await _context.TaskItems.Where(x=> x.ProjectId == projectId).ToListAsync();
            if (taskItems == null)
            {
                throw new NotFoundException($"There is no task assinged for ProjectId: {projectId}");
            }
            return taskItems;
        }

        public async Task<Object> CreateTask(CreateTaskDto dto)
        {
            var userActive = await _context.Users.Where(x => x.UserId == dto.AssigneeId).Select(x => x.IsActive).FirstOrDefaultAsync();
            if (!userActive)
            {
                throw new BusinessException("Either User does not exists or is inactive");
            }

            var userTasks = await _context.TaskItems.Where(x => x.AssigneeId == dto.AssigneeId).ToListAsync();
            int taskCounts = userTasks.Where(x => x.Status == ProjectTaskStatus.Todo && x.Status == ProjectTaskStatus.InProgress).Count();

            if (taskCounts >= 3)
            {
                throw new BusinessException("New Task cannot be assinged to user having 3 task undone.");
            }

            if (userTasks.Any(x => x.Title == dto.Title && x.AssigneeId == dto.AssigneeId))
            {
                throw new DuplicateException("Same title exists for the user");
            }

            var taskItem = new TaskItem
            {
                Title = dto.Title,
                Descripton = dto.Descripton,
                AssigneeId = dto.AssigneeId,
                ProjectId = dto.ProjectId,
                Status = dto.Status,
                DueDate = dto.DueDate,
                Priority = dto.Priority
            };

            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();
            return taskItem;
        }

        public async Task<Object> UpdateTask(int taskId, UpdateTaskDto dto)
        {
            var userActive = await _context.Users.Where(x => x.UserId == dto.AssigneeId).Select(x => x.IsActive).FirstOrDefaultAsync();
            if (!userActive)
            {
                throw new BusinessException("Either User does not exists or is inactive");
            }

            var taskItem = await _context.TaskItems.FindAsync(taskId);
            if(taskItem == null)
            {
                throw new NotFoundException($"TaskItem with taskID {taskId} does not exists");
            }

            var userTasks = await _context.TaskItems.Where(x => x.AssigneeId == dto.AssigneeId).ToListAsync();
            int taskCounts = userTasks.Where(x => x.Status == ProjectTaskStatus.Todo && x.Status == ProjectTaskStatus.InProgress).Count();

            if (taskCounts >= 3)
            {
                throw new BusinessException("New Task cannot be assinged to user having 3 task undone.");
            }

            taskItem.Title = dto.Title;
            taskItem.Descripton = dto.Descripton;
            taskItem.AssigneeId = dto.AssigneeId;
            taskItem.Status = dto.Status;
            taskItem.DueDate = dto.DueDate;
            taskItem.Priority = dto.Priority;

            await _context.SaveChangesAsync();
            return taskItem;
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
    }
}
