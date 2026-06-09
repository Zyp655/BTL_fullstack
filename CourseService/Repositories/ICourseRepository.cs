using CourseService.Models;

namespace CourseService.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetCoursesAsync(string? search, string? category, string? level, bool? isActive, int page, int pageSize);
    Task<int> GetCoursesCountAsync(string? search, string? category, string? level, bool? isActive);
    Task<Course?> GetCourseByIdAsync(int id);
    Task AddCourseAsync(Course course);
    void UpdateCourse(Course course);
    Task<int> GetClassCountAsync(int courseId);
    Task<bool> SaveChangesAsync();
}
