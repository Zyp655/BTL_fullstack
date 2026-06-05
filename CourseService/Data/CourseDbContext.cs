using Microsoft.EntityFrameworkCore;
using CourseService.Models;

namespace CourseService.Data;

public class CourseDbContext : DbContext
{
    public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options) { }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<Schedule> Schedules => Set<Schedule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Course
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasIndex(e => e.CourseName);
            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.IsActive);
        });

        // Class
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasIndex(e => e.CourseId);
            entity.HasIndex(e => e.TeacherId);
            entity.HasIndex(e => e.Status);

            entity.HasOne(e => e.Course)
                  .WithMany(c => c.Classes)
                  .HasForeignKey(e => e.CourseId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Schedule
        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasIndex(e => e.ClassId);

            entity.HasOne(e => e.Class)
                  .WithMany(c => c.Schedules)
                  .HasForeignKey(e => e.ClassId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                CourseId = 1,
                CourseName = "Tiếng Anh giao tiếp cơ bản",
                Description = "Khóa học tiếng Anh giao tiếp dành cho người mới bắt đầu, tập trung vào kỹ năng nghe và nói.",
                Level = "Beginner",
                Category = "NgoaiNgu",
                Fee = 2500000,
                TotalSessions = 30,
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Course
            {
                CourseId = 2,
                CourseName = "TOEIC 600+",
                Description = "Luyện thi TOEIC đạt 600+ điểm, bao gồm cả Listening và Reading.",
                Level = "Intermediate",
                Category = "NgoaiNgu",
                Fee = 3500000,
                TotalSessions = 40,
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Course
            {
                CourseId = 3,
                CourseName = "Lập trình Python cơ bản",
                Description = "Khóa học lập trình Python cho người mới, từ cú pháp cơ bản đến xây dựng ứng dụng đơn giản.",
                Level = "Beginner",
                Category = "TinHoc",
                Fee = 3000000,
                TotalSessions = 24,
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Course
            {
                CourseId = 4,
                CourseName = "Kỹ năng thuyết trình",
                Description = "Rèn luyện kỹ năng thuyết trình chuyên nghiệp, tự tin trước đám đông.",
                Level = "Intermediate",
                Category = "KyNang",
                Fee = 1800000,
                TotalSessions = 12,
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Course
            {
                CourseId = 5,
                CourseName = "IELTS 6.5+",
                Description = "Luyện thi IELTS đạt band 6.5+, bao gồm 4 kỹ năng Listening, Reading, Writing, Speaking.",
                Level = "Advanced",
                Category = "NgoaiNgu",
                Fee = 5000000,
                TotalSessions = 48,
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Class>().HasData(
            new Class
            {
                ClassId = 1,
                CourseId = 1,
                ClassName = "TA-CB-01",
                TeacherId = 1,
                TeacherName = "Nguyễn Văn A",
                Room = "P.101",
                MaxStudents = 25,
                CurrentStudents = 18,
                Status = "InProgress",
                StartDate = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 2, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new Class
            {
                ClassId = 2,
                CourseId = 1,
                ClassName = "TA-CB-02",
                TeacherId = 2,
                TeacherName = "Trần Thị B",
                Room = "P.102",
                MaxStudents = 30,
                CurrentStudents = 5,
                Status = "Opened",
                StartDate = new DateTime(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Class
            {
                ClassId = 3,
                CourseId = 2,
                ClassName = "TOEIC-01",
                TeacherId = 1,
                TeacherName = "Nguyễn Văn A",
                Room = "P.201",
                MaxStudents = 20,
                CurrentStudents = 20,
                Status = "InProgress",
                StartDate = new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 8, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 3, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new Class
            {
                ClassId = 4,
                CourseId = 3,
                ClassName = "PY-01",
                TeacherId = 3,
                TeacherName = "Lê Văn C",
                Room = "P.Lab1",
                MaxStudents = 20,
                CurrentStudents = 15,
                Status = "InProgress",
                StartDate = new DateTime(2024, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2024, 8, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 4, 15, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Schedule>().HasData(
            new Schedule { ScheduleId = 1, ClassId = 1, DayOfWeek = 2, Session = "Sang", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0) },
            new Schedule { ScheduleId = 2, ClassId = 1, DayOfWeek = 4, Session = "Sang", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0) },
            new Schedule { ScheduleId = 3, ClassId = 1, DayOfWeek = 6, Session = "Sang", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0) },
            new Schedule { ScheduleId = 4, ClassId = 2, DayOfWeek = 3, Session = "Chieu", StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(16, 0, 0) },
            new Schedule { ScheduleId = 5, ClassId = 2, DayOfWeek = 5, Session = "Chieu", StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(16, 0, 0) },
            new Schedule { ScheduleId = 6, ClassId = 3, DayOfWeek = 2, Session = "Toi", StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(20, 30, 0) },
            new Schedule { ScheduleId = 7, ClassId = 3, DayOfWeek = 5, Session = "Toi", StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(20, 30, 0) },
            new Schedule { ScheduleId = 8, ClassId = 4, DayOfWeek = 3, Session = "Sang", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 30, 0) },
            new Schedule { ScheduleId = 9, ClassId = 4, DayOfWeek = 7, Session = "Sang", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 30, 0) }
        );
    }
}
