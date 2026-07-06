using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Schedules.Commands;

public record CreateScheduleCommand(
    int ClassId,
    int DayOfWeek,
    string Session,
    string StartTime,
    string EndTime,
    string? Room = null
) : IRequest<ScheduleDto>;
