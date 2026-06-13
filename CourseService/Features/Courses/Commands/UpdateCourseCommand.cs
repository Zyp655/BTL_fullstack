using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Courses.Commands;

public record UpdateCourseCommand(
    int Id,
    string CourseName,
    string? Description,
    string? ImageUrl,
    string Level,
    string Category,
    decimal Fee,
    int TotalSessions,
    bool IsActive
) : IRequest<CourseDto>;
