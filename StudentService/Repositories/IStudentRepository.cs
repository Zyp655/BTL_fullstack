using StudentService.Models;

namespace StudentService.Repositories;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetStudentsAsync(string? search, string? gender, int page, int pageSize);
    Task<int> GetStudentsCountAsync(string? search, string? gender);
    Task<Student?> GetStudentByIdAsync(int id);
    Task<Student?> GetStudentByUserIdAsync(int userId);
    Task<bool> ExistsByUserIdAsync(int userId);
    Task AddStudentAsync(Student student);
    void UpdateStudent(Student student);
    Task<int> GetEnrollmentCountAsync(int studentId);
    Task<bool> SaveChangesAsync();
}
