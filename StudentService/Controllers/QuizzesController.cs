using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Models;
using StudentService.Services;
using Asp.Versioning;

namespace StudentService.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class QuizzesController : ControllerBase
{
    private readonly StudentDbContext _context;
    private readonly ICourseServiceClient _courseServiceClient;

    public QuizzesController(StudentDbContext context, ICourseServiceClient courseServiceClient)
    {
        _context = context;
        _courseServiceClient = courseServiceClient;
    }

    [HttpGet("class/{classId}")]
    public async Task<ActionResult> GetQuizzesByClass(int classId)
    {
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        
        int currentStudentId = 0;
        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(classId);
            if (classInfo == null || classInfo.TeacherId != teacherId)
                return Forbid();
        }
        else if (role == "HocVien" && int.TryParse(userIdStr, out int userId))
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
            if (student == null)
                return Forbid();
            currentStudentId = student.StudentId;
            var enrolled = await _context.Enrollments.AnyAsync(e => e.StudentId == student.StudentId && e.ClassId == classId);
            if (!enrolled)
                return Forbid();
        }

        var quizzes = await _context.Quizzes
            .Where(q => q.ClassId == classId)
            .OrderByDescending(q => q.CreatedAt)
            .ToListAsync();

        var quizDtos = new List<object>();

        foreach (var quiz in quizzes)
        {
            QuizSubmission? submission = null;
            if (role == "HocVien" && currentStudentId > 0)
            {
                submission = await _context.QuizSubmissions
                    .FirstOrDefaultAsync(s => s.QuizId == quiz.QuizId && s.StudentId == currentStudentId);
            }

            quizDtos.Add(new
            {
                quiz.QuizId,
                quiz.ClassId,
                quiz.Title,
                quiz.DurationMinutes,
                quiz.QuizType,
                quiz.LessonDate,
                quiz.IsActive,
                quiz.CreatedAt,
                HasSubmitted = submission != null,
                SubmissionScore = submission?.Score,
                IsGraded = submission?.IsGraded ?? false,
                TeacherNote = submission?.TeacherNote
            });
        }

        return Ok(quizDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetQuizById(int id)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Questions)
            .FirstOrDefaultAsync(q => q.QuizId == id);

        if (quiz == null)
            return NotFound();

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(quiz.ClassId);
            if (classInfo == null || classInfo.TeacherId != teacherId)
                return Forbid();
        }
        else if (role == "HocVien" && int.TryParse(userIdStr, out int userId))
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
            if (student == null)
                return Forbid();
            var enrolled = await _context.Enrollments.AnyAsync(e => e.StudentId == student.StudentId && e.ClassId == quiz.ClassId);
            if (!enrolled)
                return Forbid();
        }

        // Return questions without correct answers for students
        var questionList = quiz.Questions.Select(q => new
        {
            q.QuestionId,
            q.QuizId,
            q.QuestionText,
            q.Options,
            CorrectAnswer = role == "HocVien" ? null : q.CorrectAnswer
        }).ToList();

        return Ok(new
        {
            quiz.QuizId,
            quiz.ClassId,
            quiz.Title,
            quiz.DurationMinutes,
            quiz.QuizType,
            quiz.LessonDate,
            quiz.IsActive,
            quiz.CreatedAt,
            Questions = questionList
        });
    }

    [HttpPost]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<ActionResult> CreateQuiz([FromBody] CreateQuizDto dto)
    {
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(dto.ClassId);
            if (classInfo == null || classInfo.TeacherId != teacherId)
                return Forbid();
        }

        var quiz = new Quiz
        {
            ClassId = dto.ClassId,
            Title = dto.Title,
            DurationMinutes = dto.DurationMinutes,
            QuizType = dto.QuizType,
            LessonDate = dto.LessonDate,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        foreach (var q in dto.Questions)
        {
            quiz.Questions.Add(new QuizQuestion
            {
                QuestionText = q.QuestionText,
                Options = q.Options,
                CorrectAnswer = q.CorrectAnswer
            });
        }

        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuizById), new { id = quiz.QuizId }, quiz);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<IActionResult> DeleteQuiz(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null)
            return NotFound();

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(quiz.ClassId);
            if (classInfo == null || classInfo.TeacherId != teacherId)
                return Forbid();
        }

        _context.Quizzes.Remove(quiz);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{id}/submit")]
    [Authorize(Roles = "HocVien")]
    public async Task<IActionResult> SubmitQuizAnswers(int id, [FromBody] SubmitAnswersDto dto)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Questions)
            .FirstOrDefaultAsync(q => q.QuizId == id);

        if (quiz == null)
            return NotFound(new { message = "Không tìm thấy đề thi" });

        if (!quiz.IsActive)
            return BadRequest(new { message = "Bài kiểm tra này đã bị đóng" });

        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdStr, out int userId))
            return Forbid();

        var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
        if (student == null)
            return Forbid();

        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == student.StudentId && e.ClassId == quiz.ClassId);

        if (enrollment == null)
            return Forbid();

        // Check if already submitted
        var existingSubmission = await _context.QuizSubmissions
            .AnyAsync(s => s.QuizId == id && s.StudentId == student.StudentId);

        if (existingSubmission)
            return BadRequest(new { message = "Bạn đã thực hiện bài kiểm tra này rồi." });

        decimal? score = null;
        bool isGraded = false;

        if (quiz.QuizType == "TracNghiem")
        {
            int total = quiz.Questions.Count;
            int correctCount = 0;

            foreach (var q in quiz.Questions)
            {
                if (dto.Answers.TryGetValue(q.QuestionId.ToString(), out string? studentAns))
                {
                    if (studentAns != null && q.CorrectAnswer != null && 
                        studentAns.Trim().Equals(q.CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        correctCount++;
                    }
                }
            }

            score = total > 0 ? Math.Round(((decimal)correctCount / total) * 10, 2) : 0;
            isGraded = true;
        }

        var submission = new QuizSubmission
        {
            QuizId = id,
            EnrollmentId = enrollment.EnrollmentId,
            StudentId = student.StudentId,
            AnswersJson = JsonSerializer.Serialize(dto.Answers),
            Score = score,
            IsGraded = isGraded,
            SubmittedAt = DateTime.UtcNow
        };

        _context.QuizSubmissions.Add(submission);

        // Auto create ExamResult for the student if graded
        if (isGraded && score.HasValue)
        {
            var classInfo = await _courseServiceClient.GetClassInfo(quiz.ClassId);
            var result = new ExamResult
            {
                EnrollmentId = enrollment.EnrollmentId,
                ExamType = "KiemTra",
                Score = score.Value,
                Note = $"Bài kiểm tra trực tuyến: {quiz.Title}",
                GradedByTeacherId = classInfo?.TeacherId,
                ExamDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
            _context.ExamResults.Add(result);
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Nộp bài thành công",
            Score = score,
            IsGraded = isGraded
        });
    }

    [HttpGet("{id}/submissions")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<ActionResult> GetSubmissions(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null)
            return NotFound();

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(quiz.ClassId);
            if (classInfo == null || classInfo.TeacherId != teacherId)
                return Forbid();
        }

        var submissions = await _context.QuizSubmissions
            .Include(s => s.Enrollment)
            .Where(s => s.QuizId == id)
            .OrderByDescending(s => s.SubmittedAt)
            .ToListAsync();

        var studentIds = submissions.Select(s => s.StudentId).ToList();
        var students = await _context.Students
            .Where(s => studentIds.Contains(s.StudentId))
            .ToDictionaryAsync(s => s.StudentId, s => s.FullName);

        var result = submissions.Select(s => new
        {
            s.SubmissionId,
            s.QuizId,
            s.EnrollmentId,
            s.StudentId,
            StudentName = students.ContainsKey(s.StudentId) ? students[s.StudentId] : "Chưa rõ",
            s.AnswersJson,
            Answers = JsonSerializer.Deserialize<Dictionary<string, string>>(s.AnswersJson),
            s.Score,
            s.TeacherNote,
            s.IsGraded,
            s.SubmittedAt
        }).ToList();

        return Ok(result);
    }

    [HttpPost("submissions/{submissionId}/grade")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<IActionResult> GradeSubmission(int submissionId, [FromBody] GradeDto dto)
    {
        var submission = await _context.QuizSubmissions
            .Include(s => s.Quiz)
            .FirstOrDefaultAsync(s => s.SubmissionId == submissionId);

        if (submission == null)
            return NotFound();

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        int? currentTeacherId = null;
        if (int.TryParse(userIdStr, out int parsedTeacherId))
        {
            currentTeacherId = parsedTeacherId;
        }

        if (role == "GiaoVien" && currentTeacherId.HasValue)
        {
            var classInfo = await _courseServiceClient.GetClassInfo(submission.Quiz!.ClassId);
            if (classInfo == null || classInfo.TeacherId != currentTeacherId.Value)
                return Forbid();
        }

        submission.Score = dto.Score;
        submission.TeacherNote = dto.TeacherNote;
        submission.IsGraded = true;

        // Check if there is an existing ExamResult for this submission
        // We look for a KiemTra result with matching enrollment and note referencing this quiz title
        var resultNoteKeyword = $"Bài kiểm tra trực tuyến: {submission.Quiz!.Title}";
        var existingResult = await _context.ExamResults
            .FirstOrDefaultAsync(r => r.EnrollmentId == submission.EnrollmentId && r.ExamType == "KiemTra" && r.Note == resultNoteKeyword);

        if (existingResult != null)
        {
            existingResult.Score = dto.Score;
            existingResult.Note = resultNoteKeyword;
            existingResult.GradedByTeacherId = currentTeacherId;
            existingResult.ExamDate = DateTime.UtcNow;
        }
        else
        {
            var result = new ExamResult
            {
                EnrollmentId = submission.EnrollmentId,
                ExamType = "KiemTra",
                Score = dto.Score,
                Note = resultNoteKeyword,
                GradedByTeacherId = currentTeacherId,
                ExamDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
            _context.ExamResults.Add(result);
        }

        await _context.SaveChangesAsync();

        return Ok(new { message = "Chấm điểm thành công" });
    }

    [HttpPost("generate-ai")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<ActionResult<List<GeneratedQuestionDto>>> GenerateAiQuiz([FromBody] GenerateAiQuizRequestDto dto)
    {
        string contentToUse = dto.CustomTopic ?? string.Empty;

        // Nếu giáo viên chọn sinh đề theo bài học, ta lấy nội dung bài học từ DB
        if (dto.LessonDate.HasValue)
        {
            var dateOnly = dto.LessonDate.Value.Date;
            var lesson = await _context.Lessons
                .FirstOrDefaultAsync(l => l.ClassId == dto.ClassId && l.LessonDate.Year == dateOnly.Year && l.LessonDate.Month == dateOnly.Month && l.LessonDate.Day == dateOnly.Day);
            
            if (lesson != null)
            {
                contentToUse = $"Bài học: {lesson.Title}\nNội dung:\n{lesson.Content}\n\n" + contentToUse;
            }
            else if (string.IsNullOrEmpty(contentToUse))
            {
                return BadRequest(new { message = "Không tìm thấy nội dung bài học cho ngày đã chọn để sinh câu hỏi bằng AI." });
            }
        }

        if (string.IsNullOrWhiteSpace(contentToUse))
        {
            return BadRequest(new { message = "Vui lòng cung cấp chủ đề hoặc chọn bài học để sinh đề." });
        }

        try
        {
            var aiClient = HttpContext.RequestServices.GetRequiredService<IAiServiceClient>();
            var questions = await aiClient.GenerateQuestionsAsync(contentToUse, dto.QuizType, dto.QuestionCount);
            return Ok(questions);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Lỗi sinh câu hỏi bằng AI: {ex.Message}" });
        }
    }

    [HttpGet("{id}/statistics")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<ActionResult> GetQuizStatistics(int id)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Questions)
            .FirstOrDefaultAsync(q => q.QuizId == id);

        if (quiz == null)
            return NotFound(new { message = "Không tìm thấy bài kiểm tra" });

        var submissions = await _context.QuizSubmissions
            .Where(s => s.QuizId == id)
            .ToListAsync();

        var submissionCount = submissions.Count;
        if (submissionCount == 0)
        {
            return Ok(new
            {
                quizId = id,
                title = quiz.Title,
                quizType = quiz.QuizType,
                submissionCount = 0,
                averageScore = 0,
                maxScore = 0,
                minScore = 0,
                questionStats = quiz.Questions.Select(q => new
                {
                    q.QuestionId,
                    q.QuestionText,
                    q.Options,
                    q.CorrectAnswer,
                    correctCount = 0,
                    incorrectCount = 0,
                    attemptCount = 0,
                    successRate = 0
                }).ToList()
            });
        }

        var gradedSubmissions = submissions.Where(s => s.Score.HasValue).ToList();
        decimal averageScore = gradedSubmissions.Any() ? gradedSubmissions.Average(s => s.Score!.Value) : 0;
        decimal maxScore = gradedSubmissions.Any() ? gradedSubmissions.Max(s => s.Score!.Value) : 0;
        decimal minScore = gradedSubmissions.Any() ? gradedSubmissions.Min(s => s.Score!.Value) : 0;

        var questionStatsList = new List<object>();

        foreach (var q in quiz.Questions)
        {
            int correctCount = 0;
            int incorrectCount = 0;
            int attemptCount = 0;

            foreach (var sub in submissions)
            {
                try
                {
                    var answers = JsonSerializer.Deserialize<Dictionary<string, string>>(sub.AnswersJson);
                    if (answers != null && answers.TryGetValue(q.QuestionId.ToString(), out string? studentAns) && !string.IsNullOrWhiteSpace(studentAns))
                    {
                        attemptCount++;
                        if (quiz.QuizType == "TracNghiem")
                        {
                            if (q.CorrectAnswer != null && studentAns.Trim().Equals(q.CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
                            {
                                correctCount++;
                            }
                            else
                            {
                                incorrectCount++;
                            }
                        }
                    }
                }
                catch
                {
                    // Ignore parsing errors for individual submissions
                }
            }

            double successRate = attemptCount > 0 ? Math.Round(((double)correctCount / attemptCount) * 100, 2) : 0;

            questionStatsList.Add(new
            {
                q.QuestionId,
                q.QuestionText,
                q.Options,
                q.CorrectAnswer,
                correctCount,
                incorrectCount,
                attemptCount,
                successRate
            });
        }

        return Ok(new
        {
            quizId = id,
            title = quiz.Title,
            quizType = quiz.QuizType,
            submissionCount,
            averageScore = Math.Round(averageScore, 2),
            maxScore = Math.Round(maxScore, 2),
            minScore = Math.Round(minScore, 2),
            questionStats = questionStatsList
        });
    }

    [HttpPost("{id}/ai-summary")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<ActionResult> GetQuizAiSummary(int id)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Questions)
            .FirstOrDefaultAsync(q => q.QuizId == id);

        if (quiz == null)
            return NotFound(new { message = "Không tìm thấy bài kiểm tra" });

        var submissions = await _context.QuizSubmissions
            .Where(s => s.QuizId == id)
            .ToListAsync();

        if (submissions.Count == 0)
        {
            return BadRequest(new { message = "Chưa có lượt nộp bài nào để thực hiện phân tích bằng AI." });
        }

        var gradedSubmissions = submissions.Where(s => s.Score.HasValue).ToList();
        decimal averageScore = gradedSubmissions.Any() ? gradedSubmissions.Average(s => s.Score!.Value) : 0;
        decimal maxScore = gradedSubmissions.Any() ? gradedSubmissions.Max(s => s.Score!.Value) : 0;
        decimal minScore = gradedSubmissions.Any() ? gradedSubmissions.Min(s => s.Score!.Value) : 0;

        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Bài kiểm tra: {quiz.Title}");
        sb.AppendLine($"Loại bài kiểm tra: {quiz.QuizType}");
        sb.AppendLine($"Tổng số học sinh đã nộp bài: {submissions.Count}");
        sb.AppendLine($"Điểm trung bình: {Math.Round(averageScore, 2)}");
        sb.AppendLine($"Điểm cao nhất: {Math.Round(maxScore, 2)}");
        sb.AppendLine($"Điểm thấp nhất: {Math.Round(minScore, 2)}");
        sb.AppendLine();
        sb.AppendLine("Thống kê chi tiết từng câu hỏi:");

        int count = 1;
        foreach (var q in quiz.Questions)
        {
            int correctCount = 0;
            int attemptCount = 0;

            foreach (var sub in submissions)
            {
                try
                {
                    var answers = JsonSerializer.Deserialize<Dictionary<string, string>>(sub.AnswersJson);
                    if (answers != null && answers.TryGetValue(q.QuestionId.ToString(), out string? studentAns) && !string.IsNullOrWhiteSpace(studentAns))
                    {
                        attemptCount++;
                        if (quiz.QuizType == "TracNghiem" && q.CorrectAnswer != null && studentAns.Trim().Equals(q.CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            correctCount++;
                        }
                    }
                }
                catch {}
            }

            double successRate = attemptCount > 0 ? Math.Round(((double)correctCount / attemptCount) * 100, 2) : 0;
            sb.AppendLine($"Câu {count}: {q.QuestionText}");
            if (quiz.QuizType == "TracNghiem")
            {
                sb.AppendLine($"- Phương án lựa chọn: {q.Options}");
                sb.AppendLine($"- Đáp án đúng: {q.CorrectAnswer}");
                sb.AppendLine($"- Tỷ lệ trả lời đúng: {successRate}% ({correctCount}/{attemptCount} học sinh)");
            }
            else
            {
                sb.AppendLine($"- Số học sinh đã trả lời: {attemptCount}/{submissions.Count}");
            }
            sb.AppendLine();
            count++;
        }

        try
        {
            var aiClient = HttpContext.RequestServices.GetRequiredService<IAiServiceClient>();
            var summary = await aiClient.SummarizeQuizResultsAsync(sb.ToString());
            return Ok(new { summary });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Lỗi sinh phân tích AI: {ex.Message}" });
        }
    }

    [HttpPost("{id}/questions")]
    [Authorize(Roles = "HocVien")]
    public async Task<ActionResult> CreateStudentQuestion(int id, [FromBody] CreateStudentQuestionDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.QuestionText))
        {
            return BadRequest(new { message = "Nội dung câu hỏi không được trống" });
        }

        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null)
            return NotFound(new { message = "Không tìm thấy bài kiểm tra" });

        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdStr, out int userId))
            return Forbid();

        var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
        if (student == null)
            return Forbid();

        var enrolled = await _context.Enrollments.AnyAsync(e => e.StudentId == student.StudentId && e.ClassId == quiz.ClassId);
        if (!enrolled)
            return Forbid();

        var quizQuestion = new QuizStudentQuestion
        {
            QuizId = id,
            StudentId = student.StudentId,
            QuestionText = dto.QuestionText,
            CreatedAt = DateTime.UtcNow
        };

        _context.QuizStudentQuestions.Add(quizQuestion);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Gửi thắc mắc thành công", question = quizQuestion });
    }

    [HttpGet("{id}/questions")]
    public async Task<ActionResult> GetStudentQuestions(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null)
            return NotFound(new { message = "Không tìm thấy bài kiểm tra" });

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(quiz.ClassId);
            if (classInfo == null || classInfo.TeacherId != teacherId)
                return Forbid();
        }
        else if (role == "HocVien" && int.TryParse(userIdStr, out int userId))
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
            if (student == null)
                return Forbid();
            var enrolled = await _context.Enrollments.AnyAsync(e => e.StudentId == student.StudentId && e.ClassId == quiz.ClassId);
            if (!enrolled)
                return Forbid();
        }

        var questions = await _context.QuizStudentQuestions
            .Include(q => q.Student)
            .Where(q => q.QuizId == id)
            .OrderByDescending(q => q.CreatedAt)
            .Select(q => new
            {
                q.Id,
                q.QuizId,
                q.StudentId,
                StudentName = q.Student != null ? q.Student.FullName : "Chưa rõ",
                q.QuestionText,
                q.CreatedAt
            })
            .ToListAsync();

        return Ok(questions);
    }
}

public class GenerateAiQuizRequestDto
{
    public int ClassId { get; set; }
    public DateTime? LessonDate { get; set; }
    public string? CustomTopic { get; set; }
    public int QuestionCount { get; set; }
    public string QuizType { get; set; } = "TracNghiem"; // TracNghiem hoặc TuLuan
}

public class CreateQuizDto
{
    public int ClassId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public string QuizType { get; set; } = "TracNghiem";
    public DateTime? LessonDate { get; set; }
    public List<CreateQuestionDto> Questions { get; set; } = new();
}

public class CreateQuestionDto
{
    public string QuestionText { get; set; } = string.Empty;
    public string? Options { get; set; }
    public string? CorrectAnswer { get; set; }
}

public class SubmitAnswersDto
{
    public Dictionary<string, string> Answers { get; set; } = new();
}

public class GradeDto
{
    public decimal Score { get; set; }
    public string? TeacherNote { get; set; }
}

public class CreateStudentQuestionDto
{
    public string QuestionText { get; set; } = string.Empty;
}
