using Microsoft.AspNetCore.Mvc;
using CourseService.DTOs;
using CourseService.Features.Schedules.Commands;
using CourseService.Features.Schedules.Queries;
using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;

namespace CourseService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/classes/{classId}/[controller]")]
[Authorize]
public class SchedulesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SchedulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy lịch học của lớp
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<List<ScheduleDto>>> GetSchedules(int classId)
    {
        var result = await _mediator.Send(new GetSchedulesByClassQuery(classId));
        return Ok(result);
    }

    /// <summary>
    /// Thêm lịch học cho lớp
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ScheduleDto>> CreateSchedule(int classId, CreateScheduleCommand command)
    {
        command = command with { ClassId = classId };
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetSchedules), new { classId }, result);
    }

    /// <summary>
    /// Cập nhật lịch học
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut("{scheduleId}")]
    public async Task<ActionResult<ScheduleDto>> UpdateSchedule(int classId, int scheduleId, UpdateScheduleCommand command)
    {
        command = command with { ClassId = classId, ScheduleId = scheduleId };
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound(new { message = "Không tìm thấy lịch học" });

        return Ok(result);
    }

    /// <summary>
    /// Xóa lịch học
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{scheduleId}")]
    public async Task<IActionResult> DeleteSchedule(int classId, int scheduleId)
    {
        var success = await _mediator.Send(new DeleteScheduleCommand(classId, scheduleId));
        if (!success)
            return NotFound(new { message = "Không tìm thấy lịch học" });

        return Ok(new { message = "Đã xóa lịch học thành công" });
    }
}
