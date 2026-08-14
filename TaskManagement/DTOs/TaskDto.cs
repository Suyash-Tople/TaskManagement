using TaskManagement.Models.Enums;

namespace TaskManagement.DTOs
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = null!;
        public string Descripton { get; set; }
        public int AssigneeId { get; set; }
        public int ProjectId { get; set; }
        public ProjectTaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
    }

    public class UpdateTaskDto
    {
        public string Title { get; set; } = null!;
        public string Descripton { get; set; }
        public int AssigneeId { get; set; }
        public ProjectTaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }

    }
}
