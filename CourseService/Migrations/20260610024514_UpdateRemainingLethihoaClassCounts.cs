using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRemainingLethihoaClassCounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Classes SET CurrentStudents = 15 WHERE ClassId IN (6, 9, 28);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Classes SET CurrentStudents = 2 WHERE ClassId = 6;");
            migrationBuilder.Sql("UPDATE Classes SET CurrentStudents = 0 WHERE ClassId = 9;");
            migrationBuilder.Sql("UPDATE Classes SET CurrentStudents = 2 WHERE ClassId = 28;");
        }
    }
}
