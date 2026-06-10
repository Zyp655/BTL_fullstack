using System;
using System.Threading.Tasks;
using MassTransit;
using Contracts;
using StudentService.Data;
using StudentService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StudentService.Consumers;

public class StudentCreditCreatedConsumer : IConsumer<StudentCreditCreatedEvent>
{
    private readonly StudentDbContext _context;
    private readonly ILogger<StudentCreditCreatedConsumer> _logger;

    public StudentCreditCreatedConsumer(StudentDbContext context, ILogger<StudentCreditCreatedConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<StudentCreditCreatedEvent> context)
    {
        var evt = context.Message;
        _logger.LogInformation("Processing StudentCreditCreatedEvent for StudentUserId: {StudentUserId}, Amount: {Amount}", evt.StudentUserId, evt.Amount);

        try
        {
            // Find Student by UserId
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == evt.StudentUserId);

            if (student == null)
            {
                _logger.LogWarning("Student not found for UserId: {StudentUserId}. Cannot create credit balance.", evt.StudentUserId);
                return;
            }

            // Create credit balance record (Ví bảo lưu)
            var credit = new StudentCredit
            {
                StudentId = student.StudentId,
                Amount = evt.Amount,
                SourceClassId = evt.SourceClassId,
                Status = "Available",
                CreatedAt = DateTime.UtcNow
            };

            _context.StudentCredits.Add(credit);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Successfully created credit balance ID: {CreditId} of {Amount} for StudentId: {StudentId} (UserId: {UserId}) from ClassId: {ClassId}", 
                credit.CreditId, evt.Amount, student.StudentId, evt.StudentUserId, evt.SourceClassId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing StudentCreditCreatedEvent for StudentUserId: {StudentUserId}", evt.StudentUserId);
            throw;
        }
    }
}
