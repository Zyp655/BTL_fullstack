using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Courses.Queries;

public record GetCourseByIdQuery(int Id) : IRequest<CourseDto>;
