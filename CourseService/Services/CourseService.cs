using CourseService.DTOs;
using CourseService.Models;
using CourseService.Repositories;

namespace CourseService.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<PagedResult<CourseDto>> GetCoursesAsync(string? search, string? category, string? level, bool? isActive, int page, int pageSize)
    {
        var items = await _courseRepository.GetCoursesAsync(search, category, level, isActive, page, pageSize);
        var totalCount = await _courseRepository.GetCoursesCountAsync(search, category, level, isActive);

        var courseDtos = items.Select(MapToDto).ToList();

        return new PagedResult<CourseDto>
        {
            Items = courseDtos,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<CourseDto?> GetCourseByIdAsync(int id)
    {
        var course = await _courseRepository.GetCourseByIdAsync(id);
        if (course == null) return null;

        return MapToDto(course);
    }

    public async Task<CourseDto> CreateCourseAsync(CreateCourseDto dto)
    {
        var course = new Course
        {
            CourseName = dto.CourseName,
            Description = dto.Description,
            Level = dto.Level,
            Category = dto.Category,
            Fee = dto.Fee,
            TotalSessions = dto.TotalSessions,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _courseRepository.AddCourseAsync(course);
        await _courseRepository.SaveChangesAsync();

        return MapToDto(course);
    }

    public async Task<CourseDto?> UpdateCourseAsync(int id, UpdateCourseDto dto)
    {
        var course = await _courseRepository.GetCourseByIdAsync(id);
        if (course == null) return null;

        course.CourseName = dto.CourseName;
        course.Description = dto.Description;
        course.Level = dto.Level;
        course.Category = dto.Category;
        course.Fee = dto.Fee;
        course.TotalSessions = dto.TotalSessions;
        course.IsActive = dto.IsActive;
        course.UpdatedAt = DateTime.UtcNow;

        _courseRepository.UpdateCourse(course);
        await _courseRepository.SaveChangesAsync();

        return MapToDto(course);
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        var course = await _courseRepository.GetCourseByIdAsync(id);
        if (course == null) return false;

        // Soft delete
        course.IsActive = false;
        course.UpdatedAt = DateTime.UtcNow;

        _courseRepository.UpdateCourse(course);
        return await _courseRepository.SaveChangesAsync();
    }

    private static CourseDto MapToDto(Course course) => new()
    {
        CourseId = course.CourseId,
        CourseName = course.CourseName,
        Description = course.Description,
        Level = course.Level,
        Category = course.Category,
        Fee = course.Fee,
        TotalSessions = course.TotalSessions,
        IsActive = course.IsActive,
        CreatedAt = course.CreatedAt,
        UpdatedAt = course.UpdatedAt,
        ClassCount = course.Classes?.Count ?? 0
    };
}
