using Microsoft.EntityFrameworkCore;
using CourseService.Models;

namespace CourseService.Data;

public class CourseDbContext : DbContext
{
    public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options) { }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<Schedule> Schedules => Set<Schedule>();
    public DbSet<Category> Categories => Set<Category>();

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

        // Category
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.CategoryCode).IsUnique();
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, CategoryCode = "NgoaiNgu", CategoryName = "Ngoại ngữ" },
            new Category { CategoryId = 2, CategoryCode = "TinHoc", CategoryName = "Tin học" },
            new Category { CategoryId = 3, CategoryCode = "KyNang", CategoryName = "Kỹ năng mềm" }
        );

        var courses = new List<Course>
        {
            new Course { CourseId = 1, CourseName = "Tiếng Anh giao tiếp cơ bản", Description = "Khóa học tiếng Anh giao tiếp dành cho người mới bắt đầu, tập trung vào kỹ năng nghe và nói.", Level = "Beginner", Category = "NgoaiNgu", Fee = 2500000, TotalSessions = 30, IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Course { CourseId = 2, CourseName = "TOEIC 600+", Description = "Luyện thi TOEIC đạt 600+ điểm, bao gồm cả Listening và Reading.", Level = "Intermediate", Category = "NgoaiNgu", Fee = 3500000, TotalSessions = 40, IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Course { CourseId = 3, CourseName = "Lập trình Python cơ bản", Description = "Khóa học lập trình Python cho người mới, từ cú pháp cơ bản đến xây dựng ứng dụng đơn giản.", Level = "Beginner", Category = "TinHoc", Fee = 3000000, TotalSessions = 24, IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Course { CourseId = 4, CourseName = "Kỹ năng thuyết trình", Description = "Rèn luyện kỹ năng thuyết trình chuyên nghiệp, tự tin trước đám đông.", Level = "Intermediate", Category = "KyNang", Fee = 1800000, TotalSessions = 12, IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Course { CourseId = 5, CourseName = "IELTS 6.5+", Description = "Luyện thi IELTS đạt band 6.5+, bao gồm 4 kỹ năng Listening, Reading, Writing, Speaking.", Level = "Advanced", Category = "NgoaiNgu", Fee = 5000000, TotalSessions = 48, IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Course { CourseId = 8, CourseName = "Lập trình Web với React & Node.js", Description = "Khóa học Fullstack Web Development sử dụng React cho Frontend và Express/Node.js cho Backend.", Level = "Intermediate", Category = "TinHoc", Fee = 4500000, TotalSessions = 36, IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Course { CourseId = 9, CourseName = "Kỹ năng quản lý thời gian", Description = "Học cách lập kế hoạch, sắp xếp công việc, và tối ưu hóa năng suất làm việc hàng ngày.", Level = "Beginner", Category = "KyNang", Fee = 1500000, TotalSessions = 10, IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Course { CourseId = 10, CourseName = "Tiếng Nhật sơ cấp N5", Description = "Khóa học dành cho người bắt đầu làm quen với bảng chữ cái Hiragana, Katakana và giao tiếp cơ bản.", Level = "Beginner", Category = "NgoaiNgu", Fee = 2800000, TotalSessions = 32, IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Course { CourseId = 11, CourseName = "Lập trình Web với Vue.js", Description = "Khóa học phát triển giao diện Web Single Page Application hiện đại với framework Vue.js 3.", Level = "Intermediate", Category = "TinHoc", Fee = 3800000, TotalSessions = 24, IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Course { CourseId = 12, CourseName = "Kỹ năng làm việc nhóm (Teamwork)", Description = "Rèn luyện kỹ năng phối hợp, giao tiếp và giải quyết xung đột trong môi trường làm việc nhóm hiệu quả.", Level = "Beginner", Category = "KyNang", Fee = 1200000, TotalSessions = 8, IsActive = true, CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Course { CourseId = 14, CourseName = "Lập trình C# nâng cao", Description = "Khóa học lập trình C# nâng cao, tối ưu hiệu năng và phát triển ứng dụng doanh nghiệp.", Level = "Advanced", Category = "TinHoc", Fee = 4500000, TotalSessions = 36, IsActive = true, CreatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc) }
        };

        var classes = new List<Class>
        {
            new Class { ClassId = 1, CourseId = 1, ClassName = "TA-CB-01", TeacherId = 2, TeacherName = "Nguyễn Văn An", Room = "P.101", MaxStudents = 25, CurrentStudents = 5, Status = "InProgress", StartDate = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 2, 15, 0, 0, 0, DateTimeKind.Utc) },
            new Class { ClassId = 2, CourseId = 1, ClassName = "TA-CB-02", TeacherId = 3, TeacherName = "Trần Thị Bình", Room = "P.102", MaxStudents = 30, CurrentStudents = 2, Status = "Opened", StartDate = new DateTime(2026, 7, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 10, 1, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Class { ClassId = 3, CourseId = 2, ClassName = "TOEIC-01", TeacherId = 2, TeacherName = "Nguyễn Văn An", Room = "P.201", MaxStudents = 20, CurrentStudents = 4, Status = "InProgress", StartDate = new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 8, 1, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc) },
            new Class { ClassId = 4, CourseId = 3, ClassName = "PY-01", TeacherId = 4, TeacherName = "Lê Văn Cường", Room = "P.Lab1", MaxStudents = 20, CurrentStudents = 3, Status = "InProgress", StartDate = new DateTime(2026, 5, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 8, 1, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 4, 15, 0, 0, 0, DateTimeKind.Utc) },
            new Class { ClassId = 5, CourseId = 8, ClassName = "FS-REACT-01", TeacherId = 4, TeacherName = "Lê Văn Cường", Room = "P.Lab2", MaxStudents = 24, CurrentStudents = 6, Status = "InProgress", StartDate = new DateTime(2026, 5, 15, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 8, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 5, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Class { ClassId = 6, CourseId = 9, ClassName = "KN-QLTG-01", TeacherId = 5, TeacherName = "Lê Thị Hoa", Room = "P.301", MaxStudents = 40, CurrentStudents = 2, Status = "Opened", StartDate = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 8, 10, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 6, 15, 0, 0, 0, DateTimeKind.Utc) },
            new Class { ClassId = 7, CourseId = 10, ClassName = "JP-N5-01", TeacherId = 9, TeacherName = "Phạm Văn Khánh", Room = "P.103", MaxStudents = 20, CurrentStudents = 3, Status = "Opened", StartDate = new DateTime(2026, 8, 1, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 11, 1, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 7, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Class { ClassId = 8, CourseId = 11, ClassName = "VUEJS-01", TeacherId = 4, TeacherName = "Lê Văn Cường", Room = "P.Lab1", MaxStudents = 20, CurrentStudents = 3, Status = "InProgress", StartDate = new DateTime(2026, 5, 10, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 8, 10, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 4, 20, 0, 0, 0, DateTimeKind.Utc) },
            new Class { ClassId = 28, CourseId = 14, ClassName = "CS-NC-01", TeacherId = 5, TeacherName = "Lê Thị Hoa", Room = "P.Lab3", MaxStudents = 30, CurrentStudents = 2, Status = "InProgress", StartDate = new DateTime(2026, 7, 15, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2026, 10, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc) }
        };

        var schedules = new List<Schedule>
        {
            new Schedule { ScheduleId = 1, ClassId = 1, DayOfWeek = 2, Session = "Sang", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0) },
            new Schedule { ScheduleId = 2, ClassId = 1, DayOfWeek = 4, Session = "Sang", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0) },
            new Schedule { ScheduleId = 3, ClassId = 1, DayOfWeek = 6, Session = "Sang", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0) },
            new Schedule { ScheduleId = 4, ClassId = 2, DayOfWeek = 3, Session = "Chieu", StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(16, 0, 0) },
            new Schedule { ScheduleId = 5, ClassId = 2, DayOfWeek = 5, Session = "Chieu", StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(16, 0, 0) },
            new Schedule { ScheduleId = 6, ClassId = 3, DayOfWeek = 2, Session = "Toi", StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(20, 30, 0) },
            new Schedule { ScheduleId = 7, ClassId = 3, DayOfWeek = 5, Session = "Toi", StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(20, 30, 0) },
            new Schedule { ScheduleId = 8, ClassId = 4, DayOfWeek = 3, Session = "Sang", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 30, 0) },
            new Schedule { ScheduleId = 9, ClassId = 4, DayOfWeek = 7, Session = "Sang", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(11, 30, 0) },
            new Schedule { ScheduleId = 10, ClassId = 5, DayOfWeek = 2, Session = "Toi", StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(20, 30, 0) },
            new Schedule { ScheduleId = 11, ClassId = 5, DayOfWeek = 5, Session = "Toi", StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(20, 30, 0) },
            new Schedule { ScheduleId = 12, ClassId = 6, DayOfWeek = 7, Session = "Sang", StartTime = new TimeSpan(8, 30, 0), EndTime = new TimeSpan(11, 30, 0) },
            new Schedule { ScheduleId = 13, ClassId = 7, DayOfWeek = 3, Session = "Chieu", StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(16, 30, 0) },
            new Schedule { ScheduleId = 14, ClassId = 7, DayOfWeek = 5, Session = "Chieu", StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(16, 30, 0) },
            new Schedule { ScheduleId = 15, ClassId = 8, DayOfWeek = 4, Session = "Toi", StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(20, 30, 0) },
            new Schedule { ScheduleId = 16, ClassId = 8, DayOfWeek = 6, Session = "Toi", StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(20, 30, 0) },
            new Schedule { ScheduleId = 47, ClassId = 28, DayOfWeek = 3, Session = "Toi", StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(20, 30, 0) },
            new Schedule { ScheduleId = 48, ClassId = 28, DayOfWeek = 5, Session = "Toi", StartTime = new TimeSpan(18, 0, 0), EndTime = new TimeSpan(20, 30, 0) }
        };

        // Generate 100 general courses & classes
        var levelOptions = new[] { "Beginner", "Intermediate", "Advanced" };
        var categoryOptions = new[] { "NgoaiNgu", "TinHoc", "KyNang" };
        var teachers = new[]
        {
            new { Id = 2, Name = "Nguyễn Văn An" },
            new { Id = 3, Name = "Trần Thị Bình" },
            new { Id = 4, Name = "Lê Văn Cường" },
            new { Id = 6, Name = "Phạm Văn Khánh" },
            new { Id = 7, Name = "Trần Thị Lan" },
            new { Id = 8, Name = "Nguyễn Hoàng Nam" },
            new { Id = 9, Name = "Trần Thị Mai" },
            new { Id = 10, Name = "Phạm Việt Anh" },
            new { Id = 11, Name = "Hoàng Đức Duy" }
        };

        for (int i = 1; i <= 100; i++)
        {
            int cid = 100 + i;
            var cat = categoryOptions[i % categoryOptions.Length];
            var lvl = levelOptions[i % levelOptions.Length];
            var teacher = teachers[i % teachers.Length];

            courses.Add(new Course
            {
                CourseId = cid,
                CourseName = $"Môn học tự chọn {i}",
                Description = $"Mô tả môn học tự chọn số {i} cho học viên.",
                Level = lvl,
                Category = cat,
                Fee = 1500000 + (i * 20000),
                TotalSessions = 10 + (i % 20),
                IsActive = true,
                CreatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc)
            });

            classes.Add(new Class
            {
                ClassId = cid,
                CourseId = cid,
                ClassName = $"Lớp-TC-{i:000}",
                TeacherId = teacher.Id,
                TeacherName = teacher.Name,
                Room = $"P.{(100 + i % 20)}",
                MaxStudents = 30,
                CurrentStudents = 0,
                Status = "InProgress",
                StartDate = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 9, 10, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc)
            });
        }

        // Generate 20 courses & classes specifically for Lê Thị Hoa (TeacherId = 5)
        for (int i = 1; i <= 20; i++)
        {
            int cid = 200 + i;
            courses.Add(new Course
            {
                CourseId = cid,
                CourseName = $"Chuyên đề nâng cao {i}",
                Description = $"Mô tả chuyên đề nâng cao số {i} giảng dạy bởi cô Lê Thị Hoa.",
                Level = "Advanced",
                Category = "TinHoc",
                Fee = 3000000 + (i * 50000),
                TotalSessions = 15 + i,
                IsActive = true,
                CreatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc)
            });

            classes.Add(new Class
            {
                ClassId = cid,
                CourseId = cid,
                ClassName = $"Lớp-Lth-{i:00}",
                TeacherId = 5,
                TeacherName = "Lê Thị Hoa",
                Room = $"P.Lab-{i}",
                MaxStudents = 30,
                CurrentStudents = 15,
                Status = "InProgress",
                StartDate = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc),
                EndDate = new DateTime(2026, 9, 10, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc)
            });
        }

        int scheduleIdCounter = 100;
        foreach (var cls in classes)
        {
            if (cls.ClassId < 101) continue;

            schedules.Add(new Schedule
            {
                ScheduleId = scheduleIdCounter++,
                ClassId = cls.ClassId,
                DayOfWeek = 2,
                Session = "Toi",
                StartTime = new TimeSpan(18, 0, 0),
                EndTime = new TimeSpan(20, 30, 0)
            });

            schedules.Add(new Schedule
            {
                ScheduleId = scheduleIdCounter++,
                ClassId = cls.ClassId,
                DayOfWeek = 4,
                Session = "Toi",
                StartTime = new TimeSpan(18, 0, 0),
                EndTime = new TimeSpan(20, 30, 0)
            });
        }

        modelBuilder.Entity<Course>().HasData(courses.ToArray());
        modelBuilder.Entity<Class>().HasData(classes.ToArray());
        modelBuilder.Entity<Schedule>().HasData(schedules.ToArray());
    }
}

