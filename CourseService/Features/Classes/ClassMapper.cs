using CourseService.DTOs;
using CourseService.Models;

namespace CourseService.Features.Classes;

/// <summary>
/// Shared mapper for Class → ClassDto to avoid duplication across handlers
/// </summary>
public static class ClassMapper
{
    public static ClassDto MapToDto(Class cls) => new()
    {
        ClassId = cls.ClassId,
        CourseId = cls.CourseId,
        CourseName = cls.Course?.CourseName ?? "",
        ClassName = cls.ClassName,
        TeacherId = cls.TeacherId,
        TeacherName = cls.TeacherName,
        TeacherId2 = cls.TeacherId2,
        TeacherName2 = cls.TeacherName2,
        Room = cls.Room,
        MaxStudents = cls.MaxStudents,
        CurrentStudents = cls.CurrentStudents,
        Status = cls.Status,
        TotalSessions = cls.TotalSessions,
        StartDate = cls.StartDate,
        EndDate = cls.EndDate,
        CreatedAt = cls.CreatedAt,
        Schedules = cls.Schedules?.Select(s => new ScheduleDto
        {
            ScheduleId = s.ScheduleId,
            ClassId = s.ClassId,
            DayOfWeek = s.DayOfWeek,
            DayOfWeekName = GetDayName(s.DayOfWeek),
            Session = s.Session,
            StartTime = s.StartTime.ToString(@"hh\:mm"),
            EndTime = s.EndTime.ToString(@"hh\:mm")
        }).ToList() ?? new()
    };

    public static string GetDayName(int day) => day switch
    {
        0 => "Chủ nhật", 2 => "Thứ 2", 3 => "Thứ 3",
        4 => "Thứ 4", 5 => "Thứ 5", 6 => "Thứ 6", 7 => "Thứ 7",
        _ => ""
    };
}
