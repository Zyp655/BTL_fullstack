using PaymentService.DTOs;
using PaymentService.Models;

namespace PaymentService.Features.Users;

public static class UserMapper
{
    public static UserDto MapToDto(User u) => new()
    {
        UserId = u.UserId,
        Username = u.Username,
        FullName = u.FullName,
        Email = u.Email,
        Phone = u.Phone,
        Role = u.Role,
        Specialization = u.Specialization,
        Degree = u.Degree,
        BankAccount = u.BankAccount,
        IsActive = u.IsActive,
        CreatedAt = u.CreatedAt,
        UpdatedAt = u.UpdatedAt
    };
}
