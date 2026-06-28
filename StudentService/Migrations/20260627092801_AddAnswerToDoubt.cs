using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentService.Migrations
{
    /// <inheritdoc />
    public partial class AddAnswerToDoubt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnswerText",
                table: "QuizStudentQuestions",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AnsweredAt",
                table: "QuizStudentQuestions",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerText",
                table: "QuizStudentQuestions");

            migrationBuilder.DropColumn(
                name: "AnsweredAt",
                table: "QuizStudentQuestions");
        }
    }
}
