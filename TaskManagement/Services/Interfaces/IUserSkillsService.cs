using TaskManagement.DTOs;

namespace TaskManagement.Services.Interfaces
{
    public interface IUserSkillsService
    {
        Task<IEnumerable<Object>> GetAllUsersSkills();
        Task<Object> GetUserSkillsById(int userId);
        Task<Object> AddSkillToUser(AddSkillToUserDto dto);
        Task<Object> UpdateUserSkill(int userId, UpdateUserSkillDto dto);
        Task DeleteUserSkill(int id);
    }
}
