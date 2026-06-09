using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Schedules.Commands;

public record UpdateScheduleCommand(
    int ClassId,
    int ScheduleId,
    int DayOfWeek,
    string Session,
    string StartTime,
    string EndTime
) : IRequest<ScheduleDto>;
