using CourseService.DTOs;

namespace CourseService.Services;

public interface IScheduleService
{
    Task<IEnumerable<ScheduleDto>> GetSchedulesAsync(int classId);
    Task<ScheduleDto> CreateScheduleAsync(int classId, CreateScheduleDto dto);
    Task<ScheduleDto?> UpdateScheduleAsync(int classId, int scheduleId, UpdateScheduleDto dto);
    Task<bool> DeleteScheduleAsync(int classId, int scheduleId);
}
