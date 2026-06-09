using System.Threading.Tasks;
using Contracts;
using CourseService.Features.Classes.Commands;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseService.Consumers;

public class ResolveCancelledClassConsumer : IConsumer<ResolveCancelledClassEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<ResolveCancelledClassConsumer> _logger;

    public ResolveCancelledClassConsumer(IMediator mediator, ILogger<ResolveCancelledClassConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ResolveCancelledClassEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Nhận sự kiện giải quyết lớp học bị hủy. ClassId: {ClassId}, Số lượng phân bổ: {Count}", message.ClassId, message.Resolutions.Count);

        foreach (var item in message.Resolutions)
        {
            try
            {
                // Giảm sĩ số lớp cũ đi 1
                await _mediator.Send(new UpdateClassStudentCountCommand(message.ClassId, -1));
                _logger.LogInformation("Giảm sĩ số lớp cũ ClassId: {ClassId} thành công", message.ClassId);

                // Nếu là chuyển lớp, tăng sĩ số lớp mới lên 1
                if (item.Action == "ChuyenLop" && item.NewClassId.HasValue)
                {
                    await _mediator.Send(new UpdateClassStudentCountCommand(item.NewClassId.Value, 1));
                    _logger.LogInformation("Tăng sĩ số lớp mới ClassId: {NewClassId} thành công", item.NewClassId.Value);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật sĩ số trong quá trình giải quyết hủy/chuyển lớp");
            }
        }
    }
}
