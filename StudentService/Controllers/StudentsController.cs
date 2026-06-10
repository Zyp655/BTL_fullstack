using Microsoft.AspNetCore.Mvc;
using StudentService.DTOs;
using StudentService.Features.Students.Commands;
using StudentService.Features.Students.Queries;
using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;

namespace StudentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Danh sách học viên (pagination)
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<PagedResult<StudentDto>>> GetStudents(
        [FromQuery] string? search,
        [FromQuery] string? gender,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetStudentsQuery(search, gender, page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Chi tiết học viên
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDto>> GetStudent(int id)
    {
        var student = await _mediator.Send(new GetStudentByIdQuery(id));
        if (student == null)
            return NotFound(new { message = "Không tìm thấy học viên" });

        return Ok(student);
    }

    /// <summary>
    /// Tìm học viên theo UserId
    /// </summary>
    [HttpGet("by-user/{userId}")]
    public async Task<ActionResult<StudentDto>> GetStudentByUserId(int userId)
    {
        var student = await _mediator.Send(new GetStudentByUserIdQuery(userId));
        if (student == null)
            return NotFound(new { message = "Không tìm thấy học viên" });

        return Ok(student);
    }

    /// <summary>
    /// Tạo hồ sơ học viên
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<StudentDto>> CreateStudent(CreateStudentCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetStudent), new { id = result.StudentId }, result);
    }

    /// <summary>
    /// Cập nhật hồ sơ học viên
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<StudentDto>> UpdateStudent(int id, UpdateStudentCommand command)
    {
        command = command with { Id = id };
        var result = await _mediator.Send(command);
        if (result == null)
            return NotFound(new { message = "Không tìm thấy học viên" });

        return Ok(result);
    }

    /// <summary>
    /// Lấy thống kê số lượng học viên theo giới tính
    /// </summary>
    [HttpGet("stats")]
    public async Task<ActionResult<StudentStatsDto>> GetStudentStats()
    {
        var stats = await _mediator.Send(new GetStudentStatsQuery());
        return Ok(stats);
    }

    /// <summary>
    /// Khóa học của học viên
    /// </summary>
    [HttpGet("{id}/enrollments")]
    public async Task<ActionResult<List<EnrollmentDto>>> GetStudentEnrollments(int id)
    {
        var result = await _mediator.Send(new GetStudentEnrollmentsQuery(id));
        return Ok(result);
    }
}
