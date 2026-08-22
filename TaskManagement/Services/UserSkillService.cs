using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.DTOs;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Models.Enums;
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
            return await _context.UserSkills
                        .Include(x => x.Skill)
                        .Include(x => x.User)
                        .Select(x => new
                        {
                            x.Id,
                            x.ExperienceMonths,
                            x.IsCertified,
                            x.CertificateName,
                            User = new
                            {
                                x.User.Name,
                                x.User.Role,
                                ProjectDetails = new
                                {
                                    x.User.Project.Title,
                                    x.User.TaskItems.Count,
                                }
                            },
                            Skill = new
                            {
                                x.Skill.Name,
                                x.Skill.DifficultyLevel
                            }
                        }).ToListAsync();
        }

        public async Task<Object> GetUserSkillsById(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if(user == null || !user.IsActive)
            {
                throw new NotFoundException("Either user does not exists or is not active.");
            }

            var userSkills = await _context.UserSkills.Include(x => x.User).Include(x => x.Skill)
                                .Where(x => x.UserId == userId)
                                .Select(x => new
                                {
                                    x.Id,
                                    User = new
                                    {
                                        x.User.Name,
                                        x.User.Project,
                                        x.User.IsActive,
                                        x.User.Role,
                                        ProjectDetails = new
                                        {
                                            x.User.Project.Title,
                                            x.User.TaskItems.Count,
                                        }
                                    },
                                    Skill = new
                                    {
                                        x.Skill.Name,
                                        x.Skill.DifficultyLevel,
                                    }
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
            return newUserSkill;
        }

        public async Task<Object> UpdateUserSkill(int id, UpdateUserSkillDto dto)
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
            return userSkills;
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
