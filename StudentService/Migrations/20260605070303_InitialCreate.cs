using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EnrolledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentId = table.Column<int>(type: "int", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MarkedByTeacherId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceId);
                    table.ForeignKey(
                        name: "FK_Attendances_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "EnrollmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamResults",
                columns: table => new
                {
                    ResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentId = table.Column<int>(type: "int", nullable: false),
                    ExamType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Score = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GradedByTeacherId = table.Column<int>(type: "int", nullable: true),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResults", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_ExamResults_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "EnrollmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Address", "CreatedAt", "DateOfBirth", "Email", "FullName", "Gender", "Phone", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "123 Nguyễn Huệ, Q1, TP.HCM", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2000, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), "phamvand@gmail.com", "Phạm Văn D", "Nam", "0901000005", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 2, "456 Lê Lợi, Q3, TP.HCM", new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2001, 8, 20, 0, 0, 0, 0, DateTimeKind.Utc), "hoangthie@gmail.com", "Hoàng Thị E", "Nu", "0901000006", new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 3, "789 Trần Hưng Đạo, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1999, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuvang@gmail.com", "Vũ Văn G", "Nam", "0901000007", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7 }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "EnrollmentId", "ClassId", "CompletedAt", "EnrolledAt", "Status", "StudentId" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 1 },
                    { 2, 1, null, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 2 },
                    { 3, 3, null, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 3 },
                    { 4, 4, null, new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 1 }
                });

            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "AttendanceId", "CreatedAt", "EnrollmentId", "MarkedByTeacherId", "Note", "SessionDate", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2, null, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 2, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2, null, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 3, new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2, "Trễ 10 phút", new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "DiTre" },
                    { 4, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), 2, 2, null, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 5, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), 2, 2, "Không phép", new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Vang" },
                    { 6, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 2, null, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 7, new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), 3, 2, "Xin phép nghỉ", new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), "CoPhep" }
                });

            migrationBuilder.InsertData(
                table: "ExamResults",
                columns: new[] { "ResultId", "CreatedAt", "EnrollmentId", "ExamDate", "ExamType", "GradedByTeacherId", "Note", "Score" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), "KiemTra", 2, "Bài kiểm tra 15 phút", 8.5m },
                    { 2, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), "GiuaKy", 2, "Kiểm tra giữa kỳ", 7.0m },
                    { 3, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), "KiemTra", 2, null, 9.0m },
                    { 4, new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Utc), 3, new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Utc), "KiemTra", 2, "Cần cải thiện phần Listening", 6.5m },
                    { 5, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 4, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "KiemTra", 3, "Xuất sắc", 9.5m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EnrollmentId",
                table: "Attendances",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_SessionDate",
                table: "Attendances",
                column: "SessionDate");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ClassId",
                table: "Enrollments",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId_ClassId",
                table: "Enrollments",
                columns: new[] { "StudentId", "ClassId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_EnrollmentId",
                table: "ExamResults",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Students_FullName",
                table: "Students",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "ExamResults");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
