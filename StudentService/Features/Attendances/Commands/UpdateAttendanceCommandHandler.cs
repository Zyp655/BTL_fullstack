using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;

namespace StudentService.Features.Attendances.Commands;

public class UpdateAttendanceCommandHandler : IRequestHandler<UpdateAttendanceCommand, AttendanceDto?>
{
    private readonly IAttendanceRepository _attendanceRepository;

    public UpdateAttendanceCommandHandler(IAttendanceRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    public async Task<AttendanceDto?> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
    {
        var attendance = await _attendanceRepository.GetAttendanceByIdAsync(request.Id);
        if (attendance == null) return null;

        attendance.Status = request.Status;
        attendance.Note = request.Note;

        _attendanceRepository.UpdateAttendance(attendance);
        await _attendanceRepository.SaveChangesAsync();

        return AttendanceMapper.MapToDto(attendance);
    }
}
