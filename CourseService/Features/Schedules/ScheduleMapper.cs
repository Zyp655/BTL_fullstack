using CourseService.DTOs;
using CourseService.Models;

namespace CourseService.Features.Schedules;

public static class ScheduleMapper
{
    public static ScheduleDto MapToDto(Schedule s) => new()
    {
        ScheduleId = s.ScheduleId,
        ClassId = s.ClassId,
        DayOfWeek = s.DayOfWeek,
        DayOfWeekName = GetDayName(s.DayOfWeek),
        Session = s.Session,
        StartTime = s.StartTime.ToString(@"hh\:mm"),
        EndTime = s.EndTime.ToString(@"hh\:mm")
    };

    private static string GetDayName(int day) => day switch
    {
        0 => "Chủ nhật", 2 => "Thứ 2", 3 => "Thứ 3",
        4 => "Thứ 4", 5 => "Thứ 5", 6 => "Thứ 6", 7 => "Thứ 7",
        _ => ""
    };
}
