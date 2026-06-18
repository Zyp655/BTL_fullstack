using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentService.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseEvaluations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseEvaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseEvaluations_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CourseEvaluations",
                columns: new[] { "Id", "Comment", "CourseId", "CreatedAt", "Rating", "StudentId" },
                values: new object[,]
                {
                    { 1, "Khóa học tiếng Anh giao tiếp rất thiết thực, bài học dễ hiểu.", 1, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, 1 },
                    { 2, "Nội dung phong phú, tuy nhiên phòng học hơi nhỏ.", 1, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), 4, 2 },
                    { 3, "Khóa học tuyệt vời cho người bắt đầu giao tiếp tiếng Anh.", 1, new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), 5, 3 },
                    { 4, "Chương trình học bài bản, giáo viên tận tình.", 1, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), 4, 6 },
                    { 5, "Tập trung nhiều mẹo thi hữu ích, giúp tăng điểm nhanh chóng.", 2, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc), 5, 1 },
                    { 6, "Khóa học tốt, đề thi thử sát với đề thi thật.", 2, new DateTime(2026, 6, 6, 0, 0, 0, 0, DateTimeKind.Utc), 4, 5 },
                    { 7, "Giáo trình chi tiết, dễ tiếp thu.", 2, new DateTime(2026, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), 4, 7 },
                    { 8, "Lập trình Python rất cơ bản, phù hợp với người chưa biết gì.", 3, new DateTime(2026, 6, 8, 0, 0, 0, 0, DateTimeKind.Utc), 5, 1 },
                    { 9, "Thực hành nhiều bài tập trực quan.", 3, new DateTime(2026, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), 5, 2 },
                    { 10, "Nội dung hơi nhanh ở những buổi cuối.", 3, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 3, 4 },
                    { 11, "Fullstack React & Nodejs dạy rất thực tế, làm được project ngay.", 8, new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), 5, 3 },
                    { 12, "Kiến thức nhiều và nặng, cần tự học nhiều.", 8, new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), 4, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseEvaluations_CourseId",
                table: "CourseEvaluations",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEvaluations_StudentId",
                table: "CourseEvaluations",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEvaluations_StudentId_CourseId",
                table: "CourseEvaluations",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseEvaluations");
        }
    }
}
