using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Models;
using StudentService.DTOs;
using StudentService.Features.Enrollments.Commands;
using MediatR;
using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using StudentService.Hubs;

namespace StudentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/support-messages")]
[Authorize]
public class SupportMessagesController : ControllerBase
{
    private readonly StudentDbContext _context;
    private readonly IMediator _mediator;
    private readonly IHubContext<SupportHub> _hubContext;

    public SupportMessagesController(StudentDbContext context, IMediator mediator, IHubContext<SupportHub> hubContext)
    {
        _context = context;
        _mediator = mediator;
        _hubContext = hubContext;
    }

    /// <summary>
    /// Student sends a support message or class transfer request
    /// </summary>
    [Authorize(Roles = "Admin,HocVien,GiaoVien")]
    [HttpPost]
    public async Task<ActionResult<SupportMessageDto>> CreateMessage(CreateSupportMessageDto dto)
    {
        // For security, students can only request for their own student profile
        if (User.IsInRole("HocVien"))
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
            if (student == null || student.StudentId != dto.StudentId)
                return Forbid();
        }

        var studentProfile = await _context.Students.FindAsync(dto.StudentId);
        if (studentProfile == null)
            return NotFound(new { message = "Không tìm thấy học viên" });

        var msg = new SupportMessage
        {
            StudentId = dto.StudentId,
            Message = dto.Message,
            Status = "Pending",
            FromClassId = dto.FromClassId,
            ToClassId = dto.ToClassId,
            FromClassName = dto.FromClassName,
            ToClassName = dto.ToClassName,
            CreatedAt = DateTime.UtcNow
        };

        _context.SupportMessages.Add(msg);
        await _context.SaveChangesAsync();

        var result = MapToDto(msg, studentProfile.FullName);
        await _hubContext.Clients.Group("Admins").SendAsync("SupportMessageCreated", result);
        return Ok(result);
    }

    /// <summary>
    /// Admin lists all support messages
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<List<SupportMessageDto>>> GetMessages([FromQuery] string? status)
    {
        var query = _context.SupportMessages
            .Include(m => m.Student)
            .AsQueryable();

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(m => m.Status == status);
        }

        var list = await query
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();

        var result = list.Select(m => MapToDto(m, m.Student.FullName)).ToList();
        return Ok(result);
    }

    /// <summary>
    /// Student lists their own messages
    /// </summary>
    [Authorize(Roles = "HocVien")]
    [HttpGet("my-messages")]
    public async Task<ActionResult<List<SupportMessageDto>>> GetMyMessages()
    {
        var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
        var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
        if (student == null)
            return NotFound(new { message = "Hồ sơ học viên chưa được tạo" });

        var list = await _context.SupportMessages
            .Where(m => m.StudentId == student.StudentId)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();

        var result = list.Select(m => MapToDto(m, student.FullName)).ToList();
        return Ok(result);
    }

    /// <summary>
    /// Admin approves/resolves the support request.
    /// If it's a class transfer, this will trigger the class transfer logic.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/resolve")]
    public async Task<ActionResult<SupportMessageDto>> ResolveMessage(int id)
    {
        var msg = await _context.SupportMessages
            .Include(m => m.Student)
            .FirstOrDefaultAsync(m => m.Id == id);
            
        if (msg == null)
            return NotFound(new { message = "Không tìm thấy yêu cầu này" });

        if (msg.Status != "Pending")
            return BadRequest(new { message = "Yêu cầu này đã được xử lý" });

        // If it's a transfer request, run the transfer command
        if (msg.FromClassId.HasValue && msg.ToClassId.HasValue)
        {
            try
            {
                var command = new TransferClassCommand(msg.StudentId, msg.FromClassId.Value, msg.ToClassId.Value);
                await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi khi chuyển lớp tự động: {ex.Message}" });
            }
        }

        msg.Status = "Resolved";
        _context.SupportMessages.Update(msg);
        await _context.SaveChangesAsync();

        var result = MapToDto(msg, msg.Student.FullName);
        await _hubContext.Clients.Group($"Student_{msg.StudentId}").SendAsync("SupportMessageStatusChanged", result);
        return Ok(result);
    }

    /// <summary>
    /// Admin rejects the support request.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/reject")]
    public async Task<ActionResult<SupportMessageDto>> RejectMessage(int id, [FromBody] RejectSupportMessageDto dto)
    {
        var msg = await _context.SupportMessages
            .Include(m => m.Student)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (msg == null)
            return NotFound(new { message = "Không tìm thấy yêu cầu này" });

        if (msg.Status != "Pending")
            return BadRequest(new { message = "Yêu cầu này đã được xử lý" });

        msg.Status = "Rejected";
        msg.AdminResponse = dto.AdminResponse;
        _context.SupportMessages.Update(msg);
        await _context.SaveChangesAsync();

        var result = MapToDto(msg, msg.Student.FullName);
        await _hubContext.Clients.Group($"Student_{msg.StudentId}").SendAsync("SupportMessageStatusChanged", result);
        return Ok(result);
    }

    private static SupportMessageDto MapToDto(SupportMessage m, string studentName)
    {
        return new SupportMessageDto
        {
            Id = m.Id,
            StudentId = m.StudentId,
            StudentName = studentName,
            Message = m.Message,
            Status = m.Status,
            FromClassId = m.FromClassId,
            ToClassId = m.ToClassId,
            FromClassName = m.FromClassName,
            ToClassName = m.ToClassName,
            AdminResponse = m.AdminResponse,
            CreatedAt = DateTime.SpecifyKind(m.CreatedAt, DateTimeKind.Utc)
        };
    }
}
