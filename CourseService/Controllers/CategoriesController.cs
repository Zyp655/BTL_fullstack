using Microsoft.AspNetCore.Mvc;
using CourseService.DTOs;
using CourseService.Features.Categories.Commands;
using CourseService.Features.Categories.Queries;
using MediatR;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System;

namespace CourseService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy danh sách tất cả các danh mục
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var result = await _mediator.Send(new GetCategoriesQuery());
        return Ok(result);
    }

    /// <summary>
    /// Chi tiết danh mục
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var result = await _mediator.Send(new GetCategoryByIdQuery(id));
        if (result == null)
            return NotFound(new { message = "Không tìm thấy danh mục" });

        return Ok(result);
    }

    /// <summary>
    /// Tạo danh mục mới (Admin)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto dto)
    {
        try
        {
            var command = new CreateCategoryCommand(dto.CategoryName, dto.CategoryCode);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCategory), new { id = result.CategoryId }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Cập nhật thông tin danh mục (Admin)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, UpdateCategoryDto dto)
    {
        try
        {
            var command = new UpdateCategoryCommand(id, dto.CategoryName, dto.CategoryCode);
            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound(new { message = "Không tìm thấy danh mục" });

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Xóa danh mục (Admin)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        try
        {
            var result = await _mediator.Send(new DeleteCategoryCommand(id));
            if (!result)
                return NotFound(new { message = "Không tìm thấy danh mục" });

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
