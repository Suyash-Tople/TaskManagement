using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Models;
using TaskManagement.Models.Enums;

namespace TaskManagement.Data.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("TaskItems");

            //PrimaryKey
            builder.HasKey(t => t.TaskId);

            builder.Property(t => t.TaskId).ValueGeneratedOnAdd();

            //Properties
            builder.Property(t => t.Title).HasMaxLength(150);

            builder.Property(t => t.Descripton).HasMaxLength(500);

            //Enum Conversation
            builder.Property(t => t.Status).HasConversion<string>().HasMaxLength(50);

            builder.Property(t => t.Priority).HasConversion<string>().HasMaxLength(50);

            //Relationships
            //TaskItem Many => Project One

            builder.HasOne(t => t.Project)
                   .WithMany(p => p.TaskItems)
                   .HasForeignKey(t => t.ProjectId)
                   .OnDelete(DeleteBehavior.Cascade);

            //TaskItem Many => User (One)
            builder.HasOne(t => t.CreatedBy)
                .WithMany(u => u.TaskItems)
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new TaskItem
                {
                    TaskId = 1,
                    Title = "Design Login Page",
                    Descripton = "Create login UI.",
                    CreatedById = 1,
                    ProjectId = 1,
                    Status = ProjectTaskStatus.Todo,
                    Priority = Priority.High,
                    DueDate = new DateTime(2025, 03, 10)
                },

                new TaskItem
                {
                    TaskId = 2,
                    Title = "Implement Authentication",
                    Descripton = "Implement JWT authentication.",
                    CreatedById = 1,
                    ProjectId = 1,
                    Status = ProjectTaskStatus.InProgress,
                    Priority = Priority.High,
                    DueDate = new DateTime(2025, 03, 12)
                },

                new TaskItem
                {
                    TaskId = 3,
                    Title = "Write Login Tests",
                    Descripton = "Test login functionality.",
                    CreatedById = 1,
                    ProjectId = 1,
                    Status = ProjectTaskStatus.Done,
                    Priority = Priority.Medium,
                    DueDate = new DateTime(2025, 03, 14)
                },

                new TaskItem
                {
                    TaskId = 4,
                    Title = "Employee CRUD",
                    Descripton = "Develop employee CRUD module.",
                    CreatedById = 2,
                    ProjectId = 1,
                    Status = ProjectTaskStatus.Todo,
                    Priority = Priority.High,
                    DueDate = new DateTime(2025, 03, 16)
                },

                new TaskItem
                {
                    TaskId = 5,
                    Title = "Department Module",
                    Descripton = "Develop department management.",
                    CreatedById = 2,
                    ProjectId = 1,
                    Status = ProjectTaskStatus.InProgress,
                    Priority = Priority.Medium,
                    DueDate = new DateTime(2025, 03, 18)
                },

                new TaskItem
                {
                    TaskId = 6,
                    Title = "Attendance Report",
                    Descripton = "Generate attendance reports.",
                    CreatedById = 2,
                    ProjectId = 1,
                    Status = ProjectTaskStatus.Done,
                    Priority = Priority.Low,
                    DueDate = new DateTime(2025, 03, 20)
                },

                new TaskItem
                {
                    TaskId = 7,
                    Title = "Product Catalog",
                    Descripton = "Develop product catalog.",
                    CreatedById = 3,
                    ProjectId = 2,
                    Status = ProjectTaskStatus.Todo,
                    Priority = Priority.High,
                    DueDate = new DateTime(2025, 03, 22)
                },

                new TaskItem
                {
                    TaskId = 8,
                    Title = "Shopping Cart",
                    Descripton = "Implement shopping cart.",
                    CreatedById = 3,
                    ProjectId = 2,
                    Status = ProjectTaskStatus.InProgress,
                    Priority = Priority.Medium,
                    DueDate = new DateTime(2025, 03, 24)
                },

                new TaskItem
                {
                    TaskId = 9,
                    Title = "Checkout Process",
                    Descripton = "Implement checkout flow.",
                    CreatedById = 3,
                    ProjectId = 2,
                    Status = ProjectTaskStatus.Done,
                    Priority = Priority.Low,
                    DueDate = new DateTime(2025, 03, 26)
                },

                new TaskItem
                {
                    TaskId = 10,
                    Title = "Payment Gateway",
                    Descripton = "Integrate payment gateway.",
                    CreatedById = 4,
                    ProjectId = 2,
                    Status = ProjectTaskStatus.Todo,
                    Priority = Priority.High,
                    DueDate = new DateTime(2025, 03, 28)
                },

                new TaskItem
                {
                    TaskId = 11,
                    Title = "Order History",
                    Descripton = "Develop order history module.",
                    CreatedById = 4,
                    ProjectId = 2,
                    Status = ProjectTaskStatus.InProgress,
                    Priority = Priority.Medium,
                    DueDate = new DateTime(2025, 03, 30)
                },

                new TaskItem
                {
                    TaskId = 12,
                    Title = "Inventory Module",
                    Descripton = "Implement inventory management.",
                    CreatedById = 4,
                    ProjectId = 2,
                    Status = ProjectTaskStatus.Done,
                    Priority = Priority.Low,
                    DueDate = new DateTime(2025, 04, 02)
                },

                new TaskItem
                {
                    TaskId = 13,
                    Title = "UI Testing",
                    Descripton = "Perform UI testing.",
                    CreatedById = 5,
                    ProjectId = 2,
                    Status = ProjectTaskStatus.Todo,
                    Priority = Priority.High,
                    DueDate = new DateTime(2025, 04, 04)
                },

                new TaskItem
                {
                    TaskId = 14,
                    Title = "Bug Fixing",
                    Descripton = "Fix reported bugs.",
                    CreatedById = 5,
                    ProjectId = 2,
                    Status = ProjectTaskStatus.InProgress,
                    Priority = Priority.Medium,
                    DueDate = new DateTime(2025, 04, 06)
                },

                new TaskItem
                {
                    TaskId = 15,
                    Title = "Final Deployment",
                    Descripton = "Deploy application to production.",
                    CreatedById = 5,
                    ProjectId = 2,
                    Status = ProjectTaskStatus.Done,
                    Priority = Priority.Low,
                    DueDate = new DateTime(2025, 04, 08)
                }
            );
        }
    }
}
