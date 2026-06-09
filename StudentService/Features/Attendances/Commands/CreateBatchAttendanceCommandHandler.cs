using MediatR;
using StudentService.DTOs;
using StudentService.Models;
using StudentService.Repositories;

namespace StudentService.Features.Attendances.Commands;

public class CreateBatchAttendanceCommandHandler : IRequestHandler<CreateBatchAttendanceCommand, List<AttendanceDto>>
{
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;

    public CreateBatchAttendanceCommandHandler(
        IAttendanceRepository attendanceRepository,
        IEnrollmentRepository enrollmentRepository)
    {
        _attendanceRepository = attendanceRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<List<AttendanceDto>> Handle(CreateBatchAttendanceCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Attendances)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(item.EnrollmentId);
            if (enrollment == null) continue;

            var existing = await _attendanceRepository.GetAttendanceByEnrollmentAndDateAsync(item.EnrollmentId, request.SessionDate);
            if (existing != null)
            {
                existing.Status = item.Status;
                existing.Note = item.Note;
                _attendanceRepository.UpdateAttendance(existing);
            }
            else
            {
                var attendance = new Attendance
                {
                    EnrollmentId = item.EnrollmentId,
                    SessionDate = request.SessionDate,
                    Status = item.Status,
                    Note = item.Note,
                    MarkedByTeacherId = null,
                    CreatedAt = DateTime.UtcNow
                };
                await _attendanceRepository.AddAttendanceAsync(attendance);
            }
        }

        await _attendanceRepository.SaveChangesAsync();

        var reloaded = await _attendanceRepository.GetAttendancesByDateAsync(request.ClassId, request.SessionDate);
        return reloaded.Select(AttendanceMapper.MapToDto).ToList();
    }
}
