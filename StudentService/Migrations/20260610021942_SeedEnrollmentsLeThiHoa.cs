using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentService.Migrations
{
    /// <inheritdoc />
    public partial class SeedEnrollmentsLeThiHoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "EnrollmentId", "ClassId", "CompletedAt", "EnrolledAt", "Status", "StudentId" },
                values: new object[,]
                {
                    { 100, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 1 },
                    { 101, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 2 },
                    { 102, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 3 },
                    { 103, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 4 },
                    { 104, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 5 },
                    { 105, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 6 },
                    { 106, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 7 },
                    { 107, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 8 },
                    { 108, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 9 },
                    { 109, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 10 },
                    { 110, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 11 },
                    { 111, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 12 },
                    { 112, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 13 },
                    { 113, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 14 },
                    { 114, 201, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 15 },
                    { 115, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 16 },
                    { 116, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 17 },
                    { 117, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 18 },
                    { 118, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 19 },
                    { 119, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 20 },
                    { 120, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 21 },
                    { 121, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 22 },
                    { 122, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 23 },
                    { 123, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 24 },
                    { 124, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 25 },
                    { 125, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 26 },
                    { 126, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 27 },
                    { 127, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 28 },
                    { 128, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 29 },
                    { 129, 202, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 30 },
                    { 130, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 31 },
                    { 131, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 32 },
                    { 132, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 33 },
                    { 133, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 34 },
                    { 134, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 35 },
                    { 135, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 36 },
                    { 136, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 37 },
                    { 137, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 38 },
                    { 138, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 39 },
                    { 139, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 40 },
                    { 140, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 41 },
                    { 141, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 42 },
                    { 142, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 43 },
                    { 143, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 44 },
                    { 144, 203, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 45 },
                    { 145, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 46 },
                    { 146, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 47 },
                    { 147, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 48 },
                    { 148, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 49 },
                    { 149, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 50 },
                    { 150, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 51 },
                    { 151, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 52 },
                    { 152, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 53 },
                    { 153, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 54 },
                    { 154, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 55 },
                    { 155, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 56 },
                    { 156, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 57 },
                    { 157, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 58 },
                    { 158, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 59 },
                    { 159, 204, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 60 },
                    { 160, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 61 },
                    { 161, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 62 },
                    { 162, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 63 },
                    { 163, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 64 },
                    { 164, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 65 },
                    { 165, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 66 },
                    { 166, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 67 },
                    { 167, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 68 },
                    { 168, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 69 },
                    { 169, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 70 },
                    { 170, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 71 },
                    { 171, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 72 },
                    { 172, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 73 },
                    { 173, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 74 },
                    { 174, 205, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 75 },
                    { 175, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 76 },
                    { 176, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 77 },
                    { 177, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 78 },
                    { 178, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 79 },
                    { 179, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 80 },
                    { 180, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 81 },
                    { 181, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 82 },
                    { 182, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 83 },
                    { 183, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 84 },
                    { 184, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 85 },
                    { 185, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 86 },
                    { 186, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 87 },
                    { 187, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 88 },
                    { 188, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 89 },
                    { 189, 206, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 90 },
                    { 190, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 1 },
                    { 191, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 2 },
                    { 192, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 3 },
                    { 193, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 4 },
                    { 194, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 5 },
                    { 195, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 6 },
                    { 196, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 7 },
                    { 197, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 8 },
                    { 198, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 9 },
                    { 199, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 10 },
                    { 200, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 11 },
                    { 201, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 12 },
                    { 202, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 13 },
                    { 203, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 14 },
                    { 204, 207, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 15 },
                    { 205, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 16 },
                    { 206, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 17 },
                    { 207, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 18 },
                    { 208, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 19 },
                    { 209, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 20 },
                    { 210, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 21 },
                    { 211, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 22 },
                    { 212, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 23 },
                    { 213, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 24 },
                    { 214, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 25 },
                    { 215, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 26 },
                    { 216, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 27 },
                    { 217, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 28 },
                    { 218, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 29 },
                    { 219, 208, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 30 },
                    { 220, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 31 },
                    { 221, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 32 },
                    { 222, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 33 },
                    { 223, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 34 },
                    { 224, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 35 },
                    { 225, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 36 },
                    { 226, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 37 },
                    { 227, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 38 },
                    { 228, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 39 },
                    { 229, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 40 },
                    { 230, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 41 },
                    { 231, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 42 },
                    { 232, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 43 },
                    { 233, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 44 },
                    { 234, 209, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 45 },
                    { 235, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 46 },
                    { 236, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 47 },
                    { 237, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 48 },
                    { 238, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 49 },
                    { 239, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 50 },
                    { 240, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 51 },
                    { 241, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 52 },
                    { 242, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 53 },
                    { 243, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 54 },
                    { 244, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 55 },
                    { 245, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 56 },
                    { 246, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 57 },
                    { 247, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 58 },
                    { 248, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 59 },
                    { 249, 210, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 60 },
                    { 250, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 61 },
                    { 251, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 62 },
                    { 252, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 63 },
                    { 253, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 64 },
                    { 254, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 65 },
                    { 255, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 66 },
                    { 256, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 67 },
                    { 257, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 68 },
                    { 258, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 69 },
                    { 259, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 70 },
                    { 260, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 71 },
                    { 261, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 72 },
                    { 262, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 73 },
                    { 263, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 74 },
                    { 264, 211, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 75 },
                    { 265, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 76 },
                    { 266, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 77 },
                    { 267, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 78 },
                    { 268, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 79 },
                    { 269, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 80 },
                    { 270, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 81 },
                    { 271, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 82 },
                    { 272, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 83 },
                    { 273, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 84 },
                    { 274, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 85 },
                    { 275, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 86 },
                    { 276, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 87 },
                    { 277, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 88 },
                    { 278, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 89 },
                    { 279, 212, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 90 },
                    { 280, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 1 },
                    { 281, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 2 },
                    { 282, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 3 },
                    { 283, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 4 },
                    { 284, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 5 },
                    { 285, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 6 },
                    { 286, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 7 },
                    { 287, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 8 },
                    { 288, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 9 },
                    { 289, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 10 },
                    { 290, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 11 },
                    { 291, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 12 },
                    { 292, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 13 },
                    { 293, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 14 },
                    { 294, 213, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 15 },
                    { 295, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 16 },
                    { 296, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 17 },
                    { 297, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 18 },
                    { 298, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 19 },
                    { 299, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 20 },
                    { 300, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 21 },
                    { 301, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 22 },
                    { 302, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 23 },
                    { 303, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 24 },
                    { 304, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 25 },
                    { 305, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 26 },
                    { 306, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 27 },
                    { 307, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 28 },
                    { 308, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 29 },
                    { 309, 214, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 30 },
                    { 310, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 31 },
                    { 311, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 32 },
                    { 312, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 33 },
                    { 313, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 34 },
                    { 314, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 35 },
                    { 315, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 36 },
                    { 316, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 37 },
                    { 317, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 38 },
                    { 318, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 39 },
                    { 319, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 40 },
                    { 320, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 41 },
                    { 321, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 42 },
                    { 322, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 43 },
                    { 323, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 44 },
                    { 324, 215, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 45 },
                    { 325, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 46 },
                    { 326, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 47 },
                    { 327, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 48 },
                    { 328, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 49 },
                    { 329, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 50 },
                    { 330, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 51 },
                    { 331, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 52 },
                    { 332, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 53 },
                    { 333, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 54 },
                    { 334, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 55 },
                    { 335, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 56 },
                    { 336, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 57 },
                    { 337, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 58 },
                    { 338, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 59 },
                    { 339, 216, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 60 },
                    { 340, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 61 },
                    { 341, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 62 },
                    { 342, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 63 },
                    { 343, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 64 },
                    { 344, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 65 },
                    { 345, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 66 },
                    { 346, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 67 },
                    { 347, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 68 },
                    { 348, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 69 },
                    { 349, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 70 },
                    { 350, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 71 },
                    { 351, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 72 },
                    { 352, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 73 },
                    { 353, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 74 },
                    { 354, 217, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 75 },
                    { 355, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 76 },
                    { 356, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 77 },
                    { 357, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 78 },
                    { 358, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 79 },
                    { 359, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 80 },
                    { 360, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 81 },
                    { 361, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 82 },
                    { 362, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 83 },
                    { 363, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 84 },
                    { 364, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 85 },
                    { 365, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 86 },
                    { 366, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 87 },
                    { 367, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 88 },
                    { 368, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 89 },
                    { 369, 218, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 90 },
                    { 370, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 1 },
                    { 371, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 2 },
                    { 372, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 3 },
                    { 373, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 4 },
                    { 374, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 5 },
                    { 375, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 6 },
                    { 376, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 7 },
                    { 377, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 8 },
                    { 378, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 9 },
                    { 379, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 10 },
                    { 380, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 11 },
                    { 381, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 12 },
                    { 382, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 13 },
                    { 383, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 14 },
                    { 384, 219, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 15 },
                    { 385, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 16 },
                    { 386, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 17 },
                    { 387, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 18 },
                    { 388, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 19 },
                    { 389, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 20 },
                    { 390, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 21 },
                    { 391, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 22 },
                    { 392, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 23 },
                    { 393, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 24 },
                    { 394, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 25 },
                    { 395, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 26 },
                    { 396, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 27 },
                    { 397, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 28 },
                    { 398, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 29 },
                    { 399, 220, null, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "DangHoc", 30 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 293);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 294);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 295);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 296);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 297);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 298);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 299);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 303);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 304);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 305);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 306);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 307);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 308);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 309);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 310);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 311);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 312);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 313);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 314);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 315);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 316);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 317);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 318);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 319);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 320);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 321);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 322);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 323);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 324);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 325);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 326);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 327);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 328);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 329);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 330);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 331);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 332);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 333);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 334);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 335);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 336);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 337);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 338);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 339);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 340);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 341);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 342);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 343);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 344);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 345);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 346);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 347);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 348);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 349);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 350);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 351);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 352);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 353);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 354);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 355);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 356);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 357);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 358);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 359);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 360);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 361);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 362);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 368);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 369);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 370);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 371);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 372);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 373);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 374);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 375);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 376);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 377);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 378);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 379);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 380);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 381);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 382);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 383);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 384);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 385);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 386);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 387);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 388);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 389);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 390);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 391);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 392);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 393);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 394);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 395);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 396);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 397);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 398);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumn: "EnrollmentId",
                keyValue: 399);
        }
    }
}
