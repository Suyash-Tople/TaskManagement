using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.DTOs;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services
{
    public class SkillService : ISkillService
    {
        private readonly AppDbContext _context;
        public SkillService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Object>> GetAllSkills()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<Object> GetSkillById(int id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if(skill == null)
            {
                throw new NotFoundException($"There is no skill for skill id: {id}");
            }

            return skill;
        }

        public async Task<Object> AddSkill(CreateSkillDto dto)
        {
            var skillExists = await _context.Skills.AnyAsync(x => x.Name == dto.Name);
            if(skillExists)
            {
                throw new NotFoundException("There is already a skill with same skill name exists.");
            }

            Skill newSkill = new Skill
            {
                Name = dto.Name,
                Description = dto.Description,
                DifficultyLevel = dto.DifficultyLevel
            };

            _context.Skills.Add(newSkill);
            await _context.SaveChangesAsync();
            return newSkill;
        }

        public async Task<Object> UpdateSkill(int id, UpdateSkillDto dto)
        {
            var skillExists = await _context.Skills.FindAsync(id);
            if(skillExists== null)
            {
                throw new NotFoundException($"No skill exists with id: {id}");
            }

            skillExists.Name = dto.Name;
            skillExists.DifficultyLevel = dto.DifficultyLevel;
            skillExists.Description = dto.Description;

            await _context.SaveChangesAsync();
            return skillExists;
        }

        public async Task DeleteSkill(int id)
        {
            var skillExists = await _context.Skills.FindAsync(id);
            if (skillExists == null)
            {
                throw new NotFoundException($"No skill exists with id: {id}");
            }

            _context.Skills.Remove(skillExists);
            await _context.SaveChangesAsync();
        }
    }
}
