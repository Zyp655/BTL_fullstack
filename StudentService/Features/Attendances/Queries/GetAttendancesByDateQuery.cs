using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Attendances.Queries;

public record GetAttendancesByDateQuery(int ClassId, DateTime Date) : IRequest<List<AttendanceDto>>;
