using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Models;

namespace TaskManagement.Data.Configurations
{
    public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
    {
        public void Configure(EntityTypeBuilder<UserSkill> builder)
        {
            //table
            builder.ToTable("UserSkills");

            //primary key
            builder.HasKey(us => us.Id);
            builder.Property(us => us.Id).ValueGeneratedOnAdd();

            //property
            builder.Property(us => us.ExperienceMonths).IsRequired();
            builder.Property(us => us.IsCertified).HasDefaultValue(false);
            builder.Property(us => us.CertificateName).HasMaxLength(100);

            //User 1 => many UserSkills
            builder.HasOne(us => us.User)
                    .WithMany(u => u.UserSkills)
                    .HasForeignKey(us => us.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            //Skill 1 => Many UserSkills
            builder.HasOne(us => us.Skill)
                .WithMany(s => s.UserSkills)
                .HasForeignKey(us => us.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            //Prevent duplicate skill assignment to the same user
            builder.HasIndex(us => new { us.UserId, us.SkillId }).IsUnique();

            builder.HasData(
                new UserSkill { Id = 1, UserId = 1, SkillId = 1, ExperienceMonths = 36, IsCertified = true, CertificateName = "Microsoft C#" },
                new UserSkill { Id = 2, UserId = 1, SkillId = 4, ExperienceMonths = 24, IsCertified = true, CertificateName = "EF Core" },

                new UserSkill { Id = 3, UserId = 2, SkillId = 2, ExperienceMonths = 30, IsCertified = true, CertificateName = "ASP.NET Core" },
                new UserSkill { Id = 4, UserId = 2, SkillId = 3, ExperienceMonths = 20, IsCertified = false },

                new UserSkill { Id = 5, UserId = 3, SkillId = 3, ExperienceMonths = 18, IsCertified = false },

                new UserSkill { Id = 6, UserId = 4, SkillId = 5, ExperienceMonths = 15, IsCertified = false },

                new UserSkill { Id = 7, UserId = 4, SkillId = 6, ExperienceMonths = 12, IsCertified = false },

                new UserSkill { Id = 8, UserId = 5, SkillId = 1, ExperienceMonths = 10, IsCertified = false },

                new UserSkill { Id = 9, UserId = 5, SkillId = 2, ExperienceMonths = 8, IsCertified = false },

                new UserSkill { Id = 10, UserId = 5, SkillId = 5, ExperienceMonths = 14, IsCertified = false }
            );
        }
    }
}
