using TaskManagement.DTOs;

namespace TaskManagement.Services.Interfaces
{
    public interface IUserSkillsService
    {
        Task<IEnumerable<Object>> GetAllUsersSkills();
        Task<Object> GetUserSkillsById(int userId);
        Task<Object> AddSkillToUser(AddSkillToUserDto dto);
        Task<Object> UpdateUserSkill(int id, UpdateUserSkillDto dto);
        Task DeleteUserSkill(int id);
    }
}
