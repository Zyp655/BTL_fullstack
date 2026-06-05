using Microsoft.AspNetCore.Mvc;
using CourseService.DTOs;
using CourseService.Services;

namespace CourseService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    /// <summary>
    /// Lấy danh sách khóa học (có phân trang, filter)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedResult<CourseDto>>> GetCourses(
        [FromQuery] string? search,
        [FromQuery] string? category,
        [FromQuery] string? level,
        [FromQuery] bool? isActive,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _courseService.GetCoursesAsync(search, category, level, isActive, page, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// Lấy chi tiết khóa học
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDto>> GetCourse(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
            return NotFound(new { message = "Không tìm thấy khóa học" });

        return Ok(course);
    }

    /// <summary>
    /// Tạo khóa học mới
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto dto)
    {
        var result = await _courseService.CreateCourseAsync(dto);
        return CreatedAtAction(nameof(GetCourse), new { id = result.CourseId }, result);
    }

    /// <summary>
    /// Cập nhật khóa học
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<CourseDto>> UpdateCourse(int id, UpdateCourseDto dto)
    {
        var result = await _courseService.UpdateCourseAsync(id, dto);
        if (result == null)
            return NotFound(new { message = "Không tìm thấy khóa học" });

        return Ok(result);
    }

    /// <summary>
    /// Xóa khóa học (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var success = await _courseService.DeleteCourseAsync(id);
        if (!success)
            return NotFound(new { message = "Không tìm thấy khóa học" });

        return Ok(new { message = "Đã xóa khóa học thành công" });
    }
}
