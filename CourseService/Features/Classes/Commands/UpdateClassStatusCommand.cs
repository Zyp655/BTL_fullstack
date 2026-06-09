using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Classes.Commands;

public record UpdateClassStatusCommand(int Id, string Status) : IRequest<ClassDto>;
