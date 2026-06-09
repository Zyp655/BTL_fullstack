using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaymentService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PaymentTransactions",
                columns: new[] { "TransactionId", "Amount", "Note", "PaidAt", "PaymentId", "PaymentMethod", "ReceivedByUserId" },
                values: new object[,]
                {
                    { 22, 2500000m, "Thanh toán học phí Lập trình Web với React & Node.js", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, "ChuyenKhoan", 1 },
                    { 23, 3500000m, "Thanh toán học phí TOEIC 600+", new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Utc), 10, "ChuyenKhoan", 1 },
                    { 24, 4500000m, "Thanh toán học phí Tiếng Anh giao tiếp cơ bản", new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 19, "ChuyenKhoan", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "StudentUserId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2,
                column: "StudentUserId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 3,
                column: "StudentUserId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 4,
                column: "StudentUserId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 5,
                columns: new[] { "PaidAmount", "RemainingAmount", "Status", "StudentUserId", "UpdatedAt" },
                values: new object[] { 2500000m, 0m, "HoanTat", 16, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 6,
                column: "StudentUserId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 7,
                column: "StudentUserId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 8,
                column: "StudentUserId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 9,
                column: "StudentUserId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 10,
                columns: new[] { "PaidAmount", "RemainingAmount", "Status", "StudentUserId", "UpdatedAt" },
                values: new object[] { 3500000m, 0m, "HoanTat", 16, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 11,
                column: "StudentUserId",
                value: 19);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 12,
                column: "StudentUserId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 13,
                column: "StudentUserId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 14,
                column: "StudentUserId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 15,
                column: "StudentUserId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 16,
                column: "StudentUserId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 17,
                column: "StudentUserId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 18,
                column: "StudentUserId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 19,
                columns: new[] { "PaidAmount", "RemainingAmount", "Status", "StudentUserId", "UpdatedAt" },
                values: new object[] { 4500000m, 0m, "HoanTat", 16, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 20,
                column: "StudentUserId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 21,
                column: "StudentUserId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 22,
                column: "StudentUserId",
                value: 19);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 23,
                column: "StudentUserId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 24,
                column: "StudentUserId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 25,
                column: "StudentUserId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 26,
                column: "StudentUserId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 27,
                column: "StudentUserId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 28,
                column: "StudentUserId",
                value: 19);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentTransactions",
                keyColumn: "TransactionId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "PaymentTransactions",
                keyColumn: "TransactionId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "PaymentTransactions",
                keyColumn: "TransactionId",
                keyValue: 24);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1,
                column: "StudentUserId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 2,
                column: "StudentUserId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 3,
                column: "StudentUserId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 4,
                column: "StudentUserId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 5,
                columns: new[] { "PaidAmount", "RemainingAmount", "Status", "StudentUserId", "UpdatedAt" },
                values: new object[] { 0m, 2500000m, "ChuaTT", 12, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 6,
                column: "StudentUserId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 7,
                column: "StudentUserId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 8,
                column: "StudentUserId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 9,
                column: "StudentUserId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 10,
                columns: new[] { "PaidAmount", "RemainingAmount", "Status", "StudentUserId", "UpdatedAt" },
                values: new object[] { 0m, 3500000m, "ChuaTT", 12, new DateTime(2024, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 11,
                column: "StudentUserId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 12,
                column: "StudentUserId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 13,
                column: "StudentUserId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 14,
                column: "StudentUserId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 15,
                column: "StudentUserId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 16,
                column: "StudentUserId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 17,
                column: "StudentUserId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 18,
                column: "StudentUserId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 19,
                columns: new[] { "PaidAmount", "RemainingAmount", "Status", "StudentUserId", "UpdatedAt" },
                values: new object[] { 0m, 4500000m, "ChuaTT", 12, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 20,
                column: "StudentUserId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 21,
                column: "StudentUserId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 22,
                column: "StudentUserId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 23,
                column: "StudentUserId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 24,
                column: "StudentUserId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 25,
                column: "StudentUserId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 26,
                column: "StudentUserId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 27,
                column: "StudentUserId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 28,
                column: "StudentUserId",
                value: 15);
        }
    }
}
