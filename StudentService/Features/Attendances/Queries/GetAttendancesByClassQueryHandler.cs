using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;

namespace StudentService.Features.Attendances.Queries;

public class GetAttendancesByClassQueryHandler : IRequestHandler<GetAttendancesByClassQuery, List<AttendanceDto>>
{
    private readonly IAttendanceRepository _attendanceRepository;

    public GetAttendancesByClassQueryHandler(IAttendanceRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    public async Task<List<AttendanceDto>> Handle(GetAttendancesByClassQuery request, CancellationToken cancellationToken)
    {
        var attendances = await _attendanceRepository.GetAttendancesByClassAsync(request.ClassId);
        return attendances.Select(AttendanceMapper.MapToDto).ToList();
    }
}
