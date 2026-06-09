using Microsoft.AspNetCore.Mvc;
using StudentService.DTOs;
using StudentService.Features.Attendances.Commands;
using StudentService.Features.Attendances.Queries;
using StudentService.Services;
using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;

namespace StudentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class AttendancesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICourseServiceClient _courseServiceClient;

    public AttendancesController(IMediator mediator, ICourseServiceClient courseServiceClient)
    {
        _mediator = mediator;
        _courseServiceClient = courseServiceClient;
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
            if (classInfo == null || classInfo.TeacherId != teacherId)
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
            if (classInfo == null || classInfo.TeacherId != teacherId)
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
            if (classInfo == null || classInfo.TeacherId != teacherId)
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
}
