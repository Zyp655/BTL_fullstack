using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentService.Migrations
{
    /// <inheritdoc />
    public partial class SeedRemainingLethihoaEnrollments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Class 6: currently has 2 students (7, 8). Let's enroll students 1 to 15 (except 7, 8), so it has 15 total.
            migrationBuilder.Sql("INSERT INTO Enrollments (ClassId, StudentId, Status, EnrolledAt) VALUES " +
                "(6, 1, 'DangHoc', '2026-06-10T00:00:00')," +
                "(6, 2, 'DangHoc', '2026-06-10T00:00:00')," +
                "(6, 3, 'DangHoc', '2026-06-10T00:00:00')," +
                "(6, 4, 'DangHoc', '2026-06-10T00:00:00')," +
                "(6, 5, 'DangHoc', '2026-06-10T00:00:00')," +
                "(6, 6, 'DangHoc', '2026-06-10T00:00:00')," +
                "(6, 9, 'DangHoc', '2026-06-10T00:00:00')," +
                "(6, 10, 'DangHoc', '2026-06-10T00:00:00')," +
                "(6, 11, 'DangHoc', '2026-06-10T00:00:00')," +
                "(6, 12, 'DangHoc', '2026-06-10T00:00:00')," +
                "(6, 13, 'DangHoc', '2026-06-10T00:00:00')," +
                "(6, 14, 'DangHoc', '2026-06-10T00:00:00')," +
                "(6, 15, 'DangHoc', '2026-06-10T00:00:00');");

            // Class 9: currently has 0 students. Let's enroll students 16 to 30.
            migrationBuilder.Sql("INSERT INTO Enrollments (ClassId, StudentId, Status, EnrolledAt) VALUES " +
                "(9, 16, 'DangHoc', '2026-06-10T00:00:00')," +
                "(9, 17, 'DangHoc', '2026-06-10T00:00:00')," +
                "(9, 18, 'DangHoc', '2026-06-10T00:00:00')," +
                "(9, 19, 'DangHoc', '2026-06-10T00:00:00')," +
                "(9, 20, 'DangHoc', '2026-06-10T00:00:00')," +
                "(9, 21, 'DangHoc', '2026-06-10T00:00:00')," +
                "(9, 22, 'DangHoc', '2026-06-10T00:00:00')," +
                "(9, 23, 'DangHoc', '2026-06-10T00:00:00')," +
                "(9, 24, 'DangHoc', '2026-06-10T00:00:00')," +
                "(9, 25, 'DangHoc', '2026-06-10T00:00:00')," +
                "(9, 26, 'DangHoc', '2026-06-10T00:00:00')," +
                "(9, 27, 'DangHoc', '2026-06-10T00:00:00')," +
                "(9, 28, 'DangHoc', '2026-06-10T00:00:00')," +
                "(9, 29, 'DangHoc', '2026-06-10T00:00:00')," +
                "(9, 30, 'DangHoc', '2026-06-10T00:00:00');");

            // Class 28: currently has 0 students. Let's enroll students 31 to 45.
            migrationBuilder.Sql("INSERT INTO Enrollments (ClassId, StudentId, Status, EnrolledAt) VALUES " +
                "(28, 31, 'DangHoc', '2026-06-10T00:00:00')," +
                "(28, 32, 'DangHoc', '2026-06-10T00:00:00')," +
                "(28, 33, 'DangHoc', '2026-06-10T00:00:00')," +
                "(28, 34, 'DangHoc', '2026-06-10T00:00:00')," +
                "(28, 35, 'DangHoc', '2026-06-10T00:00:00')," +
                "(28, 36, 'DangHoc', '2026-06-10T00:00:00')," +
                "(28, 37, 'DangHoc', '2026-06-10T00:00:00')," +
                "(28, 38, 'DangHoc', '2026-06-10T00:00:00')," +
                "(28, 39, 'DangHoc', '2026-06-10T00:00:00')," +
                "(28, 40, 'DangHoc', '2026-06-10T00:00:00')," +
                "(28, 41, 'DangHoc', '2026-06-10T00:00:00')," +
                "(28, 42, 'DangHoc', '2026-06-10T00:00:00')," +
                "(28, 43, 'DangHoc', '2026-06-10T00:00:00')," +
                "(28, 44, 'DangHoc', '2026-06-10T00:00:00')," +
                "(28, 45, 'DangHoc', '2026-06-10T00:00:00');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Enrollments WHERE ClassId IN (6, 9, 28) AND StudentId BETWEEN 1 AND 45;");
        }
    }
}
