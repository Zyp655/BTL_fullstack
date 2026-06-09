using MediatR;
using StudentService.DTOs;
using StudentService.Repositories;

namespace StudentService.Features.ExamResults.Commands;

public class UpdateExamResultCommandHandler : IRequestHandler<UpdateExamResultCommand, ExamResultDto?>
{
    private readonly IResultRepository _resultRepository;

    public UpdateExamResultCommandHandler(IResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }

    public async Task<ExamResultDto?> Handle(UpdateExamResultCommand request, CancellationToken cancellationToken)
    {
        var result = await _resultRepository.GetResultByIdAsync(request.Id);
        if (result == null) return null;

        result.Score = request.Score;
        result.Note = request.Note;

        await _resultRepository.SaveChangesAsync();

        return ExamResultMapper.MapToDto(result);
    }
}
