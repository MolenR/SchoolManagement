using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.MVC.Migrations
{
    /// <inheritdoc />
    public partial class AddedNullClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Classes__CourseI__3F466844",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK__Classes__Lecture__3E52440B",
                table: "Classes");

            migrationBuilder.AlterColumn<int>(
                name: "LectureId",
                table: "Classes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Classes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK__Classes__CourseI__3F466844",
                table: "Classes",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Classes__Lecture__3E52440B",
                table: "Classes",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Classes__CourseI__3F466844",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK__Classes__Lecture__3E52440B",
                table: "Classes");

            migrationBuilder.AlterColumn<int>(
                name: "LectureId",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Classes__CourseI__3F466844",
                table: "Classes",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Classes__Lecture__3E52440B",
                table: "Classes",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
