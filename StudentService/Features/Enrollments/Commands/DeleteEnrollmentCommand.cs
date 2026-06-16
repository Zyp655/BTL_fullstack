using MediatR;

namespace StudentService.Features.Enrollments.Commands;

public record DeleteEnrollmentCommand(int Id) : IRequest<bool>;
