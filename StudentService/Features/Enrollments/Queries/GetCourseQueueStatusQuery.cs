using MediatR;
using System.Collections.Generic;

namespace StudentService.Features.Enrollments.Queries;

public record GetCourseQueueStatusQuery : IRequest<List<CourseQueueStatusDto>>;

public class CourseQueueStatusDto
{
    public int CourseId { get; set; }
    public int Count { get; set; }
}
