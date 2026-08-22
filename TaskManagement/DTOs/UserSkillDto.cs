namespace TaskManagement.DTOs
{
    public class AddSkillToUserDto
    {
        public int UserId { get; set; }

        public int SkillId { get; set; } 
        public int ExperienceMonths { get; set; }
        public bool IsCertified { get; set; }
        public string? CertificateName { get; set; }
    }

    public class UpdateUserSkillDto
    {
        public int UserSkillId { get; set; }
        public int ExperienceMonths { get; set; }
        public bool IsCertified { get; set; }
        public string? CertificateName { get; set; }
    }
}
