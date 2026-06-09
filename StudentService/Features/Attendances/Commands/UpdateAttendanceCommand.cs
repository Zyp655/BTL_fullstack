using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Attendances.Commands;

public record UpdateAttendanceCommand(
    int Id,
    string Status,
    string? Note
) : IRequest<AttendanceDto?>;
