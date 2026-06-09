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
                name: "CourseQueues",
                columns: table => new
                {
                    CourseQueueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    QueuedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseQueues", x => x.CourseQueueId);
                    table.ForeignKey(
                        name: "FK_CourseQueues_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
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
                    { 1, "123 Nguyễn Huệ, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2000, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), "phamvandung@gmail.com", "Phạm Văn Dũng", "Nam", "0901000005", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12 },
                    { 2, "456 Lê Lợi, Q3, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2001, 8, 20, 0, 0, 0, 0, DateTimeKind.Utc), "hoangthimai@gmail.com", "Hoàng Thị Mai", "Nữ", "0901000006", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 13 },
                    { 3, "789 Trần Hưng Đạo, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1999, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuvangiang@gmail.com", "Vũ Văn Giang", "Nam", "0901000007", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 14 },
                    { 4, "101 Cách Mạng Tháng 8, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2002, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenvanminh@gmail.com", "Nguyễn Văn Minh", "Nam", "0901000011", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 15 },
                    { 5, "202 Nguyễn Trãi, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2000, 9, 25, 0, 0, 0, 0, DateTimeKind.Utc), "hoangthinga@gmail.com", "Hoàng Thị Nga", "Nữ", "0901000012", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 16 },
                    { 6, "303 Điện Biên Phủ, Q3, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2001, 11, 5, 0, 0, 0, 0, DateTimeKind.Utc), "vuvanhai@gmail.com", "Vũ Văn Hải", "Nam", "0901000013", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 17 },
                    { 7, "404 Võ Văn Tần, Q3, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2003, 7, 14, 0, 0, 0, 0, DateTimeKind.Utc), "lethiphuong@gmail.com", "Lê Thị Phương", "Nữ", "0901000014", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 18 },
                    { 8, "505 Nguyễn Thị Minh Khai, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2000, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "tranquocquan@gmail.com", "Trần Quốc Quân", "Nam", "0901000015", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 19 },
                    { 9, "923 Cách Mạng Tháng 8, Phường 2, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1999, 11, 24, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenhoangtien@gmail.com", "Nguyễn Hoàng Tiến", "Nam", "0933341057", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 20 },
                    { 10, "743 Điện Biên Phủ, Phường 12, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1998, 9, 7, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenthiphuong@gmail.com", "Nguyễn Thị Phương", "Nữ", "0934903402", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 21 },
                    { 11, "358 Hai Bà Trưng, Phường 3, Q3, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2000, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), "phamphuongchi@gmail.com", "Phạm Phương Chi", "Nữ", "0941109031", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 22 },
                    { 12, "836 Lê Duẩn, Phường 12, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2003, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), "tranngocnhi@gmail.com", "Trần Ngọc Nhi", "Nữ", "0917022674", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 23 },
                    { 13, "56 Điện Biên Phủ, Phường 4, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2001, 12, 3, 0, 0, 0, 0, DateTimeKind.Utc), "phanminhlong@gmail.com", "Phan Minh Long", "Nam", "0947067228", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 24 },
                    { 14, "373 Nguyễn Trãi, Phường 11, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2003, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), "phamminhkien@gmail.com", "Phạm Minh Kiên", "Nam", "0948606962", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 25 },
                    { 15, "957 Điện Biên Phủ, Phường 12, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), "dangquoclong@gmail.com", "Đặng Quốc Long", "Nam", "0933741438", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 26 },
                    { 16, "77 Nguyễn Trãi, Phường 15, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2003, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), "ngovietgiang@gmail.com", "Ngô Việt Giang", "Nam", "0931538552", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 27 },
                    { 17, "772 Cách Mạng Tháng 8, Phường 9, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2002, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "phamphuongnhi@gmail.com", "Phạm Phương Nhi", "Nữ", "0983396987", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 28 },
                    { 18, "58 Nam Kỳ Khởi Nghĩa, Phường 2, Q3, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "dangkhanhlan@gmail.com", "Đặng Khánh Lan", "Nữ", "0933320821", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 29 },
                    { 19, "551 Hai Bà Trưng, Phường 9, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2004, 10, 15, 0, 0, 0, 0, DateTimeKind.Utc), "lyhuuphuc@gmail.com", "Lý Hữu Phúc", "Nam", "0917455324", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 30 },
                    { 20, "474 Lê Duẩn, Phường 12, Bình Thạnh, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2002, 7, 6, 0, 0, 0, 0, DateTimeKind.Utc), "ngothanhdat@gmail.com", "Ngô Thành Đạt", "Nam", "0962871534", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 31 },
                    { 21, "790 Nguyễn Trãi, Phường 9, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2001, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), "buithutuyet@gmail.com", "Bùi Thu Tuyết", "Nữ", "0916007072", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 32 },
                    { 22, "69 Nguyễn Trãi, Phường 15, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2003, 5, 8, 0, 0, 0, 0, DateTimeKind.Utc), "dangviettuan@gmail.com", "Đặng Việt Tuấn", "Nam", "0902876828", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 33 },
                    { 23, "281 Cách Mạng Tháng 8, Phường 14, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 9, 6, 0, 0, 0, 0, DateTimeKind.Utc), "trananhminh@gmail.com", "Trần Anh Minh", "Nam", "0923154051", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 34 },
                    { 24, "472 Lê Duẩn, Phường 4, Q3, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2003, 8, 17, 0, 0, 0, 0, DateTimeKind.Utc), "phamquynhthao@gmail.com", "Phạm Quỳnh Thảo", "Nữ", "0947693979", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 35 },
                    { 25, "656 Lê Duẩn, Phường 4, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1998, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "vuvanbach@gmail.com", "Vũ Văn Bách", "Nam", "0934694634", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 36 },
                    { 26, "145 Điện Biên Phủ, Phường 15, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 4, 18, 0, 0, 0, 0, DateTimeKind.Utc), "vuminhhuy@gmail.com", "Vũ Minh Huy", "Nam", "0935672073", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 37 },
                    { 27, "372 Trần Hưng Đạo, Phường 7, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1999, 11, 14, 0, 0, 0, 0, DateTimeKind.Utc), "phamphuonganh@gmail.com", "Phạm Phương Anh", "Nữ", "0932582524", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 38 },
                    { 28, "204 Cách Mạng Tháng 8, Phường 8, Q3, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1999, 4, 7, 0, 0, 0, 0, DateTimeKind.Utc), "ngominhgiang@gmail.com", "Ngô Minh Giang", "Nam", "0976692553", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 39 },
                    { 29, "61 Điện Biên Phủ, Phường 9, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 9, 4, 0, 0, 0, 0, DateTimeKind.Utc), "lekimhoa@gmail.com", "Lê Kim Hoa", "Nữ", "0932264748", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 40 },
                    { 30, "420 Lê Duẩn, Phường 3, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 4, 28, 0, 0, 0, 0, DateTimeKind.Utc), "lyduchung@gmail.com", "Lý Đức Hùng", "Nam", "0979147706", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 41 },
                    { 31, "313 Nguyễn Trãi, Phường 1, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 3, 7, 0, 0, 0, 0, DateTimeKind.Utc), "phanhoangviet@gmail.com", "Phan Hoàng Việt", "Nam", "0948096887", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 42 },
                    { 32, "92 Nam Kỳ Khởi Nghĩa, Phường 3, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2000, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "duongvietgiang@gmail.com", "Dương Việt Giang", "Nam", "0908999183", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 43 },
                    { 33, "439 Điện Biên Phủ, Phường 10, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1998, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), "ngoduckien@gmail.com", "Ngô Đức Kiên", "Nam", "0915130808", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 44 },
                    { 34, "670 Hai Bà Trưng, Phường 8, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2004, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc), "hoanghongtrinh@gmail.com", "Hoàng Hồng Trinh", "Nữ", "0935456272", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 45 },
                    { 35, "145 Hai Bà Trưng, Phường 15, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2001, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenanhphuc@gmail.com", "Nguyễn Anh Phúc", "Nam", "0912229110", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 46 },
                    { 36, "577 Hai Bà Trưng, Phường 15, Bình Thạnh, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1998, 11, 27, 0, 0, 0, 0, DateTimeKind.Utc), "vuhoanghung@gmail.com", "Vũ Hoàng Hùng", "Nam", "0986075395", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 47 },
                    { 37, "225 Điện Biên Phủ, Phường 6, Q3, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2002, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc), "lehoanghai@gmail.com", "Lê Hoàng Hải", "Nam", "0913607983", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 48 },
                    { 38, "13 Hai Bà Trưng, Phường 13, Q3, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2004, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), "buiphuonghanh@gmail.com", "Bùi Phương Hạnh", "Nữ", "0902548511", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 49 },
                    { 39, "935 Nguyễn Trãi, Phường 9, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1999, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "lephuongdung@gmail.com", "Lê Phương Dung", "Nữ", "0971162230", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 50 },
                    { 40, "930 Nam Kỳ Khởi Nghĩa, Phường 14, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1998, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), "dangquynhlinh@gmail.com", "Đặng Quỳnh Linh", "Nữ", "0973138181", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 51 },
                    { 41, "176 Nam Kỳ Khởi Nghĩa, Phường 13, Q3, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2000, 4, 28, 0, 0, 0, 0, DateTimeKind.Utc), "phamhongvy@gmail.com", "Phạm Hồng Vy", "Nữ", "0967817881", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 52 },
                    { 42, "728 Lê Duẩn, Phường 7, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2002, 3, 26, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenthutrinh@gmail.com", "Nguyễn Thu Trinh", "Nữ", "0975163555", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 53 },
                    { 43, "685 Nguyễn Trãi, Phường 7, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2001, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamhonghoa@gmail.com", "Phạm Hồng Hoa", "Nữ", "0966120253", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 54 },
                    { 44, "908 Hai Bà Trưng, Phường 3, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2003, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "trankimlan@gmail.com", "Trần Kim Lan", "Nữ", "0979996207", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 55 },
                    { 45, "533 Lê Duẩn, Phường 7, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2003, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenngocbich@gmail.com", "Nguyễn Ngọc Bích", "Nữ", "0976799667", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 56 },
                    { 46, "81 Điện Biên Phủ, Phường 15, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2001, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), "hoangvannam@gmail.com", "Hoàng Văn Nam", "Nam", "0909722815", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 57 },
                    { 47, "312 Cách Mạng Tháng 8, Phường 3, Q3, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2003, 7, 23, 0, 0, 0, 0, DateTimeKind.Utc), "ngongocquynh@gmail.com", "Ngô Ngọc Quỳnh", "Nữ", "0947851696", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 58 },
                    { 48, "225 Trần Hưng Đạo, Phường 13, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1998, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ngokhanhtrang@gmail.com", "Ngô Khánh Trang", "Nữ", "0947812810", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 59 },
                    { 49, "684 Lê Duẩn, Phường 5, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 12, 6, 0, 0, 0, 0, DateTimeKind.Utc), "dophuonghoa@gmail.com", "Đỗ Phương Hoa", "Nữ", "0939576076", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 60 },
                    { 50, "260 Trần Hưng Đạo, Phường 10, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "tranhongquynh@gmail.com", "Trần Hồng Quỳnh", "Nữ", "0934340845", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 61 },
                    { 51, "681 Điện Biên Phủ, Phường 1, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2004, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc), "phanthanhthao@gmail.com", "Phan Thanh Thảo", "Nữ", "0979294272", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 62 },
                    { 52, "146 Nam Kỳ Khởi Nghĩa, Phường 8, Bình Thạnh, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2001, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "phamthutuyet@gmail.com", "Phạm Thu Tuyết", "Nữ", "0981842524", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 63 },
                    { 53, "496 Trần Hưng Đạo, Phường 5, Q3, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2000, 12, 28, 0, 0, 0, 0, DateTimeKind.Utc), "lyphuongbich@gmail.com", "Lý Phương Bích", "Nữ", "0978481188", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 64 },
                    { 54, "302 Nguyễn Trãi, Phường 5, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "lyquynhngoc@gmail.com", "Lý Quỳnh Ngọc", "Nữ", "0935606980", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 65 },
                    { 55, "733 Nguyễn Trãi, Phường 2, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2004, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc), "buingoclinh@gmail.com", "Bùi Ngọc Linh", "Nữ", "0924879923", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 66 },
                    { 56, "936 Nam Kỳ Khởi Nghĩa, Phường 10, Bình Thạnh, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2001, 7, 13, 0, 0, 0, 0, DateTimeKind.Utc), "vuquynhhoa@gmail.com", "Vũ Quỳnh Hoa", "Nữ", "0972044645", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 67 },
                    { 57, "409 Nam Kỳ Khởi Nghĩa, Phường 15, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2003, 5, 25, 0, 0, 0, 0, DateTimeKind.Utc), "lyngockien@gmail.com", "Lý Ngọc Kiên", "Nam", "0981098919", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 68 },
                    { 58, "694 Điện Biên Phủ, Phường 13, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1998, 7, 11, 0, 0, 0, 0, DateTimeKind.Utc), "doducdat@gmail.com", "Đỗ Đức Đạt", "Nam", "0979147720", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 69 },
                    { 59, "448 Nguyễn Trãi, Phường 14, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1998, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "doquocphuc@gmail.com", "Đỗ Quốc Phúc", "Nam", "0907610562", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 70 },
                    { 60, "789 Trần Hưng Đạo, Phường 5, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenhoangkien@gmail.com", "Nguyễn Hoàng Kiên", "Nam", "0964551070", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 71 },
                    { 61, "809 Điện Biên Phủ, Phường 1, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2001, 11, 3, 0, 0, 0, 0, DateTimeKind.Utc), "tranphuongmai@gmail.com", "Trần Phương Mai", "Nữ", "0906871360", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 72 },
                    { 62, "127 Cách Mạng Tháng 8, Phường 4, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2000, 8, 22, 0, 0, 0, 0, DateTimeKind.Utc), "phamvanphuc@gmail.com", "Phạm Văn Phúc", "Nam", "0925002120", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 73 },
                    { 63, "36 Hai Bà Trưng, Phường 10, Bình Thạnh, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2002, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "lytructrang@gmail.com", "Lý Trúc Trang", "Nữ", "0913747534", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 74 },
                    { 64, "834 Lê Duẩn, Phường 13, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2002, 11, 20, 0, 0, 0, 0, DateTimeKind.Utc), "phanhongphuong@gmail.com", "Phan Hồng Phương", "Nữ", "0932709620", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 75 },
                    { 65, "440 Nam Kỳ Khởi Nghĩa, Phường 8, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2003, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "vuthanhnam@gmail.com", "Vũ Thành Nam", "Nam", "0962156902", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 76 },
                    { 66, "951 Cách Mạng Tháng 8, Phường 13, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2002, 10, 26, 0, 0, 0, 0, DateTimeKind.Utc), "vuphuonglinh@gmail.com", "Vũ Phương Linh", "Nữ", "0973955087", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 77 },
                    { 67, "259 Nam Kỳ Khởi Nghĩa, Phường 8, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1999, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), "phanthanhhanh@gmail.com", "Phan Thanh Hạnh", "Nữ", "0965118725", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 78 },
                    { 68, "826 Hai Bà Trưng, Phường 6, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 4, 12, 0, 0, 0, 0, DateTimeKind.Utc), "vuthingoc@gmail.com", "Vũ Thị Ngọc", "Nữ", "0964050766", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 79 },
                    { 69, "510 Cách Mạng Tháng 8, Phường 13, Q3, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2001, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), "buithituyet@gmail.com", "Bùi Thị Tuyết", "Nữ", "0932436347", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 80 },
                    { 70, "718 Nguyễn Trãi, Phường 5, Bình Thạnh, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2002, 4, 13, 0, 0, 0, 0, DateTimeKind.Utc), "ngophuonghoa@gmail.com", "Ngô Phương Hoa", "Nữ", "0902561176", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 81 },
                    { 71, "474 Hai Bà Trưng, Phường 5, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2003, 6, 23, 0, 0, 0, 0, DateTimeKind.Utc), "doquynhtuyet@gmail.com", "Đỗ Quỳnh Tuyết", "Nữ", "0968138769", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 82 },
                    { 72, "766 Trần Hưng Đạo, Phường 5, Bình Thạnh, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2000, 4, 7, 0, 0, 0, 0, DateTimeKind.Utc), "tranducphong@gmail.com", "Trần Đức Phong", "Nam", "0919990305", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 83 },
                    { 73, "735 Cách Mạng Tháng 8, Phường 3, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2000, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranhongquynh1@gmail.com", "Trần Hồng Quỳnh", "Nữ", "0937054579", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 84 },
                    { 74, "301 Trần Hưng Đạo, Phường 8, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1999, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenthanhtung@gmail.com", "Nguyễn Thành Tùng", "Nam", "0929235577", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 85 },
                    { 75, "85 Cách Mạng Tháng 8, Phường 11, Bình Thạnh, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1999, 7, 16, 0, 0, 0, 0, DateTimeKind.Utc), "lethihanh@gmail.com", "Lê Thị Hạnh", "Nữ", "0982914064", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 86 },
                    { 76, "792 Trần Hưng Đạo, Phường 10, Tân Bình, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2001, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "lequocbach@gmail.com", "Lê Quốc Bách", "Nam", "0942429098", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 87 },
                    { 77, "322 Cách Mạng Tháng 8, Phường 10, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2002, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc), "lythanhkien@gmail.com", "Lý Thành Kiên", "Nam", "0988427625", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 88 },
                    { 78, "187 Cách Mạng Tháng 8, Phường 2, Q3, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1999, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), "lyducthinh@gmail.com", "Lý Đức Thịnh", "Nam", "0935440025", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 89 },
                    { 79, "733 Hai Bà Trưng, Phường 12, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1998, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), "phananhphuc@gmail.com", "Phan Anh Phúc", "Nam", "0985886520", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 90 },
                    { 80, "673 Nguyễn Trãi, Phường 15, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1999, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ngoducdat@gmail.com", "Ngô Đức Đạt", "Nam", "0938132648", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 91 },
                    { 81, "715 Hai Bà Trưng, Phường 12, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "tranvanhung@gmail.com", "Trần Văn Hùng", "Nam", "0945842082", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 92 },
                    { 82, "340 Cách Mạng Tháng 8, Phường 5, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1998, 7, 24, 0, 0, 0, 0, DateTimeKind.Utc), "buiquynhngoc@gmail.com", "Bùi Quỳnh Ngọc", "Nữ", "0982349590", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 93 },
                    { 83, "541 Điện Biên Phủ, Phường 8, Q5, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1998, 3, 16, 0, 0, 0, 0, DateTimeKind.Utc), "phamngocbach@gmail.com", "Phạm Ngọc Bách", "Nam", "0905521644", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 94 },
                    { 84, "351 Hai Bà Trưng, Phường 11, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), "danghuuthinh@gmail.com", "Đặng Hữu Thịnh", "Nam", "0982530793", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 95 },
                    { 85, "332 Hai Bà Trưng, Phường 6, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1998, 8, 3, 0, 0, 0, 0, DateTimeKind.Utc), "vuhuutuan@gmail.com", "Vũ Hữu Tuấn", "Nam", "0947718473", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 96 },
                    { 86, "380 Cách Mạng Tháng 8, Phường 13, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1998, 4, 17, 0, 0, 0, 0, DateTimeKind.Utc), "buithidung@gmail.com", "Bùi Thị Dung", "Nữ", "0987933143", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 97 },
                    { 87, "506 Lê Duẩn, Phường 1, Bình Thạnh, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2002, 8, 23, 0, 0, 0, 0, DateTimeKind.Utc), "lythithao@gmail.com", "Lý Thị Thảo", "Nữ", "0943198037", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 98 },
                    { 88, "944 Lê Duẩn, Phường 8, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1999, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "duongquoctung@gmail.com", "Dương Quốc Tùng", "Nam", "0907844947", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 99 },
                    { 89, "477 Cách Mạng Tháng 8, Phường 3, Q10, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2005, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "dohoanghuy@gmail.com", "Đỗ Hoàng Huy", "Nam", "0947970771", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 100 },
                    { 90, "529 Hai Bà Trưng, Phường 14, Q1, TP.HCM", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2004, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), "dangthanhquan@gmail.com", "Đặng Thành Quân", "Nam", "0915634662", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 101 }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "EnrollmentId", "ClassId", "CompletedAt", "EnrolledAt", "Status", "StudentId" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 1 },
                    { 2, 1, null, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 2 },
                    { 3, 1, null, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 3 },
                    { 4, 1, null, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 4 },
                    { 5, 1, null, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 5 },
                    { 6, 2, null, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 6 },
                    { 7, 2, null, new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 7 },
                    { 8, 3, null, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 1 },
                    { 9, 3, null, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 3 },
                    { 10, 3, null, new DateTime(2024, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 5 },
                    { 11, 3, null, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 8 },
                    { 12, 4, null, new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 2 },
                    { 13, 4, null, new DateTime(2024, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 4 },
                    { 14, 4, null, new DateTime(2024, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 6 },
                    { 15, 5, null, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 1 },
                    { 16, 5, null, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 2 },
                    { 17, 5, null, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 3 },
                    { 18, 5, null, new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 4 },
                    { 19, 5, null, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 5 },
                    { 20, 5, null, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 6 },
                    { 21, 6, null, new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 7 },
                    { 22, 6, null, new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 8 },
                    { 23, 7, null, new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 3 },
                    { 24, 7, null, new DateTime(2024, 7, 16, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 4 },
                    { 25, 7, null, new DateTime(2024, 7, 17, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 5 },
                    { 26, 8, null, new DateTime(2024, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 6 },
                    { 27, 8, null, new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 7 },
                    { 28, 8, null, new DateTime(2024, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 8 }
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
                    { 5, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 4, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "KiemTra", 2, "Xuất sắc", 9.5m }
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
                name: "IX_CourseQueues_CourseId",
                table: "CourseQueues",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseQueues_StudentId",
                table: "CourseQueues",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseQueues_StudentId_CourseId",
                table: "CourseQueues",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);

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
                name: "CourseQueues");

            migrationBuilder.DropTable(
                name: "ExamResults");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
