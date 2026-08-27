using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Migrations
{
    /// <inheritdoc />
    public partial class CorrectedNaminginTaskItementity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Users_AssigneeId",
                table: "TaskItems");

            migrationBuilder.RenameColumn(
                name: "AssigneeId",
                table: "TaskItems",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItems_AssigneeId",
                table: "TaskItems",
                newName: "IX_TaskItems_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Users_CreatedById",
                table: "TaskItems",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Users_CreatedById",
                table: "TaskItems");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "TaskItems",
                newName: "AssigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItems_CreatedById",
                table: "TaskItems",
                newName: "IX_TaskItems_AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Users_AssigneeId",
                table: "TaskItems",
                column: "AssigneeId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
