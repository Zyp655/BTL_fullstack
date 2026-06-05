using Microsoft.AspNetCore.Mvc;
using CourseService.DTOs;
using CourseService.Services;

namespace CourseService.Controllers;

[ApiController]
[Route("api/classes/{classId}/[controller]")]
public class SchedulesController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public SchedulesController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    /// <summary>
    /// Lấy lịch học của lớp
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<ScheduleDto>>> GetSchedules(int classId)
    {
        try
        {
            var result = await _scheduleService.GetSchedulesAsync(classId);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Thêm lịch học cho lớp
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ScheduleDto>> CreateSchedule(int classId, CreateScheduleDto dto)
    {
        try
        {
            var result = await _scheduleService.CreateScheduleAsync(classId, dto);
            return CreatedAtAction(nameof(GetSchedules), new { classId }, result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Cập nhật lịch học
    /// </summary>
    [HttpPut("{scheduleId}")]
    public async Task<ActionResult<ScheduleDto>> UpdateSchedule(int classId, int scheduleId, UpdateScheduleDto dto)
    {
        try
        {
            var result = await _scheduleService.UpdateScheduleAsync(classId, scheduleId, dto);
            if (result == null)
                return NotFound(new { message = "Không tìm thấy lịch học" });

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Xóa lịch học
    /// </summary>
    [HttpDelete("{scheduleId}")]
    public async Task<IActionResult> DeleteSchedule(int classId, int scheduleId)
    {
        var success = await _scheduleService.DeleteScheduleAsync(classId, scheduleId);
        if (!success)
            return NotFound(new { message = "Không tìm thấy lịch học" });

        return Ok(new { message = "Đã xóa lịch học thành công" });
    }
}
