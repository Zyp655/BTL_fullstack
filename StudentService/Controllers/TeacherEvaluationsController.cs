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
[Route("api/v{version:apiVersion}/teacher-evaluations")]
[Authorize]
public class TeacherEvaluationsController : ControllerBase
{
    private readonly StudentDbContext _context;
    private readonly ICourseServiceClient _courseServiceClient;

    public TeacherEvaluationsController(StudentDbContext context, ICourseServiceClient courseServiceClient)
    {
        _context = context;
        _courseServiceClient = courseServiceClient;
    }

    /// <summary>
    /// Student submits a teacher evaluation for a class (with 4 criteria)
    /// </summary>
    [Authorize(Roles = "Admin,HocVien")]
    [HttpPost]
    public async Task<ActionResult<TeacherEvaluationDto>> CreateEvaluation(CreateTeacherEvaluationDto dto)
    {
        int studentId;
        if (User.IsInRole("Admin"))
        {
            if (!dto.StudentId.HasValue)
                return BadRequest(new { message = "Quản trị viên phải cung cấp StudentId" });
            studentId = dto.StudentId.Value;
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

        // 1. Verify student is enrolled in the class
        var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.StudentId == studentId && e.ClassId == dto.ClassId);
        if (enrollment == null || (enrollment.Status != "DangHoc" && enrollment.Status != "DaXong" && enrollment.Status != "HoanThanh"))
            return BadRequest(new { message = "Bạn không tham gia lớp học này (hoặc trạng thái học tập không hợp lệ) nên không thể thực hiện đánh giá." });

        // 2. Check if already evaluated
        var existing = await _context.TeacherEvaluations.AnyAsync(e => e.StudentId == studentId && e.ClassId == dto.ClassId);
        if (existing)
            return BadRequest(new { message = "Bạn đã thực hiện đánh giá cho lớp học này rồi." });

        // Check if evaluations are globally enabled
        var statusSetting = await _context.SystemSettings.FindAsync("IsEvaluationEnabled");
        bool isEnabled = statusSetting == null || statusSetting.Value.ToLower() == "true";
        if (!isEnabled)
            return BadRequest(new { message = "Hệ thống đánh giá giảng viên hiện đang khóa bởi Quản trị viên." });

        // Check class-level toggle
        var enabledClassesSetting = await _context.SystemSettings.FindAsync("EnabledEvaluationClassIds");
        if (enabledClassesSetting == null || string.IsNullOrWhiteSpace(enabledClassesSetting.Value))
            return BadRequest(new { message = "Lớp học này hiện đang bị khóa đánh giá bởi Quản trị viên." });

        var enabledClassIds = enabledClassesSetting.Value.Split(',')
            .Select(id => id.Trim())
            .ToList();

        if (!enabledClassIds.Contains(dto.ClassId.ToString()))
            return BadRequest(new { message = "Lớp học này hiện đang bị khóa đánh giá bởi Quản trị viên." });

        // 3. Fetch class info to get TeacherId
        var classInfo = await _courseServiceClient.GetClassInfo(dto.ClassId);
        if (classInfo == null)
            return NotFound(new { message = "Không tìm thấy thông tin lớp học từ Course Service" });

        if (!classInfo.TeacherId.HasValue)
            return BadRequest(new { message = "Lớp học này chưa được phân công giáo viên giảng dạy." });

        // Calculate average rating and handle dynamic criteria
        double avgRating = 0;
        string? detailedRatingsJson = null;

        if (dto.DetailedRatings != null && dto.DetailedRatings.Any())
        {
            avgRating = dto.DetailedRatings.Average(r => r.Rating);
            avgRating = Math.Round(avgRating, 2);
            detailedRatingsJson = System.Text.Json.JsonSerializer.Serialize(dto.DetailedRatings);

            // Populate fallback values for the 4 legacy columns
            dto.TeachingQualityRating = dto.DetailedRatings.Count > 0 ? dto.DetailedRatings[0].Rating : 0;
            dto.SupportRating = dto.DetailedRatings.Count > 1 ? dto.DetailedRatings[1].Rating : 0;
            dto.CurriculumRating = dto.DetailedRatings.Count > 2 ? dto.DetailedRatings[2].Rating : 0;
            dto.PunctualityRating = dto.DetailedRatings.Count > 3 ? dto.DetailedRatings[3].Rating : 0;
        }
        else
        {
            avgRating = (dto.TeachingQualityRating + dto.SupportRating + dto.CurriculumRating + dto.PunctualityRating) / 4.0;
            avgRating = Math.Round(avgRating, 2);
        }

        var eval = new TeacherEvaluation
        {
            StudentId = studentId,
            ClassId = dto.ClassId,
            TeacherId = classInfo.TeacherId.Value,
            TeachingQualityRating = dto.TeachingQualityRating,
            SupportRating = dto.SupportRating,
            CurriculumRating = dto.CurriculumRating,
            PunctualityRating = dto.PunctualityRating,
            Rating = avgRating,
            Comment = dto.Comment,
            DetailedRatingsJson = detailedRatingsJson,
            CreatedAt = DateTime.UtcNow
        };

        _context.TeacherEvaluations.Add(eval);
        await _context.SaveChangesAsync();

        var result = new TeacherEvaluationDto
        {
            Id = eval.Id,
            StudentId = eval.StudentId,
            StudentName = studentProfile.FullName,
            ClassId = eval.ClassId,
            ClassName = classInfo.ClassName,
            TeacherId = eval.TeacherId,
            TeachingQualityRating = eval.TeachingQualityRating,
            SupportRating = eval.SupportRating,
            CurriculumRating = eval.CurriculumRating,
            PunctualityRating = eval.PunctualityRating,
            Rating = eval.Rating,
            Comment = eval.Comment,
            DetailedRatings = dto.DetailedRatings,
            CreatedAt = DateTime.SpecifyKind(eval.CreatedAt, DateTimeKind.Utc)
        };

        return Ok(result);
    }

