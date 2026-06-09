using System.Threading.Tasks;
using Contracts;
using CourseService.Features.Classes.Commands;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseService.Consumers;

public class ClassCreatedFromQueueConsumer : IConsumer<ClassCreatedFromQueueEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<ClassCreatedFromQueueConsumer> _logger;

    public ClassCreatedFromQueueConsumer(IMediator mediator, ILogger<ClassCreatedFromQueueConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ClassCreatedFromQueueEvent> context)
    {
        var message = context.Message;
        int count = message.StudentIds?.Count ?? 0;
        _logger.LogInformation("Nhận sự kiện mở lớp từ hàng chờ. ClassId: {ClassId}, Số học viên: {Count}", message.ClassId, count);

        if (count == 0) return;

        try
        {
            await _mediator.Send(new UpdateClassStudentCountCommand(message.ClassId, count));
            _logger.LogInformation("Cập nhật sĩ số lớp học thành công cho ClassId: {ClassId} (+{Count})", message.ClassId, count);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Lỗi khi cập nhật sĩ số lớp học cho ClassId: {ClassId}", message.ClassId);
        }
    }
}
