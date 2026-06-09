using MediatR;

namespace CourseService.Features.Classes.Commands;

public record UpdateClassStudentCountCommand(
    int Id,
    int Delta
) : IRequest<bool>;
