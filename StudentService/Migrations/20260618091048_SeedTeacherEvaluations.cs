using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentService.Migrations
{
    /// <inheritdoc />
    public partial class SeedTeacherEvaluations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SystemSettings",
                columns: new[] { "Key", "Value" },
                values: new object[] { "EnabledEvaluationClassIds", "1,2,3,4,5,6,7,8,9,10" });

            migrationBuilder.InsertData(
                table: "TeacherEvaluations",
                columns: new[] { "Id", "ClassId", "Comment", "CreatedAt", "CurriculumRating", "DetailedRatingsJson", "PunctualityRating", "Rating", "StudentId", "SupportRating", "TeacherId", "TeachingQualityRating" },
                values: new object[,]
                {
                    { 1, 1, "Thầy dạy rất hay và nhiệt tình, tài liệu đầy đủ.", new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, null, 5, 4.75, 1, 5, 2, 5 },
                    { 2, 1, "Thầy hỗ trợ nhiệt tình sau giờ học.", new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), 5, null, 4, 4.5, 2, 5, 2, 4 },
                    { 3, 1, "Tác phong thầy rất chuyên nghiệp, đúng giờ.", new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), 4, null, 5, 4.5, 3, 4, 2, 5 },
                    { 4, 2, "Cô Bình dạy dễ hiểu, chuẩn bị bài kỹ lưỡng.", new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), 4, null, 4, 4.0, 6, 4, 3, 4 },
                    { 5, 2, "Lớp học rất sôi nổi, cô hỗ trợ nhiệt tình.", new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc), 5, null, 5, 5.0, 7, 5, 3, 5 },
                    { 6, 3, "Khóa học TOEIC rất chất lượng, thầy An truyền thụ nhiều mẹo thi thực tế.", new DateTime(2026, 6, 6, 0, 0, 0, 0, DateTimeKind.Utc), 5, null, 5, 5.0, 1, 5, 2, 5 },
                    { 7, 3, "Lịch học đúng giờ, tài liệu phong phú.", new DateTime(2026, 6, 7, 0, 0, 0, 0, DateTimeKind.Utc), 4, null, 5, 4.25, 5, 4, 2, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemSettings",
                keyColumn: "Key",
                keyValue: "EnabledEvaluationClassIds");

            migrationBuilder.DeleteData(
                table: "TeacherEvaluations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TeacherEvaluations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TeacherEvaluations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TeacherEvaluations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TeacherEvaluations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TeacherEvaluations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TeacherEvaluations",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
