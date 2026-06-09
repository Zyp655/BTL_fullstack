using MediatR;
using CourseService.DTOs;
using CourseService.Repositories;
using CourseService.Common.Exceptions;

namespace CourseService.Features.Classes.Commands;

public class UpdateClassCommandHandler : IRequestHandler<UpdateClassCommand, ClassDto>
{
    private readonly IClassRepository _classRepository;

    public UpdateClassCommandHandler(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<ClassDto> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
    {
        var cls = await _classRepository.GetClassByIdAsync(request.Id);
        if (cls == null)
            throw new NotFoundException("Lớp học", request.Id);

        cls.ClassName = request.ClassName;
        cls.TeacherId = request.TeacherId;
        cls.TeacherName = request.TeacherName;
        cls.Room = request.Room;
        cls.MaxStudents = request.MaxStudents;
        if (request.TotalSessions.HasValue)
        {
            cls.TotalSessions = request.TotalSessions.Value;
        }
        cls.StartDate = request.StartDate;
        cls.EndDate = request.EndDate;

        _classRepository.UpdateClass(cls);
        await _classRepository.SaveChangesAsync();

        return new ClassDto
        {
            ClassId = cls.ClassId, CourseId = cls.CourseId, CourseName = cls.Course?.CourseName ?? "",
            ClassName = cls.ClassName, TeacherId = cls.TeacherId, TeacherName = cls.TeacherName,
            Room = cls.Room, MaxStudents = cls.MaxStudents, CurrentStudents = cls.CurrentStudents,
            Status = cls.Status, TotalSessions = cls.TotalSessions, StartDate = cls.StartDate, EndDate = cls.EndDate, CreatedAt = cls.CreatedAt,
            Schedules = cls.Schedules?.Select(s => new ScheduleDto
            {
                ScheduleId = s.ScheduleId, ClassId = s.ClassId, DayOfWeek = s.DayOfWeek,
                DayOfWeekName = s.DayOfWeek switch { 0 => "Chủ nhật", 2 => "Thứ 2", 3 => "Thứ 3", 4 => "Thứ 4", 5 => "Thứ 5", 6 => "Thứ 6", 7 => "Thứ 7", _ => "" },
                Session = s.Session, StartTime = s.StartTime.ToString(@"hh\:mm"), EndTime = s.EndTime.ToString(@"hh\:mm")
            }).ToList() ?? new()
        };
    }
}
