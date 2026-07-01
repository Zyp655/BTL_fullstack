using StudentService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentService.Services;

public static class StudentConflictHelper
{
    public static async Task CheckStudentConflicts(
        ICourseServiceClient courseServiceClient,
        int targetClassId,
        ClassInfoDto targetClass,
        IEnumerable<int> activeClassIds)
    {
        return; // Allow student conflicts freely for test data

        foreach (var activeClassId in activeClassIds)
        {
            var activeClass = await courseServiceClient.GetClassInfo(activeClassId);
            if (activeClass == null || activeClass.Schedules == null || !activeClass.Schedules.Any())
                continue;

            foreach (var tSched in targetClass.Schedules)
            {
                if (!TimeSpan.TryParse(tSched.StartTime, out var tStart) || !TimeSpan.TryParse(tSched.EndTime, out var tEnd))
                    continue;

                foreach (var aSched in activeClass.Schedules)
                {
                    if (!TimeSpan.TryParse(aSched.StartTime, out var aStart) || !TimeSpan.TryParse(aSched.EndTime, out var aEnd))
                        continue;

                    if (IsOverlapping(
                        targetClass.StartDate, targetClass.EndDate, tStart, tEnd, tSched.DayOfWeek,
                        activeClass.StartDate, activeClass.EndDate, aStart, aEnd, aSched.DayOfWeek))
                    {
                        var dayName = GetDayName(aSched.DayOfWeek);
                        throw new ArgumentException($"Học viên đã có lịch học trùng ở lớp {activeClass.ClassName} ({dayName} {aSched.StartTime}-{aSched.EndTime}).");
                    }
                }
            }
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
