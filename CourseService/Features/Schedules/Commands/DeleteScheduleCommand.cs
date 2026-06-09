using MediatR;

namespace CourseService.Features.Schedules.Commands;

public record DeleteScheduleCommand(int ClassId, int ScheduleId) : IRequest<bool>;
