using CourseService.Data;
using CourseService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseService.Common;

public class ConflictDetector
{
    private readonly CourseDbContext _context;

    public ConflictDetector(CourseDbContext context)
    {
        _context = context;
    }

    public async Task CheckTeacherConflictAsync(int teacherId, int currentClassId, int dayOfWeek, TimeSpan startTime, TimeSpan endTime, DateTime? startDate, DateTime? endDate, string teacherName, int? excludeScheduleId = null)
    {
        var query = _context.Schedules
            .Include(s => s.Class)
            .Where(s => s.Class!.TeacherId == teacherId 
                        && s.Class.ClassId != currentClassId 
                        && (s.Class.Status == "Opened" || s.Class.Status == "InProgress"));

        if (excludeScheduleId.HasValue)
        {
            query = query.Where(s => s.ScheduleId != excludeScheduleId.Value);
        }

        var teacherSchedules = await query.ToListAsync();

        foreach (var s in teacherSchedules)
        {
            if (IsOverlapping(startDate, endDate, startTime, endTime, dayOfWeek,
                             s.Class!.StartDate, s.Class.EndDate, s.StartTime, s.EndTime, s.DayOfWeek))
            {
                var dayName = GetDayName(s.DayOfWeek);
                throw new ArgumentException($"Giảng viên {teacherName} đã có lịch dạy vào khung giờ này ở lớp {s.Class.ClassName} ({dayName} {s.StartTime:hh\\:mm}-{s.EndTime:hh\\:mm}).");
            }
        }
    }

    public async Task CheckRoomConflictAsync(string room, int currentClassId, int dayOfWeek, TimeSpan startTime, TimeSpan endTime, DateTime? startDate, DateTime? endDate, int? excludeScheduleId = null)
    {
        var query = _context.Schedules
            .Include(s => s.Class)
            .Where(s => s.Class!.Room == room 
                        && s.Class.ClassId != currentClassId 
                        && (s.Class.Status == "Opened" || s.Class.Status == "InProgress"));

        if (excludeScheduleId.HasValue)
        {
            query = query.Where(s => s.ScheduleId != excludeScheduleId.Value);
        }

        var roomSchedules = await query.ToListAsync();

        foreach (var s in roomSchedules)
        {
            if (IsOverlapping(startDate, endDate, startTime, endTime, dayOfWeek,
                             s.Class!.StartDate, s.Class.EndDate, s.StartTime, s.EndTime, s.DayOfWeek))
            {
                var dayName = GetDayName(s.DayOfWeek);
                throw new ArgumentException($"Phòng học {room} đã được sử dụng vào khung giờ này ở lớp {s.Class.ClassName} ({dayName} {s.StartTime:hh\\:mm}-{s.EndTime:hh\\:mm}).");
            }
        }
    }

    public async Task CheckClassTeacherConflictAsync(int classId, int teacherId, string teacherName, DateTime? startDate, DateTime? endDate)
    {
        var currentSchedules = await _context.Schedules.Where(s => s.ClassId == classId).ToListAsync();
        if (!currentSchedules.Any()) return;

        foreach (var schedule in currentSchedules)
        {
            await CheckTeacherConflictAsync(teacherId, classId, schedule.DayOfWeek, schedule.StartTime, schedule.EndTime, startDate, endDate, teacherName);
        }
    }

    public async Task CheckClassRoomConflictAsync(int classId, string room, DateTime? startDate, DateTime? endDate)
    {
        var currentSchedules = await _context.Schedules.Where(s => s.ClassId == classId).ToListAsync();
        if (!currentSchedules.Any()) return;

        foreach (var schedule in currentSchedules)
        {
            await CheckRoomConflictAsync(room, classId, schedule.DayOfWeek, schedule.StartTime, schedule.EndTime, startDate, endDate);
        }
    }

    private static bool IsOverlapping(
        DateTime? start1, DateTime? end1, TimeSpan startTime1, TimeSpan endTime1, int dayOfWeek1,
        DateTime? start2, DateTime? end2, TimeSpan startTime2, TimeSpan endTime2, int dayOfWeek2)
    {
        if (dayOfWeek1 != dayOfWeek2) return false;

        if (start1.HasValue && end1.HasValue && start2.HasValue && end2.HasValue)
        {
            if (start1.Value.Date > end2.Value.Date || start2.Value.Date > end1.Value.Date)
                return false;
        }

        return startTime1 < endTime2 && startTime2 < endTime1;
    }

    private static string GetDayName(int day) => day switch
    {
        0 => "Chủ nhật", 2 => "Thứ 2", 3 => "Thứ 3",
        4 => "Thứ 4", 5 => "Thứ 5", 6 => "Thứ 6", 7 => "Thứ 7",
        _ => ""
    };
}
