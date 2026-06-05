using CourseService.DTOs;
using CourseService.Models;
using CourseService.Repositories;

namespace CourseService.Services;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IClassRepository _classRepository;

    public ScheduleService(IScheduleRepository scheduleRepository, IClassRepository classRepository)
    {
        _scheduleRepository = scheduleRepository;
        _classRepository = classRepository;
    }

    public async Task<IEnumerable<ScheduleDto>> GetSchedulesAsync(int classId)
    {
        var classExists = await _classRepository.GetClassByIdAsync(classId) != null;
        if (!classExists)
            throw new KeyNotFoundException("Không tìm thấy lớp học");

        var schedules = await _scheduleRepository.GetSchedulesByClassAsync(classId);
        return schedules.Select(MapToDto);
    }

    public async Task<ScheduleDto> CreateScheduleAsync(int classId, CreateScheduleDto dto)
    {
        var classExists = await _classRepository.GetClassByIdAsync(classId) != null;
        if (!classExists)
            throw new KeyNotFoundException("Không tìm thấy lớp học");

        if (!TimeSpan.TryParse(dto.StartTime, out var startTime))
            throw new ArgumentException("StartTime không hợp lệ (format: HH:mm)");

        if (!TimeSpan.TryParse(dto.EndTime, out var endTime))
            throw new ArgumentException("EndTime không hợp lệ (format: HH:mm)");

        var schedule = new Schedule
        {
            ClassId = classId,
            DayOfWeek = dto.DayOfWeek,
            Session = dto.Session,
            StartTime = startTime,
            EndTime = endTime
        };

        await _scheduleRepository.AddScheduleAsync(schedule);
        await _scheduleRepository.SaveChangesAsync();

        return MapToDto(schedule);
    }

    public async Task<ScheduleDto?> UpdateScheduleAsync(int classId, int scheduleId, UpdateScheduleDto dto)
    {
        var schedule = await _scheduleRepository.GetScheduleByIdAsync(scheduleId, classId);
        if (schedule == null) return null;

        if (!TimeSpan.TryParse(dto.StartTime, out var startTime))
            throw new ArgumentException("StartTime không hợp lệ");

        if (!TimeSpan.TryParse(dto.EndTime, out var endTime))
            throw new ArgumentException("EndTime không hợp lệ");

        schedule.DayOfWeek = dto.DayOfWeek;
        schedule.Session = dto.Session;
        schedule.StartTime = startTime;
        schedule.EndTime = endTime;

        _scheduleRepository.UpdateSchedule(schedule);
        await _scheduleRepository.SaveChangesAsync();

        return MapToDto(schedule);
    }

    public async Task<bool> DeleteScheduleAsync(int classId, int scheduleId)
    {
        var schedule = await _scheduleRepository.GetScheduleByIdAsync(scheduleId, classId);
        if (schedule == null) return false;

        _scheduleRepository.DeleteSchedule(schedule);
        return await _scheduleRepository.SaveChangesAsync();
    }

    private static ScheduleDto MapToDto(Schedule s) => new()
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
        0 => "Chủ nhật",
        2 => "Thứ 2",
        3 => "Thứ 3",
        4 => "Thứ 4",
        5 => "Thứ 5",
        6 => "Thứ 6",
        7 => "Thứ 7",
        _ => ""
    };
}
