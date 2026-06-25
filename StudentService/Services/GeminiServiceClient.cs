using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace StudentService.Services;

public class GeminiServiceClient : IAiServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly ILogger<GeminiServiceClient> _logger;

    // Try multiple models in order - if one hits rate limit, try next
    private static readonly string[] FallbackModels = new[]
    {
        "gemini-3.1-flash-lite",
        "gemini-flash-lite-latest",
        "gemini-2.5-flash-lite",
        "gemini-2.0-flash-lite",
        "gemini-2.0-flash"
    };

    public GeminiServiceClient(HttpClient httpClient, IConfiguration configuration, ILogger<GeminiServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        // Fallback to environment variable if appsettings is empty
        _apiKey = configuration["Gemini:ApiKey"] ?? string.Empty;
        if (string.IsNullOrEmpty(_apiKey))
        {
            _apiKey = System.Environment.GetEnvironmentVariable("GEMINI_API_KEY") ?? string.Empty;
        }
    }

    public async Task<List<GeneratedQuestionDto>> GenerateQuestionsAsync(string content, string quizType, int questionCount)
    {
        if (string.IsNullOrEmpty(_apiKey))
        {
            throw new System.InvalidOperationException("Chưa cấu hình Gemini API Key. Vui lòng thiết lập key trong appsettings.json hoặc biến môi trường GEMINI_API_KEY.");
        }

        // Build the JSON output format instructions directly in the prompt
        var jsonExample = quizType == "TracNghiem"
            ? "[{\"questionText\": \"Câu hỏi?\", \"options\": \"Đáp án A|Đáp án B|Đáp án C|Đáp án D\", \"correctAnswer\": \"A\"}]"
            : "[{\"questionText\": \"Câu hỏi?\", \"options\": null, \"correctAnswer\": null}]";

        var prompt = $"Hãy tạo {questionCount} câu hỏi kiểm tra loại '{quizType}' (TracNghiem hoặc TuLuan) dựa trên nội dung sau bằng tiếng Việt:\n\n{content}\n\n" +
                     "Lưu ý quan trọng: Nếu nội dung được cung cấp là quá ngắn, vô nghĩa, hoặc không thể dùng để tạo câu hỏi kiểm tra, hãy tự động chọn một chủ đề kiến thức phổ thông ngẫu nhiên về khoa học máy tính, lập trình Web, công nghệ thông tin hoặc lập trình OOP để sinh câu hỏi đầy đủ nội dung, chất lượng cao.\n\n";
        
        if (quizType == "TracNghiem")
        {
            prompt += "Mỗi câu hỏi trắc nghiệm phải có đúng 4 phương án lựa chọn, được nối với nhau bằng dấu gạch đứng '|' (Ví dụ: 'Đáp án A|Đáp án B|Đáp án C|Đáp án D'). correctAnswer phải là một ký tự duy nhất 'A', 'B', 'C', hoặc 'D'. Cấm tuyệt đối không để trống trường options và correctAnswer.";
        }
        else
        {
            prompt += "Đối với câu hỏi tự luận (TuLuan), trường options và correctAnswer hãy để giá trị null.";
        }

        prompt += $"\n\nTRẢ LỜI CHỈ BẰNG JSON THUẦN TÚY, KHÔNG CÓ MARKDOWN, KHÔNG CÓ ```json, KHÔNG CÓ GIẢI THÍCH. Chỉ trả về mảng JSON theo đúng format sau:\n{jsonExample}";

        // Build request body - support both text and base64 file content
        object requestBody;
        
        var dataUrlStartIndex = content.IndexOf("data:");
        if (dataUrlStartIndex >= 0)
        {
            var base64SeparatorIndex = content.IndexOf(",", dataUrlStartIndex);
            if (base64SeparatorIndex > dataUrlStartIndex)
            {
                var prefix = content.Substring(dataUrlStartIndex, base64SeparatorIndex - dataUrlStartIndex);
                var mimeType = "application/pdf";
                var mimeTypeMatch = System.Text.RegularExpressions.Regex.Match(prefix, @"data:([^;]+);base64");
                if (mimeTypeMatch.Success)
                {
                    mimeType = mimeTypeMatch.Groups[1].Value;
                }
                
                var base64Data = content.Substring(base64SeparatorIndex + 1).Trim();
                var cleanContent = content.Substring(0, dataUrlStartIndex).Trim();
                
                var filePrompt = $"Hãy tạo {questionCount} câu hỏi kiểm tra loại '{quizType}' (TracNghiem hoặc TuLuan) dựa trên tài liệu đính kèm bằng tiếng Việt.";
                if (!string.IsNullOrWhiteSpace(cleanContent))
                {
                    filePrompt += $"\nThông tin bổ sung: {cleanContent}";
                }
                filePrompt += "\n\nLưu ý quan trọng: Nếu nội dung được cung cấp là quá ngắn, vô nghĩa, hoặc không thể dùng để tạo câu hỏi kiểm tra, hãy tự động chọn một chủ đề kiến thức phổ thông ngẫu nhiên về khoa học máy tính, lập trình Web, công nghệ thông tin hoặc lập trình OOP để sinh câu hỏi đầy đủ nội dung, chất lượng cao.\n\n";

                if (quizType == "TracNghiem")
                {
                    filePrompt += "Mỗi câu hỏi trắc nghiệm phải có đúng 4 phương án lựa chọn, được nối với nhau bằng dấu gạch đứng '|' (Ví dụ: 'Đáp án A|Đáp án B|Đáp án C|Đáp án D'). correctAnswer phải là một ký tự duy nhất 'A', 'B', 'C', hoặc 'D'. Cấm tuyệt đối không để trống trường options và correctAnswer.";
                }
                else
                {
                    filePrompt += "Đối với câu hỏi tự luận (TuLuan), trường options và correctAnswer hãy để giá trị null.";
                }
                filePrompt += $"\n\nTRẢ LỜI CHỈ BẰNG JSON THUẦN TÚY, KHÔNG CÓ MARKDOWN, KHÔNG CÓ ```json, KHÔNG CÓ GIẢI THÍCH. Chỉ trả về mảng JSON theo đúng format sau:\n{jsonExample}";

                requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new object[]
                            {
                                new { inlineData = new { mimeType = mimeType, data = base64Data } },
                                new { text = filePrompt }
                            }
                        }
                    }
                };
            }
            else
            {
                requestBody = new
                {
                    contents = new[] { new { parts = new[] { new { text = prompt } } } }
                };
            }
        }
        else
        {
            requestBody = new
            {
                contents = new[] { new { parts = new[] { new { text = prompt } } } }
            };
        }

        var json = JsonSerializer.Serialize(requestBody);

        // Try each model with retry logic
        string? lastError = null;
        foreach (var model in FallbackModels)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={_apiKey}";
            _logger.LogInformation("Đang thử model {Model} để tạo bài kiểm tra. Số câu: {QuestionCount}, Loại: {QuizType}", model, questionCount, quizType);

            // Try up to 2 times per model (with delay on retry)
            for (int attempt = 1; attempt <= 2; attempt++)
            {
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("Gemini API ({Model}) phản hồi thành công.", model);
                    
                    using var doc = JsonDocument.Parse(responseString);
                    
                    var textResult = doc.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();

                    if (string.IsNullOrEmpty(textResult))
                    {
                        _logger.LogWarning("Gemini API ({Model}) không trả về nội dung text.", model);
                        return new List<GeneratedQuestionDto>();
                    }

                    // Clean up markdown code fences if present
                    textResult = textResult.Trim();
                    if (textResult.StartsWith("```json"))
                        textResult = textResult.Substring(7);
                    else if (textResult.StartsWith("```"))
                        textResult = textResult.Substring(3);
                    if (textResult.EndsWith("```"))
                        textResult = textResult.Substring(0, textResult.Length - 3);
                    textResult = textResult.Trim();

                    try
                    {
                        var questions = JsonSerializer.Deserialize<List<GeneratedQuestionDto>>(textResult, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        return questions ?? new List<GeneratedQuestionDto>();
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError(ex, "Lỗi phân tích JSON từ kết quả Gemini ({Model}): {RawText}", model, textResult);
                        throw;
                    }
                }

                var errContent = await response.Content.ReadAsStringAsync();
                lastError = errContent;

                if ((int)response.StatusCode == 429)
                {
                    _logger.LogWarning("Model {Model} bị rate limit (lần thử {Attempt}/2). Chuyển sang model khác...", model, attempt);
                    
                    if (attempt == 1)
                    {
                        // Wait a bit before retry on same model
                        await Task.Delay(2000);
                        continue;
                    }
                    // Move to next model
                    break;
                }
                else
                {
                    _logger.LogError("Gemini API ({Model}) trả về lỗi: {StatusCode} - {Error}", model, response.StatusCode, errContent);
                    // For non-rate-limit errors, try next model
                    break;
                }
            }
        }

        // All models exhausted
        throw new HttpRequestException($"Tất cả các model Gemini đều bị lỗi. Lỗi cuối cùng: {lastError}");
    }

    public async Task<string> SummarizeQuizResultsAsync(string statisticsData)
    {
        if (string.IsNullOrEmpty(_apiKey))
        {
            throw new System.InvalidOperationException("Chưa cấu hình Gemini API Key. Vui lòng thiết lập key trong appsettings.json hoặc biến môi trường GEMINI_API_KEY.");
        }

        var prompt = $"Dưới đây là thống kê kết quả làm bài kiểm tra của một lớp học:\n\n{statisticsData}\n\n" +
                     "Hãy đóng vai trò là một trợ lý giảng dạy AI xuất sắc. Hãy phân tích dữ liệu thống kê trên (tập trung vào điểm số trung bình, tỷ lệ câu trả lời đúng/sai của từng câu hỏi) và viết một bản tóm tắt ngắn gọn bằng tiếng Việt:\n" +
                     "1. Đánh giá chung về tình hình học tập và mức độ hiểu bài của học sinh (Điểm trung bình, tỷ lệ làm bài,...).\n" +
                     "2. Chỉ ra các câu hỏi/kiến thức mà nhiều học sinh trả lời sai nhất (các câu hỏi yếu cần giải thích lại).\n" +
                     "3. Đưa ra đề xuất hoặc gợi ý cho giảng viên về những nội dung cần giảng giải lại trên lớp để cải thiện hiệu quả học tập.\n\n" +
                     "Hãy viết ngắn gọn, súc tích dưới dạng markdown, không bao gồm lời mở đầu hay kết luận thừa thãi.";

        var requestBody = new
        {
            contents = new[]
            {
                new { parts = new[] { new { text = prompt } } }
            }
        };

        var json = JsonSerializer.Serialize(requestBody);

        // Try each model with retry logic
        string? lastError = null;
        foreach (var model in FallbackModels)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={_apiKey}";
            _logger.LogInformation("Đang thử model {Model} để tóm tắt kết quả bài kiểm tra.", model);

            for (int attempt = 1; attempt <= 2; attempt++)
            {
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(responseString);

                    var textResult = doc.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();

                    return textResult ?? "Không thể tạo tóm tắt.";
                }

                var errContent = await response.Content.ReadAsStringAsync();
                lastError = errContent;

                if ((int)response.StatusCode == 429)
                {
                    _logger.LogWarning("Model {Model} bị rate limit khi tóm tắt (lần thử {Attempt}/2). Chuyển sang model khác...", model, attempt);
                    if (attempt == 1)
                    {
                        await Task.Delay(2000);
                        continue;
                    }
                    break;
                }
                else
                {
                    _logger.LogError("Gemini API ({Model}) trả về lỗi khi tóm tắt: {StatusCode} - {Error}", model, response.StatusCode, errContent);
                    break;
                }
            }
        }

        throw new HttpRequestException($"Tất cả các model Gemini đều bị lỗi. Lỗi cuối cùng: {lastError}");
    }
}

