using MediatR;

namespace CourseService.Features.Classes.Commands;

public record DeleteClassCommand(int Id) : IRequest<bool>;
