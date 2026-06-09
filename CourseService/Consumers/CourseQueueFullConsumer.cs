using System;
using System.Threading.Tasks;
using MassTransit;
using Contracts;
using CourseService.Models;
using CourseService.Repositories;
using Microsoft.Extensions.Logging;

namespace CourseService.Consumers;

public class CourseQueueFullConsumer : IConsumer<CourseQueueFullEvent>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IClassRepository _classRepository;
    private readonly ILogger<CourseQueueFullConsumer> _logger;

    public CourseQueueFullConsumer(
        ICourseRepository courseRepository,
        IClassRepository classRepository,
        ILogger<CourseQueueFullConsumer> logger)
    {
        _courseRepository = courseRepository;
        _classRepository = classRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CourseQueueFullEvent> context)
    {
        var evt = context.Message;
        _logger.LogInformation("Processing CourseQueueFullEvent for CourseId: {CourseId}, CourseName: {CourseName}", evt.CourseId, evt.CourseName);

        try
        {
            var course = await _courseRepository.GetCourseByIdAsync(evt.CourseId);
            if (course == null)
            {
                _logger.LogWarning("Course with CourseId {CourseId} was not found. Cannot create class.", evt.CourseId);
                return;
            }

            var newClass = new Class
            {
                CourseId = evt.CourseId,
                ClassName = $"Lớp {course.CourseName} - Tự Động",
                MaxStudents = 5,
                CurrentStudents = 0,
                Status = "Opened",
                CreatedAt = DateTime.UtcNow
            };

            await _classRepository.AddClassAsync(newClass);
            await _classRepository.SaveChangesAsync();

            _logger.LogInformation("Successfully created class {ClassName} with ClassId: {ClassId}", newClass.ClassName, newClass.ClassId);

            // Publish ClassCreatedFromQueueEvent
            await context.Publish<ClassCreatedFromQueueEvent>(new ClassCreatedFromQueueEvent
            {
                ClassId = newClass.ClassId,
                CourseId = evt.CourseId,
                CourseName = course.CourseName,
                StudentIds = evt.StudentIds,
                StudentUserIds = evt.StudentUserIds
            });

            // Publish ClassOpenedEvent
            await context.Publish<ClassOpenedEvent>(new ClassOpenedEvent
            {
                ClassId = newClass.ClassId,
                CourseId = newClass.CourseId,
                ClassName = newClass.ClassName,
                CourseName = course.CourseName,
                TeacherId = newClass.TeacherId,
                TeacherName = newClass.TeacherName,
                StartDate = newClass.StartDate
            });

            _logger.LogInformation("Successfully published events for ClassId: {ClassId}", newClass.ClassId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing CourseQueueFullEvent for CourseId: {CourseId}", evt.CourseId);
            throw;
        }
    }
}
