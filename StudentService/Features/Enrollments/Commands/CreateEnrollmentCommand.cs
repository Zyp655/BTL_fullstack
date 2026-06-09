using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Enrollments.Commands;

public record CreateEnrollmentCommand(
    int StudentId,
    int ClassId
) : IRequest<EnrollmentDto>;
