using MediatR;
using System.Collections.Generic;

namespace StudentService.Features.Enrollments.Commands;

public record StudentResolutionDto(int StudentId, string Action, int? NewClassId);

public record ResolveCancelledClassCommand(
    int ClassId,
    List<StudentResolutionDto> Resolutions
) : IRequest<bool>;
