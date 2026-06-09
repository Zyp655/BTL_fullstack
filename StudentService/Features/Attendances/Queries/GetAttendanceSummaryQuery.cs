using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Attendances.Queries;

public record GetAttendanceSummaryQuery(int StudentId) : IRequest<List<AttendanceSummaryDto>>;
