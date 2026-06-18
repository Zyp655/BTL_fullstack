using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;
using StudentService.Services;

namespace StudentService.Features.Attendances.Queries;

public class GetAttendanceSummaryQueryHandler : IRequestHandler<GetAttendanceSummaryQuery, List<AttendanceSummaryDto>>
{
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ICourseServiceClient _courseServiceClient;

    public GetAttendanceSummaryQueryHandler(
        IAttendanceRepository attendanceRepository, 
        IStudentRepository studentRepository,
        ICourseServiceClient courseServiceClient)
    {
        _attendanceRepository = attendanceRepository;
        _studentRepository = studentRepository;
        _courseServiceClient = courseServiceClient;
    }

    public async Task<List<AttendanceSummaryDto>> Handle(GetAttendanceSummaryQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetStudentByIdAsync(request.StudentId);
        if (student == null)
            throw new KeyNotFoundException("Không tìm thấy học viên");

        var enrollments = await _attendanceRepository.GetEnrollmentsWithAttendancesByStudentAsync(request.StudentId);

        // Fetch class and course info in parallel to optimize inter-service communication performance
        var classIdsToFetch = enrollments.Where(e => e.ClassId >= 0).Select(e => e.ClassId).Distinct().ToList();
        var courseIdsToFetch = enrollments.Where(e => e.ClassId < 0).Select(e => -e.ClassId).Distinct().ToList();

        var classTasks = classIdsToFetch.Select(id => _courseServiceClient.GetClassInfo(id)).ToList();
        var courseTasks = courseIdsToFetch.Select(id => _courseServiceClient.GetCourseInfo(id)).ToList();

        await Task.WhenAll(classTasks.Concat(courseTasks.Cast<Task>()));

        var classInfoMap = classTasks
            .Select(t => t.Result)
            .Where(x => x != null)
            .Select(x => x!)
            .ToDictionary(x => x.ClassId, x => x);

        var courseInfoMap = courseTasks
            .Select(t => t.Result)
            .Where(x => x != null)
            .Select(x => x!)
            .ToDictionary(x => x.CourseId, x => x);

        var result = new List<AttendanceSummaryDto>();

        foreach (var e in enrollments)
        {
            var total = e.Attendances.Count;
            var present = e.Attendances.Count(a => a.Status == "CoMat");
            var absent = e.Attendances.Count(a => a.Status == "Vang");
            var late = e.Attendances.Count(a => a.Status == "DiTre");
            var excused = e.Attendances.Count(a => a.Status == "CoPhep");

            var dto = new AttendanceSummaryDto
            {
                StudentId = request.StudentId,
                StudentName = student.FullName,
                ClassId = e.ClassId,
                TotalSessions = total,
                Present = present,
                Absent = absent,
                Late = late,
                Excused = excused,
                AttendanceRate = total > 0 ? Math.Min(100.0, Math.Round((double)(present + excused + late) / total * 100, 1)) : 100.0,
                Sessions = e.Attendances.Select(a => new AttendanceSessionDto
                {
                    AttendanceId = a.AttendanceId,
                    SessionDate = a.SessionDate,
                    Status = a.Status,
                    Note = a.Note
                }).OrderByDescending(a => a.SessionDate).ToList()
            };

            if (e.ClassId < 0)
            {
                var courseId = -e.ClassId;
                if (courseInfoMap.TryGetValue(courseId, out var courseInfo))
                {
                    dto.ClassName = "Chờ xếp lớp";
                    dto.CourseName = courseInfo.CourseName;
                }
            }
            else
            {
                if (classInfoMap.TryGetValue(e.ClassId, out var classInfo))
                {
                    dto.ClassName = classInfo.ClassName;
                    dto.CourseName = classInfo.CourseName;
                }
            }

            result.Add(dto);
        }

        return result;
    }
}
