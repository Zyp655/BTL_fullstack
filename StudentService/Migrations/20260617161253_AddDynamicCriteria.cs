using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentService.Migrations
{
    /// <inheritdoc />
    public partial class AddDynamicCriteria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DetailedRatingsJson",
                table: "TeacherEvaluations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EvaluationCriteria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCriteria", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EvaluationCriteria",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Truyền tải kiến thức, dễ hiểu, nhiệt huyết", true, "Chất lượng giảng dạy" },
                    { 2, "Tận tình hỗ trợ học viên, giải đáp thắc mắc", true, "Thái độ & Hỗ trợ" },
                    { 3, "Đầy đủ tài liệu học tập, bài tập, slide", true, "Tài liệu & Giáo trình" },
                    { 4, "Vào lớp đúng giờ, chuyên nghiệp, chuẩn mực", true, "Tác phong & Đúng giờ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriteria_Name",
                table: "EvaluationCriteria",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationCriteria");

            migrationBuilder.DropColumn(
                name: "DetailedRatingsJson",
                table: "TeacherEvaluations");
        }
    }
}
