using Microsoft.AspNetCore.Mvc;
using StudentService.DTOs;
using StudentService.Features.ExamResults.Commands;
using StudentService.Features.ExamResults.Queries;
using StudentService.Services;
using StudentService.Repositories;
using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;

namespace StudentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class ResultsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICourseServiceClient _courseServiceClient;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IResultRepository _resultRepository;

    public ResultsController(
        IMediator mediator, 
        ICourseServiceClient courseServiceClient,
        IEnrollmentRepository enrollmentRepository,
        IResultRepository resultRepository)
    {
        _mediator = mediator;
        _courseServiceClient = courseServiceClient;
        _enrollmentRepository = enrollmentRepository;
        _resultRepository = resultRepository;
    }

    /// <summary>
    /// Kết quả của enrollment
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien,HocVien")]
    [HttpGet("enrollment/{enrollmentId}")]
    public async Task<ActionResult<List<ExamResultDto>>> GetResultsByEnrollment(int enrollmentId)
    {
        var result = await _mediator.Send(new GetResultsByEnrollmentQuery(enrollmentId));
        return Ok(result);
    }

    /// <summary>
    /// Nhập điểm
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien")]
    [HttpPost]
    public async Task<ActionResult<ExamResultDto>> CreateResult(CreateExamResultCommand command)
    {
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        
        if (role == "GiaoVien" && !string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int teacherId))
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(command.EnrollmentId);
            if (enrollment == null)
            {
                return BadRequest(new { message = "Không tìm thấy bản ghi đăng ký" });
            }
            var classInfo = await _courseServiceClient.GetClassInfo(enrollment.ClassId);
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
            {
                return Forbid();
            }
        }

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetResultsByEnrollment), new { enrollmentId = result.EnrollmentId }, result);
    }

    /// <summary>
    /// Sửa điểm
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ExamResultDto>> UpdateResult(int id, UpdateExamResultCommand command)
    {
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        
        if (role == "GiaoVien" && !string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int teacherId))
        {
            var examResult = await _resultRepository.GetResultByIdAsync(id);
            if (examResult == null)
            {
                return NotFound(new { message = "Không tìm thấy kết quả" });
            }
            var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(examResult.EnrollmentId);
            if (enrollment == null)
            {
                return BadRequest(new { message = "Không tìm thấy bản ghi đăng ký" });
            }
            var classInfo = await _courseServiceClient.GetClassInfo(enrollment.ClassId);
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
            {
                return Forbid();
            }
        }

        command = command with { Id = id };
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound(new { message = "Không tìm thấy kết quả" });

        return Ok(result);
    }

    /// <summary>
    /// Tổng kết điểm lớp
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien")]
    [HttpGet("class/{classId}/summary")]
    public async Task<ActionResult<ClassResultSummaryDto>> GetClassResultSummary(int classId)
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

        var result = await _mediator.Send(new GetClassResultSummaryQuery(classId));
        return Ok(result);
    }
}
