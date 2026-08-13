using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Models;
using TaskManagement.Models.Enums;

namespace TaskManagement.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            //PrimaryKey
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId).ValueGeneratedOnAdd();

            //Decimal Precision
            builder.Property(u => u.Salary).HasPrecision(10, 2);

            //Store enum as string
            builder.Property(u => u.Role).HasConversion<string>();

            //Default values
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("GetDate()");

            builder.Property(u => u.IsActive).HasDefaultValue(true);

            //Unique Indexes
            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasIndex(u => u.PhoneNumber).IsUnique().HasFilter("[PhoneNumber] IS NOT NULL");

            //Relationships
            // User (1) -> (1) Project
            builder.HasOne(u => u.Project)
                   .WithOne(p => p.CreatedBy)
                   .HasForeignKey<Project>(p => p.CreatedById);

            builder.HasMany(u => u.TaskItems)
                   .WithOne(t => t.Assignee)
                   .HasForeignKey(t => t.AssigneeId);

            builder.HasMany(u => u.UserSkills)
                   .WithOne(us => us.User)
                   .HasForeignKey(us => us.UserId);

            builder.HasData(
                new User
                {
                    UserId = 1,
                    Name = "John Smith",
                    Email = "john@example.com",
                    Password = "Password@123",
                    PhoneNumber = "9876543210",
                    DateOfBirth = new DateTime(1995, 5, 10),
                    Salary = 50000,
                    Gender = "Male",
                    Role = UserRole.Manager,
                    CreatedAt = new DateTime(2025, 1, 1),
                    IsActive = true
                },
                new User
                {
                    UserId = 2,
                    Name = "Alice Johnson",
                    Email = "alice@example.com",
                    Password = "Password@123",
                    PhoneNumber = "9876543211",
                    DateOfBirth = new DateTime(1996, 7, 15),
                    Salary = 45000,
                    Gender = "Female",
                    Role = UserRole.Manager,
                    CreatedAt = new DateTime(2025, 1, 2),
                    IsActive = true
                },
                new User
                {
                    UserId = 3,
                    Name = "Bob Williams",
                    Email = "bob@example.com",
                    Password = "Password@123",
                    PhoneNumber = "9876543212",
                    DateOfBirth = new DateTime(1998, 3, 20),
                    Salary = 35000,
                    Gender = "Male",
                    Role = UserRole.Developer,
                    CreatedAt = new DateTime(2025, 1, 3),
                    IsActive = true
                },
                new User
                {
                    UserId = 4,
                    Name = "Emma Davis",
                    Email = "emma@example.com",
                    Password = "Password@123",
                    PhoneNumber = "9876543213",
                    DateOfBirth = new DateTime(1997, 9, 12),
                    Salary = 38000,
                    Gender = "Female",
                    Role = UserRole.Developer,
                    CreatedAt = new DateTime(2025, 1, 4),
                    IsActive = true
                },
                new User
                {
                    UserId = 5,
                    Name = "Michael Brown",
                    Email = "michael@example.com",
                    Password = "Password@123",
                    PhoneNumber = "9876543214",
                    DateOfBirth = new DateTime(1994, 11, 5),
                    Salary = 42000,
                    Gender = "Male",
                    Role = UserRole.Tester,
                    CreatedAt = new DateTime(2025, 1, 5),
                    IsActive = true
                }
           );
        }
    }
}
