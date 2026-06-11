using MediatR;
using CourseService.DTOs;
using CourseService.Repositories;
using CourseService.Common;
using CourseService.Common.Exceptions;

namespace CourseService.Features.Schedules.Commands;

public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, ScheduleDto>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IClassRepository _classRepository;
    private readonly ConflictDetector _conflictDetector;

    public UpdateScheduleCommandHandler(
        IScheduleRepository scheduleRepository, 
        IClassRepository classRepository,
        ConflictDetector conflictDetector)
    {
        _scheduleRepository = scheduleRepository;
        _classRepository = classRepository;
        _conflictDetector = conflictDetector;
    }

    public async Task<ScheduleDto> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
    {
        var schedule = await _scheduleRepository.GetScheduleByIdAsync(request.ScheduleId, request.ClassId);
        if (schedule == null)
            throw new NotFoundException("Lịch học", request.ScheduleId);

        var cls = await _classRepository.GetClassByIdAsync(request.ClassId);
        if (cls == null)
            throw new NotFoundException("Lớp học", request.ClassId);

        if (!TimeSpan.TryParse(request.StartTime, out var startTime))
            throw new ArgumentException("StartTime không hợp lệ");

        if (!TimeSpan.TryParse(request.EndTime, out var endTime))
            throw new ArgumentException("EndTime không hợp lệ");

        // Check teacher conflict
        if (cls.TeacherId.HasValue)
        {
            await _conflictDetector.CheckTeacherConflictAsync(
                cls.TeacherId.Value, cls.ClassId, request.DayOfWeek, startTime, endTime, 
                cls.StartDate, cls.EndDate, cls.TeacherName ?? "Giáo viên", schedule.ScheduleId);
        }

        // Check room conflict
        if (!string.IsNullOrWhiteSpace(cls.Room))
        {
            await _conflictDetector.CheckRoomConflictAsync(
                cls.Room, cls.ClassId, request.DayOfWeek, startTime, endTime, 
                cls.StartDate, cls.EndDate, schedule.ScheduleId);
        }

        schedule.DayOfWeek = request.DayOfWeek;
        schedule.Session = request.Session;
        schedule.StartTime = startTime;
        schedule.EndTime = endTime;

        _scheduleRepository.UpdateSchedule(schedule);
        await _scheduleRepository.SaveChangesAsync();

        return ScheduleMapper.MapToDto(schedule);
    }
}
