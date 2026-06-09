using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PaymentService.DTOs;
using PaymentService.Features.Users.Commands;
using PaymentService.Features.Users.Queries;
using PaymentService.Services;
using PaymentService.Repositories;
using MediatR;
using Asp.Versioning;

namespace PaymentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly OtpService _otpService;
    private readonly IUserRepository _userRepository;

    public AuthController(IMediator mediator, OtpService otpService, IUserRepository userRepository)
    {
        _mediator = mediator;
        _otpService = otpService;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Yêu cầu gửi mã OTP quên mật khẩu (Công khai)
    /// </summary>
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email))
            return BadRequest(new { message = "Email không được để trống" });

        var user = await _userRepository.GetUserByEmailAsync(dto.Email);
        if (user == null)
            return NotFound(new { message = "Không tìm thấy người dùng có địa chỉ email này" });

        // Generate and log/send OTP
        var otp = _otpService.GenerateOtp(dto.Email);

        // For development/test ease, we return the OTP directly in the body
        return Ok(new { 
            message = "Mã OTP đã được tạo và gửi đến email của bạn.", 
            otp = otp 
        });
    }

    /// <summary>
    /// Đặt lại mật khẩu sử dụng mã OTP (Công khai)
    /// </summary>
    [HttpPost("reset-password-otp")]
    public async Task<IActionResult> ResetPasswordWithOtp(ResetPasswordWithOtpDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Otp) || string.IsNullOrWhiteSpace(dto.NewPassword))
            return BadRequest(new { message = "Vui lòng nhập đầy đủ các trường thông tin" });

        if (dto.NewPassword.Length < 6)
            return BadRequest(new { message = "Mật khẩu mới phải có tối thiểu 6 ký tự" });

        var isOtpValid = _otpService.VerifyOtp(dto.Email, dto.Otp);
        if (!isOtpValid)
            return BadRequest(new { message = "Mã OTP không đúng hoặc đã hết hạn" });

        var user = await _userRepository.GetUserByEmailAsync(dto.Email);
        if (user == null)
            return NotFound(new { message = "Không tìm thấy tài khoản người dùng" });

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        _userRepository.UpdateUser(user);
        await _userRepository.SaveChangesAsync();

        return Ok(new { message = "Đặt lại mật khẩu thành công. Vui lòng đăng nhập lại." });
    }

    /// <summary>
    /// Đăng nhập, trả JWT token
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result == null)
                return Unauthorized(new { message = "Tên đăng nhập hoặc mật khẩu không đúng" });

            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Đăng ký tài khoản mới (chỉ Admin)
    /// </summary>
    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UserDto>> Register(RegisterCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProfile), result);
    }

    /// <summary>
    /// Học viên tự đăng ký tài khoản (Công khai)
    /// </summary>
    [HttpPost("signup")]
    public async Task<ActionResult<UserDto>> SignUp(SignUpDto dto)
    {
        var command = new RegisterCommand(
            dto.Username,
            dto.Password,
            dto.FullName,
            dto.Email,
            dto.Phone,
            "HocVien"
        );
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetProfile), result);
    }

    /// <summary>
    /// Lấy thông tin profile hiện tại
    /// </summary>
    [HttpGet("profile")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetProfile()
    {
        var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
        var user = await _mediator.Send(new GetUserByIdQuery(userId));
        if (user == null)
            return NotFound(new { message = "Không tìm thấy người dùng" });

        return Ok(user);
    }

    /// <summary>
    /// Cập nhật profile hiện tại
    /// </summary>
    [HttpPut("profile")]
    [Authorize]
    public async Task<ActionResult<UserDto>> UpdateProfile(UpdateProfileDto dto)
    {
        var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
        var user = await _mediator.Send(new GetUserByIdQuery(userId));
        if (user == null)
            return NotFound(new { message = "Không tìm thấy người dùng" });

        var command = new UpdateUserCommand(
            userId,
            dto.FullName,
            dto.Email,
            dto.Phone,
            user.Role
        );

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Đổi mật khẩu
    /// </summary>
    [HttpPut("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
    {
        var userId = int.Parse(User.FindFirst("userId")?.Value ?? "0");
        command = command with { Id = userId };
        
        var success = await _mediator.Send(command);
        if (!success)
            return NotFound(new { message = "Không tìm thấy người dùng" });

        return Ok(new { message = "Đổi mật khẩu thành công" });
    }
}
