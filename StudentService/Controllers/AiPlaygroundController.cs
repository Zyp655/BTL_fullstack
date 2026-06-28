using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentService.Services;
using Asp.Versioning;

namespace StudentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class AiPlaygroundController : ControllerBase
{
    private readonly IAiServiceClient _aiServiceClient;

    public AiPlaygroundController(IAiServiceClient aiServiceClient)
    {
        _aiServiceClient = aiServiceClient;
    }

    [HttpGet("challenge")]
    public async Task<ActionResult<CodingChallengeDto>> GetChallenge([FromQuery] string topic = "Loops", [FromQuery] string language = "JavaScript")
    {
        try
        {
            var challenge = await _aiServiceClient.GenerateCodingChallengeAsync(topic, language);
            return Ok(challenge);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Lỗi khi tạo đề bài từ AI", error = ex.Message });
        }
    }

    [HttpPost("grade")]
    public async Task<ActionResult<CodingGradeDto>> GradeChallenge([FromBody] GradeChallengeRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.ProblemDescription) || string.IsNullOrWhiteSpace(request.SolutionCode))
        {
            return BadRequest(new { message = "Dữ liệu bài làm không hợp lệ" });
        }

        try
        {
            var result = await _aiServiceClient.GradeCodingChallengeAsync(request.ProblemDescription, request.SolutionCode, request.Language);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Lỗi khi chấm điểm bài làm bằng AI", error = ex.Message });
        }
    }
}

public class GradeChallengeRequest
{
    public string ProblemDescription { get; set; } = string.Empty;
    public string SolutionCode { get; set; } = string.Empty;
    public string Language { get; set; } = "JavaScript";
}
