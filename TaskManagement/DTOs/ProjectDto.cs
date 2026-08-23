namespace TaskManagement.DTOs
{
    public class ProjectDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public class CreateProjectDto : ProjectDto
    {
        public int CreatedById { get; set; }

    }
}
