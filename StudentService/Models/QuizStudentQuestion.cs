using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentService.Models;

public class QuizStudentQuestion
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int QuizId { get; set; }

    [Required]
    public int StudentId { get; set; }

    [Required]
    [MaxLength(1000)]
    public string QuestionText { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(2000)]
    public string? AnswerText { get; set; }

    public DateTime? AnsweredAt { get; set; }

    // Navigation
    [ForeignKey("QuizId")]
    public Quiz? Quiz { get; set; }

    [ForeignKey("StudentId")]
    public Student? Student { get; set; }
}
