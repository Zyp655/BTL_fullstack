using MediatR;

namespace StudentService.Features.Enrollments.Commands;

public record EnrollInCourseQueueCommand(
    int StudentId,
    int CourseId
) : IRequest<bool>;
