using Microsoft.AspNetCore.Mvc;
using StudentService.DTOs;
using StudentService.Features.Enrollments.Commands;
using StudentService.Features.Enrollments.Queries;
using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;

namespace StudentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class EnrollmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EnrollmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Danh sách đăng ký (filter by classId, studentId)
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien")]
    [HttpGet]
    public async Task<ActionResult<PagedResult<EnrollmentDto>>> GetEnrollments(
        [FromQuery] int? classId,
        [FromQuery] int? studentId,
        [FromQuery] string? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetEnrollmentsQuery(classId, studentId, status, page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Đăng ký học viên vào lớp
    /// </summary>
    [Authorize(Roles = "Admin,HocVien")]
    [HttpPost]
    public async Task<ActionResult<EnrollmentDto>> CreateEnrollment(CreateEnrollmentCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetEnrollments), result);
    }

    /// <summary>
    /// Chuyển lớp học cho học viên (do học viên tự chuyển hoặc Admin đổi)
    /// </summary>
    [Authorize(Roles = "Admin,HocVien")]
    [HttpPost("transfer")]
    public async Task<ActionResult<EnrollmentDto>> TransferClass(TransferClassCommand command)
    {
        // Phân quyền: Học viên chỉ được phép tự chuyển lớp của chính mình
        if (User.IsInRole("HocVien"))
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            var student = await _mediator.Send(new StudentService.Features.Students.Queries.GetStudentByUserIdQuery(userId));
            if (student == null || student.StudentId != command.StudentId)
                return Forbid();
        }

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Đăng ký học viên vào hàng chờ khóa học
    /// </summary>
    [Authorize(Roles = "Admin,HocVien")]
    [HttpPost("course-queue")]
    public async Task<ActionResult<bool>> EnrollInCourseQueue(EnrollInCourseQueueCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Lấy trạng thái hàng chờ của các khóa học
    /// </summary>
    [AllowAnonymous]
    [HttpGet("course-queue/status")]
    public async Task<ActionResult<List<CourseQueueStatusDto>>> GetCourseQueueStatus()
    {
        var result = await _mediator.Send(new GetCourseQueueStatusQuery());
        return Ok(result);
    }

    /// <summary>
    /// Lấy danh sách ID các khóa học học viên đang xếp hàng chờ
    /// </summary>
    [Authorize(Roles = "Admin,HocVien")]
    [HttpGet("course-queue/student/{studentId}")]
    public async Task<ActionResult<List<int>>> GetStudentCourseQueue(int studentId)
    {
        var result = await _mediator.Send(new GetStudentCourseQueueQuery(studentId));
        return Ok(result);
    }

    /// <summary>
    /// Lấy danh sách học viên đang xếp hàng chờ cho một khóa học
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien")]
    [HttpGet("course-queue/course/{courseId}/students")]
    public async Task<ActionResult<List<StudentDto>>> GetStudentsInCourseQueue(int courseId)
    {
        var result = await _mediator.Send(new GetStudentsInCourseQueueQuery(courseId));
        return Ok(result);
    }

    /// <summary>
    /// Cập nhật trạng thái đăng ký
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/status")]
    public async Task<ActionResult<EnrollmentDto>> UpdateEnrollmentStatus(int id, UpdateEnrollmentStatusCommand command)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound(new { message = "Không tìm thấy đăng ký" });

        return Ok(result);
    }

    /// <summary>
    /// Xóa đăng ký học viên khỏi lớp
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteEnrollment(int id)
    {
        var success = await _mediator.Send(new DeleteEnrollmentCommand(id));
        if (!success)
            return NotFound(new { message = "Không tìm thấy đăng ký" });

        return Ok(success);
    }


    /// <summary>
    /// Giải quyết lớp học bị hủy (bảo lưu, chuyển lớp, hoàn tiền)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("resolve-cancelled-class")]
    public async Task<ActionResult<bool>> ResolveCancelledClass(ResolveCancelledClassCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Khai giảng lớp từ hàng chờ khóa học
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("course-queue/launch")]
    public async Task<ActionResult<bool>> LaunchClassFromQueue(LaunchClassFromQueueCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Lấy báo cáo thống kê chuyên cần, hàng chờ và tín dụng học viên (Admin)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpGet("analytics")]
    public async Task<ActionResult<StudentAnalyticsDto>> GetStudentAnalytics()
    {
        var result = await _mediator.Send(new GetStudentAnalyticsQuery());
        return Ok(result);
    }

    /// <summary>
    /// Danh sách học viên trong lớp (cho inter-service)
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien")]
    [HttpGet("class/{classId}/students")]
    public async Task<ActionResult<List<StudentDto>>> GetStudentsByClass(int classId)
    {
        var result = await _mediator.Send(new GetStudentsByClassQuery(classId));
        return Ok(result);
    }

    /// <summary>
    /// Lấy thông tin số dư và lịch sử ví bảo lưu học phí của học viên
    /// </summary>
    [Authorize(Roles = "Admin,HocVien")]
    [HttpGet("student-credits/{studentId}")]
    public async Task<ActionResult<StudentCreditSummaryDto>> GetStudentCredits(int studentId)
    {
        var result = await _mediator.Send(new GetStudentCreditsQuery(studentId));
        return Ok(result);
    }
}

