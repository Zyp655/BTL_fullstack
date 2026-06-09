using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Attendances.Commands;

public record CreateBatchAttendanceCommand(
    int ClassId,
    DateTime SessionDate,
    List<CreateAttendanceDto> Attendances
) : IRequest<List<AttendanceDto>>;
