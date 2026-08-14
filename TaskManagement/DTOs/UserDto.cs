using System.Reflection;
using TaskManagement.Models.Enums;

namespace TaskManagement.DTOs
{
    public class CreateUserDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public decimal Salary { get; set; }
        public UserRole Role { get; set; }
    }

    public class UpdateUserDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public decimal Salary { get; set; }
        public string Gender { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
    }
}
