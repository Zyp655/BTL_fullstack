using MediatR;
using CourseService.Repositories;
using CourseService.Common.Exceptions;

namespace CourseService.Features.Schedules.Commands;

public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, bool>
{
    private readonly IScheduleRepository _scheduleRepository;

    public DeleteScheduleCommandHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<bool> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _scheduleRepository.GetScheduleByIdAsync(request.ScheduleId, request.ClassId);
        if (schedule == null)
            throw new NotFoundException("Lịch học", request.ScheduleId);

        _scheduleRepository.DeleteSchedule(schedule);
        return await _scheduleRepository.SaveChangesAsync();
    }
}
