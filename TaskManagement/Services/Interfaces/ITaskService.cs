using TaskManagement.DTOs;
using TaskManagement.Models.Enums;

namespace TaskManagement.Services.Interfaces
{
    public interface ITaskService
    {
        Task<Object> GetTasksByProjectId(int projectId);
        Task<Object> CreateTask(TaskItemDto dto);
        Task<Object> UpdateTask(int taskId, TaskItemDto dto);
        Task DeleteTask(int taskId);
        Task<Object> UpdateTaskStatus(int taskId, ProjectTaskStatus status);
    }
}
