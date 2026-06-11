using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PaymentService.DTOs;
using PaymentService.Features.Payments.Commands;
using PaymentService.Features.Payments.Queries;
using PaymentService.Repositories;
using PaymentService.Services;
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
    private readonly IPaymentRepository _paymentRepository;
    private readonly ICourseServiceClient _courseServiceClient;

    public PaymentsController(
        IMediator mediator, 
        IConfiguration configuration, 
        IPaymentRepository paymentRepository,
        ICourseServiceClient courseServiceClient)
    {
        _mediator = mediator;
        _configuration = configuration;
        _paymentRepository = paymentRepository;
        _courseServiceClient = courseServiceClient;
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

        var gateway = dto.Gateway ?? string.Empty;
        var transactionDate = dto.TransactionDate ?? dto.TransactionDateCamel ?? string.Empty;
        var accountNumber = dto.AccountNumber ?? dto.AccountNumberCamel ?? string.Empty;
        var amountIn = dto.AmountIn ?? dto.TransferAmount ?? dto.AmountInCamel ?? 0;
        var amountOut = dto.AmountOut ?? dto.AmountOutCamel ?? 0;
        var code = dto.Code ?? string.Empty;
        var transactionContent = dto.TransactionContent ?? dto.Content ?? dto.TransactionContentCamel ?? string.Empty;
        var referenceNumber = dto.ReferenceNumber ?? dto.ReferenceCode ?? dto.ReferenceNumberCamel ?? string.Empty;

        Console.WriteLine($"[DEBUG SEPAY] Received DTO: ID={dto.Id}, Gateway={gateway}, Date={transactionDate}, Account={accountNumber}, AmountIn={amountIn}, AmountOut={amountOut}, Code='{code}', Content='{transactionContent}', Ref={referenceNumber}");

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

        var paymentId = ParsePaymentId(transactionContent, code);
        if (!paymentId.HasValue)
        {
            return BadRequest(new { message = "Không thể phân tích PaymentId từ nội dung chuyển khoản. Cú pháp mẫu: PAY[Mã phiếu] hoặc PaymentId [Mã phiếu]" });
        }

        try
        {
            var command = new AddTransactionCommand(
                PaymentId: paymentId.Value,
                Amount: amountIn,
                PaymentMethod: "ChuyenKhoan",
                Note: $"Thanh toan tu dong qua Sepay. Ref: {referenceNumber}. ND: {transactionContent}",
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

    private int? ParsePaymentId(string? content, string? code)
    {
        if (!string.IsNullOrEmpty(code))
        {
            var codeClean = code.ToUpper().Replace("PAY", "").Replace("PAYMENTID", "").Trim();
            if (int.TryParse(codeClean, out var id))
                return id;
        }

        if (string.IsNullOrEmpty(content))
            return null;

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

    /// <summary>
    /// Tạo thông tin thanh toán động qua cổng chuyển khoản (PayOS, VNPAY, MoMo, SePay)
    /// </summary>
    [HttpGet("{id}/checkout")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCheckoutInfo(int id, [FromQuery] string gateway = "SePay")
    {
        var payment = await _paymentRepository.GetPaymentByIdAsync(id);
        if (payment == null)
            return NotFound(new { message = "Không tìm thấy hóa đơn học phí" });

        if (payment.Status == "HoanTat")
            return BadRequest(new { message = "Hóa đơn này đã được thanh toán hoàn tất" });

        string className = "Chờ xếp lớp";
        string courseName = "";

        if (payment.ClassId < 0)
        {
            var courseId = -payment.ClassId;
            var courseInfo = await _courseServiceClient.GetCourseInfo(courseId);
            if (courseInfo != null)
            {
                courseName = courseInfo.CourseName;
            }
        }
        else if (payment.ClassId > 0)
        {
            var classInfo = await _courseServiceClient.GetClassInfo(payment.ClassId);
            if (classInfo != null)
            {
                className = classInfo.ClassName;
                courseName = classInfo.CourseName;
            }
        }

        var bankId = _configuration["Sepay:BankId"] ?? "MB";
        var accountNo = _configuration["Sepay:AccountNo"] ?? "0366265607";
        var accountName = _configuration["Sepay:AccountName"] ?? "NGUYEN DINH MINH HIEU";
        var bankName = _configuration["Sepay:BankName"] ?? "MBBank";

        var amount = payment.RemainingAmount;
        var code = $"PAY{payment.PaymentId}";

        string checkoutUrl = $"/checkout-mock?gateway={gateway}&id={payment.PaymentId}&amount={amount}";
        string qrUrl = "";

        switch (gateway.ToLower())
        {
            case "sepay":
                qrUrl = $"https://qr.sepay.vn/img?acc={accountNo}&bank={bankId}&amount={amount}&des={code}&template=compact";
                break;
            case "payos":
                qrUrl = $"https://img.vietqr.io/image/{bankId}-{accountNo}-compact.png?amount={amount}&addInfo={code}&accountName={Uri.EscapeDataString(accountName)}";
                break;
            case "vnpay":
                qrUrl = $"https://api.qrserver.com/v1/create-qr-code/?size=250x250&data=https://vnpay.vn/payment/gate?txnref={code}%26amount={amount}";
                break;
            case "momo":
                var momoData = $"2|99|0366265607|NGUYEN%20DINH%20MINH%20HIEU|hieu.nguyen@gmail.com|0|0|{amount}|{code}|transfer_my_momo";
                qrUrl = $"https://api.qrserver.com/v1/create-qr-code/?size=250x250&data={Uri.EscapeDataString(momoData)}";
                break;
            default:
                qrUrl = $"https://qr.sepay.vn/img?acc={accountNo}&bank={bankId}&amount={amount}&des={code}&template=compact";
                break;
        }

        return Ok(new {
            paymentId = payment.PaymentId,
            classId = payment.ClassId,
            className = className,
            courseName = courseName,
            remainingAmount = amount,
            paymentCode = code,
            gateway = gateway,
            checkoutUrl = checkoutUrl,
            qrUrl = qrUrl,
            bankId = bankId,
            accountNo = accountNo,
            accountName = accountName,
            bankName = bankName
        });
    }

    /// <summary>
    /// Webhook nhận callback thanh toán tự động giả lập PayOS
    /// </summary>
    [HttpPost("callback/payos")]
    [AllowAnonymous]
    public async Task<IActionResult> PayosCallback([FromBody] PayosCallbackDto dto)
    {
        if (dto == null || string.IsNullOrEmpty(dto.OrderCode))
            return BadRequest(new { message = "Dữ liệu không hợp lệ" });

        var paymentIdStr = dto.OrderCode.ToUpper().Replace("PAY", "").Trim();
        if (!int.TryParse(paymentIdStr, out var paymentId))
            return BadRequest(new { message = "Không phân tích được ID hóa đơn" });

        try
        {
            var command = new AddTransactionCommand(
                PaymentId: paymentId,
                Amount: dto.Amount,
                PaymentMethod: "ChuyenKhoan",
                Note: $"Thanh toán tự động qua PayOS. Code: {dto.OrderCode}",
                ReceivedByUserId: 1
            );
            var result = await _mediator.Send(command);
            return Ok(new { success = true, message = "PayOS Callback Success", transaction = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Webhook nhận callback thanh toán tự động giả lập VNPAY (IPN)
    /// </summary>
    [HttpGet("callback/vnpay")]
    [AllowAnonymous]
    public async Task<IActionResult> VnpayCallback([FromQuery] string vnp_TxnRef, [FromQuery] decimal vnp_Amount, [FromQuery] string vnp_ResponseCode)
    {
        if (string.IsNullOrEmpty(vnp_TxnRef))
            return BadRequest(new { message = "Dữ liệu không hợp lệ" });

        var paymentIdStr = vnp_TxnRef.ToUpper().Replace("PAY", "").Trim();
        if (!int.TryParse(paymentIdStr, out var paymentId))
            return BadRequest(new { message = "Không phân tích được ID hóa đơn" });

        if (vnp_ResponseCode != "00")
            return BadRequest(new { message = "Giao dịch VNPAY thất bại" });

        try
        {
            var realAmount = vnp_Amount / 100;
            var command = new AddTransactionCommand(
                PaymentId: paymentId,
                Amount: realAmount,
                PaymentMethod: "TheTD",
                Note: $"Thanh toán tự động qua VNPAY-QR.",
                ReceivedByUserId: 1
            );
            var result = await _mediator.Send(command);
            return Ok(new { success = true, message = "VNPAY IPN Callback Success", transaction = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Webhook nhận callback thanh toán tự động giả lập MoMo
    /// </summary>
    [HttpPost("callback/momo")]
    [AllowAnonymous]
    public async Task<IActionResult> MomoCallback([FromBody] MomoCallbackDto dto)
    {
        if (dto == null || string.IsNullOrEmpty(dto.OrderId))
            return BadRequest(new { message = "Dữ liệu không hợp lệ" });

        var paymentIdStr = dto.OrderId.ToUpper().Replace("PAY", "").Trim();
        if (!int.TryParse(paymentIdStr, out var paymentId))
            return BadRequest(new { message = "Không phân tích được ID hóa đơn" });

        if (dto.ResultCode != 0)
            return BadRequest(new { message = "Giao dịch MoMo thất bại" });

        try
        {
            var command = new AddTransactionCommand(
                PaymentId: paymentId,
                Amount: dto.Amount,
                PaymentMethod: "ViDienTu",
                Note: $"Thanh toán tự động qua Ví MoMo. Mã GD: {dto.TransId}",
                ReceivedByUserId: 1
            );
            var result = await _mediator.Send(command);
            return Ok(new { success = true, message = "MoMo IPN Callback Success", transaction = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

public class PayosCallbackDto
{
    public string OrderCode { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class MomoCallbackDto
{
    public string OrderId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public long TransId { get; set; }
    public int ResultCode { get; set; }
}
