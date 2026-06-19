using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentService.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreCourseEvaluationsSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CourseEvaluations",
                columns: new[] { "Id", "Comment", "CourseId", "CreatedAt", "Rating", "StudentId" },
                values: new object[,]
                {
                    { 13, "Khóa học rất thiết thực, học xong tự tin thuyết trình hẳn.", 4, new DateTime(2026, 6, 12, 0, 0, 0, 0, DateTimeKind.Utc), 5, 1 },
                    { 14, "Nhiều bài tập thực hành bổ ích.", 4, new DateTime(2026, 6, 13, 0, 0, 0, 0, DateTimeKind.Utc), 4, 2 },
                    { 15, "Giáo trình cực kỳ sát đề thi, thầy cô chấm chữa bài viết rất chi tiết.", 5, new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), 5, 3 },
                    { 16, "Dạy từ cơ bản đến nâng cao Vue 3, bài tập lớn rất hay.", 11, new DateTime(2026, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), 5, 4 },
                    { 17, "Khóa học C# nâng cao rất hay, cô Hoa giảng dạy cực kỳ chi tiết và dễ hiểu.", 14, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5, 7 },
                    { 18, "Kiến thức nâng cao rất bổ ích cho việc tối ưu hóa code backend.", 14, new DateTime(2026, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc), 4, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseEvaluations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CourseEvaluations",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CourseEvaluations",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CourseEvaluations",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "CourseEvaluations",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "CourseEvaluations",
                keyColumn: "Id",
                keyValue: 18);
        }
    }
}
