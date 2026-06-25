using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StudentService.Models;

public class QuizQuestion
{
    [Key]
    public int QuestionId { get; set; }

    [Required]
    public int QuizId { get; set; }

    [Required]
    [MaxLength(1000)]
    public string QuestionText { get; set; } = string.Empty;

    // For multiple choice, e.g. "A. Option A|B. Option B|C. Option C|D. Option D"
    public string? Options { get; set; }

    [MaxLength(500)]
    public string? CorrectAnswer { get; set; } // For multiple choice: "A", "B", "C", or "D". For essay: guiding answer text.

    // Navigation
    [ForeignKey("QuizId")]
    [JsonIgnore]
    public Quiz? Quiz { get; set; }
}
