using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Models;

namespace TaskManagement.Data.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            //table
            builder.ToTable("Skills");

            //Primary key
            builder.HasKey(s => s.SkillId);
            builder.Property(s => s.SkillId).ValueGeneratedOnAdd();

            //Properties
            builder.Property(s => s.Name)
                .IsRequired().HasMaxLength(40);

            builder.Property(s => s.DifficultyLevel).IsRequired();

            builder.Property(s => s.Description).HasMaxLength(200);

            //Unique Skill Name
            builder.HasIndex(s => s.Name).IsUnique();

            //Skill 1 to Many UserSkills
            builder.HasMany(s => s.UserSkills)
                .WithOne(us => us.Skill)
                .HasForeignKey(us => us.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Skill { SkillId = 1, Name = "C#", Description = ".NET Programming", DifficultyLevel = 6 },
                new Skill { SkillId = 2, Name = "ASP.NET Core", Description = "Web Development", DifficultyLevel = 7 },
                new Skill { SkillId = 3, Name = "SQL Server", Description = "Database", DifficultyLevel = 5 },
                new Skill { SkillId = 4, Name = "Entity Framework", Description = "ORM", DifficultyLevel = 7 },
                new Skill { SkillId = 5, Name = "HTML/CSS", Description = "Frontend", DifficultyLevel = 3 },
                new Skill { SkillId = 6, Name = "JavaScript", Description = "Client Side", DifficultyLevel = 5 }
            );
        }
    }
}
