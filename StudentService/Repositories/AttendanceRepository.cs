using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Models;

namespace StudentService.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly StudentDbContext _context;

    public AttendanceRepository(StudentDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Attendance>> GetAttendancesByClassAsync(int classId)
    {
        return await _context.Attendances
            .Include(a => a.Enrollment)
                .ThenInclude(e => e!.Student)
            .Where(a => a.Enrollment!.ClassId == classId)
            .OrderByDescending(a => a.SessionDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Attendance>> GetAttendancesByDateAsync(int classId, DateTime date)
    {
        var targetDate = date.Date;
        return await _context.Attendances
            .Include(a => a.Enrollment)
                .ThenInclude(e => e!.Student)
            .Where(a => a.Enrollment!.ClassId == classId && a.SessionDate.Date == targetDate)
            .ToListAsync();
    }

    public async Task<Attendance?> GetAttendanceByIdAsync(int id)
    {
        return await _context.Attendances
            .Include(a => a.Enrollment)
                .ThenInclude(e => e!.Student)
            .FirstOrDefaultAsync(a => a.AttendanceId == id);
    }

    public async Task<Attendance?> GetAttendanceByEnrollmentAndDateAsync(int enrollmentId, DateTime date)
    {
        var targetDate = date.Date;
        return await _context.Attendances
            .FirstOrDefaultAsync(a => a.EnrollmentId == enrollmentId && a.SessionDate.Date == targetDate);
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsWithAttendancesByStudentAsync(int studentId)
    {
        return await _context.Enrollments
            .Include(e => e.Attendances)
            .AsNoTracking()
            .Where(e => e.StudentId == studentId)
            .ToListAsync();
    }

    public async Task AddAttendanceAsync(Attendance attendance)
    {
        await _context.Attendances.AddAsync(attendance);
    }

    public void UpdateAttendance(Attendance attendance)
    {
        _context.Attendances.Update(attendance);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
