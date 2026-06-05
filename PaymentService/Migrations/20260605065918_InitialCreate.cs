using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaymentService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentUserId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Users_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReceivedByUserId = table.Column<int>(type: "int", nullable: true),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "Role", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@trainingcenter.vn", "Quản trị viên", true, "$2a$11$7wHWG7dZnyt5WV1Gwe6PK.weH5mleI3aSegpTfqGNr5UdMvha6URC", "0901000001", "Admin", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenvana@trainingcenter.vn", "Nguyễn Văn A", true, "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2", "0901000002", "GiaoVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenvana" },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranthib@trainingcenter.vn", "Trần Thị B", true, "$2a$11$a65jsmNide60EpGGVc1Ft.THLyYl73hO5blSL/YEvUaOVROiZL8Dq", "0901000003", "GiaoVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranthib" },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "levanc@trainingcenter.vn", "Lê Văn C", true, "$2a$11$w9aGuf8EgEOtvtmTk8EO9OM.m8g9ZyusZo1u5bP/L0c3/EprYXGAm", "0901000004", "GiaoVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "levanc" },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamvand@gmail.com", "Phạm Văn D", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0901000005", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamvand" },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hoangthie@gmail.com", "Hoàng Thị E", true, "$2a$11$CxLT8mClksX6DaIqPY/o0eElofHwX7w3diDw4PPnzdEg.CGYfcaTq", "0901000006", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hoangthie" },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuvang@gmail.com", "Vũ Văn G", true, "$2a$11$AqFRtTKVmDdo8gO9Fxb6LuNagQk1Cg74vqA1gjFft4VWo4/bkApdW", "0901000007", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuvang" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "ClassId", "CreatedAt", "DueDate", "PaidAmount", "RemainingAmount", "Status", "StudentUserId", "TotalAmount", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2500000m, 0m, "HoanTat", 5, 2500000m, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1500000m, 1000000m, "DangTT", 6, 2500000m, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 3, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, 3500000m, "ChuaTT", 7, 3500000m, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 4, new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3000000m, 0m, "HoanTat", 5, 3000000m, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "PaymentTransactions",
                columns: new[] { "TransactionId", "Amount", "Note", "PaidAt", "PaymentId", "PaymentMethod", "ReceivedByUserId" },
                values: new object[,]
                {
                    { 1, 2500000m, "Thanh toán đầy đủ", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "ChuyenKhoan", 1 },
                    { 2, 1000000m, "Đợt 1", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "TienMat", 1 },
                    { 3, 500000m, "Đợt 2", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, "ChuyenKhoan", 1 },
                    { 4, 3000000m, "Thanh toán đầy đủ bằng thẻ", new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, "TheTD", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClassId",
                table: "Payments",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Status",
                table: "Payments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StudentUserId",
                table: "Payments",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_PaymentId",
                table: "PaymentTransactions",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsActive",
                table: "Users",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role",
                table: "Users",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
