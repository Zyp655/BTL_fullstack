using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Classes.Queries;

public record GetClassByIdQuery(int Id) : IRequest<ClassDto>;
