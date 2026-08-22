using TaskManagement.DTOs;

namespace TaskManagement.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<Object>> GetAllTaskItems();
        Task<Object> GetTasksByProjectId(int projectId);
        Task<Object> CreateTask(CreateTaskDto dto);
        Task<Object> UpdateTask(int taskId, UpdateTaskDto dto);
        Task DeleteTask(int taskId);
    }
}
