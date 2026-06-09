using MediatR;
using CourseService.Repositories;
using CourseService.Common.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace CourseService.Features.Courses.Commands;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, bool>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IMemoryCache _memoryCache;

    public DeleteCourseCommandHandler(ICourseRepository courseRepository, IMemoryCache memoryCache)
    {
        _courseRepository = courseRepository;
        _memoryCache = memoryCache;
    }

    public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetCourseByIdAsync(request.Id);
        if (course == null)
            throw new NotFoundException("Khóa học", request.Id);

        // Soft delete
        course.IsActive = false;
        course.UpdatedAt = DateTime.UtcNow;

        _courseRepository.UpdateCourse(course);
        var success = await _courseRepository.SaveChangesAsync();

        if (success)
        {
            // Invalidate cache
            var currentVersion = _memoryCache.GetOrCreate("courses_version", entry => 1);
            _memoryCache.Set("courses_version", currentVersion + 1);
        }

        return success;
    }
}
