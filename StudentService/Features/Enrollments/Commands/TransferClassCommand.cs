using MediatR;
using StudentService.DTOs;

namespace StudentService.Features.Enrollments.Commands;

public record TransferClassCommand(
    int StudentId,
    int FromClassId,
    int ToClassId
) : IRequest<EnrollmentDto>;
