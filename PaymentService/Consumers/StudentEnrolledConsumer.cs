using System;
using System.Threading.Tasks;
using MassTransit;
using Contracts;
using PaymentService.Services;
using PaymentService.DTOs;
using PaymentService.Features.Payments.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PaymentService.Consumers;

public class StudentEnrolledConsumer : IConsumer<StudentEnrolledEvent>
{
    private readonly IMediator _mediator;
    private readonly ICourseServiceClient _courseServiceClient;
    private readonly ILogger<StudentEnrolledConsumer> _logger;

    public StudentEnrolledConsumer(
        IMediator mediator,
        ICourseServiceClient courseServiceClient,
        ILogger<StudentEnrolledConsumer> logger)
    {
        _mediator = mediator;
        _courseServiceClient = courseServiceClient;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<StudentEnrolledEvent> context)
    {
        var evt = context.Message;
        _logger.LogInformation("Processing StudentEnrolledEvent for StudentId: {StudentId}, ClassId: {ClassId}", evt.StudentId, evt.ClassId);

        try
        {
            decimal fee = 0;

            if (evt.ClassId < 0)
            {
                var courseId = -evt.ClassId;
                var courseInfo = await _courseServiceClient.GetCourseInfo(courseId);
                if (courseInfo == null)
                {
                    _logger.LogError("CourseInfo not found for CourseId: {CourseId}", courseId);
                    return;
                }
                fee = courseInfo.Fee;
            }
            else
            {
                // 1. Fetch Class Info
                var classInfo = await _courseServiceClient.GetClassInfo(evt.ClassId);
                if (classInfo == null)
                {
                    _logger.LogError("ClassInfo not found for ClassId: {ClassId}", evt.ClassId);
                    return;
                }

                // 2. Fetch Course Info
                var courseInfo = await _courseServiceClient.GetCourseInfo(classInfo.CourseId);
                if (courseInfo == null)
                {
                    _logger.LogError("CourseInfo not found for CourseId: {CourseId}", classInfo.CourseId);
                    return;
                }
                fee = courseInfo.Fee;
            }

            // 3. Create Payment (Invoice) via MediatR Command
            var command = new CreatePaymentCommand(
                StudentUserId: evt.UserId,
                ClassId: evt.ClassId,
                TotalAmount: fee,
                DueDate: DateTime.UtcNow.AddDays(14) // 14 days payment window
            );

            var payment = await _mediator.Send(command);
            _logger.LogInformation("Successfully created payment invoice ID: {PaymentId} for StudentUserId: {StudentUserId}", payment.PaymentId, evt.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating payment for StudentId: {StudentId}, ClassId: {ClassId}", evt.StudentId, evt.ClassId);
        }
    }
}
