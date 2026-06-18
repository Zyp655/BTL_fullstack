using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;
using StudentService.Services;
using StudentService.Features.Enrollments;

namespace StudentService.Features.Enrollments.Queries;

public class GetEnrollmentsQueryHandler : IRequestHandler<GetEnrollmentsQuery, PagedResult<EnrollmentDto>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseServiceClient _courseServiceClient;

    public GetEnrollmentsQueryHandler(IEnrollmentRepository enrollmentRepository, ICourseServiceClient courseServiceClient)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseServiceClient = courseServiceClient;
    }

    public async Task<PagedResult<EnrollmentDto>> Handle(GetEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        var items = await _enrollmentRepository.GetEnrollmentsAsync(request.ClassId, request.StudentId, request.Status, request.Page, request.PageSize);
        var totalCount = await _enrollmentRepository.GetEnrollmentsCountAsync(request.ClassId, request.StudentId, request.Status);

        var dtoList = new List<EnrollmentDto>();
        foreach (var e in items)
        {
            var dto = EnrollmentMapper.MapToDto(e);
            var classInfo = await _courseServiceClient.GetClassInfo(e.ClassId);
            if (classInfo != null)
            {
                dto.ClassName = classInfo.ClassName;
                dto.CourseName = classInfo.CourseName;
                dto.CourseId = classInfo.CourseId;
                dto.TeacherId = classInfo.TeacherId;
                dto.TeacherName = classInfo.TeacherName;
                dto.Room = classInfo.Room;
                dto.StartDate = classInfo.StartDate;
                dto.EndDate = classInfo.EndDate;
            }
            dtoList.Add(dto);
        }

        return new PagedResult<EnrollmentDto>
        {
            Items = dtoList,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
