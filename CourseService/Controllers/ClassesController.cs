using Microsoft.AspNetCore.Mvc;
using CourseService.DTOs;
using CourseService.Features.Classes.Commands;
using CourseService.Features.Classes.Queries;
using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;

namespace CourseService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class ClassesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClassesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy danh sách lớp học (filter by courseId, teacherId, status)
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<PagedResult<ClassDto>>> GetClasses(
        [FromQuery] int? courseId,
        [FromQuery] int? teacherId,
        [FromQuery] string? status,
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetClassesQuery(courseId, teacherId, status, search, page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Lấy chi tiết lớp học
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClassDto>> GetClass(int id)
    {
        var cls = await _mediator.Send(new GetClassByIdQuery(id));
        if (cls == null)
            return NotFound(new { message = "Không tìm thấy lớp học" });

        return Ok(cls);
    }

    /// <summary>
    /// Lấy danh sách lớp theo giáo viên
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien")]
    [HttpGet("teacher/{teacherId}")]
    public async Task<ActionResult<List<ClassDto>>> GetClassesByTeacher(int teacherId)
    {
        var classes = await _mediator.Send(new GetClassesByTeacherQuery(teacherId));
        return Ok(classes);
    }

    /// <summary>
    /// Mở lớp học mới
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ClassDto>> CreateClass(CreateClassCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetClass), new { id = result.ClassId }, result);
    }

    /// <summary>
    /// Cập nhật lớp học
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ClassDto>> UpdateClass(int id, UpdateClassCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { message = "Id không trùng khớp" });

        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound(new { message = "Không tìm thấy lớp học" });

        return Ok(result);
    }

    /// <summary>
    /// Cập nhật trạng thái lớp
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}/status")]
    public async Task<ActionResult<ClassDto>> UpdateClassStatus(int id, UpdateClassStatusCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { message = "Id không trùng khớp" });

        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound(new { message = "Không tìm thấy lớp học" });

        return Ok(result);
    }

    /// <summary>
    /// Cập nhật số lượng học viên hiện tại của lớp
    /// </summary>
    [AllowAnonymous]
    [HttpPut("{id:int}/students/count")]
    public async Task<ActionResult<bool>> UpdateClassStudentCount(int id, [FromQuery] int delta)
    {
        var result = await _mediator.Send(new UpdateClassStudentCountCommand(id, delta));
        return Ok(result);
    }

    /// <summary>
    /// Xóa lớp học
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteClass(int id)
    {
        var success = await _mediator.Send(new DeleteClassCommand(id));
        if (!success)
            return NotFound(new { message = "Không tìm thấy lớp học" });

        return Ok(new { message = "Đã xóa lớp học thành công" });
    }

    /// <summary>
    /// Lấy báo cáo thống kê vận hành lớp học
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpGet("analytics")]
    public async Task<ActionResult<CourseAnalyticsDto>> GetCourseAnalytics()
    {
        var result = await _mediator.Send(new GetCourseAnalyticsQuery());
        return Ok(result);
    }
}
