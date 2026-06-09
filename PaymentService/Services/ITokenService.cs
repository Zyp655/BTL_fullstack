using PaymentService.Models;

namespace PaymentService.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
