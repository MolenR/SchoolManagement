using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.MVC.Migrations
{
    /// <inheritdoc />
    public partial class RebuildDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentsId",
                table: "Enrollments",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_StudentsId",
                table: "Enrollments",
                newName: "IX_Enrollments_StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Enrollments",
                newName: "StudentsId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                newName: "IX_Enrollments_StudentsId");
        }
    }
}
