using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Classes.Queries;

public record GetClassesByTeacherQuery(int TeacherId) : IRequest<List<ClassDto>>;
