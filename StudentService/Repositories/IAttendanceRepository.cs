using StudentService.Models;

namespace StudentService.Repositories;

public interface IAttendanceRepository
{
    Task<IEnumerable<Attendance>> GetAttendancesByClassAsync(int classId);
    Task<IEnumerable<Attendance>> GetAttendancesByDateAsync(int classId, DateTime date);
    Task<Attendance?> GetAttendanceByIdAsync(int id);
    Task<Attendance?> GetAttendanceByEnrollmentAndDateAsync(int enrollmentId, DateTime date);
    Task<IEnumerable<Enrollment>> GetEnrollmentsWithAttendancesByStudentAsync(int studentId);
    Task AddAttendanceAsync(Attendance attendance);
    void UpdateAttendance(Attendance attendance);
    Task<bool> SaveChangesAsync();
}
