using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class UserSkill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Required]
        [ForeignKey(nameof(Skill))]
        public int SkillId { get; set; }

        [Range(1, 40)]
        public int ExperienceMonths { get; set; }

        [DefaultValue(false)]
        public bool IsCertified { get; set; }

        [StringLength(100)]
        public string? CertificateName { get; set; }
        public User User { get; set; } = null!;
        public Skill Skill { get; set; } = null!;
    }
}
