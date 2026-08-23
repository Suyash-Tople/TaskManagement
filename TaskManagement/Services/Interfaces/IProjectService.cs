using TaskManagement.DTOs;

namespace TaskManagement.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<object>> GetProjectsAsync();
        Task<Object> GetProjectByIdAsync(int projectId);
        Task<Object> CreateProjectAsync(CreateProjectDto dto);
        Task<Object> UpdateProjectAsync(int projectId, int managerId, ProjectDto dto);
        Task DeleteProjectAsync(int projectId, int managerId);
        Task<Object> ChangeProjectManager(int projectId, int currentManagerId, int newManagerId);
    }
}
