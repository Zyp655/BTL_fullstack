using System.Threading.Tasks;
using System.Collections.Generic;

namespace StudentService.Services;

public interface IAiServiceClient
{
    Task<List<GeneratedQuestionDto>> GenerateQuestionsAsync(string content, string quizType, int questionCount);
    Task<string> SummarizeQuizResultsAsync(string statisticsData);
    Task<CodingChallengeDto> GenerateCodingChallengeAsync(string topic, string language);
    Task<CodingGradeDto> GradeCodingChallengeAsync(string problemDescription, string solutionCode, string language);
}

public class GeneratedQuestionDto
{
    public string QuestionText { get; set; } = string.Empty;
    public string? Options { get; set; }
    public string? CorrectAnswer { get; set; }
}

public class CodingChallengeDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StarterCode { get; set; } = string.Empty;
    public string ExpectedExplanation { get; set; } = string.Empty;
}

public class CodingGradeDto
{
    public decimal Score { get; set; }
    public string Feedback { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
