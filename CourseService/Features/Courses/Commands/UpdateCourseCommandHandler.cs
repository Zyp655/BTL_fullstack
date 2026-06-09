using MediatR;
using CourseService.DTOs;
using CourseService.Repositories;
using CourseService.Common.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace CourseService.Features.Courses.Commands;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, CourseDto>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMemoryCache _memoryCache;

    public UpdateCourseCommandHandler(ICourseRepository courseRepository, IMemoryCache memoryCache)
    {
        _courseRepository = courseRepository;
        _memoryCache = memoryCache;
    }

    public async Task<CourseDto> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetCourseByIdAsync(request.Id);
        if (course == null)
            throw new NotFoundException("Khóa học", request.Id);

        course.CourseName = request.CourseName;
        course.Description = request.Description;
        course.Level = request.Level;
        course.Category = request.Category;
        course.Fee = request.Fee;
        course.TotalSessions = request.TotalSessions;
        course.IsActive = request.IsActive;
        course.UpdatedAt = DateTime.UtcNow;

        _courseRepository.UpdateCourse(course);
        await _courseRepository.SaveChangesAsync();

        // Invalidate cache
        var currentVersion = _memoryCache.GetOrCreate("courses_version", entry => 1);
        _memoryCache.Set("courses_version", currentVersion + 1);

        return new CourseDto
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
}
