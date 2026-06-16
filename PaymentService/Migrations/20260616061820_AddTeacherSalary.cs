using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaymentService.Migrations
{
    /// <inheritdoc />
    public partial class AddTeacherSalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalarySlips",
                columns: table => new
                {
                    SalarySlipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RatePerSession = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SessionsTaught = table.Column<int>(type: "int", nullable: false),
                    TotalStudentSessions = table.Column<int>(type: "int", nullable: false),
                    StudentAllowanceRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CalculatedSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deductions = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalarySlips", x => x.SalarySlipId);
                    table.ForeignKey(
                        name: "FK_SalarySlips_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSalaryConfigs",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RatePerSession = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StudentAllowanceRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSalaryConfigs", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_TeacherSalaryConfigs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TeacherSalaryConfigs",
                columns: new[] { "UserId", "BaseSalary", "Notes", "RatePerSession", "StudentAllowanceRate" },
                values: new object[,]
                {
                    { 2, 0m, "Mặc định Nguyễn Văn An", 300000m, 0m },
                    { 3, 0m, "Mặc định Trần Thị Bình", 300000m, 0m },
                    { 4, 0m, "Mặc định Lê Văn Cường (Cao cấp)", 350000m, 0m },
                    { 5, 2000000m, "Hợp đồng Lê Thị Hoa (Cố định + Phụ cấp)", 400000m, 10000m },
                    { 6, 0m, "Mặc định Phạm Văn Khánh", 300000m, 0m },
                    { 7, 0m, "Mặc định Trần Thị Lan", 300000m, 0m },
                    { 8, 0m, "Mặc định Nguyễn Hoàng Nam", 300000m, 0m },
                    { 9, 0m, "Mặc định Trần Thị Mai", 300000m, 0m },
                    { 10, 0m, "Mặc định Phạm Việt Anh", 300000m, 0m },
                    { 11, 0m, "Mặc định Hoàng Đức Duy", 300000m, 0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalarySlips_TeacherId_Month_Year",
                table: "SalarySlips",
                columns: new[] { "TeacherId", "Month", "Year" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalarySlips");

            migrationBuilder.DropTable(
                name: "TeacherSalaryConfigs");
        }
    }
}
