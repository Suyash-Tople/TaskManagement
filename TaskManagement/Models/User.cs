using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Name is required and should be of atleast minimum length of 5.")]
        [StringLength(100, MinimumLength = 5)]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [NotMapped]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(0, 1000000)]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [RegularExpression("Male|Female|Other")]
        public string Gender { get; set; } = "Male";

        [Required]
        public UserRole Role { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DefaultValue(true)]
        public bool IsActive { get; set; } = true;

        //Navigation Properties

        //1 : 1 
        public Project Project { get; set; }

        //1 : Many
        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();

        //Many : Many
        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();

    }
}
