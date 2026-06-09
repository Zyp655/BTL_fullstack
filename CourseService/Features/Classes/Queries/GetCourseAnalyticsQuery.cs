using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Classes.Queries;

public record GetCourseAnalyticsQuery : IRequest<CourseAnalyticsDto>;
