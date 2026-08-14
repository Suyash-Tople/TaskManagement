namespace TaskManagement.DTOs
{
    public class CreateSkillDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; }
        public int DifficultyLevel { get; set; }
    }

    public class UpdateSkillDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DifficultyLevel { get; set; }
    }
}
