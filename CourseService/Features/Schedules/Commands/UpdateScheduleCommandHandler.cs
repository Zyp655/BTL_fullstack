using MediatR;
using CourseService.DTOs;
using CourseService.Repositories;
using CourseService.Common.Exceptions;

namespace CourseService.Features.Schedules.Commands;

public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, ScheduleDto>
{
    private readonly IScheduleRepository _scheduleRepository;

    public UpdateScheduleCommandHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<ScheduleDto> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _scheduleRepository.GetScheduleByIdAsync(request.ScheduleId, request.ClassId);
        if (schedule == null)
            throw new NotFoundException("Lịch học", request.ScheduleId);

        if (!TimeSpan.TryParse(request.StartTime, out var startTime))
            throw new ArgumentException("StartTime không hợp lệ");

        if (!TimeSpan.TryParse(request.EndTime, out var endTime))
            throw new ArgumentException("EndTime không hợp lệ");

        schedule.DayOfWeek = request.DayOfWeek;
        schedule.Session = request.Session;
        schedule.StartTime = startTime;
        schedule.EndTime = endTime;

        _scheduleRepository.UpdateSchedule(schedule);
        await _scheduleRepository.SaveChangesAsync();

        return ScheduleMapper.MapToDto(schedule);
    }
}
