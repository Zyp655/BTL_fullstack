using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Attendances.Queries;

public record GetAttendancesByClassQuery(int ClassId) : IRequest<List<AttendanceDto>>;
