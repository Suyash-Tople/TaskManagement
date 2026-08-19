using TaskManagement.DTOs;

namespace TaskManagement.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Object>> GetUserAsync();
        Task<Object> GetUserByIdAsync(int id);
        Task<Object> CreateUserAsync(CreateUserDto dto);
        Task<Object> UpdateUserAsync(int id, UpdateUserDto dto);
        Task DeleteAsync(int id);
    }
}
