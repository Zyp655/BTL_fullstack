using MediatR;

namespace CourseService.Features.Courses.Commands;

public record DeleteCourseCommand(int Id) : IRequest<bool>;
