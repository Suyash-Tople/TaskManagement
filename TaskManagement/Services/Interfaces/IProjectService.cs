using TaskManagement.DTOs;

namespace TaskManagement.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<object>> GetProjectsAsync();
        Task<Object> GetProjectByIdAsync(int id);
        Task<Object> CreateProjectAsync(CreateProjectDto dto);
        Task<Object> UpdateProjectAsync(int id, UpdateProjectDto dto);
        Task DeleteProjectAsync(int id);
    }
}