    /// <summary>
    /// Student views their own evaluations
    /// </summary>
    [Authorize(Roles = "HocVien")]
    [HttpGet("my-evaluations")]
    public async Task<ActionResult<List<TeacherEvaluationDto>>> GetMyEvaluations()
    {
        var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
        var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
        if (student == null)
            return NotFound(new { message = "Hồ sơ học viên chưa được tạo" });

        var list = await _context.TeacherEvaluations
            .Where(e => e.StudentId == student.StudentId)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

        var result = new List<TeacherEvaluationDto>();
        var classCache = new Dictionary<int, string>();

        foreach (var eval in list)
        {
            if (!classCache.ContainsKey(eval.ClassId))
            {
                var classInfo = await _courseServiceClient.GetClassInfo(eval.ClassId);
                classCache[eval.ClassId] = classInfo?.ClassName ?? "Lớp học #" + eval.ClassId;
            }

            result.Add(new TeacherEvaluationDto
            {
                Id = eval.Id,
                StudentId = eval.StudentId,
                StudentName = student.FullName,
                ClassId = eval.ClassId,
                ClassName = classCache[eval.ClassId],
                TeacherId = eval.TeacherId,
                TeachingQualityRating = eval.TeachingQualityRating,
                SupportRating = eval.SupportRating,
                CurriculumRating = eval.CurriculumRating,
                PunctualityRating = eval.PunctualityRating,
                Rating = eval.Rating,
                Comment = eval.Comment,
                DetailedRatings = ParseDetailedRatings(eval.DetailedRatingsJson),
                CreatedAt = DateTime.SpecifyKind(eval.CreatedAt, DateTimeKind.Utc)
            });
        }

        return Ok(result);
    }

    /// <summary>
    /// Admin or Teacher views evaluations for a specific teacher
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien")]
    [HttpGet("teacher/{teacherId:int}")]
    public async Task<ActionResult<TeacherEvaluationSummaryDto>> GetTeacherEvaluations(int teacherId)
    {
        // For security, teachers can only view their own evaluations
        if (User.IsInRole("GiaoVien"))
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
            if (userId != teacherId)
                return Forbid();
        }

        var list = await _context.TeacherEvaluations
            .Include(e => e.Student)
            .Where(e => e.TeacherId == teacherId)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

