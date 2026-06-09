using Microsoft.AspNetCore.Mvc;
using CourseService.DTOs;
using CourseService.Features.Courses.Commands;
using CourseService.Features.Courses.Queries;
using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;

namespace CourseService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class CoursesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy danh sách khóa học (có phân trang, filter)
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<PagedResult<CourseDto>>> GetCourses(
        [FromQuery] string? search,
        [FromQuery] string? category,
        [FromQuery] string? level,
        [FromQuery] bool? isActive,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetCoursesQuery(search, category, level, isActive, page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Lấy chi tiết khóa học
    /// </summary>
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetCourse(int id)
    {
        var course = await _mediator.Send(new GetCourseByIdQuery(id));
        if (course == null)
            return NotFound(new { message = "Không tìm thấy khóa học" });

        return Ok(course);
    }

    /// <summary>
    /// Tạo khóa học mới
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCourse), new { id = result.CourseId }, result);
    }

    /// <summary>
    /// Cập nhật khóa học
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<CourseDto>> UpdateCourse(int id, UpdateCourseCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { message = "Id không trùng khớp" });

        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound(new { message = "Không tìm thấy khóa học" });

        return Ok(result);
    }

    /// <summary>
    /// Xóa khóa học (soft delete)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var success = await _mediator.Send(new DeleteCourseCommand(id));
        if (!success)
            return NotFound(new { message = "Không tìm thấy khóa học" });

        return Ok(new { message = "Đã xóa khóa học thành công" });
    }
}
