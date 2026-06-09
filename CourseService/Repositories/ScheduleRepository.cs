using Microsoft.EntityFrameworkCore;
using CourseService.Data;
using CourseService.Models;

namespace CourseService.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly CourseDbContext _context;

    public ScheduleRepository(CourseDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Schedule>> GetSchedulesByClassAsync(int classId)
    {
        return await _context.Schedules
            .Where(s => s.ClassId == classId)
            .OrderBy(s => s.DayOfWeek)
            .ThenBy(s => s.StartTime)
            .ToListAsync();
    }

    public async Task<Schedule?> GetScheduleByIdAsync(int scheduleId, int classId)
    {
        return await _context.Schedules
            .FirstOrDefaultAsync(s => s.ScheduleId == scheduleId && s.ClassId == classId);
    }

    public async Task AddScheduleAsync(Schedule schedule)
    {
        await _context.Schedules.AddAsync(schedule);
    }

    public void UpdateSchedule(Schedule schedule)
    {
        _context.Schedules.Update(schedule);
    }

    public void DeleteSchedule(Schedule schedule)
    {
        _context.Schedules.Remove(schedule);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
