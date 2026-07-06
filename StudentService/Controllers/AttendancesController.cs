using Microsoft.AspNetCore.Mvc;
using StudentService.DTOs;
using StudentService.Features.Attendances.Commands;
using StudentService.Features.Attendances.Queries;
using StudentService.Services;
using StudentService.Data;
using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace StudentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class AttendancesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICourseServiceClient _courseServiceClient;
    private readonly StudentDbContext _context;

    public AttendancesController(IMediator mediator, ICourseServiceClient courseServiceClient, StudentDbContext context)
    {
        _mediator = mediator;
        _courseServiceClient = courseServiceClient;
        _context = context;
    }

    /// <summary>
    /// Điểm danh của lớp
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien")]
    [HttpGet("class/{classId}")]
    public async Task<ActionResult<List<AttendanceDto>>> GetAttendancesByClass(int classId)
    {
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        
        if (role == "GiaoVien" && !string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(classId);
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
            {
                return Forbid();
            }
        }

        var result = await _mediator.Send(new GetAttendancesByClassQuery(classId));
        return Ok(result);
    }

    /// <summary>
    /// Điểm danh theo ngày
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien")]
    [HttpGet("class/{classId}/date/{date}")]
    public async Task<ActionResult<List<AttendanceDto>>> GetAttendancesByDate(int classId, DateTime date)
    {
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        
        if (role == "GiaoVien" && !string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(classId);
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
            {
                return Forbid();
            }
        }

        var result = await _mediator.Send(new GetAttendancesByDateQuery(classId, date));
        return Ok(result);
    }

    /// <summary>
    /// Điểm danh batch (nhiều học viên cùng lúc)
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien")]
    [HttpPost]
    public async Task<ActionResult<List<AttendanceDto>>> CreateAttendance(CreateBatchAttendanceCommand command)
    {
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        
        if (role == "GiaoVien" && !string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(command.ClassId);
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
            {
                return Forbid();
            }
        }

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Sửa điểm danh
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien")]
    [HttpPut("{id}")]
    public async Task<ActionResult<AttendanceDto>> UpdateAttendance(int id, UpdateAttendanceCommand command)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound(new { message = "Không tìm thấy bản ghi điểm danh" });

        return Ok(result);
    }

    /// <summary>
    /// Tỷ lệ chuyên cần của học viên
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien,HocVien")]
    [HttpGet("student/{studentId}/summary")]
    public async Task<ActionResult<List<AttendanceSummaryDto>>> GetAttendanceSummary(int studentId)
    {
        var result = await _mediator.Send(new GetAttendanceSummaryQuery(studentId));
        return Ok(result);
    }
    /// <summary>
    /// Lấy thống kê số buổi dạy và số lượt học viên tham gia (cho tính lương)
    /// </summary>
    [AllowAnonymous]
    [HttpPost("stats")]
    public async Task<ActionResult<TeacherAttendanceStatsDto>> GetAttendanceStats([FromBody] AttendanceStatsRequest request)
    {
        if (request.ClassIds == null || !request.ClassIds.Any())
        {
            return Ok(new TeacherAttendanceStatsDto { SessionsTaught = 0, TotalStudentSessions = 0 });
        }

        var enrollments = await _context.Enrollments
            .Where(e => request.ClassIds.Contains(e.ClassId))
            .ToListAsync();
        
        var enrollmentIds = enrollments.Select(e => e.EnrollmentId).ToList();

        var attendances = await _context.Attendances
            .Where(a => enrollmentIds.Contains(a.EnrollmentId) 
                        && a.SessionDate.Month == request.Month 
                        && a.SessionDate.Year == request.Year)
            .ToListAsync();

        var sessions = attendances
            .Join(enrollments, a => a.EnrollmentId, e => e.EnrollmentId, (a, e) => new { e.ClassId, Date = a.SessionDate.Date })
            .Distinct()
            .ToList();

        int sessionsTaught = sessions.Count;

        int totalStudentSessions = attendances
            .Count(a => a.Status == "CoMat" || a.Status == "DiTre");

        return Ok(new TeacherAttendanceStatsDto
        {
            SessionsTaught = sessionsTaught,
            TotalStudentSessions = totalStudentSessions
        });
    }
}
