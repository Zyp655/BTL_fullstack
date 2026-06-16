using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentService.Data;
using PaymentService.DTOs;
using PaymentService.Models;
using PaymentService.Services;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class TeacherSalaryController : ControllerBase
{
    private readonly PaymentDbContext _context;
    private readonly ICourseServiceClient _courseServiceClient;
    private readonly IStudentServiceClient _studentServiceClient;

    public TeacherSalaryController(
        PaymentDbContext context,
        ICourseServiceClient courseServiceClient,
        IStudentServiceClient studentServiceClient)
    {
        _context = context;
        _courseServiceClient = courseServiceClient;
        _studentServiceClient = studentServiceClient;
    }

    /// <summary>
    /// Lấy danh sách cấu hình lương của tất cả giảng viên (Admin)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpGet("configs")]
    public async Task<ActionResult<IEnumerable<TeacherSalaryConfigDto>>> GetConfigs()
    {
        var teachers = await _context.Users
            .Where(u => u.Role == "GiaoVien" && u.IsActive)
            .ToListAsync();

        var configs = await _context.TeacherSalaryConfigs.ToListAsync();
        var result = new List<TeacherSalaryConfigDto>();

        foreach (var t in teachers)
        {
            var config = configs.FirstOrDefault(c => c.UserId == t.UserId);
            result.Add(new TeacherSalaryConfigDto
            {
                UserId = t.UserId,
                FullName = t.FullName,
                Username = t.Username,
                BaseSalary = config?.BaseSalary ?? 0,
                RatePerSession = config?.RatePerSession ?? 300000,
                StudentAllowanceRate = config?.StudentAllowanceRate ?? 0,
                Notes = config?.Notes
            });
        }

        return Ok(result);
    }

    /// <summary>
    /// Cập nhật cấu hình lương của giảng viên (Admin)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut("configs/{teacherId:int}")]
    public async Task<ActionResult<TeacherSalaryConfigDto>> UpdateConfig(int teacherId, [FromBody] UpdateTeacherSalaryConfigDto dto)
    {
        var teacher = await _context.Users.FindAsync(teacherId);
        if (teacher == null || teacher.Role != "GiaoVien")
            return NotFound(new { message = $"Không tìm thấy giảng viên ID {teacherId}" });

        var config = await _context.TeacherSalaryConfigs.FindAsync(teacherId);
        if (config == null)
        {
            config = new TeacherSalaryConfig { UserId = teacherId };
            _context.TeacherSalaryConfigs.Add(config);
        }

        config.BaseSalary = dto.BaseSalary;
        config.RatePerSession = dto.RatePerSession;
        config.StudentAllowanceRate = dto.StudentAllowanceRate;
        config.Notes = dto.Notes;

        await _context.SaveChangesAsync();

        return Ok(new TeacherSalaryConfigDto
        {
            UserId = teacher.UserId,
            FullName = teacher.FullName,
            Username = teacher.Username,
            BaseSalary = config.BaseSalary,
            RatePerSession = config.RatePerSession,
            StudentAllowanceRate = config.StudentAllowanceRate,
            Notes = config.Notes
        });
    }

    /// <summary>
    /// Lấy danh sách phiếu lương theo tháng/năm
    /// Admin: Lấy toàn bộ
    /// Giáo viên: Chỉ lấy phiếu lương của bản thân
    /// </summary>
    [HttpGet("slips")]
    public async Task<ActionResult<IEnumerable<SalarySlipDto>>> GetSlips([FromQuery] int month, [FromQuery] int year)
    {
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        var query = _context.SalarySlips
            .Include(s => s.Teacher)
            .Where(s => s.Month == month && s.Year == year);

        if (role == "GiaoVien" && !string.IsNullOrEmpty(userIdStr) && int.TryParse(userIdStr, out int teacherId))
        {
            query = query.Where(s => s.TeacherId == teacherId);
        }
        else if (role != "Admin")
        {
            return Forbid();
        }

        var slips = await query.ToListAsync();

        var result = slips.Select(s => new SalarySlipDto
        {
            SalarySlipId = s.SalarySlipId,
            TeacherId = s.TeacherId,
            TeacherName = s.Teacher?.FullName ?? $"ID {s.TeacherId}",
            Month = s.Month,
            Year = s.Year,
            BaseSalary = s.BaseSalary,
            RatePerSession = s.RatePerSession,
            SessionsTaught = s.SessionsTaught,
            TotalStudentSessions = s.TotalStudentSessions,
            StudentAllowanceRate = s.StudentAllowanceRate,
            CalculatedSalary = s.CalculatedSalary,
            Bonus = s.Bonus,
            Deductions = s.Deductions,
            TotalAmount = s.TotalAmount,
            Status = s.Status,
            Notes = s.Notes,
            PaidAt = s.PaidAt,
            CreatedAt = s.CreatedAt
        }).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Tính toán bảng lương tháng (Admin)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("slips/calculate")]
    public async Task<ActionResult<IEnumerable<SalarySlipDto>>> CalculatePayroll([FromBody] CalculatePayrollDto dto)
    {
        var teachers = await _context.Users
            .Where(u => u.Role == "GiaoVien" && u.IsActive)
            .ToListAsync();

        var configs = await _context.TeacherSalaryConfigs.ToListAsync();
        var calculatedSlips = new List<SalarySlipDto>();

        foreach (var teacher in teachers)
        {
            var config = configs.FirstOrDefault(c => c.UserId == teacher.UserId)
                ?? new TeacherSalaryConfig { UserId = teacher.UserId, BaseSalary = 0, RatePerSession = 300000, StudentAllowanceRate = 0 };

            // 1. Lấy danh sách lớp học của GV trong CourseService
            var classes = await _courseServiceClient.GetClassesByTeacher(teacher.UserId);
            
            int sessionsTaught = 0;
            int totalStudentSessions = 0;

            if (classes != null && classes.Any())
            {
                var classIds = classes.Select(c => c.ClassId).ToList();

                // 2. Lấy thống kê điểm danh của các lớp này trong tháng/năm qua StudentService
                var stats = await _studentServiceClient.GetAttendanceStats(classIds, dto.Month, dto.Year);
                if (stats != null)
                {
                    sessionsTaught = stats.SessionsTaught;
                    totalStudentSessions = stats.TotalStudentSessions;
                }
            }

            // 3. Tính toán lương
            decimal calculatedSalary = config.BaseSalary + (sessionsTaught * config.RatePerSession) + (totalStudentSessions * config.StudentAllowanceRate);

            // 4. Lưu hoặc cập nhật phiếu lương
            var slip = await _context.SalarySlips
                .FirstOrDefaultAsync(s => s.TeacherId == teacher.UserId && s.Month == dto.Month && s.Year == dto.Year);

            if (slip != null)
            {
                // Chỉ cập nhật nếu chưa thanh toán
                if (slip.Status != "Paid")
                {
                    slip.BaseSalary = config.BaseSalary;
                    slip.RatePerSession = config.RatePerSession;
                    slip.SessionsTaught = sessionsTaught;
                    slip.TotalStudentSessions = totalStudentSessions;
                    slip.StudentAllowanceRate = config.StudentAllowanceRate;
                    slip.CalculatedSalary = calculatedSalary;
                    slip.TotalAmount = calculatedSalary + slip.Bonus - slip.Deductions;
                }
            }
            else
            {
                slip = new SalarySlip
                {
                    TeacherId = teacher.UserId,
                    Month = dto.Month,
                    Year = dto.Year,
                    BaseSalary = config.BaseSalary,
                    RatePerSession = config.RatePerSession,
                    SessionsTaught = sessionsTaught,
                    TotalStudentSessions = totalStudentSessions,
                    StudentAllowanceRate = config.StudentAllowanceRate,
                    CalculatedSalary = calculatedSalary,
                    Bonus = 0,
                    Deductions = 0,
                    TotalAmount = calculatedSalary,
                    Status = "Pending"
                };
                _context.SalarySlips.Add(slip);
            }

            await _context.SaveChangesAsync();

            calculatedSlips.Add(new SalarySlipDto
            {
                SalarySlipId = slip.SalarySlipId,
                TeacherId = slip.TeacherId,
                TeacherName = teacher.FullName,
                Month = slip.Month,
                Year = slip.Year,
                BaseSalary = slip.BaseSalary,
                RatePerSession = slip.RatePerSession,
                SessionsTaught = slip.SessionsTaught,
                TotalStudentSessions = slip.TotalStudentSessions,
                StudentAllowanceRate = slip.StudentAllowanceRate,
                CalculatedSalary = slip.CalculatedSalary,
                Bonus = slip.Bonus,
                Deductions = slip.Deductions,
                TotalAmount = slip.TotalAmount,
                Status = slip.Status,
                Notes = slip.Notes,
                PaidAt = slip.PaidAt,
                CreatedAt = slip.CreatedAt
            });
        }

        return Ok(calculatedSlips);
    }

    /// <summary>
    /// Phê duyệt hoặc Thanh toán phiếu lương (Admin)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut("slips/{slipId:int}/status")]
    public async Task<ActionResult<SalarySlipDto>> UpdateSlipStatus(int slipId, [FromBody] UpdateSalarySlipStatusDto dto)
    {
        var slip = await _context.SalarySlips
            .Include(s => s.Teacher)
            .FirstOrDefaultAsync(s => s.SalarySlipId == slipId);

        if (slip == null)
            return NotFound(new { message = $"Không tìm thấy phiếu lương ID {slipId}" });

        slip.Bonus = dto.Bonus;
        slip.Deductions = dto.Deductions;
        slip.Notes = dto.Notes;
        
        if (dto.Status == "Paid" && slip.Status != "Paid")
        {
            slip.Status = "Paid";
            slip.PaidAt = DateTime.UtcNow;
        }
        else if (dto.Status == "Approved" && slip.Status != "Paid")
        {
            slip.Status = "Approved";
        }

        slip.TotalAmount = slip.CalculatedSalary + slip.Bonus - slip.Deductions;

        await _context.SaveChangesAsync();

        return Ok(new SalarySlipDto
        {
            SalarySlipId = slip.SalarySlipId,
            TeacherId = slip.TeacherId,
            TeacherName = slip.Teacher?.FullName ?? $"ID {slip.TeacherId}",
            Month = slip.Month,
            Year = slip.Year,
            BaseSalary = slip.BaseSalary,
            RatePerSession = slip.RatePerSession,
            SessionsTaught = slip.SessionsTaught,
            TotalStudentSessions = slip.TotalStudentSessions,
            StudentAllowanceRate = slip.StudentAllowanceRate,
            CalculatedSalary = slip.CalculatedSalary,
            Bonus = slip.Bonus,
            Deductions = slip.Deductions,
            TotalAmount = slip.TotalAmount,
            Status = slip.Status,
            Notes = slip.Notes,
            PaidAt = slip.PaidAt,
            CreatedAt = slip.CreatedAt
        });
    }
}
