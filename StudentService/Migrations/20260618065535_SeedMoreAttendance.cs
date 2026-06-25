using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentService.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "AttendanceId", "CreatedAt", "EnrollmentId", "MarkedByTeacherId", "Note", "SessionDate", "Status" },
                values: new object[,]
                {
                    { 8, new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), 8, 2, null, new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 9, new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), 9, 2, null, new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 10, new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), 10, 2, null, new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), "DiTre" },
                    { 11, new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), 11, 2, null, new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 12, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 8, 2, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 13, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 9, 2, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Vang" },
                    { 14, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 10, 2, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 15, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 11, 2, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 16, new DateTime(2026, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), 8, 2, null, new DateTime(2026, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 17, new DateTime(2026, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), 9, 2, null, new DateTime(2026, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 18, new DateTime(2026, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), 10, 2, null, new DateTime(2026, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 19, new DateTime(2026, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), 11, 2, null, new DateTime(2026, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 20, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), 8, 2, null, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 21, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), 9, 2, null, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 22, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), 10, 2, null, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "DiTre" },
                    { 23, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), 11, 2, null, new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Vang" },
                    { 24, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc), 406, 5, null, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 25, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc), 407, 5, null, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 26, new DateTime(2026, 7, 27, 0, 0, 0, 0, DateTimeKind.Utc), 406, 5, null, new DateTime(2026, 7, 27, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 27, new DateTime(2026, 7, 27, 0, 0, 0, 0, DateTimeKind.Utc), 407, 5, null, new DateTime(2026, 7, 27, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 28, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), 12, 4, null, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 29, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), 13, 4, null, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 30, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), 14, 4, null, new DateTime(2026, 6, 4, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 31, new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), 12, 4, null, new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 32, new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), 13, 4, null, new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), "DiTre" },
                    { 33, new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), 14, 4, null, new DateTime(2026, 6, 11, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 34, new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Utc), 12, 4, null, new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 35, new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Utc), 13, 4, null, new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Vang" },
                    { 36, new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Utc), 14, 4, null, new DateTime(2026, 6, 18, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 37, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), 12, 4, null, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 38, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), 13, 4, null, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" },
                    { 39, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), 14, 4, null, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "CoMat" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Attendances",
                keyColumn: "AttendanceId",
                keyValue: 39);
        }
    }
}
