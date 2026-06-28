using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentService.Models;

public class QuizSubmission
{
    [Key]
    public int SubmissionId { get; set; }

    [Required]
    public int QuizId { get; set; }

    [Required]
    public int EnrollmentId { get; set; }

    [Required]
    public int StudentId { get; set; }

    [Required]
    public string AnswersJson { get; set; } = string.Empty; // Maps questionId -> answer selected/written

    [Column(TypeName = "decimal(5,2)")]
    public decimal? Score { get; set; }

    public string? TeacherNote { get; set; }

    public bool IsGraded { get; set; } = false;

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    [ForeignKey("QuizId")]
    public Quiz? Quiz { get; set; }

    [ForeignKey("EnrollmentId")]
    public Enrollment? Enrollment { get; set; }
}
