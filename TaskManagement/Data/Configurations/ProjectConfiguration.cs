using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Models;

namespace TaskManagement.Data.Configurations
{
    public class ProjectConfiguration :  IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            //table
            builder.ToTable("Projects");

            //primary key
            builder.HasKey(p => p.ProjectId);

            builder.Property(p => p.ProjectId).ValueGeneratedOnAdd();

            //unique property title
            builder.HasIndex(p => p.Title).IsUnique();

            //Default Value
            builder.Property(p => p.CreatedDate).HasDefaultValueSql("GETDATE()");

            //User 1 => Project 1
            builder.HasOne(p => p.CreatedBy)
                    .WithOne(u => u.Project)
                    .HasForeignKey<Project>(p => p.CreatedById);

            //ReEnforce one to one
            builder.HasIndex(p => p.CreatedById)
                    .IsUnique();

            //Project 1 => Many TaskItems
            builder.HasMany(p => p.TaskItems)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

            builder.HasData(
                new Project
                {
                    ProjectId = 1,
                    Title = "Employee Management System",
                    Description = "Manage employees and departments",
                    CreatedById = 1,
                    CreatedDate = new DateTime(2025, 2, 1)
                },
                new Project
                {
                    ProjectId = 2,
                    Title = "Online Shopping Portal",
                    Description = "E-Commerce Application",
                    CreatedById = 2,
                    CreatedDate = new DateTime(2025, 2, 5)
                }
            );
        }
    }
}
