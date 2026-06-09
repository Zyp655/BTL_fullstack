using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSessions = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: true),
                    TeacherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Room = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaxStudents = table.Column<int>(type: "int", nullable: false),
                    CurrentStudents = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_Classes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Category", "CourseName", "CreatedAt", "Description", "Fee", "IsActive", "Level", "TotalSessions", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "NgoaiNgu", "Tiếng Anh giao tiếp cơ bản", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Khóa học tiếng Anh giao tiếp dành cho người mới bắt đầu, tập trung vào kỹ năng nghe và nói.", 2500000m, true, "Beginner", 30, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "NgoaiNgu", "TOEIC 600+", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Luyện thi TOEIC đạt 600+ điểm, bao gồm cả Listening và Reading.", 3500000m, true, "Intermediate", 40, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "TinHoc", "Lập trình Python cơ bản", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Khóa học lập trình Python cho người mới, từ cú pháp cơ bản đến xây dựng ứng dụng đơn giản.", 3000000m, true, "Beginner", 24, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "KyNang", "Kỹ năng thuyết trình", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rèn luyện kỹ năng thuyết trình chuyên nghiệp, tự tin trước đám đông.", 1800000m, true, "Intermediate", 12, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "NgoaiNgu", "IELTS 6.5+", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Luyện thi IELTS đạt band 6.5+, bao gồm 4 kỹ năng Listening, Reading, Writing, Speaking.", 5000000m, true, "Advanced", 48, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, "TinHoc", "Lập trình Web với React & Node.js", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Khóa học Fullstack Web Development sử dụng React cho Frontend và Express/Node.js cho Backend.", 4500000m, true, "Intermediate", 36, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, "KyNang", "Kỹ năng quản lý thời gian", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Học cách lập kế hoạch, sắp xếp công việc, và tối ưu hóa năng suất làm việc hàng ngày.", 1500000m, true, "Beginner", 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, "NgoaiNgu", "Tiếng Nhật sơ cấp N5", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Khóa học dành cho người bắt đầu làm quen với bảng chữ cái Hiragana, Katakana và giao tiếp cơ bản.", 2800000m, true, "Beginner", 32, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, "TinHoc", "Lập trình Web với Vue.js", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Khóa học phát triển giao diện Web Single Page Application hiện đại với framework Vue.js 3.", 3800000m, true, "Intermediate", 24, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, "KyNang", "Kỹ năng làm việc nhóm (Teamwork)", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rèn luyện kỹ năng phối hợp, giao tiếp và giải quyết xung đột trong môi trường làm việc nhóm hiệu quả.", 1200000m, true, "Beginner", 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "ClassId", "ClassName", "CourseId", "CreatedAt", "CurrentStudents", "EndDate", "MaxStudents", "Room", "StartDate", "Status", "TeacherId", "TeacherName" },
                values: new object[,]
                {
                    { 1, "TA-CB-01", 1, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 25, "P.101", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 2, "Nguyễn Văn An" },
                    { 2, "TA-CB-02", 1, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 30, "P.102", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Opened", 3, "Trần Thị Bình" },
                    { 3, "TOEIC-01", 2, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, "P.201", new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 2, "Nguyễn Văn An" },
                    { 4, "PY-01", 3, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, "P.Lab1", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 4, "Lê Văn Cường" },
                    { 5, "FS-REACT-01", 8, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), 24, "P.Lab2", new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 4, "Lê Văn Cường" },
                    { 6, "KN-QLTG-01", 9, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), 40, "P.301", new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Opened", 8, "Lê Thị Hoa" },
                    { 7, "JP-N5-01", 10, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20, "P.103", new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Opened", 9, "Phạm Văn Khánh" },
                    { 8, "VUEJS-01", 11, new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Utc), 3, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), 20, "P.Lab1", new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "InProgress", 4, "Lê Văn Cường" }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "ScheduleId", "ClassId", "DayOfWeek", "EndTime", "Session", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, 2, new TimeSpan(0, 10, 0, 0, 0), "Sang", new TimeSpan(0, 8, 0, 0, 0) },
                    { 2, 1, 4, new TimeSpan(0, 10, 0, 0, 0), "Sang", new TimeSpan(0, 8, 0, 0, 0) },
                    { 3, 1, 6, new TimeSpan(0, 10, 0, 0, 0), "Sang", new TimeSpan(0, 8, 0, 0, 0) },
                    { 4, 2, 3, new TimeSpan(0, 16, 0, 0, 0), "Chieu", new TimeSpan(0, 14, 0, 0, 0) },
                    { 5, 2, 5, new TimeSpan(0, 16, 0, 0, 0), "Chieu", new TimeSpan(0, 14, 0, 0, 0) },
                    { 6, 3, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 7, 3, 5, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 8, 4, 3, new TimeSpan(0, 11, 30, 0, 0), "Sang", new TimeSpan(0, 9, 0, 0, 0) },
                    { 9, 4, 7, new TimeSpan(0, 11, 30, 0, 0), "Sang", new TimeSpan(0, 9, 0, 0, 0) },
                    { 10, 5, 2, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 11, 5, 5, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 12, 6, 7, new TimeSpan(0, 11, 30, 0, 0), "Sang", new TimeSpan(0, 8, 30, 0, 0) },
                    { 13, 7, 3, new TimeSpan(0, 16, 30, 0, 0), "Chieu", new TimeSpan(0, 14, 0, 0, 0) },
                    { 14, 7, 5, new TimeSpan(0, 16, 30, 0, 0), "Chieu", new TimeSpan(0, 14, 0, 0, 0) },
                    { 15, 8, 4, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) },
                    { 16, 8, 6, new TimeSpan(0, 20, 30, 0, 0), "Toi", new TimeSpan(0, 18, 0, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CourseId",
                table: "Classes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Status",
                table: "Classes",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TeacherId",
                table: "Classes",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Category",
                table: "Courses",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseName",
                table: "Courses",
                column: "CourseName");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_IsActive",
                table: "Courses",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ClassId",
                table: "Schedules",
                column: "ClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
