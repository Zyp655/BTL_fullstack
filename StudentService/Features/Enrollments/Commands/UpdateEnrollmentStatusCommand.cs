using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Enrollments.Commands;

public record UpdateEnrollmentStatusCommand(
    int Id,
    string Status
) : IRequest<EnrollmentDto?>;
