using MediatR;
using CourseService.DTOs;
using CourseService.Models;
using CourseService.Repositories;
using CourseService.Common.Exceptions;

namespace CourseService.Features.Schedules.Commands;

public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, ScheduleDto>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IClassRepository _classRepository;

    public CreateScheduleCommandHandler(IScheduleRepository scheduleRepository, IClassRepository classRepository)
    {
        _scheduleRepository = scheduleRepository;
        _classRepository = classRepository;
    }

    public async Task<ScheduleDto> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        var classExists = await _classRepository.GetClassByIdAsync(request.ClassId) != null;
        if (!classExists)
            throw new NotFoundException("Lớp học", request.ClassId);

        if (!TimeSpan.TryParse(request.StartTime, out var startTime))
            throw new ArgumentException("StartTime không hợp lệ (format: HH:mm)");

        if (!TimeSpan.TryParse(request.EndTime, out var endTime))
            throw new ArgumentException("EndTime không hợp lệ (format: HH:mm)");

        var schedule = new Schedule
        {
            ClassId = request.ClassId,
            DayOfWeek = request.DayOfWeek,
            Session = request.Session,
            StartTime = startTime,
            EndTime = endTime
        };

        await _scheduleRepository.AddScheduleAsync(schedule);
        await _scheduleRepository.SaveChangesAsync();

        return ScheduleMapper.MapToDto(schedule);
    }
}
