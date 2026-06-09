using MediatR;
using StudentService.DTOs;
using StudentService.Models;
using StudentService.Repositories;

namespace StudentService.Features.ExamResults.Commands;

public class CreateExamResultCommandHandler : IRequestHandler<CreateExamResultCommand, ExamResultDto>
{
    private readonly IResultRepository _resultRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;

    public CreateExamResultCommandHandler(IResultRepository resultRepository, IEnrollmentRepository enrollmentRepository)
    {
        _resultRepository = resultRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<ExamResultDto> Handle(CreateExamResultCommand request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(request.EnrollmentId);
        if (enrollment == null)
            throw new KeyNotFoundException("Không tìm thấy đăng ký");

        var result = new ExamResult
        {
            EnrollmentId = request.EnrollmentId,
            ExamType = request.ExamType,
            Score = request.Score,
            Note = request.Note,
            ExamDate = request.ExamDate ?? DateTime.UtcNow,
            GradedByTeacherId = null,
            CreatedAt = DateTime.UtcNow
        };

        await _resultRepository.AddResultAsync(result);
        await _resultRepository.SaveChangesAsync();

        var resultDto = ExamResultMapper.MapToDto(result);
        resultDto.StudentId = enrollment.StudentId;
        resultDto.StudentName = enrollment.Student?.FullName;
        return resultDto;
    }
}
