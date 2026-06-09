using StudentService.Models;

namespace StudentService.Repositories;

public interface IEnrollmentRepository
{
    Task<IEnumerable<Enrollment>> GetEnrollmentsAsync(int? classId, int? studentId, string? status, int page, int pageSize);
    Task<int> GetEnrollmentsCountAsync(int? classId, int? studentId, string? status);
    Task<Enrollment?> GetEnrollmentByIdAsync(int id);
    Task<bool> HasActiveEnrollmentAsync(int studentId, int classId);
    Task AddEnrollmentAsync(Enrollment enrollment);
    void UpdateEnrollment(Enrollment enrollment);
    Task<IEnumerable<Student>> GetStudentsByClassAsync(int classId);
    Task<bool> SaveChangesAsync();
}
