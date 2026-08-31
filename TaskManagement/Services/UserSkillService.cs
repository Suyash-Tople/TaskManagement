using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.DTOs;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services
{
    public class UserSkillService : IUserSkillsService
    {
        private readonly AppDbContext _context;
        public UserSkillService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Object>> GetAllUsersSkills()
        {
            return await _context.Users
                        .Select(x => new
                        {
                            x.UserId,
                            x.Name,
                            x.Role,
                            Skill = x.UserSkills.Select(us => new {
                                us.Id,
                                us.Skill.Name,
                                us.ExperienceMonths,
                                us.IsCertified,
                                us.CertificateName,
                                }).ToList()
                        }).ToListAsync();
        }

        public async Task<Object> GetUserSkillsById(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if(user == null || !user.IsActive)
            {
                throw new NotFoundException("Either user does not exists or is not active.");
            }

            var userSkills = await _context.Users
                             .Where(u => u.UserId == userId)
                             .Select(u => new
                             {
                                 u.Name,
                                 u.Role,
                                 u.IsActive,
                                 Skills = u.UserSkills.Select(us => new
                                 {
                                     us.Id,
                                     us.Skill.Name,
                                     us.ExperienceMonths,
                                     us.IsCertified,
                                     us.CertificateName
                                 }).ToList()
                             }).ToListAsync();
            return userSkills;
        }

        public async Task<Object> AddSkillToUser(AddSkillToUserDto dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null || !user.IsActive)
            {
                throw new NotFoundException("Either user does not exists or is not active.");
            }

            var userSkills = await _context.UserSkills.Where(x => x.UserId == dto.UserId).ToListAsync();

            if(userSkills.Any(x => x.SkillId == dto.SkillId))
            {
                throw new DuplicateException("Already skill added to user");
            }

            UserSkill newUserSkill = new UserSkill
            {
                UserId = dto.UserId,
                SkillId = dto.SkillId,
                ExperienceMonths = dto.ExperienceMonths,
                IsCertified = dto.IsCertified,
                CertificateName = dto.CertificateName
            };

            _context.UserSkills.Add(newUserSkill);
            await _context.SaveChangesAsync();
            return new { userId = user.UserId };
        }

        public async Task<Object> UpdateUserSkill(int userId, UpdateUserSkillDto dto)
        {
            var userSkills = await _context.UserSkills.FindAsync(dto.UserSkillId);

            if(userSkills == null)
            {
                throw new NotFoundException("Not found skill for specified user.");
            }

            userSkills.ExperienceMonths = dto.ExperienceMonths;
            userSkills.IsCertified = dto.IsCertified;
            userSkills.CertificateName = dto.CertificateName;

            await _context.SaveChangesAsync();
            return new { userId = userId };
        }

        public async Task DeleteUserSkill(int id)
        {
            var userSkillExists = await _context.UserSkills.FindAsync(id);
            if(userSkillExists == null)
            {
                throw new NotFoundException("User Skill does not exists");
            }

            _context.UserSkills.Remove(userSkillExists);
            await _context.SaveChangesAsync();
        }
    }
}
