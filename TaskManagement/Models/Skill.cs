using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 5)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(250)]
        public string? Description { get; set; }

        [Required]
        [Range(1, 10)]
        public int DifficultyLevel { get; set; }
        public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
    }
}
