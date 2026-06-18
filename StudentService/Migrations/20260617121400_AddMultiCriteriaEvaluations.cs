using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentService.Migrations
{
    /// <inheritdoc />
    public partial class AddMultiCriteriaEvaluations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "TeacherEvaluations",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CurriculumRating",
                table: "TeacherEvaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PunctualityRating",
                table: "TeacherEvaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupportRating",
                table: "TeacherEvaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeachingQualityRating",
                table: "TeacherEvaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurriculumRating",
                table: "TeacherEvaluations");

            migrationBuilder.DropColumn(
                name: "PunctualityRating",
                table: "TeacherEvaluations");

            migrationBuilder.DropColumn(
                name: "SupportRating",
                table: "TeacherEvaluations");

            migrationBuilder.DropColumn(
                name: "TeachingQualityRating",
                table: "TeacherEvaluations");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "TeacherEvaluations",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
