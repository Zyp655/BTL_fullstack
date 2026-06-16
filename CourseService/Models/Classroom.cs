using System.ComponentModel.DataAnnotations;

namespace CourseService.Models;

public class Classroom
{
    [Key]
    [MaxLength(50)]
    public string RoomNumber { get; set; } = string.Empty;

    public bool IsMaintenance { get; set; } = false;

    [MaxLength(250)]
    public string? Notes { get; set; }
}
