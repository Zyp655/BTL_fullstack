using CourseService.DTOs;
using CourseService.Models;
using CourseService.Repositories;
using CourseService.Validators;

namespace CourseService.Services;

public class ClassService : IClassService
{
    private readonly IClassRepository _classRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IClassStatusValidator _statusValidator;

    public ClassService(
        IClassRepository classRepository,
        ICourseRepository courseRepository,
        IClassStatusValidator statusValidator)
    {
        _classRepository = classRepository;
        _courseRepository = courseRepository;
        _statusValidator = statusValidator;
    }

    public async Task<PagedResult<ClassDto>> GetClassesAsync(int? courseId, int? teacherId, string? status, string? search, int page, int pageSize)
    {
        var items = await _classRepository.GetClassesAsync(courseId, teacherId, status, search, page, pageSize);
        var totalCount = await _classRepository.GetClassesCountAsync(courseId, teacherId, status, search);

        return new PagedResult<ClassDto>
        {
            Items = items.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<ClassDto?> GetClassByIdAsync(int id)
    {
        var cls = await _classRepository.GetClassByIdAsync(id);
        if (cls == null) return null;

        return MapToDto(cls);
    }

    public async Task<IEnumerable<ClassDto>> GetClassesByTeacherAsync(int teacherId)
    {
        var classes = await _classRepository.GetClassesByTeacherAsync(teacherId);
        return classes.Select(MapToDto);
    }

    public async Task<ClassDto> CreateClassAsync(CreateClassDto dto)
    {
        var course = await _courseRepository.GetCourseByIdAsync(dto.CourseId);
        if (course == null)
            throw new KeyNotFoundException("Khóa học không tồn tại");

        var cls = new Class
        {
            CourseId = dto.CourseId,
            ClassName = dto.ClassName,
            TeacherId = dto.TeacherId,
            TeacherName = dto.TeacherName,
            Room = dto.Room,
            MaxStudents = dto.MaxStudents,
            CurrentStudents = 0,
            Status = "Opened",
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            CreatedAt = DateTime.UtcNow
        };

        await _classRepository.AddClassAsync(cls);
        await _classRepository.SaveChangesAsync();

        // Reload with includes
        var reloadedCls = await _classRepository.GetClassByIdAsync(cls.ClassId);
        return MapToDto(reloadedCls!);
    }

    public async Task<ClassDto?> UpdateClassAsync(int id, UpdateClassDto dto)
    {
        var cls = await _classRepository.GetClassByIdAsync(id);
        if (cls == null) return null;

        cls.ClassName = dto.ClassName;
        cls.TeacherId = dto.TeacherId;
        cls.TeacherName = dto.TeacherName;
        cls.Room = dto.Room;
        cls.MaxStudents = dto.MaxStudents;
        cls.StartDate = dto.StartDate;
        cls.EndDate = dto.EndDate;

        _classRepository.UpdateClass(cls);
        await _classRepository.SaveChangesAsync();

        return MapToDto(cls);
    }

    public async Task<ClassDto?> UpdateClassStatusAsync(int id, string status)
    {
        var cls = await _classRepository.GetClassByIdAsync(id);
        if (cls == null) return null;

        if (!_statusValidator.CanTransition(cls.Status, status, out var errorMessage))
            throw new ArgumentException(errorMessage);

        cls.Status = status;
        _classRepository.UpdateClass(cls);
        await _classRepository.SaveChangesAsync();

        return MapToDto(cls);
    }

    public async Task<bool> DeleteClassAsync(int id)
    {
        var cls = await _classRepository.GetClassByIdAsync(id);
        if (cls == null) return false;

        if (cls.CurrentStudents > 0)
            throw new InvalidOperationException("Không thể xóa lớp đang có học viên");

        _classRepository.DeleteClass(cls);
        return await _classRepository.SaveChangesAsync();
    }

    private static ClassDto MapToDto(Class cls) => new()
    {
        ClassId = cls.ClassId,
        CourseId = cls.CourseId,
        CourseName = cls.Course?.CourseName ?? "",
        ClassName = cls.ClassName,
        TeacherId = cls.TeacherId,
        TeacherName = cls.TeacherName,
        Room = cls.Room,
        MaxStudents = cls.MaxStudents,
        CurrentStudents = cls.CurrentStudents,
        Status = cls.Status,
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
        0 => "Chủ nhật",
        2 => "Thứ 2",
        3 => "Thứ 3",
        4 => "Thứ 4",
        5 => "Thứ 5",
        6 => "Thứ 6",
        7 => "Thứ 7",
        _ => ""
    };
}
