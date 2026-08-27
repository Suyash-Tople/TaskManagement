using TaskManagement.Models.Enums;

namespace TaskManagement.DTOs
{
    public class TaskItemDto
    {
        public string Title { get; set; } = null!;
        public string Descripton { get; set; }
        public int ProjectId { get; set; }
        public int CreatedById { get; set; }
        public ProjectTaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
    }
}
