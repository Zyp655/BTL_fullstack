using System.Threading.Tasks;
using System.Collections.Generic;

namespace StudentService.Services;

public interface IAiServiceClient
{
    Task<List<GeneratedQuestionDto>> GenerateQuestionsAsync(string content, string quizType, int questionCount);
    Task<string> SummarizeQuizResultsAsync(string statisticsData);
}

public class GeneratedQuestionDto
{
    public string QuestionText { get; set; } = string.Empty;
    public string? Options { get; set; }
    public string? CorrectAnswer { get; set; }
}
