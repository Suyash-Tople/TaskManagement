namespace TaskManagement.DTOs
{
    public class CreateSkillDto : UpdateSkillDto
    {
        public string Name { get; set; } = null!;
    }

    public class UpdateSkillDto
    {
        public string Description { get; set; } = null!;
        public int DifficultyLevel { get; set; }
    }
}
