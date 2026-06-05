using CourseService.DTOs;

namespace CourseService.Services;

public interface ICourseService
{
    Task<PagedResult<CourseDto>> GetCoursesAsync(string? search, string? category, string? level, bool? isActive, int page, int pageSize);
    Task<CourseDto?> GetCourseByIdAsync(int id);
    Task<CourseDto> CreateCourseAsync(CreateCourseDto dto);
    Task<CourseDto?> UpdateCourseAsync(int id, UpdateCourseDto dto);
    Task<bool> DeleteCourseAsync(int id);
}
