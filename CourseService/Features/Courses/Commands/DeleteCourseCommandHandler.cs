using MediatR;
using CourseService.Repositories;
using CourseService.Common.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using MassTransit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CourseService.Features.Courses.Commands;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, bool>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IClassRepository _classRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMemoryCache _memoryCache;

    public DeleteCourseCommandHandler(
        ICourseRepository courseRepository, 
        IClassRepository classRepository,
        IPublishEndpoint publishEndpoint,
        IMemoryCache memoryCache)
    {
        _courseRepository = courseRepository;
        _classRepository = classRepository;
        _publishEndpoint = publishEndpoint;
        _memoryCache = memoryCache;
    }

    public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetCourseByIdAsync(request.Id);
        if (course == null)
            throw new NotFoundException("Khóa học", request.Id);

        // Soft delete the course
        course.IsActive = false;
        course.UpdatedAt = DateTime.UtcNow;
        _courseRepository.UpdateCourse(course);

        // Get all classes for this course and cancel them
        var classes = await _classRepository.GetClassesAsync(request.Id, null, null, null, 1, 10000);
        var classList = classes.ToList();
        var classIds = classList.Select(c => c.ClassId).ToList();

        foreach (var cls in classList)
        {
            if (cls.Status != "Cancelled")
            {
                cls.Status = "Cancelled";
                _classRepository.UpdateClass(cls);
            }
        }

        var success = await _courseRepository.SaveChangesAsync();

        if (success)
        {
            // Publish CourseDeletedEvent to RabbitMQ
            await _publishEndpoint.Publish<Contracts.CourseDeletedEvent>(new Contracts.CourseDeletedEvent
            {
                CourseId = request.Id,
                ClassIds = classIds
            }, cancellationToken);

            // Invalidate cache
            var currentVersion = _memoryCache.GetOrCreate("courses_version", entry => 1);
            _memoryCache.Set("courses_version", currentVersion + 1);
        }

        return success;
    }
}
