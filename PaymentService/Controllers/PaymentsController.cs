using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PaymentService.DTOs;
using PaymentService.Features.Payments.Commands;
using PaymentService.Features.Payments.Queries;
using MediatR;
using Asp.Versioning;
using Microsoft.Extensions.Configuration;

namespace PaymentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public PaymentsController(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    /// <summary>
    /// Danh sách phiếu học phí (Admin)
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PagedResult<PaymentDto>>> GetPayments(
        [FromQuery] string? search,
        [FromQuery] string? status,
        [FromQuery] int? studentUserId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetPaymentsQuery(search, status, studentUserId, page, pageSize));
        return Ok(result);
    }

    /// <summary>
    /// Học phí của học viên
    /// </summary>
    [HttpGet("student/{userId}")]
    public async Task<ActionResult<List<PaymentDto>>> GetPaymentsByStudent(int userId)
    {
        // Self or Admin check
        var currentUserId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
        var currentRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        if (currentRole != "Admin" && currentUserId != userId)
            return Forbid();

        var result = await _mediator.Send(new GetPaymentsByStudentQuery(userId));
        return Ok(result);
    }

    /// <summary>
    /// Tạo phiếu thu học phí
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PaymentDto>> CreatePayment(CreatePaymentCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetPaymentsByStudent), new { userId = result.StudentUserId }, result);
    }

    /// <summary>
    /// Ghi nhận thanh toán
    /// </summary>
    [HttpPost("{id}/transactions")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TransactionDto>> AddTransaction(int id, CreateTransactionDto dto)
    {
        var currentUserId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
        var command = new AddTransactionCommand(
            PaymentId: id,
            Amount: dto.Amount,
            PaymentMethod: dto.PaymentMethod,
            Note: dto.Note,
            ReceivedByUserId: currentUserId
        );
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Lịch sử thanh toán của phiếu
    /// </summary>
    [HttpGet("{id}/transactions")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<TransactionDto>>> GetTransactions(int id)
    {
        var result = await _mediator.Send(new GetTransactionsQuery(id));
        return Ok(result);
    }

    /// <summary>
    /// Danh sách công nợ
    /// </summary>
    [HttpGet("debts")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<PaymentDto>>> GetDebts()
    {
        var result = await _mediator.Send(new GetDebtsQuery());
        return Ok(result);
    }

    /// <summary>
    /// Webhook nhận callback thanh toán tự động (Giả lập Sepay)
    /// </summary>
    [HttpPost("callback/sepay")]
    [AllowAnonymous]
    public async Task<IActionResult> SepayCallback(SepayWebhookDto dto)
    {
        if (dto == null)
            return BadRequest(new { message = "Dữ liệu trống" });

        Console.WriteLine($"[DEBUG SEPAY] Received DTO: ID={dto.Id}, Gateway={dto.Gateway}, Date={dto.TransactionDate}, Account={dto.AccountNumber}, AmountIn={dto.AmountIn}, AmountOut={dto.AmountOut}, Code='{dto.Code}', Content='{dto.TransactionContent}', Ref={dto.ReferenceNumber}");

        // Verify Webhook Token (Standard Enterprise Webhook authentication)
        var apiKey = _configuration["Sepay:WebhookToken"];
        if (!string.IsNullOrEmpty(apiKey))
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            Console.WriteLine($"[DEBUG SEPAY] Received Authorization Header: '{authHeader}'");
            Console.WriteLine($"[DEBUG SEPAY] Expected WebhookToken: '{apiKey}'");
            if (string.IsNullOrEmpty(authHeader) || 
                (!authHeader.Equals($"Apikey {apiKey}", StringComparison.OrdinalIgnoreCase) && 
                 !authHeader.Equals($"Bearer {apiKey}", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"[DEBUG SEPAY] Authentication FAILED!");
                return Unauthorized(new { message = "Xác thực Webhook thất bại" });
            }
            Console.WriteLine($"[DEBUG SEPAY] Authentication SUCCESS!");
        }

        var paymentId = ParsePaymentId(dto.TransactionContent, dto.Code);
        if (!paymentId.HasValue)
        {
            return BadRequest(new { message = "Không thể phân tích PaymentId từ nội dung chuyển khoản. Cú pháp mẫu: PAY[Mã phiếu] hoặc PaymentId [Mã phiếu]" });
        }

        try
        {
            var command = new AddTransactionCommand(
                PaymentId: paymentId.Value,
                Amount: dto.AmountIn,
                PaymentMethod: "ChuyenKhoan",
                Note: $"Thanh toan tu dong qua Sepay. Ref: {dto.ReferenceNumber}. ND: {dto.TransactionContent}",
                ReceivedByUserId: 1 // Sử dụng ID = 1 làm tài khoản hệ thống tự động ghi nhận
            );

            var result = await _mediator.Send(command);
            return Ok(new { success = true, message = "Thanh toán thành công", transaction = result });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    private int? ParsePaymentId(string content, string code)
    {
        if (!string.IsNullOrEmpty(code))
        {
            var codeClean = code.ToUpper().Replace("PAY", "").Replace("PAYMENTID", "").Trim();
            if (int.TryParse(codeClean, out var id))
                return id;
        }

        var contentUpper = content.ToUpper();
        
        // Search for PAYxx (e.g. PAY4)
        var matchIndex = contentUpper.IndexOf("PAY");
        if (matchIndex >= 0)
        {
            var sub = contentUpper.Substring(matchIndex + 3);
            var digitStr = "";
            foreach (var c in sub)
            {
                if (char.IsDigit(c))
                    digitStr += c;
                else if (digitStr.Length > 0)
                    break;
            }
            if (int.TryParse(digitStr, out var id))
                return id;
        }

        // Search for PAYMENTID xx
        var matchIndexId = contentUpper.IndexOf("PAYMENTID");
        if (matchIndexId >= 0)
        {
            var sub = contentUpper.Substring(matchIndexId + 9);
            var digitStr = "";
            foreach (var c in sub)
            {
                if (char.IsDigit(c))
                    digitStr += c;
                else if (digitStr.Length > 0)
                    break;
            }
            if (int.TryParse(digitStr, out var id))
                return id;
        }

        return null;
    }
}
