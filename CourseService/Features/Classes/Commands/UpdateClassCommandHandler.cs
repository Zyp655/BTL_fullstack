using MediatR;
using CourseService.DTOs;
using CourseService.Repositories;
using CourseService.Common;
using CourseService.Common.Exceptions;
using CourseService.Data;

namespace CourseService.Features.Classes.Commands;

public class UpdateClassCommandHandler : IRequestHandler<UpdateClassCommand, ClassDto>
{
    private readonly IClassRepository _classRepository;
    private readonly ConflictDetector _conflictDetector;
    private readonly CourseDbContext _context;

    public UpdateClassCommandHandler(
        IClassRepository classRepository,
        ConflictDetector conflictDetector,
        CourseDbContext context)
    {
        _classRepository = classRepository;
        _conflictDetector = conflictDetector;
        _context = context;
    }

    public async Task<ClassDto> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
    {
        var cls = await _classRepository.GetClassByIdAsync(request.Id);
        if (cls == null)
            throw new NotFoundException("Lớp học", request.Id);

        // If teacher is updated or dates are updated
        if (request.TeacherId.HasValue && (request.TeacherId != cls.TeacherId || request.StartDate != cls.StartDate || request.EndDate != cls.EndDate))
        {
            await _conflictDetector.CheckClassTeacherConflictAsync(cls.ClassId, request.TeacherId.Value, request.TeacherName ?? "Giáo viên", request.StartDate, request.EndDate);
        }

        // If secondary teacher is updated or dates are updated
        if (request.TeacherId2.HasValue && (request.TeacherId2 != cls.TeacherId2 || request.StartDate != cls.StartDate || request.EndDate != cls.EndDate))
        {
            await _conflictDetector.CheckClassTeacherConflictAsync(cls.ClassId, request.TeacherId2.Value, request.TeacherName2 ?? "Giáo viên phụ", request.StartDate, request.EndDate);
        }

        // If room is updated or dates are updated
        if (!string.IsNullOrWhiteSpace(request.Room) && (request.Room != cls.Room || request.StartDate != cls.StartDate || request.EndDate != cls.EndDate))
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

            await _conflictDetector.CheckClassRoomConflictAsync(cls.ClassId, request.Room, request.StartDate, request.EndDate);
        }

        cls.ClassName = request.ClassName;
        cls.TeacherId = request.TeacherId;
        cls.TeacherName = request.TeacherName;
        cls.TeacherId2 = request.TeacherId2;
        cls.TeacherName2 = request.TeacherName2;
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
            TeacherId2 = cls.TeacherId2, TeacherName2 = cls.TeacherName2,
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