        var summary = new TeacherEvaluationSummaryDto
        {
            TeacherId = teacherId,
            TotalEvaluations = list.Count,
            AverageRating = list.Any() ? Math.Round(list.Average(e => e.Rating), 2) : 0,
            RatingDistribution = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 } }
        };

        var classCache = new Dictionary<int, string>();
        foreach (var eval in list)
        {
            // Map rounded overall rating to distribution bucket
            int roundedRating = (int)Math.Round(eval.Rating);
            if (roundedRating >= 1 && roundedRating <= 5)
            {
                summary.RatingDistribution[roundedRating]++;
            }

            if (!classCache.ContainsKey(eval.ClassId))
            {
                var classInfo = await _courseServiceClient.GetClassInfo(eval.ClassId);
                classCache[eval.ClassId] = classInfo?.ClassName ?? "Lớp học #" + eval.ClassId;
            }

            summary.Evaluations.Add(new TeacherEvaluationDto
            {
                Id = eval.Id,
                StudentId = eval.StudentId,
                StudentName = eval.Student?.FullName ?? "Học viên ẩn danh",
                ClassId = eval.ClassId,
                ClassName = classCache[eval.ClassId],
                TeacherId = eval.TeacherId,
                TeachingQualityRating = eval.TeachingQualityRating,
                SupportRating = eval.SupportRating,
                CurriculumRating = eval.CurriculumRating,
                PunctualityRating = eval.PunctualityRating,
                Rating = eval.Rating,
                Comment = eval.Comment,
                DetailedRatings = ParseDetailedRatings(eval.DetailedRatingsJson),
                CreatedAt = DateTime.SpecifyKind(eval.CreatedAt, DateTimeKind.Utc)
            });
        }

        return Ok(summary);
    }

    /// <summary>
    /// Admin or Teacher views evaluations for a specific class
    /// </summary>
    [Authorize(Roles = "Admin,GiaoVien")]
    [HttpGet("class/{classId:int}")]
    public async Task<ActionResult<List<TeacherEvaluationDto>>> GetClassEvaluations(int classId)
    {
        var list = await _context.TeacherEvaluations
            .Include(e => e.Student)
            .Where(e => e.ClassId == classId)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

        var classInfo = await _courseServiceClient.GetClassInfo(classId);
        var className = classInfo?.ClassName ?? "Lớp học #" + classId;

        var result = list.Select(eval => new TeacherEvaluationDto
        {
            Id = eval.Id,
            StudentId = eval.StudentId,
            StudentName = eval.Student?.FullName ?? "Học viên ẩn danh",
            ClassId = eval.ClassId,
            ClassName = className,
            TeacherId = eval.TeacherId,
            TeachingQualityRating = eval.TeachingQualityRating,
            SupportRating = eval.SupportRating,
            CurriculumRating = eval.CurriculumRating,
            PunctualityRating = eval.PunctualityRating,
            Rating = eval.Rating,
            Comment = eval.Comment,
            DetailedRatings = ParseDetailedRatings(eval.DetailedRatingsJson),
            CreatedAt = DateTime.SpecifyKind(eval.CreatedAt, DateTimeKind.Utc)
        }).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Lấy điểm đánh giá trung bình của giáo viên (cho học viên xem)
    /// </summary>
    [AllowAnonymous]
    [HttpGet("teacher/{teacherId:int}/average")]
    public async Task<ActionResult<double>> GetTeacherAverageRating(int teacherId)
    {
        var ratings = await _context.TeacherEvaluations
            .Where(e => e.TeacherId == teacherId)
            .Select(e => e.Rating)
            .ToListAsync();

        if (!ratings.Any())
            return Ok(0.0);

        return Ok(Math.Round(ratings.Average(), 1));
    }

    /// <summary>
    /// Admin views all evaluations in the system
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<ActionResult<List<TeacherEvaluationDto>>> GetAllEvaluations()
    {
        var list = await _context.TeacherEvaluations
            .Include(e => e.Student)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

        var result = new List<TeacherEvaluationDto>();
        var classCache = new Dictionary<int, string>();

        foreach (var eval in list)
        {
            if (!classCache.ContainsKey(eval.ClassId))
            {
                var classInfo = await _courseServiceClient.GetClassInfo(eval.ClassId);
                classCache[eval.ClassId] = classInfo?.ClassName ?? "Lớp học #" + eval.ClassId;
            }

            result.Add(new TeacherEvaluationDto
            {
                Id = eval.Id,
                StudentId = eval.StudentId,
                StudentName = eval.Student?.FullName ?? "Học viên ẩn danh",
                ClassId = eval.ClassId,
                ClassName = classCache[eval.ClassId],
                TeacherId = eval.TeacherId,
                TeachingQualityRating = eval.TeachingQualityRating,
                SupportRating = eval.SupportRating,
                CurriculumRating = eval.CurriculumRating,
                PunctualityRating = eval.PunctualityRating,
                Rating = eval.Rating,
                Comment = eval.Comment,
                DetailedRatings = ParseDetailedRatings(eval.DetailedRatingsJson),
                CreatedAt = DateTime.SpecifyKind(eval.CreatedAt, DateTimeKind.Utc)
            });
        }

        return Ok(result);
    }

    /// <summary>
    /// Học viên & Admin lấy trạng thái bật/khóa hệ thống đánh giá (hỗ trợ check theo ClassId)
    /// </summary>
    [AllowAnonymous]
    [HttpGet("status")]
    public async Task<ActionResult<object>> GetEvaluationStatus([FromQuery] int? classId)
    {
        var setting = await _context.SystemSettings.FindAsync("IsEvaluationEnabled");
        bool isGlobalEnabled = setting != null && setting.Value.ToLower() == "true";
        
        if (!isGlobalEnabled)
        {
            return Ok(new { isEvaluationEnabled = false });
        }

        if (classId.HasValue)
        {
            var enabledClassesSetting = await _context.SystemSettings.FindAsync("EnabledEvaluationClassIds");
            if (enabledClassesSetting == null || string.IsNullOrWhiteSpace(enabledClassesSetting.Value))
            {
                return Ok(new { isEvaluationEnabled = false });
            }

            var enabledClassIds = enabledClassesSetting.Value.Split(',')
                .Select(id => id.Trim())
                .ToList();

            bool isClassEnabled = enabledClassIds.Contains(classId.Value.ToString());
            return Ok(new { isEvaluationEnabled = isClassEnabled });
        }

        return Ok(new { isEvaluationEnabled = isGlobalEnabled });
    }

    /// <summary>
    /// Admin bật/khóa hệ thống đánh giá
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("status")]
    public async Task<ActionResult<object>> SetEvaluationStatus([FromBody] EvaluationStatusUpdateDto dto)
    {
        var setting = await _context.SystemSettings.FindAsync("IsEvaluationEnabled");
        if (setting == null)
        {
            setting = new SystemSetting { Key = "IsEvaluationEnabled", Value = dto.IsEvaluationEnabled.ToString().ToLower() };
            _context.SystemSettings.Add(setting);
        }
        else
        {
            setting.Value = dto.IsEvaluationEnabled.ToString().ToLower();
        }

        await _context.SaveChangesAsync();
        return Ok(new { isEvaluationEnabled = dto.IsEvaluationEnabled });
    }

    /// <summary>
    /// Admin hoặc Học viên lấy danh sách các lớp học được phép đánh giá
    /// </summary>
    [Authorize(Roles = "Admin,HocVien")]
    [HttpGet("enabled-classes")]
    public async Task<ActionResult<object>> GetEnabledClasses()
    {
        var setting = await _context.SystemSettings.FindAsync("EnabledEvaluationClassIds");
        if (setting == null || string.IsNullOrWhiteSpace(setting.Value))
        {
            return Ok(new { classIds = new List<int>() });
        }

        var ids = setting.Value.Split(',')
            .Select(s => int.TryParse(s, out var i) ? (int?)i : null)
            .Where(x => x.HasValue)
            .Select(x => x!.Value)
            .ToList();

        return Ok(new { classIds = ids });
    }

    /// <summary>
    /// Admin cập nhật danh sách các lớp học được phép đánh giá
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("enabled-classes")]
    public async Task<ActionResult<object>> SetEnabledClasses([FromBody] EvaluationClassesUpdateDto dto)
    {
        var setting = await _context.SystemSettings.FindAsync("EnabledEvaluationClassIds");
        var valStr = string.Join(",", dto.ClassIds);
        
        if (setting == null)
        {
            setting = new SystemSetting { Key = "EnabledEvaluationClassIds", Value = valStr };
            _context.SystemSettings.Add(setting);
        }
        else
        {
            setting.Value = valStr;
        }

        await _context.SaveChangesAsync();
        return Ok(new { classIds = dto.ClassIds });
    }

    private List<DetailedRatingDto>? ParseDetailedRatings(string? json)
    {
        if (string.IsNullOrEmpty(json)) return null;
        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<List<DetailedRatingDto>>(json);
        }
        catch
        {
            return null;
        }
    }
}
