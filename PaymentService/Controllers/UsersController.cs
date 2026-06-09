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
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Danh sách user (filter by role, pagination)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedResult<UserDto>>> GetUsers(
        [FromQuery] string? search,
        [FromQuery] string? role,
        [FromQuery] bool? isActive,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetUsersQuery(search, role, isActive, page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Chi tiết user
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        if (user == null)
            return NotFound(new { message = "Không tìm thấy người dùng" });

        return Ok(user);
    }

    /// <summary>
    /// Cập nhật user
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int id, UpdateUserCommand command)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound(new { message = "Không tìm thấy người dùng" });

        return Ok(result);
    }

    /// <summary>
    /// Khóa/mở tài khoản
    /// </summary>
    [HttpPut("{id}/toggle-active")]
    public async Task<IActionResult> ToggleActive(int id)
    {
        var success = await _mediator.Send(new ToggleActiveCommand(id));
        if (!success)
            return NotFound(new { message = "Không tìm thấy người dùng" });

        var user = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(new { message = user!.IsActive ? "Đã mở khóa tài khoản" : "Đã khóa tài khoản", isActive = user.IsActive });
    }

    /// <summary>
    /// Lấy thống kê số lượng tài khoản theo vai trò
    /// </summary>
    [HttpGet("stats")]
    public async Task<ActionResult<UserStatsDto>> GetUserStats()
    {
        var stats = await _mediator.Send(new GetUserStatsQuery());
        return Ok(stats);
    }

    /// <summary>
    /// Lấy danh sách giáo viên (cho dropdown)
    /// </summary>
    [HttpGet("teachers")]
    [AllowAnonymous]
    public async Task<ActionResult<List<UserDto>>> GetTeachers()
    {
        var result = await _mediator.Send(new GetUsersQuery(Search: null, Role: "GiaoVien", IsActive: true, Page: 1, PageSize: 9999));
        return Ok(result.Items);
    }
}
