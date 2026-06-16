using MediatR;
using CourseService.DTOs;

namespace CourseService.Features.Classes.Commands;

public record CreateClassCommand(
    int CourseId,
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
