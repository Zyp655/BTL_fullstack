using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Models;
using StudentService.DTOs;
using StudentService.Services;
using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/course-evaluations")]
[Authorize]
public class CourseEvaluationsController : ControllerBase
{
    private readonly StudentDbContext _context;
    private readonly ICourseServiceClient _courseServiceClient;

    public CourseEvaluationsController(StudentDbContext context, ICourseServiceClient courseServiceClient)
    {
        _context = context;
        _courseServiceClient = courseServiceClient;
    }

    /// <summary>
    /// Student submits a course evaluation
    /// </summary>
    [Authorize(Roles = "Admin,HocVien")]
    [HttpPost]
    public async Task<ActionResult<CourseEvaluationDto>> CreateEvaluation(CreateCourseEvaluationDto dto)
    {
        int studentId;
        if (User.IsInRole("Admin"))
        {
            return BadRequest(new { message = "Quản trị viên không thể thực hiện đánh giá môn học." });
        }
        else
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
            if (student == null)
                return NotFound(new { message = "Hồ sơ học viên chưa được liên kết với tài khoản này" });
            studentId = student.StudentId;
        }

        var studentProfile = await _context.Students.FindAsync(studentId);
        if (studentProfile == null)
            return NotFound(new { message = "Không tìm thấy học viên" });

        // 1. Verify student is enrolled in any class of this course
        var enrollments = await _context.Enrollments
            .Where(e => e.StudentId == studentId && (e.Status == "DangHoc" || e.Status == "DaXong" || e.Status == "HoanThanh"))
            .ToListAsync();

        bool isEnrolled = false;
        foreach (var e in enrollments)
        {
            var classInfo = await _courseServiceClient.GetClassInfo(e.ClassId);
            if (classInfo != null && classInfo.CourseId == dto.CourseId)
            {
                isEnrolled = true;
                break;
            }
        }

        if (!isEnrolled)
        {
            return BadRequest(new { message = "Bạn chưa từng tham gia học môn học này nên không thể thực hiện đánh giá." });
        }

        // 2. Check if already evaluated this course
        var existing = await _context.CourseEvaluations.AnyAsync(e => e.StudentId == studentId && e.CourseId == dto.CourseId);
        if (existing)
            return BadRequest(new { message = "Bạn đã thực hiện đánh giá cho môn học này rồi." });

        // Check if evaluations are globally enabled (reusing system settings for teacher evaluations)
        var statusSetting = await _context.SystemSettings.FindAsync("IsEvaluationEnabled");
        bool isEnabled = statusSetting == null || statusSetting.Value.ToLower() == "true";
        if (!isEnabled)
            return BadRequest(new { message = "Hệ thống đánh giá hiện đang khóa bởi Quản trị viên." });

        var eval = new CourseEvaluation
        {
            StudentId = studentId,
            CourseId = dto.CourseId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            CreatedAt = DateTime.UtcNow
        };

        _context.CourseEvaluations.Add(eval);
        await _context.SaveChangesAsync();

        var result = new CourseEvaluationDto
        {
            Id = eval.Id,
            StudentId = eval.StudentId,
            StudentName = studentProfile.FullName,
            CourseId = eval.CourseId,
            Rating = eval.Rating,
            Comment = eval.Comment,
            CreatedAt = DateTime.SpecifyKind(eval.CreatedAt, DateTimeKind.Utc)
        };

        return Ok(result);
    }

    /// <summary>
    /// Get statistics and reviews list for a course
    /// </summary>
    [AllowAnonymous]
    [HttpGet("course/{courseId:int}")]
    public async Task<ActionResult<CourseEvaluationSummaryDto>> GetCourseEvaluations(int courseId)
    {
        var list = await _context.CourseEvaluations
            .Include(e => e.Student)
            .Where(e => e.CourseId == courseId)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

        var summary = new CourseEvaluationSummaryDto
        {
            CourseId = courseId,
            TotalReviews = list.Count,
            AverageRating = list.Any() ? Math.Round(list.Average(e => e.Rating), 1) : 0.0,
            RatingDistribution = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 } }
        };

        foreach (var eval in list)
        {
            if (eval.Rating >= 1 && eval.Rating <= 5)
            {
                summary.RatingDistribution[eval.Rating]++;
            }

            summary.Reviews.Add(new CourseEvaluationDto
            {
                Id = eval.Id,
                StudentId = eval.StudentId,
                StudentName = eval.Student?.FullName ?? "Học viên ẩn danh",
                CourseId = eval.CourseId,
                Rating = eval.Rating,
                Comment = eval.Comment,
                CreatedAt = DateTime.SpecifyKind(eval.CreatedAt, DateTimeKind.Utc)
            });
        }

        return Ok(summary);
    }

    /// <summary>
    /// Student views their own course evaluations
    /// </summary>
    [Authorize(Roles = "HocVien")]
    [HttpGet("my-evaluations")]
    public async Task<ActionResult<List<CourseEvaluationDto>>> GetMyEvaluations()
    {
        var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
        var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
        if (student == null)
            return NotFound(new { message = "Hồ sơ học viên chưa được tạo" });

        var list = await _context.CourseEvaluations
            .Where(e => e.StudentId == student.StudentId)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

        var result = list.Select(eval => new CourseEvaluationDto
        {
            Id = eval.Id,
            StudentId = eval.StudentId,
            StudentName = student.FullName,
            CourseId = eval.CourseId,
            Rating = eval.Rating,
            Comment = eval.Comment,
            CreatedAt = DateTime.SpecifyKind(eval.CreatedAt, DateTimeKind.Utc)
        }).ToList();

        return Ok(result);
    }
}
