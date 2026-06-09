using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Courses.Commands;

public record CreateCourseCommand(
    string CourseName,
    string? Description,
    string Level,
    string Category,
    decimal Fee,
    int TotalSessions
) : IRequest<CourseDto>;
