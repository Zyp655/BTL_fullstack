using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Classes.Commands;

public record UpdateClassCommand(
    int Id,
    string ClassName,
    int? TeacherId,
    string? TeacherName,
    int? TeacherId2,
    string? TeacherName2,
    string? Room,
    int MaxStudents,
    int? TotalSessions,
    DateTime? StartDate,
    DateTime? EndDate
) : IRequest<ClassDto>;
