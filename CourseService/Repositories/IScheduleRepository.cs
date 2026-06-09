using CourseService.Models;

namespace CourseService.Repositories;

public interface IScheduleRepository
{
    Task<IEnumerable<Schedule>> GetSchedulesByClassAsync(int classId);
    Task<Schedule?> GetScheduleByIdAsync(int scheduleId, int classId);
    Task AddScheduleAsync(Schedule schedule);
    void UpdateSchedule(Schedule schedule);
    void DeleteSchedule(Schedule schedule);
    Task<bool> SaveChangesAsync();
}
