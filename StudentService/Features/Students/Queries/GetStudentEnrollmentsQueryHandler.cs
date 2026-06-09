using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;
using StudentService.Services;

namespace StudentService.Features.Students.Queries;

public class GetStudentEnrollmentsQueryHandler : IRequestHandler<GetStudentEnrollmentsQuery, List<EnrollmentDto>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseServiceClient _courseServiceClient;

    public GetStudentEnrollmentsQueryHandler(IEnrollmentRepository enrollmentRepository, ICourseServiceClient courseServiceClient)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseServiceClient = courseServiceClient;
    }

    public async Task<List<EnrollmentDto>> Handle(GetStudentEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentRepository.GetEnrollmentsAsync(classId: null, studentId: request.StudentId, status: null, page: 1, pageSize: 9999);
        
        var dtoList = new List<EnrollmentDto>();
        foreach (var e in enrollments)
        {
            var dto = new EnrollmentDto
            {
                EnrollmentId = e.EnrollmentId,
                StudentId = e.StudentId,
                StudentName = e.Student?.FullName,
                ClassId = e.ClassId,
                Status = e.Status,
                EnrolledAt = e.EnrolledAt,
                CompletedAt = e.CompletedAt
            };

            if (e.ClassId < 0)
            {
                var courseId = -e.ClassId;
                var courseInfo = await _courseServiceClient.GetCourseInfo(courseId);
                if (courseInfo != null)
                {
                    dto.ClassName = "Chờ xếp lớp";
                    dto.CourseName = courseInfo.CourseName;
                    dto.CourseId = courseId;
                }
            }
            else
            {
                var classInfo = await _courseServiceClient.GetClassInfo(e.ClassId);
                if (classInfo != null)
                {
                    dto.ClassName = classInfo.ClassName;
                    dto.CourseName = classInfo.CourseName;
                    dto.CourseId = classInfo.CourseId;
                }
            }

            dtoList.Add(dto);
        }

        return dtoList;
    }
}
