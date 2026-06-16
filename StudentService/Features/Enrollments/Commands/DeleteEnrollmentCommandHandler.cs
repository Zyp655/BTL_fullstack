using MediatR;
using StudentService.Repositories;
using StudentService.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace StudentService.Features.Enrollments.Commands;

public class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, bool>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly StudentDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public DeleteEnrollmentCommandHandler(
        IEnrollmentRepository enrollmentRepository,
        StudentDbContext context,
        IPublishEndpoint publishEndpoint)
    {
        _enrollmentRepository = enrollmentRepository;
        _context = context;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<bool> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(request.Id);
        if (enrollment == null)
            return false;

        // Check if student attended <= 1 session and has a payment to refund before deleting enrollment
        var student = await _context.Students.FindAsync(new object[] { enrollment.StudentId }, cancellationToken);
        if (student != null)
        {
            int attendedCount = await _context.Attendances
                .CountAsync(a => a.EnrollmentId == enrollment.EnrollmentId && (a.Status == "CoMat" || a.Status == "DiTre"), cancellationToken);

            if (attendedCount <= 1)
            {
                await _publishEndpoint.Publish<Contracts.SingleSessionRefundRequestEvent>(new Contracts.SingleSessionRefundRequestEvent
                {
                    StudentUserId = student.UserId,
                    ClassId = enrollment.ClassId
                }, cancellationToken);
            }
        }

        _enrollmentRepository.DeleteEnrollment(enrollment);
        return await _enrollmentRepository.SaveChangesAsync();
    }
}
