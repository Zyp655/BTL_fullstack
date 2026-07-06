using MediatR;
using CourseService.DTOs;
using CourseService.Models;
using CourseService.Repositories;
using CourseService.Common;
using CourseService.Common.Exceptions;

namespace CourseService.Features.Schedules.Commands;

public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, ScheduleDto>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IClassRepository _classRepository;
    private readonly ConflictDetector _conflictDetector;

    public CreateScheduleCommandHandler(
        IScheduleRepository scheduleRepository, 
        IClassRepository classRepository,
        ConflictDetector conflictDetector)
    {
        _scheduleRepository = scheduleRepository;
        _classRepository = classRepository;
        _conflictDetector = conflictDetector;
    }

    public async Task<ScheduleDto> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        var cls = await _classRepository.GetClassByIdAsync(request.ClassId);
        if (cls == null)
            throw new NotFoundException("Lớp học", request.ClassId);

        if (!TimeSpan.TryParse(request.StartTime, out var startTime))
            throw new ArgumentException("StartTime không hợp lệ (format: HH:mm)");

        if (!TimeSpan.TryParse(request.EndTime, out var endTime))
            throw new ArgumentException("EndTime không hợp lệ (format: HH:mm)");

        // Check teacher conflict
        if (cls.TeacherId.HasValue)
        {
            await _conflictDetector.CheckTeacherConflictAsync(
                cls.TeacherId.Value, cls.ClassId, request.DayOfWeek, startTime, endTime, 
                cls.StartDate, cls.EndDate, cls.TeacherName ?? "Giáo viên");
        }

        var scheduleRoom = string.IsNullOrWhiteSpace(request.Room) ? cls.Room : request.Room;

        // Check room conflict
        if (!string.IsNullOrWhiteSpace(scheduleRoom))
        {
            await _conflictDetector.CheckRoomConflictAsync(
                scheduleRoom, cls.ClassId, request.DayOfWeek, startTime, endTime, 
                cls.StartDate, cls.EndDate);
        }

        var schedule = new Schedule
        {
            ClassId = request.ClassId,
            DayOfWeek = request.DayOfWeek,
            Session = request.Session,
            StartTime = startTime,
            EndTime = endTime,
            Room = request.Room
        };

        await _scheduleRepository.AddScheduleAsync(schedule);
        await _scheduleRepository.SaveChangesAsync();

        return ScheduleMapper.MapToDto(schedule);
    }
}
