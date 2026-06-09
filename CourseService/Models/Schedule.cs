using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseService.Models;

public class Schedule
{
    [Key]
    public int ScheduleId { get; set; }

    [Required]
    public int ClassId { get; set; }

    public int DayOfWeek { get; set; } // 0=CN, 1=T2, 2=T3...6=T7

    [MaxLength(20)]
    public string Session { get; set; } = "Sang"; // Sang, Chieu, Toi

    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    // Navigation
    [ForeignKey("ClassId")]
    public Class? Class { get; set; }
}
