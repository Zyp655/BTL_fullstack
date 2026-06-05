using CourseService.Models;

namespace CourseService.Repositories;

public interface IClassRepository
{
    Task<IEnumerable<Class>> GetClassesAsync(int? courseId, int? teacherId, string? status, string? search, int page, int pageSize);
    Task<int> GetClassesCountAsync(int? courseId, int? teacherId, string? status, string? search);
    Task<Class?> GetClassByIdAsync(int id);
    Task<IEnumerable<Class>> GetClassesByTeacherAsync(int teacherId);
    Task AddClassAsync(Class cls);
    void UpdateClass(Class cls);
    void DeleteClass(Class cls);
    Task<bool> SaveChangesAsync();
}
