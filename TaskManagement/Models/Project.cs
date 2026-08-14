using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required]
        [StringLength(150, ErrorMessage = "Project title is required!!")]
        public string Title { get; set; } = string.Empty;
        [StringLength(500)]
        public string? Description { get; set; }
        [Required(ErrorMessage = "CreatedBy that is UserId is required!!")]
        [ForeignKey(nameof(CreatedBy))]
        public int CreatedById { get; set; }
        [Required(ErrorMessage = "Date of Creating project is required!!")]
        public DateTime CreatedDate { get; set; } = DateTime.Today;
        public User CreatedBy { get; set; } = null!;
        //1 : many
        public ICollection<TaskItem> TaskItems { get; set; }
            = new List<TaskItem>();
    }
}
