using TaskManagement.DTOs;

namespace TaskManagement.Services.Interfaces
{
    public interface ISkillService
    {
        Task<IEnumerable<Object>> GetAllSkills();
        Task<Object> GetSkillById(int id);
        Task<Object> AddSkill(CreateSkillDto dto);
        Task<Object> UpdateSkill(int id, UpdateSkillDto dto);
        Task DeleteSkill(int id);
    }
}
