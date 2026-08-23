using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.DTOs;
using TaskManagement.Exceptions;
using TaskManagement.Models;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Object>> GetUserAsync()
        {
            var users = await _context.Users
            .Select(x => new
            {
                x.UserId,
                x.Name,
                x.Email,
                x.PhoneNumber,
                x.DateOfBirth,
                x.Salary,
                x.Gender,
                x.Role,
                x.CreatedAt,
                x.IsActive,

                Skills = x.UserSkills.Select(us => new
                {
                    us.Skill.Name,
                    us.ExperienceMonths,
                    us.IsCertified
                })
            })
            .ToListAsync();
            return users; 
        }

        public async Task<object> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.Select(
                x => new
                {
                    x.UserId,
                    x.Name,
                    x.Email,
                    x.PhoneNumber,
                    x.DateOfBirth,
                    x.Salary,
                    x.Gender,
                    x.Role,
                    x.CreatedAt,
                    x.IsActive,
                    Skills = x.UserSkills.Select(us => new
                    {
                        us.Skill.Name,
                        us.ExperienceMonths,
                        us.IsCertified
                    })
                }).FirstOrDefaultAsync(x => x.UserId == id);

            if(user == null)
            {
                throw new NotFoundException("User not found");
            }

            return user;
        }

        public async Task<object> CreateUserAsync(CreateUserDto dto)
        {
            if(dto.Password != dto.ConfirmPassword)
            {
                throw new ValidationException("Password and ConfirmPassword do not match.");
            }

            var emailExist = await _context.Users.AnyAsync(x => x.Email == dto.Email);
            if (emailExist)
            {
                throw new DuplicateException("A user with this email already exists.");
            }

            var phoneExists = await _context.Users.AnyAsync(x => x.PhoneNumber == dto.PhoneNumber);
            if(phoneExists)
            {
                throw new DuplicateException("A user with this phone number already exists.");
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                PhoneNumber = dto.PhoneNumber,
                DateOfBirth = dto.DateOfBirth,
                Salary = dto.Salary,
                Gender = dto.Gender,
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new
            {
                user.UserId,
                user
            };
        }

        public async Task<object> UpdateUserAsync(int id, UserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if(user == null)
            {
                throw new NotFoundException("user not found.");
            }

            var emailExists = await _context.Users.AnyAsync(x => x.Email == dto.Email && x.UserId != id);
            if(emailExists)
            {
                throw new DuplicateException("Another user already has this email.");
            }

            var phoneExists = await _context.Users.AnyAsync(x => x.PhoneNumber == dto.PhoneNumber);
            if (phoneExists)
            {
                throw new DuplicateException("Another user already has this phone number.");
            }

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.DateOfBirth = dto.DateOfBirth;
            user.Salary = dto.Salary;
            user.Gender = dto.Gender;
            user.Role = dto.Role;
            user.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if(user == null)
            {
                throw new NotFoundException("User not found.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
