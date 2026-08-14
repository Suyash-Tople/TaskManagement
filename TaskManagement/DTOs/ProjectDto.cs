namespace TaskManagement.DTOs
{
    public class CreateProjectDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CreatedById { get; set; } 
    }

    public class UpdateProjectDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
