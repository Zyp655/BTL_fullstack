using StudentService.Models;

namespace StudentService.Repositories;

public interface IResultRepository
{
    Task<IEnumerable<ExamResult>> GetResultsByEnrollmentAsync(int enrollmentId);
    Task<ExamResult?> GetResultByIdAsync(int id);
    Task<IEnumerable<Enrollment>> GetEnrollmentsWithResultsByClassAsync(int classId);
    Task AddResultAsync(ExamResult result);
    Task<bool> SaveChangesAsync();
}
