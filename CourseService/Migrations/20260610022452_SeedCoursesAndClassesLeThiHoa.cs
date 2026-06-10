using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseService.Migrations
{
    /// <inheritdoc />
    public partial class SeedCoursesAndClassesLeThiHoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 6,
                column: "TeacherId",
                value: 5);

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Category", "CourseName", "CreatedAt", "Description", "Fee", "IsActive", "Level", "TotalSessions", "UpdatedAt" },
                values: new object[,]
                {
                    { 14, "TinHoc", "Lập trình C# nâng cao", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Khóa học lập trình C# nâng cao, tối ưu hiệu năng và phát triển ứng dụng doanh nghiệp.", 4500000m, true, "Advanced", 36, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 101, "TinHoc", "Môn học tự chọn 1", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 1 cho học viên.", 1520000m, true, "Intermediate", 11, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 102, "KyNang", "Môn học tự chọn 2", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 2 cho học viên.", 1540000m, true, "Advanced", 12, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 103, "NgoaiNgu", "Môn học tự chọn 3", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 3 cho học viên.", 1560000m, true, "Beginner", 13, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 104, "TinHoc", "Môn học tự chọn 4", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 4 cho học viên.", 1580000m, true, "Intermediate", 14, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 105, "KyNang", "Môn học tự chọn 5", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 5 cho học viên.", 1600000m, true, "Advanced", 15, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 106, "NgoaiNgu", "Môn học tự chọn 6", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 6 cho học viên.", 1620000m, true, "Beginner", 16, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 107, "TinHoc", "Môn học tự chọn 7", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 7 cho học viên.", 1640000m, true, "Intermediate", 17, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 108, "KyNang", "Môn học tự chọn 8", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 8 cho học viên.", 1660000m, true, "Advanced", 18, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 109, "NgoaiNgu", "Môn học tự chọn 9", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 9 cho học viên.", 1680000m, true, "Beginner", 19, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 110, "TinHoc", "Môn học tự chọn 10", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 10 cho học viên.", 1700000m, true, "Intermediate", 20, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 111, "KyNang", "Môn học tự chọn 11", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 11 cho học viên.", 1720000m, true, "Advanced", 21, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 112, "NgoaiNgu", "Môn học tự chọn 12", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 12 cho học viên.", 1740000m, true, "Beginner", 22, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 113, "TinHoc", "Môn học tự chọn 13", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 13 cho học viên.", 1760000m, true, "Intermediate", 23, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 114, "KyNang", "Môn học tự chọn 14", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 14 cho học viên.", 1780000m, true, "Advanced", 24, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 115, "NgoaiNgu", "Môn học tự chọn 15", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 15 cho học viên.", 1800000m, true, "Beginner", 25, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 116, "TinHoc", "Môn học tự chọn 16", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 16 cho học viên.", 1820000m, true, "Intermediate", 26, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 117, "KyNang", "Môn học tự chọn 17", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 17 cho học viên.", 1840000m, true, "Advanced", 27, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 118, "NgoaiNgu", "Môn học tự chọn 18", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 18 cho học viên.", 1860000m, true, "Beginner", 28, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 119, "TinHoc", "Môn học tự chọn 19", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 19 cho học viên.", 1880000m, true, "Intermediate", 29, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 120, "KyNang", "Môn học tự chọn 20", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 20 cho học viên.", 1900000m, true, "Advanced", 10, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 121, "NgoaiNgu", "Môn học tự chọn 21", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 21 cho học viên.", 1920000m, true, "Beginner", 11, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 122, "TinHoc", "Môn học tự chọn 22", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 22 cho học viên.", 1940000m, true, "Intermediate", 12, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 123, "KyNang", "Môn học tự chọn 23", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 23 cho học viên.", 1960000m, true, "Advanced", 13, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 124, "NgoaiNgu", "Môn học tự chọn 24", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 24 cho học viên.", 1980000m, true, "Beginner", 14, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 125, "TinHoc", "Môn học tự chọn 25", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 25 cho học viên.", 2000000m, true, "Intermediate", 15, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 126, "KyNang", "Môn học tự chọn 26", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 26 cho học viên.", 2020000m, true, "Advanced", 16, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 127, "NgoaiNgu", "Môn học tự chọn 27", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 27 cho học viên.", 2040000m, true, "Beginner", 17, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 128, "TinHoc", "Môn học tự chọn 28", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 28 cho học viên.", 2060000m, true, "Intermediate", 18, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 129, "KyNang", "Môn học tự chọn 29", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 29 cho học viên.", 2080000m, true, "Advanced", 19, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 130, "NgoaiNgu", "Môn học tự chọn 30", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 30 cho học viên.", 2100000m, true, "Beginner", 20, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 131, "TinHoc", "Môn học tự chọn 31", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 31 cho học viên.", 2120000m, true, "Intermediate", 21, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 132, "KyNang", "Môn học tự chọn 32", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 32 cho học viên.", 2140000m, true, "Advanced", 22, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 133, "NgoaiNgu", "Môn học tự chọn 33", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 33 cho học viên.", 2160000m, true, "Beginner", 23, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 134, "TinHoc", "Môn học tự chọn 34", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 34 cho học viên.", 2180000m, true, "Intermediate", 24, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 135, "KyNang", "Môn học tự chọn 35", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 35 cho học viên.", 2200000m, true, "Advanced", 25, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 136, "NgoaiNgu", "Môn học tự chọn 36", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 36 cho học viên.", 2220000m, true, "Beginner", 26, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 137, "TinHoc", "Môn học tự chọn 37", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 37 cho học viên.", 2240000m, true, "Intermediate", 27, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 138, "KyNang", "Môn học tự chọn 38", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 38 cho học viên.", 2260000m, true, "Advanced", 28, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 139, "NgoaiNgu", "Môn học tự chọn 39", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 39 cho học viên.", 2280000m, true, "Beginner", 29, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 140, "TinHoc", "Môn học tự chọn 40", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 40 cho học viên.", 2300000m, true, "Intermediate", 10, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 141, "KyNang", "Môn học tự chọn 41", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 41 cho học viên.", 2320000m, true, "Advanced", 11, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 142, "NgoaiNgu", "Môn học tự chọn 42", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 42 cho học viên.", 2340000m, true, "Beginner", 12, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 143, "TinHoc", "Môn học tự chọn 43", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 43 cho học viên.", 2360000m, true, "Intermediate", 13, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 144, "KyNang", "Môn học tự chọn 44", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 44 cho học viên.", 2380000m, true, "Advanced", 14, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 145, "NgoaiNgu", "Môn học tự chọn 45", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 45 cho học viên.", 2400000m, true, "Beginner", 15, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 146, "TinHoc", "Môn học tự chọn 46", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 46 cho học viên.", 2420000m, true, "Intermediate", 16, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 147, "KyNang", "Môn học tự chọn 47", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 47 cho học viên.", 2440000m, true, "Advanced", 17, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 148, "NgoaiNgu", "Môn học tự chọn 48", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 48 cho học viên.", 2460000m, true, "Beginner", 18, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 149, "TinHoc", "Môn học tự chọn 49", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 49 cho học viên.", 2480000m, true, "Intermediate", 19, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 150, "KyNang", "Môn học tự chọn 50", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 50 cho học viên.", 2500000m, true, "Advanced", 20, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 151, "NgoaiNgu", "Môn học tự chọn 51", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 51 cho học viên.", 2520000m, true, "Beginner", 21, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 152, "TinHoc", "Môn học tự chọn 52", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 52 cho học viên.", 2540000m, true, "Intermediate", 22, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 153, "KyNang", "Môn học tự chọn 53", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 53 cho học viên.", 2560000m, true, "Advanced", 23, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 154, "NgoaiNgu", "Môn học tự chọn 54", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 54 cho học viên.", 2580000m, true, "Beginner", 24, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 155, "TinHoc", "Môn học tự chọn 55", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 55 cho học viên.", 2600000m, true, "Intermediate", 25, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 156, "KyNang", "Môn học tự chọn 56", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 56 cho học viên.", 2620000m, true, "Advanced", 26, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 157, "NgoaiNgu", "Môn học tự chọn 57", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 57 cho học viên.", 2640000m, true, "Beginner", 27, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 158, "TinHoc", "Môn học tự chọn 58", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 58 cho học viên.", 2660000m, true, "Intermediate", 28, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 159, "KyNang", "Môn học tự chọn 59", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 59 cho học viên.", 2680000m, true, "Advanced", 29, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 160, "NgoaiNgu", "Môn học tự chọn 60", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 60 cho học viên.", 2700000m, true, "Beginner", 10, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 161, "TinHoc", "Môn học tự chọn 61", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 61 cho học viên.", 2720000m, true, "Intermediate", 11, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 162, "KyNang", "Môn học tự chọn 62", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 62 cho học viên.", 2740000m, true, "Advanced", 12, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 163, "NgoaiNgu", "Môn học tự chọn 63", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 63 cho học viên.", 2760000m, true, "Beginner", 13, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 164, "TinHoc", "Môn học tự chọn 64", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 64 cho học viên.", 2780000m, true, "Intermediate", 14, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 165, "KyNang", "Môn học tự chọn 65", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 65 cho học viên.", 2800000m, true, "Advanced", 15, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 166, "NgoaiNgu", "Môn học tự chọn 66", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 66 cho học viên.", 2820000m, true, "Beginner", 16, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 167, "TinHoc", "Môn học tự chọn 67", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 67 cho học viên.", 2840000m, true, "Intermediate", 17, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 168, "KyNang", "Môn học tự chọn 68", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 68 cho học viên.", 2860000m, true, "Advanced", 18, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 169, "NgoaiNgu", "Môn học tự chọn 69", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 69 cho học viên.", 2880000m, true, "Beginner", 19, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 170, "TinHoc", "Môn học tự chọn 70", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 70 cho học viên.", 2900000m, true, "Intermediate", 20, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 171, "KyNang", "Môn học tự chọn 71", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 71 cho học viên.", 2920000m, true, "Advanced", 21, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 172, "NgoaiNgu", "Môn học tự chọn 72", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 72 cho học viên.", 2940000m, true, "Beginner", 22, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 173, "TinHoc", "Môn học tự chọn 73", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 73 cho học viên.", 2960000m, true, "Intermediate", 23, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 174, "KyNang", "Môn học tự chọn 74", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 74 cho học viên.", 2980000m, true, "Advanced", 24, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 175, "NgoaiNgu", "Môn học tự chọn 75", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 75 cho học viên.", 3000000m, true, "Beginner", 25, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 176, "TinHoc", "Môn học tự chọn 76", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 76 cho học viên.", 3020000m, true, "Intermediate", 26, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 177, "KyNang", "Môn học tự chọn 77", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 77 cho học viên.", 3040000m, true, "Advanced", 27, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 178, "NgoaiNgu", "Môn học tự chọn 78", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 78 cho học viên.", 3060000m, true, "Beginner", 28, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 179, "TinHoc", "Môn học tự chọn 79", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 79 cho học viên.", 3080000m, true, "Intermediate", 29, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 180, "KyNang", "Môn học tự chọn 80", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 80 cho học viên.", 3100000m, true, "Advanced", 10, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 181, "NgoaiNgu", "Môn học tự chọn 81", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 81 cho học viên.", 3120000m, true, "Beginner", 11, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 182, "TinHoc", "Môn học tự chọn 82", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 82 cho học viên.", 3140000m, true, "Intermediate", 12, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 183, "KyNang", "Môn học tự chọn 83", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 83 cho học viên.", 3160000m, true, "Advanced", 13, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 184, "NgoaiNgu", "Môn học tự chọn 84", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 84 cho học viên.", 3180000m, true, "Beginner", 14, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 185, "TinHoc", "Môn học tự chọn 85", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 85 cho học viên.", 3200000m, true, "Intermediate", 15, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 186, "KyNang", "Môn học tự chọn 86", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 86 cho học viên.", 3220000m, true, "Advanced", 16, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 187, "NgoaiNgu", "Môn học tự chọn 87", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 87 cho học viên.", 3240000m, true, "Beginner", 17, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 188, "TinHoc", "Môn học tự chọn 88", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 88 cho học viên.", 3260000m, true, "Intermediate", 18, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 189, "KyNang", "Môn học tự chọn 89", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 89 cho học viên.", 3280000m, true, "Advanced", 19, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 190, "NgoaiNgu", "Môn học tự chọn 90", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 90 cho học viên.", 3300000m, true, "Beginner", 20, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 191, "TinHoc", "Môn học tự chọn 91", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 91 cho học viên.", 3320000m, true, "Intermediate", 21, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 192, "KyNang", "Môn học tự chọn 92", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 92 cho học viên.", 3340000m, true, "Advanced", 22, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 193, "NgoaiNgu", "Môn học tự chọn 93", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 93 cho học viên.", 3360000m, true, "Beginner", 23, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 194, "TinHoc", "Môn học tự chọn 94", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 94 cho học viên.", 3380000m, true, "Intermediate", 24, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 195, "KyNang", "Môn học tự chọn 95", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 95 cho học viên.", 3400000m, true, "Advanced", 25, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 196, "NgoaiNgu", "Môn học tự chọn 96", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 96 cho học viên.", 3420000m, true, "Beginner", 26, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 197, "TinHoc", "Môn học tự chọn 97", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 97 cho học viên.", 3440000m, true, "Intermediate", 27, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 198, "KyNang", "Môn học tự chọn 98", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 98 cho học viên.", 3460000m, true, "Advanced", 28, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 199, "NgoaiNgu", "Môn học tự chọn 99", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 99 cho học viên.", 3480000m, true, "Beginner", 29, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 200, "TinHoc", "Môn học tự chọn 100", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả môn học tự chọn số 100 cho học viên.", 3500000m, true, "Intermediate", 10, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 201, "TinHoc", "Chuyên đề nâng cao 1", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 1 giảng dạy bởi cô Lê Thị Hoa.", 3050000m, true, "Advanced", 16, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 202, "TinHoc", "Chuyên đề nâng cao 2", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 2 giảng dạy bởi cô Lê Thị Hoa.", 3100000m, true, "Advanced", 17, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 203, "TinHoc", "Chuyên đề nâng cao 3", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 3 giảng dạy bởi cô Lê Thị Hoa.", 3150000m, true, "Advanced", 18, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 204, "TinHoc", "Chuyên đề nâng cao 4", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 4 giảng dạy bởi cô Lê Thị Hoa.", 3200000m, true, "Advanced", 19, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 205, "TinHoc", "Chuyên đề nâng cao 5", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 5 giảng dạy bởi cô Lê Thị Hoa.", 3250000m, true, "Advanced", 20, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 206, "TinHoc", "Chuyên đề nâng cao 6", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 6 giảng dạy bởi cô Lê Thị Hoa.", 3300000m, true, "Advanced", 21, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 207, "TinHoc", "Chuyên đề nâng cao 7", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 7 giảng dạy bởi cô Lê Thị Hoa.", 3350000m, true, "Advanced", 22, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 208, "TinHoc", "Chuyên đề nâng cao 8", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 8 giảng dạy bởi cô Lê Thị Hoa.", 3400000m, true, "Advanced", 23, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 209, "TinHoc", "Chuyên đề nâng cao 9", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 9 giảng dạy bởi cô Lê Thị Hoa.", 3450000m, true, "Advanced", 24, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 210, "TinHoc", "Chuyên đề nâng cao 10", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 10 giảng dạy bởi cô Lê Thị Hoa.", 3500000m, true, "Advanced", 25, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 211, "TinHoc", "Chuyên đề nâng cao 11", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 11 giảng dạy bởi cô Lê Thị Hoa.", 3550000m, true, "Advanced", 26, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 212, "TinHoc", "Chuyên đề nâng cao 12", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 12 giảng dạy bởi cô Lê Thị Hoa.", 3600000m, true, "Advanced", 27, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 213, "TinHoc", "Chuyên đề nâng cao 13", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 13 giảng dạy bởi cô Lê Thị Hoa.", 3650000m, true, "Advanced", 28, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 214, "TinHoc", "Chuyên đề nâng cao 14", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 14 giảng dạy bởi cô Lê Thị Hoa.", 3700000m, true, "Advanced", 29, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 215, "TinHoc", "Chuyên đề nâng cao 15", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 15 giảng dạy bởi cô Lê Thị Hoa.", 3750000m, true, "Advanced", 30, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 216, "TinHoc", "Chuyên đề nâng cao 16", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 16 giảng dạy bởi cô Lê Thị Hoa.", 3800000m, true, "Advanced", 31, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 217, "TinHoc", "Chuyên đề nâng cao 17", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 17 giảng dạy bởi cô Lê Thị Hoa.", 3850000m, true, "Advanced", 32, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 218, "TinHoc", "Chuyên đề nâng cao 18", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 18 giảng dạy bởi cô Lê Thị Hoa.", 3900000m, true, "Advanced", 33, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 219, "TinHoc", "Chuyên đề nâng cao 19", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 19 giảng dạy bởi cô Lê Thị Hoa.", 3950000m, true, "Advanced", 34, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 220, "TinHoc", "Chuyên đề nâng cao 20", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Mô tả chuyên đề nâng cao số 20 giảng dạy bởi cô Lê Thị Hoa.", 4000000m, true, "Advanced", 35, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "ClassId", "ClassName", "CourseId", "CreatedAt", "CurrentStudents", "EndDate", "MaxStudents", "Room", "StartDate", "Status", "TeacherId", "TeacherName", "TotalSessions" },
                values: new object[,]
                {
                    { 28, "CS-NC-01", 14, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab3", new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 101, "Lớp-TC-001", 101, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.101", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 3, "Trần Thị Bình", 0 },
                    { 102, "Lớp-TC-002", 102, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.102", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 4, "Lê Văn Cường", 0 },
                    { 103, "Lớp-TC-003", 103, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.103", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 6, "Phạm Văn Khánh", 0 },
                    { 104, "Lớp-TC-004", 104, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.104", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 7, "Trần Thị Lan", 0 },
                    { 105, "Lớp-TC-005", 105, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.105", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 8, "Nguyễn Hoàng Nam", 0 },
                    { 106, "Lớp-TC-006", 106, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.106", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 9, "Trần Thị Mai", 0 },
                    { 107, "Lớp-TC-007", 107, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.107", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 10, "Phạm Việt Anh", 0 },
                    { 108, "Lớp-TC-008", 108, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.108", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 11, "Hoàng Đức Duy", 0 },
                    { 109, "Lớp-TC-009", 109, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.109", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 2, "Nguyễn Văn An", 0 },
                    { 110, "Lớp-TC-010", 110, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.110", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 3, "Trần Thị Bình", 0 },
                    { 111, "Lớp-TC-011", 111, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.111", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 4, "Lê Văn Cường", 0 },
                    { 112, "Lớp-TC-012", 112, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.112", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 6, "Phạm Văn Khánh", 0 },
                    { 113, "Lớp-TC-013", 113, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.113", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 7, "Trần Thị Lan", 0 },
                    { 114, "Lớp-TC-014", 114, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.114", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 8, "Nguyễn Hoàng Nam", 0 },
                    { 115, "Lớp-TC-015", 115, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.115", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 9, "Trần Thị Mai", 0 },
                    { 116, "Lớp-TC-016", 116, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.116", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 10, "Phạm Việt Anh", 0 },
                    { 117, "Lớp-TC-017", 117, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.117", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 11, "Hoàng Đức Duy", 0 },
                    { 118, "Lớp-TC-018", 118, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.118", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 2, "Nguyễn Văn An", 0 },
                    { 119, "Lớp-TC-019", 119, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.119", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 3, "Trần Thị Bình", 0 },
                    { 120, "Lớp-TC-020", 120, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.100", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 4, "Lê Văn Cường", 0 },
                    { 121, "Lớp-TC-021", 121, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.101", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 6, "Phạm Văn Khánh", 0 },
                    { 122, "Lớp-TC-022", 122, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.102", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 7, "Trần Thị Lan", 0 },
                    { 123, "Lớp-TC-023", 123, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.103", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 8, "Nguyễn Hoàng Nam", 0 },
                    { 124, "Lớp-TC-024", 124, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.104", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 9, "Trần Thị Mai", 0 },
                    { 125, "Lớp-TC-025", 125, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.105", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 10, "Phạm Việt Anh", 0 },
                    { 126, "Lớp-TC-026", 126, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.106", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 11, "Hoàng Đức Duy", 0 },
                    { 127, "Lớp-TC-027", 127, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.107", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 2, "Nguyễn Văn An", 0 },
                    { 128, "Lớp-TC-028", 128, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.108", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 3, "Trần Thị Bình", 0 },
                    { 129, "Lớp-TC-029", 129, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.109", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 4, "Lê Văn Cường", 0 },
                    { 130, "Lớp-TC-030", 130, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.110", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 6, "Phạm Văn Khánh", 0 },
                    { 131, "Lớp-TC-031", 131, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.111", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 7, "Trần Thị Lan", 0 },
                    { 132, "Lớp-TC-032", 132, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.112", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 8, "Nguyễn Hoàng Nam", 0 },
                    { 133, "Lớp-TC-033", 133, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.113", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 9, "Trần Thị Mai", 0 },
                    { 134, "Lớp-TC-034", 134, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.114", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 10, "Phạm Việt Anh", 0 },
                    { 135, "Lớp-TC-035", 135, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.115", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 11, "Hoàng Đức Duy", 0 },
                    { 136, "Lớp-TC-036", 136, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.116", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 2, "Nguyễn Văn An", 0 },
                    { 137, "Lớp-TC-037", 137, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.117", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 3, "Trần Thị Bình", 0 },
                    { 138, "Lớp-TC-038", 138, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.118", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 4, "Lê Văn Cường", 0 },
                    { 139, "Lớp-TC-039", 139, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.119", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 6, "Phạm Văn Khánh", 0 },
                    { 140, "Lớp-TC-040", 140, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.100", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 7, "Trần Thị Lan", 0 },
                    { 141, "Lớp-TC-041", 141, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.101", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 8, "Nguyễn Hoàng Nam", 0 },
                    { 142, "Lớp-TC-042", 142, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.102", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 9, "Trần Thị Mai", 0 },
                    { 143, "Lớp-TC-043", 143, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.103", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 10, "Phạm Việt Anh", 0 },
                    { 144, "Lớp-TC-044", 144, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.104", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 11, "Hoàng Đức Duy", 0 },
                    { 145, "Lớp-TC-045", 145, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.105", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 2, "Nguyễn Văn An", 0 },
                    { 146, "Lớp-TC-046", 146, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.106", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 3, "Trần Thị Bình", 0 },
                    { 147, "Lớp-TC-047", 147, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.107", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 4, "Lê Văn Cường", 0 },
                    { 148, "Lớp-TC-048", 148, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.108", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 6, "Phạm Văn Khánh", 0 },
                    { 149, "Lớp-TC-049", 149, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.109", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 7, "Trần Thị Lan", 0 },
                    { 150, "Lớp-TC-050", 150, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.110", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 8, "Nguyễn Hoàng Nam", 0 },
                    { 151, "Lớp-TC-051", 151, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.111", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 9, "Trần Thị Mai", 0 },
                    { 152, "Lớp-TC-052", 152, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.112", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 10, "Phạm Việt Anh", 0 },
                    { 153, "Lớp-TC-053", 153, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.113", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 11, "Hoàng Đức Duy", 0 },
                    { 154, "Lớp-TC-054", 154, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.114", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 2, "Nguyễn Văn An", 0 },
                    { 155, "Lớp-TC-055", 155, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.115", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 3, "Trần Thị Bình", 0 },
                    { 156, "Lớp-TC-056", 156, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.116", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 4, "Lê Văn Cường", 0 },
                    { 157, "Lớp-TC-057", 157, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.117", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 6, "Phạm Văn Khánh", 0 },
                    { 158, "Lớp-TC-058", 158, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.118", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 7, "Trần Thị Lan", 0 },
                    { 159, "Lớp-TC-059", 159, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.119", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 8, "Nguyễn Hoàng Nam", 0 },
                    { 160, "Lớp-TC-060", 160, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.100", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 9, "Trần Thị Mai", 0 },
                    { 161, "Lớp-TC-061", 161, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.101", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 10, "Phạm Việt Anh", 0 },
                    { 162, "Lớp-TC-062", 162, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.102", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 11, "Hoàng Đức Duy", 0 },
                    { 163, "Lớp-TC-063", 163, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.103", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 2, "Nguyễn Văn An", 0 },
                    { 164, "Lớp-TC-064", 164, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.104", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 3, "Trần Thị Bình", 0 },
                    { 165, "Lớp-TC-065", 165, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.105", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 4, "Lê Văn Cường", 0 },
                    { 166, "Lớp-TC-066", 166, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.106", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 6, "Phạm Văn Khánh", 0 },
                    { 167, "Lớp-TC-067", 167, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.107", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 7, "Trần Thị Lan", 0 },
                    { 168, "Lớp-TC-068", 168, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.108", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 8, "Nguyễn Hoàng Nam", 0 },
                    { 169, "Lớp-TC-069", 169, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.109", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 9, "Trần Thị Mai", 0 },
                    { 170, "Lớp-TC-070", 170, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.110", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 10, "Phạm Việt Anh", 0 },
                    { 171, "Lớp-TC-071", 171, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.111", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 11, "Hoàng Đức Duy", 0 },
                    { 172, "Lớp-TC-072", 172, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.112", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 2, "Nguyễn Văn An", 0 },
                    { 173, "Lớp-TC-073", 173, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.113", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 3, "Trần Thị Bình", 0 },
                    { 174, "Lớp-TC-074", 174, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.114", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 4, "Lê Văn Cường", 0 },
                    { 175, "Lớp-TC-075", 175, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.115", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 6, "Phạm Văn Khánh", 0 },
                    { 176, "Lớp-TC-076", 176, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.116", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 7, "Trần Thị Lan", 0 },
                    { 177, "Lớp-TC-077", 177, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.117", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 8, "Nguyễn Hoàng Nam", 0 },
                    { 178, "Lớp-TC-078", 178, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.118", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 9, "Trần Thị Mai", 0 },
                    { 179, "Lớp-TC-079", 179, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.119", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 10, "Phạm Việt Anh", 0 },
                    { 180, "Lớp-TC-080", 180, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.100", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 11, "Hoàng Đức Duy", 0 },
                    { 181, "Lớp-TC-081", 181, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.101", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 2, "Nguyễn Văn An", 0 },
                    { 182, "Lớp-TC-082", 182, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.102", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 3, "Trần Thị Bình", 0 },
                    { 183, "Lớp-TC-083", 183, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.103", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 4, "Lê Văn Cường", 0 },
                    { 184, "Lớp-TC-084", 184, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.104", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 6, "Phạm Văn Khánh", 0 },
                    { 185, "Lớp-TC-085", 185, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.105", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 7, "Trần Thị Lan", 0 },
                    { 186, "Lớp-TC-086", 186, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.106", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 8, "Nguyễn Hoàng Nam", 0 },
                    { 187, "Lớp-TC-087", 187, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.107", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 9, "Trần Thị Mai", 0 },
                    { 188, "Lớp-TC-088", 188, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.108", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 10, "Phạm Việt Anh", 0 },
                    { 189, "Lớp-TC-089", 189, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.109", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 11, "Hoàng Đức Duy", 0 },
                    { 190, "Lớp-TC-090", 190, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.110", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 2, "Nguyễn Văn An", 0 },
                    { 191, "Lớp-TC-091", 191, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.111", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 3, "Trần Thị Bình", 0 },
                    { 192, "Lớp-TC-092", 192, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.112", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 4, "Lê Văn Cường", 0 },
                    { 193, "Lớp-TC-093", 193, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.113", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 6, "Phạm Văn Khánh", 0 },
                    { 194, "Lớp-TC-094", 194, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.114", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 7, "Trần Thị Lan", 0 },
                    { 195, "Lớp-TC-095", 195, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.115", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 8, "Nguyễn Hoàng Nam", 0 },
                    { 196, "Lớp-TC-096", 196, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.116", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 9, "Trần Thị Mai", 0 },
                    { 197, "Lớp-TC-097", 197, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.117", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 10, "Phạm Việt Anh", 0 },
                    { 198, "Lớp-TC-098", 198, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.118", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 11, "Hoàng Đức Duy", 0 },
                    { 199, "Lớp-TC-099", 199, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.119", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 2, "Nguyễn Văn An", 0 },
                    { 200, "Lớp-TC-100", 200, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 0, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.100", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 3, "Trần Thị Bình", 0 },
                    { 201, "Lớp-Lth-01", 201, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-1", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 202, "Lớp-Lth-02", 202, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-2", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 203, "Lớp-Lth-03", 203, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-3", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 204, "Lớp-Lth-04", 204, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-4", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 205, "Lớp-Lth-05", 205, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-5", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 206, "Lớp-Lth-06", 206, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-6", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 207, "Lớp-Lth-07", 207, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-7", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 208, "Lớp-Lth-08", 208, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-8", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 209, "Lớp-Lth-09", 209, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-9", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 210, "Lớp-Lth-10", 210, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-10", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 211, "Lớp-Lth-11", 211, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-11", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 212, "Lớp-Lth-12", 212, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-12", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 213, "Lớp-Lth-13", 213, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-13", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 214, "Lớp-Lth-14", 214, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-14", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 215, "Lớp-Lth-15", 215, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-15", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 216, "Lớp-Lth-16", 216, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-16", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 217, "Lớp-Lth-17", 217, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-17", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 218, "Lớp-Lth-18", 218, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-18", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 219, "Lớp-Lth-19", 219, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-19", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 },
                    { 220, "Lớp-Lth-20", 220, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.Lab-20", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 5, "Lê Thị Hoa", 0 }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "ScheduleId", "ClassId", "DayOfWeek", "EndTime", "Session", "StartTime" },
                values: new object[,]
                {
                    { 47, 28, 3, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 48, 28, 5, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 100, 101, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 101, 101, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 102, 102, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 103, 102, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 104, 103, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 105, 103, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 106, 104, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 107, 104, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 108, 105, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 109, 105, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 110, 106, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 111, 106, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 112, 107, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 113, 107, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 114, 108, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 115, 108, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 116, 109, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 117, 109, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 118, 110, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 119, 110, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 120, 111, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 121, 111, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 122, 112, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 123, 112, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 124, 113, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 125, 113, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 126, 114, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 127, 114, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 128, 115, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 129, 115, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 130, 116, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 131, 116, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 132, 117, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 133, 117, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 134, 118, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 135, 118, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 136, 119, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 137, 119, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 138, 120, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 139, 120, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 140, 121, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 141, 121, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 142, 122, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 143, 122, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 144, 123, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 145, 123, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 146, 124, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 147, 124, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 148, 125, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 149, 125, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 150, 126, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 151, 126, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 152, 127, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 153, 127, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 154, 128, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 155, 128, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 156, 129, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 157, 129, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 158, 130, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 159, 130, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 160, 131, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 161, 131, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 162, 132, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 163, 132, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 164, 133, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 165, 133, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 166, 134, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 167, 134, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 168, 135, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 169, 135, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 170, 136, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 171, 136, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 172, 137, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 173, 137, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 174, 138, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 175, 138, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 176, 139, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 177, 139, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 178, 140, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 179, 140, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 180, 141, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 181, 141, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 182, 142, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 183, 142, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 184, 143, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 185, 143, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 186, 144, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 187, 144, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 188, 145, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 189, 145, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 190, 146, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 191, 146, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 192, 147, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 193, 147, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 194, 148, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 195, 148, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 196, 149, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 197, 149, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 198, 150, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 199, 150, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 200, 151, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 201, 151, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 202, 152, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 203, 152, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 204, 153, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 205, 153, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 206, 154, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 207, 154, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 208, 155, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 209, 155, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 210, 156, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 211, 156, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 212, 157, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 213, 157, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 214, 158, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 215, 158, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 216, 159, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 217, 159, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 218, 160, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 219, 160, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 220, 161, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 221, 161, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 222, 162, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 223, 162, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 224, 163, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 225, 163, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 226, 164, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 227, 164, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 228, 165, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 229, 165, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 230, 166, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 231, 166, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 232, 167, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 233, 167, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 234, 168, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 235, 168, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 236, 169, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 237, 169, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 238, 170, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 239, 170, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 240, 171, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 241, 171, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 242, 172, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 243, 172, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 244, 173, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 245, 173, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 246, 174, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 247, 174, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 248, 175, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 249, 175, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 250, 176, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 251, 176, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 252, 177, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 253, 177, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 254, 178, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 255, 178, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 256, 179, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 257, 179, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 258, 180, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 259, 180, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 260, 181, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 261, 181, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 262, 182, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 263, 182, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 264, 183, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 265, 183, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 266, 184, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 267, 184, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 268, 185, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 269, 185, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 270, 186, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 271, 186, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 272, 187, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 273, 187, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 274, 188, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 275, 188, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 276, 189, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 277, 189, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 278, 190, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 279, 190, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 280, 191, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 281, 191, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 282, 192, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 283, 192, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 284, 193, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 285, 193, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 286, 194, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 287, 194, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 288, 195, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 289, 195, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 290, 196, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 291, 196, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 292, 197, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 293, 197, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 294, 198, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 295, 198, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 296, 199, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 297, 199, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 298, 200, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 299, 200, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 300, 201, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 301, 201, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 302, 202, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 303, 202, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 304, 203, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 305, 203, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 306, 204, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 307, 204, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 308, 205, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 309, 205, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 310, 206, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 311, 206, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 312, 207, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 313, 207, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 314, 208, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 315, 208, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 316, 209, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 317, 209, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 318, 210, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 319, 210, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 320, 211, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 321, 211, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 322, 212, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 323, 212, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 324, 213, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 325, 213, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 326, 214, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 327, 214, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 328, 215, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 329, 215, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 330, 216, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 331, 216, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 332, 217, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 333, 217, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 334, 218, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 335, 218, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 336, 219, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 337, 219, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 338, 220, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 339, 220, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 220);

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 6,
                column: "TeacherId",
                value: 8);
        }
    }
}
