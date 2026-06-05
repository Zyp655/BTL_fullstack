using Microsoft.AspNetCore.Mvc;
using CourseService.DTOs;
using CourseService.Services;

namespace CourseService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly IClassService _classService;

    public ClassesController(IClassService classService)
    {
        _classService = classService;
    }

    /// <summary>
    /// Lấy danh sách lớp học (filter by courseId, teacherId, status)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedResult<ClassDto>>> GetClasses(
        [FromQuery] int? courseId,
        [FromQuery] int? teacherId,
        [FromQuery] string? status,
        [FromQuery] string? search,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _classService.GetClassesAsync(courseId, teacherId, status, search, page, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// Lấy chi tiết lớp học
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ClassDto>> GetClass(int id)
    {
        var cls = await _classService.GetClassByIdAsync(id);
        if (cls == null)
            return NotFound(new { message = "Không tìm thấy lớp học" });

        return Ok(cls);
    }

    /// <summary>
    /// Lấy danh sách lớp theo giáo viên
    /// </summary>
    [HttpGet("teacher/{teacherId}")]
    public async Task<ActionResult<List<ClassDto>>> GetClassesByTeacher(int teacherId)
    {
        var classes = await _classService.GetClassesByTeacherAsync(teacherId);
        return Ok(classes);
    }

    /// <summary>
    /// Mở lớp học mới
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ClassDto>> CreateClass(CreateClassDto dto)
    {
        try
        {
            var result = await _classService.CreateClassAsync(dto);
            return CreatedAtAction(nameof(GetClass), new { id = result.ClassId }, result);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Cập nhật lớp học
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<ClassDto>> UpdateClass(int id, UpdateClassDto dto)
    {
        var result = await _classService.UpdateClassAsync(id, dto);
        if (result == null)
            return NotFound(new { message = "Không tìm thấy lớp học" });

        return Ok(result);
    }

    /// <summary>
    /// Cập nhật trạng thái lớp
    /// </summary>
    [HttpPut("{id}/status")]
    public async Task<ActionResult<ClassDto>> UpdateClassStatus(int id, UpdateClassStatusDto dto)
    {
        try
        {
            var result = await _classService.UpdateClassStatusAsync(id, dto.Status);
            if (result == null)
                return NotFound(new { message = "Không tìm thấy lớp học" });

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Xóa lớp học
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClass(int id)
    {
        try
        {
            var success = await _classService.DeleteClassAsync(id);
            if (!success)
                return NotFound(new { message = "Không tìm thấy lớp học" });

            return Ok(new { message = "Đã xóa lớp học thành công" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
