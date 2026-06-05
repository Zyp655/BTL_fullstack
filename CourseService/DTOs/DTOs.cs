namespace CourseService.DTOs;

// ===== Course DTOs =====
public class CreateCourseDto
{
    public string CourseName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Level { get; set; } = "Beginner";
    public string Category { get; set; } = "NgoaiNgu";
    public decimal Fee { get; set; }
    public int TotalSessions { get; set; }
}

public class UpdateCourseDto
{
    public string CourseName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Level { get; set; } = "Beginner";
    public string Category { get; set; } = "NgoaiNgu";
    public decimal Fee { get; set; }
    public int TotalSessions { get; set; }
    public bool IsActive { get; set; } = true;
}

public class CourseDto
{
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Level { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Fee { get; set; }
    public int TotalSessions { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ClassCount { get; set; }
}

// ===== Class DTOs =====
public class CreateClassDto
{
    public int CourseId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public int? TeacherId { get; set; }
    public string? TeacherName { get; set; }
    public string? Room { get; set; }
    public int MaxStudents { get; set; } = 30;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class UpdateClassDto
{
    public string ClassName { get; set; } = string.Empty;
    public int? TeacherId { get; set; }
    public string? TeacherName { get; set; }
    public string? Room { get; set; }
    public int MaxStudents { get; set; } = 30;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class ClassDto
{
    public int ClassId { get; set; }
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public int? TeacherId { get; set; }
    public string? TeacherName { get; set; }
    public string? Room { get; set; }
    public int MaxStudents { get; set; }
    public int CurrentStudents { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ScheduleDto> Schedules { get; set; } = new();
}

public class UpdateClassStatusDto
{
    public string Status { get; set; } = string.Empty; // Opened, InProgress, Completed, Cancelled
}

// ===== Schedule DTOs =====
public class CreateScheduleDto
{
    public int DayOfWeek { get; set; }
    public string Session { get; set; } = "Sang";
    public string StartTime { get; set; } = "08:00";
    public string EndTime { get; set; } = "10:00";
}

public class UpdateScheduleDto
{
    public int DayOfWeek { get; set; }
    public string Session { get; set; } = "Sang";
    public string StartTime { get; set; } = "08:00";
    public string EndTime { get; set; } = "10:00";
}

public class ScheduleDto
{
    public int ScheduleId { get; set; }
    public int ClassId { get; set; }
    public int DayOfWeek { get; set; }
    public string DayOfWeekName { get; set; } = string.Empty;
    public string Session { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
}

// ===== Pagination =====
public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
