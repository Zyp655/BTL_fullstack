using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentService.Models;

public class Quiz
{
    [Key]
    public int QuizId { get; set; }

    [Required]
    public int ClassId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public int DurationMinutes { get; set; } = 15;

    [Required]
    [MaxLength(50)]
    public string QuizType { get; set; } = "TracNghiem"; // TracNghiem, TuLuan

    public int MaxAttempts { get; set; } = 1;

    public DateTime? LessonDate { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
}
