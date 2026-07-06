using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentService.Migrations
{
    /// <inheritdoc />
    public partial class AddQuizAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableFrom",
                table: "Quizzes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AvailableTo",
                table: "Quizzes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxAttempts",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TeacherNote",
                table: "QuizSubmissions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableFrom",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "AvailableTo",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "MaxAttempts",
                table: "Quizzes");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherNote",
                table: "QuizSubmissions",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
