using System.Collections.Concurrent;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PaymentService.Services;

public class OtpService
{
    private readonly ConcurrentDictionary<string, (string Otp, DateTime Expiry)> _otps = new();
    private readonly IConfiguration _configuration;
    private readonly ILogger<OtpService> _logger;

    public OtpService(IConfiguration configuration, ILogger<OtpService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public string GenerateOtp(string email)
    {
        var random = new Random();
        var otp = random.Next(100000, 999999).ToString();
        var expiry = DateTime.UtcNow.AddMinutes(5); // OTP is valid for 5 minutes
        _otps[email.ToLower()] = (otp, expiry);

        var emailBody = $"Mã OTP để đặt lại mật khẩu của bạn là: **{otp}**.\nMã này sẽ hết hạn trong vòng 5 phút.\n\nNếu bạn không yêu cầu hành động này, vui lòng bỏ qua email.";
        
        // Log to console/logger clearly
        _logger.LogInformation("================================================");
        _logger.LogInformation($"[OTP GENERATED FOR {email}]: {otp}");
        _logger.LogInformation("================================================");

        // Attempt to send email via SMTP (if configured in appsettings.json)
        try
        {
            var host = _configuration["Smtp:Host"];
            var portStr = _configuration["Smtp:Port"];
            var username = _configuration["Smtp:Username"];
            var password = _configuration["Smtp:Password"];
            var from = _configuration["Smtp:From"] ?? "noreply@trungtamdaotao.com";

            if (!string.IsNullOrEmpty(host) && int.TryParse(portStr, out int port))
            {
                using var client = new SmtpClient(host, port)
                {
                    Credentials = new NetworkCredential(username, password),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(from),
                    Subject = "Mã OTP đặt lại mật khẩu của bạn",
                    Body = emailBody,
                    IsBodyHtml = false
                };
                mailMessage.To.Add(email);

                client.Send(mailMessage);
                _logger.LogInformation($"Successfully sent OTP email to {email}");
            }
            else
            {
                _logger.LogWarning("SMTP is not configured. Falling back to Console/Logger for OTP.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to send email to {email}. Falling back to Console/Logger.");
        }

        return otp;
    }

    public bool VerifyOtp(string email, string otp)
    {
        var key = email.ToLower();
        if (_otps.TryGetValue(key, out var val))
        {
            if (val.Otp == otp && val.Expiry > DateTime.UtcNow)
            {
                _otps.TryRemove(key, out _);
                return true;
            }
        }
        return false;
    }
}
