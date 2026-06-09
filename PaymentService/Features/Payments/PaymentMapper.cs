using PaymentService.DTOs;
using PaymentService.Models;

namespace PaymentService.Features.Payments;

public static class PaymentMapper
{
    public static PaymentDto MapToDto(Payment p) => new()
    {
        PaymentId = p.PaymentId,
        StudentUserId = p.StudentUserId,
        StudentName = p.StudentUser?.FullName,
        ClassId = p.ClassId,
        TotalAmount = p.TotalAmount,
        PaidAmount = p.PaidAmount,
        RemainingAmount = p.RemainingAmount,
        Status = p.Status,
        DueDate = p.DueDate,
        CreatedAt = p.CreatedAt,
        UpdatedAt = p.UpdatedAt,
        Transactions = p.Transactions?.Select(t => new TransactionDto
        {
            TransactionId = t.TransactionId,
            PaymentId = t.PaymentId,
            Amount = t.Amount,
            PaymentMethod = t.PaymentMethod,
            Note = t.Note,
            ReceivedByUserId = t.ReceivedByUserId,
            PaidAt = t.PaidAt
        }).OrderByDescending(t => t.PaidAt).ToList() ?? new()
    };
}
