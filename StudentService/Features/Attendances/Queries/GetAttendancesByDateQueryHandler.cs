using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;

namespace StudentService.Features.Attendances.Queries;

public class GetAttendancesByDateQueryHandler : IRequestHandler<GetAttendancesByDateQuery, List<AttendanceDto>>
{
    private readonly IAttendanceRepository _attendanceRepository;

    public GetAttendancesByDateQueryHandler(IAttendanceRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    public async Task<List<AttendanceDto>> Handle(GetAttendancesByDateQuery request, CancellationToken cancellationToken)
    {
        var attendances = await _attendanceRepository.GetAttendancesByDateAsync(request.ClassId, request.Date);
        return attendances.Select(AttendanceMapper.MapToDto).ToList();
    }
}
