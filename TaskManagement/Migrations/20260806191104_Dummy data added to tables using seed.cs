using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagement.Migrations
{
    /// <inheritdoc />
    public partial class Dummydataaddedtotablesusingseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "Description", "DifficultyLevel", "Name" },
                values: new object[,]
                {
                    { 1, ".NET Programming", 6, "C#" },
                    { 2, "Web Development", 7, "ASP.NET Core" },
                    { 3, "Database", 5, "SQL Server" },
                    { 4, "ORM", 7, "Entity Framework" },
                    { 5, "Frontend", 3, "HTML/CSS" },
                    { 6, "Client Side", 5, "JavaScript" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "DateOfBirth", "Email", "Gender", "IsActive", "Name", "Password", "PhoneNumber", "Role", "Salary" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1995, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "john@example.com", "Male", true, "John Smith", "Password@123", "9876543210", "Manager", 50000m },
                    { 2, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1996, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice@example.com", "Female", true, "Alice Johnson", "Password@123", "9876543211", "Manager", 45000m },
                    { 3, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1998, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob@example.com", "Male", true, "Bob Williams", "Password@123", "9876543212", "Developer", 35000m },
                    { 4, new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1997, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "emma@example.com", "Female", true, "Emma Davis", "Password@123", "9876543213", "Developer", 38000m },
                    { 5, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1994, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael@example.com", "Male", true, "Michael Brown", "Password@123", "9876543214", "Tester", 42000m }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "CreatedById", "CreatedDate", "Description", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manage employees and departments", "Employee Management System" },
                    { 2, 2, new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "E-Commerce Application", "Online Shopping Portal" }
                });

            migrationBuilder.InsertData(
                table: "UserSkills",
                columns: new[] { "Id", "CertificateName", "ExperienceMonths", "IsCertified", "SkillId", "UserId" },
                values: new object[,]
                {
                    { 1, "Microsoft C#", 36, true, 1, 1 },
                    { 2, "EF Core", 24, true, 4, 1 },
                    { 3, "ASP.NET Core", 30, true, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "UserSkills",
                columns: new[] { "Id", "CertificateName", "ExperienceMonths", "SkillId", "UserId" },
                values: new object[,]
                {
                    { 4, null, 20, 3, 2 },
                    { 5, null, 18, 3, 3 },
                    { 6, null, 15, 5, 4 },
                    { 7, null, 12, 6, 4 },
                    { 8, null, 10, 1, 5 },
                    { 9, null, 8, 2, 5 },
                    { 10, null, 14, 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "TaskItems",
                columns: new[] { "TaskId", "AssigneeId", "Descripton", "DueDate", "Priority", "ProjectId", "Status", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Create login UI.", new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "High", 1, "Todo", "Design Login Page" },
                    { 2, 1, "Implement JWT authentication.", new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "High", 1, "InProgress", "Implement Authentication" },
                    { 3, 1, "Test login functionality.", new DateTime(2025, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", 1, "Done", "Write Login Tests" },
                    { 4, 2, "Develop employee CRUD module.", new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "High", 1, "Todo", "Employee CRUD" },
                    { 5, 2, "Develop department management.", new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", 1, "InProgress", "Department Module" },
                    { 6, 2, "Generate attendance reports.", new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Low", 1, "Done", "Attendance Report" },
                    { 7, 3, "Develop product catalog.", new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "High", 2, "Todo", "Product Catalog" },
                    { 8, 3, "Implement shopping cart.", new DateTime(2025, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", 2, "InProgress", "Shopping Cart" },
                    { 9, 3, "Implement checkout flow.", new DateTime(2025, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Low", 2, "Done", "Checkout Process" },
                    { 10, 4, "Integrate payment gateway.", new DateTime(2025, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "High", 2, "Todo", "Payment Gateway" },
                    { 11, 4, "Develop order history module.", new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", 2, "InProgress", "Order History" },
                    { 12, 4, "Implement inventory management.", new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Low", 2, "Done", "Inventory Module" },
                    { 13, 5, "Perform UI testing.", new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "High", 2, "Todo", "UI Testing" },
                    { 14, 5, "Fix reported bugs.", new DateTime(2025, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", 2, "InProgress", "Bug Fixing" },
                    { 15, 5, "Deploy application to production.", new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Low", 2, "Done", "Final Deployment" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "TaskId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "UserSkills",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "SkillId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "SkillId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "SkillId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "SkillId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "SkillId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "SkillId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);
        }
    }
}
