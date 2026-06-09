using MediatR;
using System.Collections.Generic;

namespace StudentService.Features.Enrollments.Commands;

public record LaunchClassFromQueueCommand(
    int CourseId,
    int ClassId,
    List<int> StudentIds
) : IRequest<bool>;
