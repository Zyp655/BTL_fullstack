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
        try
        {
            _context.Database.ExecuteSqlRaw("IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Quizzes') AND name = 'MaxAttempts') ALTER TABLE Quizzes ADD MaxAttempts INT NOT NULL DEFAULT 1;");
            _context.Database.ExecuteSqlRaw("ALTER TABLE QuizSubmissions ALTER COLUMN TeacherNote NVARCHAR(MAX);");
        }
        catch { }
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
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
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
            var studentSubmissions = new List<QuizSubmission>();
            if (role == "HocVien" && currentStudentId > 0)
            {
                studentSubmissions = await _context.QuizSubmissions
                    .Where(s => s.QuizId == quiz.QuizId && s.StudentId == currentStudentId)
                    .OrderByDescending(s => s.SubmittedAt)
                    .ToListAsync();
            }

            var bestSubmission = studentSubmissions
                .OrderByDescending(s => s.Score ?? 0)
                .ThenByDescending(s => s.SubmittedAt)
                .FirstOrDefault();

            quizDtos.Add(new
            {
                quiz.QuizId,
                quiz.ClassId,
                quiz.Title,
                quiz.DurationMinutes,
                quiz.QuizType,
                quiz.MaxAttempts,
                quiz.LessonDate,
                quiz.IsActive,
                quiz.AvailableFrom,
                quiz.AvailableTo,
                quiz.CreatedAt,
                AttemptsCount = studentSubmissions.Count,
                HasSubmitted = studentSubmissions.Count > 0,
                SubmissionScore = bestSubmission?.Score,
                IsGraded = bestSubmission?.IsGraded ?? false,
                TeacherNote = bestSubmission?.TeacherNote
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
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
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

            var attempts = await _context.QuizSubmissions.CountAsync(s => s.QuizId == id && s.StudentId == student.StudentId);
            if (attempts >= quiz.MaxAttempts)
            {
                return BadRequest(new { message = $"Bạn đã đạt giới hạn tối đa số lần làm bài ({quiz.MaxAttempts} lần)." });
            }
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
            quiz.MaxAttempts,
            quiz.LessonDate,
            quiz.IsActive,
            quiz.AvailableFrom,
            quiz.AvailableTo,
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
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
                return Forbid();
        }

        var quiz = new Quiz
        {
            ClassId = dto.ClassId,
            Title = dto.Title,
            DurationMinutes = dto.DurationMinutes,
            QuizType = dto.QuizType,
            MaxAttempts = dto.MaxAttempts > 0 ? dto.MaxAttempts : 1,
            LessonDate = dto.LessonDate,
            IsActive = true,
            AvailableFrom = dto.AvailableFrom,
            AvailableTo = dto.AvailableTo,
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
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
                return Forbid();
        }

        _context.Quizzes.Remove(quiz);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{id}/toggle")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<IActionResult> ToggleQuizActive(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null)
            return NotFound();

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (role == "GiaoVien" && int.TryParse(userIdStr, out int toggleTeacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(quiz.ClassId);
            if (classInfo == null || (classInfo.TeacherId != toggleTeacherId && classInfo.TeacherId2 != toggleTeacherId))
                return Forbid();
        }

        quiz.IsActive = !quiz.IsActive;
        await _context.SaveChangesAsync();

        return Ok(new { quiz.QuizId, quiz.IsActive });
    }

    [HttpPatch("{id}/availability")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<IActionResult> UpdateQuizAvailability(int id, [FromBody] UpdateAvailabilityDto dto)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null)
            return NotFound();

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (role == "GiaoVien" && int.TryParse(userIdStr, out int availTeacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(quiz.ClassId);
            if (classInfo == null || (classInfo.TeacherId != availTeacherId && classInfo.TeacherId2 != availTeacherId))
                return Forbid();
        }

        quiz.AvailableFrom = dto.AvailableFrom;
        quiz.AvailableTo = dto.AvailableTo;
        await _context.SaveChangesAsync();

        return Ok(new { quiz.QuizId, quiz.AvailableFrom, quiz.AvailableTo });
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
            return BadRequest(new { message = "Bài kiểm tra này đã bị khóa" });

        var now = DateTime.UtcNow;
        if (quiz.AvailableFrom.HasValue && now < quiz.AvailableFrom.Value)
            return BadRequest(new { message = $"Bài kiểm tra này chưa mở. Thời gian mở: {quiz.AvailableFrom.Value:dd/MM/yyyy HH:mm}" });
        if (quiz.AvailableTo.HasValue && now > quiz.AvailableTo.Value)
            return BadRequest(new { message = $"Bài kiểm tra này đã hết hạn. Hạn nộp: {quiz.AvailableTo.Value:dd/MM/yyyy HH:mm}" });

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

        // Check if already submitted maximum attempts
        var attemptsCount = await _context.QuizSubmissions
            .CountAsync(s => s.QuizId == id && s.StudentId == student.StudentId);
 
        if (attemptsCount >= quiz.MaxAttempts)
            return BadRequest(new { message = $"Bạn đã đạt giới hạn tối đa số lần làm bài ({quiz.MaxAttempts} lần)." });

        decimal? score = null;
        bool isGraded = false;
        string? feedback = null;

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
        else if (quiz.QuizType == "LapTrinh")
        {
            var singleQuestion = quiz.Questions.FirstOrDefault();
            if (singleQuestion != null)
            {
                dto.Answers.TryGetValue(singleQuestion.QuestionId.ToString(), out string? solutionCode);
                solutionCode ??= string.Empty;
                var language = singleQuestion.CorrectAnswer ?? "JavaScript";
                var problemDesc = singleQuestion.QuestionText;

                var aiClient = HttpContext.RequestServices.GetRequiredService<IAiServiceClient>();
                try
                {
                    var gradeResult = await aiClient.GradeCodingChallengeAsync(problemDesc, solutionCode, language);
                    score = gradeResult.Score;
                    isGraded = true;
                    feedback = gradeResult.Feedback;
                }
                catch (Exception ex)
                {
                    score = 0;
                    isGraded = true;
                    feedback = $"Lỗi chấm bài tự động bằng AI: {ex.Message}";
                }
            }
        }

        var submission = new QuizSubmission
        {
            QuizId = id,
            EnrollmentId = enrollment.EnrollmentId,
            StudentId = student.StudentId,
            AnswersJson = JsonSerializer.Serialize(dto.Answers),
            Score = score,
            IsGraded = isGraded,
            TeacherNote = feedback,
            SubmittedAt = DateTime.UtcNow
        };

        _context.QuizSubmissions.Add(submission);

        // NOTE: ExamResult is NOT auto-created here.
        // The teacher must explicitly save the official score from the grade management UI.

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
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
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
            if (classInfo == null || (classInfo.TeacherId != currentTeacherId.Value && classInfo.TeacherId2 != currentTeacherId.Value))
                return Forbid();
        }

        submission.Score = dto.Score;
        submission.TeacherNote = dto.TeacherNote;
        submission.IsGraded = true;

        // NOTE: ExamResult is NOT created/updated here.
        // The teacher must use "Lưu điểm chính thức" from the submissions management UI.

        await _context.SaveChangesAsync();
 
        return Ok(new { message = "Chấm điểm thành công" });
    }

    [HttpPost("{id}/save-official-score")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<IActionResult> SaveOfficialScore(int id, [FromBody] SaveOfficialScoreDto dto)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null)
            return NotFound(new { message = "Không tìm thấy đề thi" });

        var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == dto.StudentId);
        if (student == null)
            return BadRequest(new { message = "Không tìm thấy học viên" });

        var enrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == student.StudentId && e.ClassId == quiz.ClassId);

        if (enrollment == null)
            return BadRequest(new { message = "Học viên không tham gia lớp học này" });

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        int? teacherId = null;
        if (int.TryParse(userIdStr, out int parsedTeacherId)) teacherId = parsedTeacherId;

        // Step 1: Save/update per-quiz score with unique note per quiz
        var perQuizNote = $"Quiz_{quiz.QuizId}:{quiz.Title}";
        var perQuizResult = await _context.ExamResults
            .FirstOrDefaultAsync(r => r.EnrollmentId == enrollment.EnrollmentId && 
                                     r.ExamType == "KiemTra" && 
                                     r.Note != null && r.Note.StartsWith($"Quiz_{quiz.QuizId}:"));

        if (perQuizResult == null)
        {
            perQuizResult = new ExamResult
            {
                EnrollmentId = enrollment.EnrollmentId,
                ExamType = "KiemTra",
                Score = dto.Score,
                Note = perQuizNote,
                GradedByTeacherId = teacherId,
                ExamDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
            _context.ExamResults.Add(perQuizResult);
        }
        else
        {
            perQuizResult.Score = dto.Score;
            perQuizResult.Note = perQuizNote;
            perQuizResult.GradedByTeacherId = teacherId;
            perQuizResult.ExamDate = DateTime.UtcNow;
            _context.Entry(perQuizResult).State = EntityState.Modified;
        }

        await _context.SaveChangesAsync();

        // Step 2: Recalculate average across all saved quiz scores for this enrollment
        var allQuizResults = await _context.ExamResults
            .Where(r => r.EnrollmentId == enrollment.EnrollmentId && 
                       r.ExamType == "KiemTra" && 
                       r.Note != null && r.Note.StartsWith("Quiz_"))
            .ToListAsync();

        if (allQuizResults.Any())
        {
            var averageScore = Math.Round(allQuizResults.Average(r => r.Score), 1);
            var quizCount = allQuizResults.Count;
            var avgNote = $"Trung bình {quizCount} bài kiểm tra";

            // Find or create the aggregated KiemTra result (note does NOT start with "Quiz_")
            var aggregatedResult = await _context.ExamResults
                .FirstOrDefaultAsync(r => r.EnrollmentId == enrollment.EnrollmentId && 
                                         r.ExamType == "KiemTra" && 
                                         (r.Note == null || !r.Note.StartsWith("Quiz_")) &&
                                         r.Note != null && r.Note.StartsWith("Trung bình"));

            // Also clean up old-style "Bài kiểm tra trực tuyến:" results
            var oldStyleResults = await _context.ExamResults
                .Where(r => r.EnrollmentId == enrollment.EnrollmentId && 
                           r.ExamType == "KiemTra" && 
                           r.Note != null && r.Note.StartsWith("Bài kiểm tra trực tuyến:"))
                .ToListAsync();
            if (oldStyleResults.Any())
            {
                _context.ExamResults.RemoveRange(oldStyleResults);
            }

            if (aggregatedResult == null)
            {
                aggregatedResult = new ExamResult
                {
                    EnrollmentId = enrollment.EnrollmentId,
                    ExamType = "KiemTra",
                    Score = averageScore,
                    Note = avgNote,
                    GradedByTeacherId = teacherId,
                    ExamDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };
                _context.ExamResults.Add(aggregatedResult);
            }
            else
            {
                aggregatedResult.Score = averageScore;
                aggregatedResult.Note = avgNote;
                aggregatedResult.GradedByTeacherId = teacherId;
                aggregatedResult.ExamDate = DateTime.UtcNow;
                _context.Entry(aggregatedResult).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }

        return Ok(new { message = "Lưu điểm chính thức thành công", score = dto.Score });
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
            if (dto.QuizType == "LapTrinh")
            {
                var topic = string.IsNullOrWhiteSpace(dto.CustomTopic) ? "Basic Programming" : dto.CustomTopic;
                var language = "JavaScript";
                if (topic.Contains("Python", StringComparison.OrdinalIgnoreCase)) language = "Python";
                else if (topic.Contains("C#", StringComparison.OrdinalIgnoreCase) || topic.Contains("Csharp", StringComparison.OrdinalIgnoreCase)) language = "C#";

                var challenge = await aiClient.GenerateCodingChallengeAsync(topic, language);
                var questions = new List<GeneratedQuestionDto>
                {
                    new GeneratedQuestionDto
                    {
                        QuestionText = $"# {challenge.Title}\n\n{challenge.Description}",
                        Options = challenge.StarterCode,
                        CorrectAnswer = language
                    }
                };
                return Ok(questions);
            }
            else
            {
                var questions = await aiClient.GenerateQuestionsAsync(contentToUse, dto.QuizType, dto.QuestionCount);
                return Ok(questions);
            }
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
            var optionBreakdown = new Dictionary<string, int>
            {
                { "A", 0 },
                { "B", 0 },
                { "C", 0 },
                { "D", 0 }
            };

            foreach (var sub in submissions)
            {
                try
                {
                    var answers = JsonSerializer.Deserialize<Dictionary<string, string>>(sub.AnswersJson);
                    if (answers != null && answers.TryGetValue(q.QuestionId.ToString(), out string? studentAns) && !string.IsNullOrWhiteSpace(studentAns))
                    {
                        attemptCount++;
                        var cleanAns = studentAns.Trim().ToUpper();
                        if (optionBreakdown.ContainsKey(cleanAns))
                        {
                            optionBreakdown[cleanAns]++;
                        }
                        else
                        {
                            optionBreakdown[cleanAns] = 1;
                        }

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
                successRate,
                optionBreakdown
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

    [HttpGet("student-questions/all")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<ActionResult> GetAllStudentQuestions()
    {
        var questions = await _context.QuizStudentQuestions
            .Include(q => q.Quiz)
            .Include(q => q.Student)
            .OrderByDescending(q => q.CreatedAt)
            .Select(q => new
            {
                q.Id,
                q.QuizId,
                QuizTitle = q.Quiz != null ? q.Quiz.Title : "Bài kiểm tra",
                ClassId = q.Quiz != null ? q.Quiz.ClassId : 0,
                q.StudentId,
                StudentName = q.Student != null ? q.Student.FullName : "Học viên",
                q.QuestionText,
                q.CreatedAt,
                q.AnswerText,
                q.AnsweredAt
            })
            .ToListAsync();

        return Ok(questions);
    }

    [HttpGet("student-questions/my-answered")]
    [Authorize(Roles = "HocVien")]
    public async Task<ActionResult> GetMyAnsweredQuestions()
    {
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdStr, out int userId))
            return Forbid();

        var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
        if (student == null)
            return Forbid();

        var questions = await _context.QuizStudentQuestions
            .Include(q => q.Quiz)
            .Where(q => q.StudentId == student.StudentId && !string.IsNullOrEmpty(q.AnswerText))
            .OrderByDescending(q => q.AnsweredAt)
            .Select(q => new
            {
                q.Id,
                q.QuizId,
                QuizTitle = q.Quiz != null ? q.Quiz.Title : "Bài kiểm tra",
                ClassId = q.Quiz != null ? q.Quiz.ClassId : 0,
                q.QuestionText,
                q.CreatedAt,
                q.AnswerText,
                q.AnsweredAt
            })
            .ToListAsync();

        return Ok(questions);
    }

    [HttpGet("student-questions/my-all")]
    [Authorize(Roles = "HocVien")]
    public async Task<ActionResult> GetMyAllQuestions()
    {
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdStr, out int userId))
            return Forbid();

        var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
        if (student == null)
            return Forbid();

        var questions = await _context.QuizStudentQuestions
            .Include(q => q.Quiz)
            .Where(q => q.StudentId == student.StudentId)
            .OrderByDescending(q => q.CreatedAt)
            .Select(q => new
            {
                q.Id,
                q.QuizId,
                QuizTitle = q.Quiz != null ? q.Quiz.Title : "Bài kiểm tra",
                ClassId = q.Quiz != null ? q.Quiz.ClassId : 0,
                q.QuestionText,
                q.CreatedAt,
                q.AnswerText,
                q.AnsweredAt
            })
            .ToListAsync();

        return Ok(questions);
    }

    [HttpPut("questions/{doubtId}/answer")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<IActionResult> AnswerDoubt(int doubtId, [FromBody] AnswerDoubtDto dto)
    {
        var doubt = await _context.QuizStudentQuestions
            .Include(d => d.Quiz)
            .FirstOrDefaultAsync(d => d.Id == doubtId);

        if (doubt == null)
            return NotFound(new { message = "Không tìm thấy thắc mắc" });

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(doubt.Quiz!.ClassId);
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
                return Forbid();
        }

        doubt.AnswerText = dto.AnswerText;
        doubt.AnsweredAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Trả lời thắc mắc thành công", doubt });
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
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
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
                q.CreatedAt,
                q.AnswerText,
                q.AnsweredAt
            })
            .ToListAsync();

        return Ok(questions);
    }

    [HttpGet("{id}/my-submission")]
    [Authorize(Roles = "HocVien")]
    public async Task<ActionResult> GetMySubmission(int id)
    {
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdStr, out int userId))
            return Forbid();

        var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
        if (student == null)
            return Forbid();

        var submission = await _context.QuizSubmissions
            .Include(s => s.Quiz)
            .ThenInclude(q => q!.Questions)
            .Where(s => s.QuizId == id && s.StudentId == student.StudentId)
            .OrderByDescending(s => s.Score ?? 0)
            .ThenByDescending(s => s.SubmittedAt)
            .FirstOrDefaultAsync();

        if (submission == null)
            return NotFound(new { message = "Bạn chưa nộp bài làm cho bài kiểm tra này." });

        var answers = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(submission.AnswersJson))
        {
            try
            {
                answers = JsonSerializer.Deserialize<Dictionary<string, string>>(submission.AnswersJson) ?? new Dictionary<string, string>();
            }
            catch {}
        }

        var questionsList = submission.Quiz!.Questions.Select(q => new
        {
            q.QuestionId,
            q.QuizId,
            q.QuestionText,
            q.Options,
            q.CorrectAnswer
        }).ToList();

        return Ok(new
        {
            submission.SubmissionId,
            submission.QuizId,
            submission.EnrollmentId,
            submission.StudentId,
            submission.Score,
            submission.TeacherNote,
            submission.IsGraded,
            submission.SubmittedAt,
            answers,
            questions = questionsList
        });
    }

    [HttpPut("questions/{questionId}")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<IActionResult> UpdateQuestion(int questionId, [FromBody] UpdateQuestionDto dto)
    {
        var question = await _context.QuizQuestions
            .Include(q => q.Quiz)
            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

        if (question == null)
            return NotFound(new { message = "Không tìm thấy câu hỏi" });

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(question.Quiz!.ClassId);
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
                return Forbid();
        }

        question.QuestionText = dto.QuestionText;
        question.Options = dto.Options;
        question.CorrectAnswer = dto.CorrectAnswer;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Cập nhật câu hỏi thành công", question });
    }

    [HttpDelete("questions/{questionId}")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<IActionResult> DeleteQuestion(int questionId)
    {
        var question = await _context.QuizQuestions
            .Include(q => q.Quiz)
            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

        if (question == null)
            return NotFound(new { message = "Không tìm thấy câu hỏi" });

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(question.Quiz!.ClassId);
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
                return Forbid();
        }

        _context.QuizQuestions.Remove(question);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Xóa câu hỏi thành công" });
    }

    [HttpPost("{id}/questions-admin")]
    [Authorize(Roles = "Admin,GiaoVien")]
    public async Task<IActionResult> AddQuestion(int id, [FromBody] CreateQuestionDto dto)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz == null)
            return NotFound(new { message = "Không tìm thấy bài kiểm tra" });

        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        var userIdStr = User.FindFirst("userId")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (role == "GiaoVien" && int.TryParse(userIdStr, out int teacherId))
        {
            var classInfo = await _courseServiceClient.GetClassInfo(quiz.ClassId);
            if (classInfo == null || (classInfo.TeacherId != teacherId && classInfo.TeacherId2 != teacherId))
                return Forbid();
        }

        var newQuestion = new QuizQuestion
        {
            QuizId = id,
            QuestionText = dto.QuestionText,
            Options = dto.Options,
            CorrectAnswer = dto.CorrectAnswer
        };

        _context.QuizQuestions.Add(newQuestion);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuizById), new { id = quiz.QuizId }, new { message = "Thêm câu hỏi thành công", question = newQuestion });
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
    public int MaxAttempts { get; set; } = 1;
    public DateTime? LessonDate { get; set; }
    public DateTime? AvailableFrom { get; set; }
    public DateTime? AvailableTo { get; set; }
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

public class UpdateQuestionDto
{
    public string QuestionText { get; set; } = string.Empty;
    public string? Options { get; set; }
    public string? CorrectAnswer { get; set; }
}

public class AnswerDoubtDto
{
    public string AnswerText { get; set; } = string.Empty;
}

public class SaveOfficialScoreDto
{
    public int StudentId { get; set; }
    public decimal Score { get; set; }
}

public class UpdateAvailabilityDto
{
    public DateTime? AvailableFrom { get; set; }
    public DateTime? AvailableTo { get; set; }
}
