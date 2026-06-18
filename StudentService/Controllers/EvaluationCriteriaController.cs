using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Models;
using StudentService.DTOs;
using Asp.Versioning;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/teacher-evaluations/criteria")]
[Authorize]
public class EvaluationCriteriaController : ControllerBase
{
    private readonly StudentDbContext _context;

    public EvaluationCriteriaController(StudentDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Học viên & Admin lấy danh sách tiêu chí đang hoạt động
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<List<EvaluationCriterionDto>>> GetActiveCriteria()
    {
        var list = await _context.EvaluationCriteria
            .Where(c => c.IsActive)
            .Select(c => new EvaluationCriterionDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                IsActive = c.IsActive
            })
            .ToListAsync();

        return Ok(list);
    }

    /// <summary>
    /// Admin lấy toàn bộ danh sách tiêu chí (cả hoạt động và ngưng hoạt động)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<ActionResult<List<EvaluationCriterionDto>>> GetAllCriteria()
    {
        var list = await _context.EvaluationCriteria
            .Select(c => new EvaluationCriterionDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                IsActive = c.IsActive
            })
            .ToListAsync();

        return Ok(list);
    }

    /// <summary>
    /// Admin thêm tiêu chí mới
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<EvaluationCriterionDto>> CreateCriterion(CreateEvaluationCriterionDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest(new { message = "Tên tiêu chí không được để trống" });

        var exists = await _context.EvaluationCriteria.AnyAsync(c => c.Name.ToLower() == dto.Name.ToLower());
        if (exists)
            return BadRequest(new { message = "Tiêu chí này đã tồn tại" });

        var criterion = new EvaluationCriterion
        {
            Name = dto.Name.Trim(),
            Description = dto.Description?.Trim(),
            IsActive = dto.IsActive
        };

        _context.EvaluationCriteria.Add(criterion);
        await _context.SaveChangesAsync();

        return Ok(new EvaluationCriterionDto
        {
            Id = criterion.Id,
            Name = criterion.Name,
            Description = criterion.Description,
            IsActive = criterion.IsActive
        });
    }

    /// <summary>
    /// Admin sửa tiêu chí
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult<EvaluationCriterionDto>> UpdateCriterion(int id, CreateEvaluationCriterionDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest(new { message = "Tên tiêu chí không được để trống" });

        var criterion = await _context.EvaluationCriteria.FindAsync(id);
        if (criterion == null)
            return NotFound(new { message = "Không tìm thấy tiêu chí" });

        var exists = await _context.EvaluationCriteria.AnyAsync(c => c.Id != id && c.Name.ToLower() == dto.Name.ToLower());
        if (exists)
            return BadRequest(new { message = "Tên tiêu chí này đã bị trùng" });

        criterion.Name = dto.Name.Trim();
        criterion.Description = dto.Description?.Trim();
        criterion.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return Ok(new EvaluationCriterionDto
        {
            Id = criterion.Id,
            Name = criterion.Name,
            Description = criterion.Description,
            IsActive = criterion.IsActive
        });
    }

    /// <summary>
    /// Admin xóa hoặc đổi trạng thái tiêu chí
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCriterion(int id)
    {
        var criterion = await _context.EvaluationCriteria.FindAsync(id);
        if (criterion == null)
            return NotFound(new { message = "Không tìm thấy tiêu chí" });

        criterion.IsActive = !criterion.IsActive;
        await _context.SaveChangesAsync();

        return Ok(new { message = $"Đã thay đổi trạng thái hoạt động của tiêu chí thành {(criterion.IsActive ? "Kích hoạt" : "Ngưng hoạt động")}" });
    }
}
