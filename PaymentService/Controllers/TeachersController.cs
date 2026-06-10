using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PaymentService.DTOs;
using PaymentService.Features.Users.Commands;
using PaymentService.Features.Users.Queries;
using MediatR;
using Asp.Versioning;

namespace PaymentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles = "Admin")]
public class TeachersController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeachersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy danh sách giảng viên (có phân trang, tìm kiếm)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedResult<UserDto>>> GetTeachers(
        [FromQuery] string? search,
        [FromQuery] bool? isActive,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetUsersQuery(search, "GiaoVien", isActive, page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Chi tiết giảng viên
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetTeacher(int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        if (user == null || user.Role != "GiaoVien")
            return NotFound(new { message = "Không tìm thấy giảng viên" });

        return Ok(user);
    }

    /// <summary>
    /// Tạo mới giảng viên
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateTeacher(CreateTeacherRequest request)
    {
        var command = new RegisterCommand(
            request.Username,
            request.Password,
            request.FullName,
            request.Email,
            request.Phone,
            "GiaoVien",
            request.Specialization,
            request.Degree
        );
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTeacher), new { id = result.UserId }, result);
    }

    /// <summary>
    /// Cập nhật giảng viên
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateTeacher(int id, UpdateTeacherRequest request)
    {
        var command = new UpdateUserCommand(
            id,
            request.FullName,
            request.Email,
            request.Phone,
            "GiaoVien",
            request.Specialization,
            request.Degree
        );
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound(new { message = "Không tìm thấy giảng viên" });

        return Ok(result);
    }

    /// <summary>
    /// Khóa/Mở tài khoản giảng viên
    /// </summary>
    [HttpPut("{id}/toggle-active")]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        if (user == null || user.Role != "GiaoVien")
            return NotFound(new { message = "Không tìm thấy giảng viên" });

        var success = await _mediator.Send(new ToggleActiveCommand(id));
        if (!success)
            return NotFound(new { message = "Không tìm thấy giảng viên" });

        var updated = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(new { message = updated!.IsActive ? "Đã mở khóa tài khoản giảng viên" : "Đã khóa tài khoản giảng viên", isActive = updated.IsActive });
    }

    /// <summary>
    /// Ngưng hoạt động giảng viên (Soft delete/deactivate)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeactivateTeacher(int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        if (user == null || user.Role != "GiaoVien")
            return NotFound(new { message = "Không tìm thấy giảng viên" });

        if (user.IsActive)
        {
            await _mediator.Send(new ToggleActiveCommand(id));
        }
        return Ok(new { message = "Đã ngưng hoạt động giảng viên thành công" });
    }
}

public class CreateTeacherRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Specialization { get; set; }
    public string? Degree { get; set; }
}

public class UpdateTeacherRequest
{
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Specialization { get; set; }
    public string? Degree { get; set; }
}
