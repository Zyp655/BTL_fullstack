using MediatR;
using CourseService.DTOs;
using CourseService.Models;
using CourseService.Repositories;
using CourseService.Common.Exceptions;
using CourseService.Data;
using MassTransit;

namespace CourseService.Features.Classes.Commands;

public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, ClassDto>
{
    private readonly IClassRepository _classRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    private readonly CourseDbContext _context;

    public CreateClassCommandHandler(
        IClassRepository classRepository,
        ICourseRepository courseRepository,
        IPublishEndpoint publishEndpoint,
        CourseDbContext context)
    {
        _classRepository = classRepository;
        _courseRepository = courseRepository;
        _publishEndpoint = publishEndpoint;
        _context = context;
    }

    public async Task<ClassDto> Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetCourseByIdAsync(request.CourseId);
        if (course == null)
            throw new NotFoundException("Khóa học", request.CourseId);

        if (!string.IsNullOrWhiteSpace(request.Room))
        {
            var normalizedRoom = request.Room;
            if (request.Room.StartsWith("P.", StringComparison.OrdinalIgnoreCase))
            {
                normalizedRoom = request.Room.Substring(2);
            }
            else if (request.Room.StartsWith("Phòng ", StringComparison.OrdinalIgnoreCase))
            {
                normalizedRoom = request.Room.Substring(6);
            }

            var classroom = await _context.Classrooms.FindAsync(normalizedRoom);
            if (classroom != null && classroom.IsMaintenance)
            {
                throw new ArgumentException($"Phòng học {request.Room} đang trong trạng thái bảo trì và không thể sử dụng.");
            }
        }

        var cls = new Class
        {
            CourseId = request.CourseId,
            ClassName = request.ClassName,
            TeacherId = request.TeacherId,
            TeacherName = request.TeacherName,
            TeacherId2 = request.TeacherId2,
            TeacherName2 = request.TeacherName2,
            Room = request.Room,
            MaxStudents = request.MaxStudents,
            CurrentStudents = 0,
            Status = "Opened",
            TotalSessions = request.TotalSessions ?? course.TotalSessions,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            CreatedAt = DateTime.UtcNow
        };

        await _classRepository.AddClassAsync(cls);
        await _classRepository.SaveChangesAsync();

        // Publish event class.opened when a class is opened
        await _publishEndpoint.Publish<Contracts.ClassOpenedEvent>(new Contracts.ClassOpenedEvent
        {
            ClassId = cls.ClassId,
            CourseId = cls.CourseId,
            ClassName = cls.ClassName,
            CourseName = course.CourseName,
            TeacherId = cls.TeacherId,
            TeacherName = cls.TeacherName,
            TeacherId2 = cls.TeacherId2,
            TeacherName2 = cls.TeacherName2,
            StartDate = cls.StartDate
        }, cancellationToken);

        var reloaded = await _classRepository.GetClassByIdAsync(cls.ClassId);
        return MapToDto(reloaded!);
    }

    private static ClassDto MapToDto(Class cls) => new()
    {
        ClassId = cls.ClassId,
        CourseId = cls.CourseId,
        CourseName = cls.Course?.CourseName ?? "",
        ClassName = cls.ClassName,
        TeacherId = cls.TeacherId,
        TeacherName = cls.TeacherName,
        TeacherId2 = cls.TeacherId2,
        TeacherName2 = cls.TeacherName2,
        Room = cls.Room,
        MaxStudents = cls.MaxStudents,
        CurrentStudents = cls.CurrentStudents,
        Status = cls.Status,
        TotalSessions = cls.TotalSessions,
        StartDate = cls.StartDate,
        EndDate = cls.EndDate,
        CreatedAt = cls.CreatedAt,
        Schedules = cls.Schedules?.Select(s => new ScheduleDto
        {
            ScheduleId = s.ScheduleId,
            ClassId = s.ClassId,
            DayOfWeek = s.DayOfWeek,
            DayOfWeekName = GetDayName(s.DayOfWeek),
            Session = s.Session,
            StartTime = s.StartTime.ToString(@"hh\:mm"),
            EndTime = s.EndTime.ToString(@"hh\:mm")
        }).ToList() ?? new()
    };

    private static string GetDayName(int day) => day switch
    {
        0 => "Chủ nhật", 2 => "Thứ 2", 3 => "Thứ 3",
        4 => "Thứ 4", 5 => "Thứ 5", 6 => "Thứ 6", 7 => "Thứ 7",
        _ => ""
    };
}
