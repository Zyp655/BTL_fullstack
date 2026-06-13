using MediatR;
using CourseService.DTOs;
using CourseService.Models;
using CourseService.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace CourseService.Features.Courses.Commands;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CourseDto>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMemoryCache _memoryCache;

    public CreateCourseCommandHandler(ICourseRepository courseRepository, IMemoryCache memoryCache)
    {
        _courseRepository = courseRepository;
        _memoryCache = memoryCache;
    }

    public async Task<CourseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            CourseName = request.CourseName,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            Level = request.Level,
            Category = request.Category,
            Fee = request.Fee,
            TotalSessions = request.TotalSessions,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _courseRepository.AddCourseAsync(course);
        await _courseRepository.SaveChangesAsync();

        // Invalidate cache
        var currentVersion = _memoryCache.GetOrCreate("courses_version", entry => 1);
        _memoryCache.Set("courses_version", currentVersion + 1);

        return new CourseDto
        {
            CourseId = course.CourseId,
            CourseName = course.CourseName,
            Description = course.Description,
            ImageUrl = course.ImageUrl,
            Level = course.Level,
            Category = course.Category,
            Fee = course.Fee,
            TotalSessions = course.TotalSessions,
            IsActive = course.IsActive,
            CreatedAt = course.CreatedAt,
            UpdatedAt = course.UpdatedAt,
            ClassCount = 0
        };
    }
}
