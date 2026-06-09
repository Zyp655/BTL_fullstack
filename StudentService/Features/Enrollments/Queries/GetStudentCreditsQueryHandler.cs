using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.DTOs;
using StudentService.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StudentService.Features.Enrollments.Queries;

public class GetStudentCreditsQueryHandler : IRequestHandler<GetStudentCreditsQuery, StudentCreditSummaryDto>
{
    private readonly StudentDbContext _context;
    private readonly ICourseServiceClient _courseServiceClient;

    public GetStudentCreditsQueryHandler(StudentDbContext context, ICourseServiceClient courseServiceClient)
    {
        _context = context;
        _courseServiceClient = courseServiceClient;
    }

    public async Task<StudentCreditSummaryDto> Handle(GetStudentCreditsQuery request, CancellationToken cancellationToken)
    {
        var credits = await _context.StudentCredits
            .Where(c => c.StudentId == request.StudentId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync(cancellationToken);

        var totalAvailable = credits.Where(c => c.Status == "Available").Sum(c => c.Amount);
        var totalUsed = credits.Where(c => c.Status == "Used").Sum(c => c.Amount);
        var totalRefunded = credits.Where(c => c.Status == "Refunded").Sum(c => c.Amount);

        // Fetch class info for each credit item asynchronously
        var creditDtos = await Task.WhenAll(credits.Select(async c =>
        {
            var className = "N/A";
            if (c.SourceClassId > 0)
            {
                var classInfo = await _courseServiceClient.GetClassInfo(c.SourceClassId);
                if (classInfo != null)
                {
                    className = classInfo.ClassName;
                }
            }

            return new StudentCreditDto
            {
                CreditId = c.CreditId,
                StudentId = c.StudentId,
                Amount = c.Amount,
                SourceClassId = c.SourceClassId,
                SourceClassName = className,
                Status = c.Status,
                CreatedAt = c.CreatedAt
            };
        }));

        return new StudentCreditSummaryDto
        {
            TotalAvailable = totalAvailable,
            TotalUsed = totalUsed,
            TotalRefunded = totalRefunded,
            Credits = creditDtos.ToList()
        };
    }
}
