using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PaymentService.DTOs;
using PaymentService.Features.Payments.Queries;
using PaymentService.Features.Users.Queries;
using MediatR;
using Asp.Versioning;

namespace PaymentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles = "Admin")]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Báo cáo doanh thu (theo tháng)
    /// </summary>
    [HttpGet("revenue")]
    public async Task<ActionResult<RevenueReportDto>> GetRevenueReport(
        [FromQuery] int? year,
        [FromQuery] int? month)
    {
        var result = await _mediator.Send(new GetRevenueReportQuery(year, month));
        return Ok(result);
    }

    /// <summary>
    /// Doanh thu theo khóa học
    /// </summary>
    [HttpGet("revenue/course/{courseId}")]
    public async Task<ActionResult> GetRevenueByCourse(int courseId)
    {
        var result = await _mediator.Send(new GetRevenueByCourseQuery(courseId));
        return Ok(result);
    }

    /// <summary>
    /// Doanh thu theo lớp
    /// </summary>
    [HttpGet("revenue/class/{classId}")]
    public async Task<ActionResult> GetRevenueByClass(int classId)
    {
        var result = await _mediator.Send(new GetRevenueByClassQuery(classId));
        return Ok(result);
    }

    /// <summary>
    /// Dashboard tổng quan
    /// </summary>
    [HttpGet("dashboard")]
    public async Task<ActionResult<DashboardDto>> GetDashboard()
    {
        var result = await _mediator.Send(new GetDashboardQuery());
        return Ok(result);
    }
}
