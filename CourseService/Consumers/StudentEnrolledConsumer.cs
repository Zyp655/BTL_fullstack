using System.Threading.Tasks;
using Contracts;
using CourseService.Features.Classes.Commands;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseService.Consumers;

public class StudentEnrolledConsumer : IConsumer<StudentEnrolledEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<StudentEnrolledConsumer> _logger;

    public StudentEnrolledConsumer(IMediator mediator, ILogger<StudentEnrolledConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<StudentEnrolledEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Nhận sự kiện học viên ghi danh. ClassId: {ClassId}, StudentId: {StudentId}", message.ClassId, message.StudentId);

        try
        {
            // Tăng sĩ số lớp học lên 1
            await _mediator.Send(new UpdateClassStudentCountCommand(message.ClassId, 1));
            _logger.LogInformation("Cập nhật sĩ số lớp học thành công cho ClassId: {ClassId}", message.ClassId);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Lỗi khi cập nhật sĩ số lớp học cho ClassId: {ClassId}", message.ClassId);
        }
    }
}
