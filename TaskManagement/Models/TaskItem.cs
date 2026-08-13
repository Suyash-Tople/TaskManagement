using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagement.Models.Enums;

namespace TaskManagement.Models
{
    public class TaskItem
    {
        [Key]
        [Required]
        public int TaskId { get; set; }
        [StringLength(150, ErrorMessage = "Task title is required!!")]
        public string Title { get; set; } = string.Empty;
        [StringLength(5000)]
        public string? Descripton { get; set; }
        [ForeignKey(nameof(Assignee))]
        public int AssigneeId { get; set; }
        [Required]
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
        [Required]
        public ProjectTaskStatus Status { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public Priority Priority { get; set; } = Priority.Low;

        public User Assignee { get; set; } = null!;
        public Project Project { get; set; } = null!;
    }
}
