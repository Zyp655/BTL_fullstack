using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Enrollments.Queries;

public record GetStudentAnalyticsQuery : IRequest<StudentAnalyticsDto>;
