using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Schedules.Queries;

public record GetSchedulesByClassQuery(int ClassId) : IRequest<List<ScheduleDto>>;
