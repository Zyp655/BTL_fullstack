using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Models;
using StudentService.Services;
using Asp.Versioning;

namespace StudentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class LessonsController : ControllerBase
{
    private readonly StudentDbContext _context;
    private readonly ICourseServiceClient _courseServiceClient;

    public LessonsController(StudentDbContext context, ICourseServiceClient courseServiceClient)
    {
        _context = context;
        _courseServiceClient = courseServiceClient;
    }

    [HttpGet("class/{classId}")]
    public async Task<ActionResult<List<LessonDto>>> GetLessonsByClass(int classId)
    {
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        
        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(classId);
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
                return Forbid();
        }
        else if (role == "HocVien" && int.TryParse(userIdStr, out int userId))
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
            if (student == null)
                return Forbid();
            var enrolled = await _context.Enrollments.AnyAsync(e => e.StudentId == student.StudentId && e.ClassId == classId);
            if (!enrolled)
                return Forbid();
        }

        var lessons = await _context.Lessons
            .Where(l => l.ClassId == classId)
            .OrderBy(l => l.LessonDate)
            .Select(l => new LessonDto
            {
                LessonId = l.LessonId,
                ClassId = l.ClassId,
                LessonDate = l.LessonDate,
                Title = l.Title,
                FileName = l.FileName,
                CreatedAt = l.CreatedAt
            })
            .ToListAsync();

        return Ok(lessons);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Lesson>> GetLesson(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null)
            return NotFound();

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        
        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(lesson.ClassId);
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
                return Forbid();
        }
        else if (role == "HocVien" && int.TryParse(userIdStr, out int userId))
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
            if (student == null)
                return Forbid();
            var enrolled = await _context.Enrollments.AnyAsync(e => e.StudentId == student.StudentId && e.ClassId == lesson.ClassId);
            if (!enrolled)
                return Forbid();
        }

        return Ok(lesson);
    }

    [HttpGet("class/{classId}/date/{date}")]
    public async Task<ActionResult<Lesson>> GetLessonByDate(int classId, string date)
    {
        if (!DateTime.TryParse(date, out DateTime parsedDate))
            return BadRequest(new { message = "Định dạng ngày không hợp lệ. Sử dụng yyyy-MM-dd." });

        var dateOnly = parsedDate.Date;
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        
        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(classId);
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
                return Forbid();
        }
        else if (role == "HocVien" && int.TryParse(userIdStr, out int userId))
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
            if (student == null)
                return Forbid();
            var enrolled = await _context.Enrollments.AnyAsync(e => e.StudentId == student.StudentId && e.ClassId == classId);
            if (!enrolled)
                return Forbid();
        }

        var lesson = await _context.Lessons
            .FirstOrDefaultAsync(l => l.ClassId == classId && l.LessonDate.Year == dateOnly.Year && l.LessonDate.Month == dateOnly.Month && l.LessonDate.Day == dateOnly.Day);

        if (lesson == null)
            return NotFound(new { message = "Không tìm thấy nội dung bài học cho ngày này" });

        return Ok(lesson);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<ActionResult<Lesson>> SaveLesson([FromBody] SaveLessonDto dto)
    {
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        
        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(dto.ClassId);
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
                return Forbid();
        }

        var dateOnly = dto.LessonDate.Date;
        var existing = await _context.Lessons
            .FirstOrDefaultAsync(l => l.ClassId == dto.ClassId && l.LessonDate.Year == dateOnly.Year && l.LessonDate.Month == dateOnly.Month && l.LessonDate.Day == dateOnly.Day);

        if (existing != null)
        {
            existing.Title = dto.Title;
            existing.Content = dto.Content;
            existing.FileName = dto.FileName;
            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        var lesson = new Lesson
        {
            ClassId = dto.ClassId,
            LessonDate = dateOnly,
            Title = dto.Title,
            Content = dto.Content,
            FileName = dto.FileName,
            CreatedAt = DateTime.UtcNow
        };

        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLessonByDate), new { classId = lesson.ClassId, date = lesson.LessonDate.ToString("yyyy-MM-dd") }, lesson);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<IActionResult> DeleteLesson(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null)
            return NotFound();

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        
        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(lesson.ClassId);
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
                return Forbid();
        }

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

public class SaveLessonDto
{
    public int ClassId { get; set; }
    public DateTime LessonDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? FileName { get; set; }
}

public class LessonDto
{
    public int LessonId { get; set; }
    public int ClassId { get; set; }
    public DateTime LessonDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? FileName { get; set; }
    public DateTime CreatedAt { get; set; }
}
