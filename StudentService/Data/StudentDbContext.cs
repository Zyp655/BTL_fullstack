using Microsoft.EntityFrameworkCore;
using StudentService.Models;

namespace StudentService.Data;

public class StudentDbContext : DbContext
{
    public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<ExamResult> ExamResults => Set<ExamResult>();
    public DbSet<CourseQueue> CourseQueues => Set<CourseQueue>();
    public DbSet<StudentCredit> StudentCredits => Set<StudentCredit>();
    public DbSet<SupportMessage> SupportMessages => Set<SupportMessage>();
    public DbSet<TeacherEvaluation> TeacherEvaluations => Set<TeacherEvaluation>();
    public DbSet<EvaluationCriterion> EvaluationCriteria => Set<EvaluationCriterion>();
    public DbSet<SystemSetting> SystemSettings => Set<SystemSetting>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // EvaluationCriterion
        modelBuilder.Entity<EvaluationCriterion>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Student
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.UserId).IsUnique();
            entity.HasIndex(e => e.FullName);
            entity.HasIndex(e => e.Email);
        });

        // Enrollment
        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasIndex(e => e.StudentId);
            entity.HasIndex(e => e.ClassId);
            entity.HasIndex(e => new { e.StudentId, e.ClassId }).IsUnique();

            entity.HasOne(e => e.Student)
                  .WithMany(s => s.Enrollments)
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // CourseQueue
        modelBuilder.Entity<CourseQueue>(entity =>
        {
            entity.HasIndex(e => e.StudentId);
            entity.HasIndex(e => e.CourseId);
            entity.HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();

            entity.HasOne(e => e.Student)
                  .WithMany()
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // StudentCredit
        modelBuilder.Entity<StudentCredit>(entity =>
        {
            entity.HasIndex(e => e.StudentId);
            entity.HasOne(e => e.Student)
                  .WithMany()
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // SupportMessage
        modelBuilder.Entity<SupportMessage>(entity =>
        {
            entity.HasIndex(e => e.StudentId);
            entity.HasOne(e => e.Student)
                  .WithMany()
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // TeacherEvaluation
        modelBuilder.Entity<TeacherEvaluation>(entity =>
        {
            entity.HasIndex(e => e.StudentId);
            entity.HasIndex(e => e.ClassId);
            entity.HasIndex(e => e.TeacherId);
            entity.HasIndex(e => new { e.StudentId, e.ClassId }).IsUnique();

            entity.HasOne(e => e.Student)
                  .WithMany()
                  .HasForeignKey(e => e.StudentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Attendance
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasIndex(e => e.EnrollmentId);
            entity.HasIndex(e => e.SessionDate);

            entity.HasOne(e => e.Enrollment)
                  .WithMany(en => en.Attendances)
                  .HasForeignKey(e => e.EnrollmentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // ExamResult
        modelBuilder.Entity<ExamResult>(entity =>
        {
            entity.HasIndex(e => e.EnrollmentId);

            entity.HasOne(e => e.Enrollment)
                  .WithMany(en => en.ExamResults)
                  .HasForeignKey(e => e.EnrollmentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed default evaluation criteria
        modelBuilder.Entity<EvaluationCriterion>().HasData(
            new EvaluationCriterion { Id = 1, Name = "Chất lượng giảng dạy", Description = "Truyền tải kiến thức, dễ hiểu, nhiệt huyết", IsActive = true },
            new EvaluationCriterion { Id = 2, Name = "Thái độ & Hỗ trợ", Description = "Tận tình hỗ trợ học viên, giải đáp thắc mắc", IsActive = true },
            new EvaluationCriterion { Id = 3, Name = "Tài liệu & Giáo trình", Description = "Đầy đủ tài liệu học tập, bài tập, slide", IsActive = true },
            new EvaluationCriterion { Id = 4, Name = "Tác phong & Đúng giờ", Description = "Vào lớp đúng giờ, chuyên nghiệp, chuẩn mực", IsActive = true }
        );

        // Seed default system settings
        modelBuilder.Entity<SystemSetting>().HasData(
            new SystemSetting { Key = "IsEvaluationEnabled", Value = "true" },
            new SystemSetting { Key = "EnabledEvaluationClassIds", Value = "1,2,3,4,5,6,7,8,9,10" }
        );

        // Seed students (matching UserIds from PaymentService)
        modelBuilder.Entity<Student>().HasData(
new Student
            {
                StudentId = 1,
                UserId = 12,
                FullName = "Phạm Văn Dũng",
                DateOfBirth = new DateTime(2000, 5, 15, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0901000005",
                Email = "phamvandung@gmail.com",
                Address = "123 Nguyễn Huệ, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 2,
                UserId = 13,
                FullName = "Hoàng Thị Mai",
                DateOfBirth = new DateTime(2001, 8, 20, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0901000006",
                Email = "hoangthimai@gmail.com",
                Address = "456 Lê Lợi, Q3, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 3,
                UserId = 14,
                FullName = "Vũ Văn Giang",
                DateOfBirth = new DateTime(1999, 12, 1, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0901000007",
                Email = "vuvangiang@gmail.com",
                Address = "789 Trần Hưng Đạo, Q5, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 4,
                UserId = 15,
                FullName = "Nguyễn Văn Minh",
                DateOfBirth = new DateTime(2002, 3, 10, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0901000011",
                Email = "nguyenvanminh@gmail.com",
                Address = "101 Cách Mạng Tháng 8, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 5,
                UserId = 16,
                FullName = "Hoàng Thị Nga",
                DateOfBirth = new DateTime(2000, 9, 25, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0901000012",
                Email = "hoangthinga@gmail.com",
                Address = "202 Nguyễn Trãi, Q5, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 6,
                UserId = 17,
                FullName = "Vũ Văn Hải",
                DateOfBirth = new DateTime(2001, 11, 5, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0901000013",
                Email = "vuvanhai@gmail.com",
                Address = "303 Điện Biên Phủ, Q3, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 7,
                UserId = 18,
                FullName = "Lê Thị Phương",
                DateOfBirth = new DateTime(2003, 7, 14, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0901000014",
                Email = "lethiphuong@gmail.com",
                Address = "404 Võ Văn Tần, Q3, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 8,
                UserId = 19,
                FullName = "Trần Quốc Quân",
                DateOfBirth = new DateTime(2000, 1, 30, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0901000015",
                Email = "tranquocquan@gmail.com",
                Address = "505 Nguyễn Thị Minh Khai, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 9,
                UserId = 20,
                FullName = "Nguyễn Hoàng Tiến",
                DateOfBirth = new DateTime(1999, 11, 24, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0933341057",
                Email = "nguyenhoangtien@gmail.com",
                Address = "923 Cách Mạng Tháng 8, Phường 2, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 10,
                UserId = 21,
                FullName = "Nguyễn Thị Phương",
                DateOfBirth = new DateTime(1998, 9, 7, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0934903402",
                Email = "nguyenthiphuong@gmail.com",
                Address = "743 Điện Biên Phủ, Phường 12, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 11,
                UserId = 22,
                FullName = "Phạm Phương Chi",
                DateOfBirth = new DateTime(2000, 12, 14, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0941109031",
                Email = "phamphuongchi@gmail.com",
                Address = "358 Hai Bà Trưng, Phường 3, Q3, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 12,
                UserId = 23,
                FullName = "Trần Ngọc Nhi",
                DateOfBirth = new DateTime(2003, 10, 9, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0917022674",
                Email = "tranngocnhi@gmail.com",
                Address = "836 Lê Duẩn, Phường 12, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 13,
                UserId = 24,
                FullName = "Phan Minh Long",
                DateOfBirth = new DateTime(2001, 12, 3, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0947067228",
                Email = "phanminhlong@gmail.com",
                Address = "56 Điện Biên Phủ, Phường 4, Q5, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 14,
                UserId = 25,
                FullName = "Phạm Minh Kiên",
                DateOfBirth = new DateTime(2003, 3, 12, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0948606962",
                Email = "phamminhkien@gmail.com",
                Address = "373 Nguyễn Trãi, Phường 11, Q5, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 15,
                UserId = 26,
                FullName = "Đặng Quốc Long",
                DateOfBirth = new DateTime(2005, 7, 9, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0933741438",
                Email = "dangquoclong@gmail.com",
                Address = "957 Điện Biên Phủ, Phường 12, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 16,
                UserId = 27,
                FullName = "Ngô Việt Giang",
                DateOfBirth = new DateTime(2003, 7, 9, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0931538552",
                Email = "ngovietgiang@gmail.com",
                Address = "77 Nguyễn Trãi, Phường 15, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 17,
                UserId = 28,
                FullName = "Phạm Phương Nhi",
                DateOfBirth = new DateTime(2002, 3, 8, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0983396987",
                Email = "phamphuongnhi@gmail.com",
                Address = "772 Cách Mạng Tháng 8, Phường 9, Q5, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 18,
                UserId = 29,
                FullName = "Đặng Khánh Lan",
                DateOfBirth = new DateTime(2005, 2, 25, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0933320821",
                Email = "dangkhanhlan@gmail.com",
                Address = "58 Nam Kỳ Khởi Nghĩa, Phường 2, Q3, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 19,
                UserId = 30,
                FullName = "Lý Hữu Phúc",
                DateOfBirth = new DateTime(2004, 10, 15, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0917455324",
                Email = "lyhuuphuc@gmail.com",
                Address = "551 Hai Bà Trưng, Phường 9, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 20,
                UserId = 31,
                FullName = "Ngô Thành Đạt",
                DateOfBirth = new DateTime(2002, 7, 6, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0962871534",
                Email = "ngothanhdat@gmail.com",
                Address = "474 Lê Duẩn, Phường 12, Bình Thạnh, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 21,
                UserId = 32,
                FullName = "Bùi Thu Tuyết",
                DateOfBirth = new DateTime(2001, 3, 12, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0916007072",
                Email = "buithutuyet@gmail.com",
                Address = "790 Nguyễn Trãi, Phường 9, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 22,
                UserId = 33,
                FullName = "Đặng Việt Tuấn",
                DateOfBirth = new DateTime(2003, 5, 8, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0902876828",
                Email = "dangviettuan@gmail.com",
                Address = "69 Nguyễn Trãi, Phường 15, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 23,
                UserId = 34,
                FullName = "Trần Anh Minh",
                DateOfBirth = new DateTime(2005, 9, 6, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0923154051",
                Email = "trananhminh@gmail.com",
                Address = "281 Cách Mạng Tháng 8, Phường 14, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 24,
                UserId = 35,
                FullName = "Phạm Quỳnh Thảo",
                DateOfBirth = new DateTime(2003, 8, 17, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0947693979",
                Email = "phamquynhthao@gmail.com",
                Address = "472 Lê Duẩn, Phường 4, Q3, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 25,
                UserId = 36,
                FullName = "Vũ Văn Bách",
                DateOfBirth = new DateTime(1998, 2, 23, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0934694634",
                Email = "vuvanbach@gmail.com",
                Address = "656 Lê Duẩn, Phường 4, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 26,
                UserId = 37,
                FullName = "Vũ Minh Huy",
                DateOfBirth = new DateTime(2005, 4, 18, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0935672073",
                Email = "vuminhhuy@gmail.com",
                Address = "145 Điện Biên Phủ, Phường 15, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 27,
                UserId = 38,
                FullName = "Phạm Phương Anh",
                DateOfBirth = new DateTime(1999, 11, 14, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0932582524",
                Email = "phamphuonganh@gmail.com",
                Address = "372 Trần Hưng Đạo, Phường 7, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 28,
                UserId = 39,
                FullName = "Ngô Minh Giang",
                DateOfBirth = new DateTime(1999, 4, 7, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0976692553",
                Email = "ngominhgiang@gmail.com",
                Address = "204 Cách Mạng Tháng 8, Phường 8, Q3, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 29,
                UserId = 40,
                FullName = "Lê Kim Hoa",
                DateOfBirth = new DateTime(2005, 9, 4, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0932264748",
                Email = "lekimhoa@gmail.com",
                Address = "61 Điện Biên Phủ, Phường 9, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 30,
                UserId = 41,
                FullName = "Lý Đức Hùng",
                DateOfBirth = new DateTime(2005, 4, 28, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0979147706",
                Email = "lyduchung@gmail.com",
                Address = "420 Lê Duẩn, Phường 3, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 31,
                UserId = 42,
                FullName = "Phan Hoàng Việt",
                DateOfBirth = new DateTime(2005, 3, 7, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0948096887",
                Email = "phanhoangviet@gmail.com",
                Address = "313 Nguyễn Trãi, Phường 1, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 32,
                UserId = 43,
                FullName = "Dương Việt Giang",
                DateOfBirth = new DateTime(2000, 1, 17, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0908999183",
                Email = "duongvietgiang@gmail.com",
                Address = "92 Nam Kỳ Khởi Nghĩa, Phường 3, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 33,
                UserId = 44,
                FullName = "Ngô Đức Kiên",
                DateOfBirth = new DateTime(1998, 10, 3, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0915130808",
                Email = "ngoduckien@gmail.com",
                Address = "439 Điện Biên Phủ, Phường 10, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 34,
                UserId = 45,
                FullName = "Hoàng Hồng Trinh",
                DateOfBirth = new DateTime(2004, 3, 22, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0935456272",
                Email = "hoanghongtrinh@gmail.com",
                Address = "670 Hai Bà Trưng, Phường 8, Q5, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 35,
                UserId = 46,
                FullName = "Nguyễn Anh Phúc",
                DateOfBirth = new DateTime(2001, 9, 9, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0912229110",
                Email = "nguyenanhphuc@gmail.com",
                Address = "145 Hai Bà Trưng, Phường 15, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 36,
                UserId = 47,
                FullName = "Vũ Hoàng Hùng",
                DateOfBirth = new DateTime(1998, 11, 27, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0986075395",
                Email = "vuhoanghung@gmail.com",
                Address = "577 Hai Bà Trưng, Phường 15, Bình Thạnh, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 37,
                UserId = 48,
                FullName = "Lê Hoàng Hải",
                DateOfBirth = new DateTime(2002, 5, 20, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0913607983",
                Email = "lehoanghai@gmail.com",
                Address = "225 Điện Biên Phủ, Phường 6, Q3, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 38,
                UserId = 49,
                FullName = "Bùi Phương Hạnh",
                DateOfBirth = new DateTime(2004, 5, 2, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0902548511",
                Email = "buiphuonghanh@gmail.com",
                Address = "13 Hai Bà Trưng, Phường 13, Q3, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 39,
                UserId = 50,
                FullName = "Lê Phương Dung",
                DateOfBirth = new DateTime(1999, 2, 23, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0971162230",
                Email = "lephuongdung@gmail.com",
                Address = "935 Nguyễn Trãi, Phường 9, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 40,
                UserId = 51,
                FullName = "Đặng Quỳnh Linh",
                DateOfBirth = new DateTime(1998, 5, 12, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0973138181",
                Email = "dangquynhlinh@gmail.com",
                Address = "930 Nam Kỳ Khởi Nghĩa, Phường 14, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 41,
                UserId = 52,
                FullName = "Phạm Hồng Vy",
                DateOfBirth = new DateTime(2000, 4, 28, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0967817881",
                Email = "phamhongvy@gmail.com",
                Address = "176 Nam Kỳ Khởi Nghĩa, Phường 13, Q3, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 42,
                UserId = 53,
                FullName = "Nguyễn Thu Trinh",
                DateOfBirth = new DateTime(2002, 3, 26, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0975163555",
                Email = "nguyenthutrinh@gmail.com",
                Address = "728 Lê Duẩn, Phường 7, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 43,
                UserId = 54,
                FullName = "Phạm Hồng Hoa",
                DateOfBirth = new DateTime(2001, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0966120253",
                Email = "phamhonghoa@gmail.com",
                Address = "685 Nguyễn Trãi, Phường 7, Q5, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 44,
                UserId = 55,
                FullName = "Trần Kim Lan",
                DateOfBirth = new DateTime(2003, 1, 4, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0979996207",
                Email = "trankimlan@gmail.com",
                Address = "908 Hai Bà Trưng, Phường 3, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 45,
                UserId = 56,
                FullName = "Nguyễn Ngọc Bích",
                DateOfBirth = new DateTime(2003, 7, 20, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0976799667",
                Email = "nguyenngocbich@gmail.com",
                Address = "533 Lê Duẩn, Phường 7, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 46,
                UserId = 57,
                FullName = "Hoàng Văn Nam",
                DateOfBirth = new DateTime(2001, 6, 14, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0909722815",
                Email = "hoangvannam@gmail.com",
                Address = "81 Điện Biên Phủ, Phường 15, Q5, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 47,
                UserId = 58,
                FullName = "Ngô Ngọc Quỳnh",
                DateOfBirth = new DateTime(2003, 7, 23, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0947851696",
                Email = "ngongocquynh@gmail.com",
                Address = "312 Cách Mạng Tháng 8, Phường 3, Q3, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 48,
                UserId = 59,
                FullName = "Ngô Khánh Trang",
                DateOfBirth = new DateTime(1998, 5, 10, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0947812810",
                Email = "ngokhanhtrang@gmail.com",
                Address = "225 Trần Hưng Đạo, Phường 13, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 49,
                UserId = 60,
                FullName = "Đỗ Phương Hoa",
                DateOfBirth = new DateTime(2005, 12, 6, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0939576076",
                Email = "dophuonghoa@gmail.com",
                Address = "684 Lê Duẩn, Phường 5, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 50,
                UserId = 61,
                FullName = "Trần Hồng Quỳnh",
                DateOfBirth = new DateTime(2000, 1, 2, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0934340845",
                Email = "tranhongquynh@gmail.com",
                Address = "260 Trần Hưng Đạo, Phường 10, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 51,
                UserId = 62,
                FullName = "Phan Thanh Thảo",
                DateOfBirth = new DateTime(2004, 4, 5, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0979294272",
                Email = "phanthanhthao@gmail.com",
                Address = "681 Điện Biên Phủ, Phường 1, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 52,
                UserId = 63,
                FullName = "Phạm Thu Tuyết",
                DateOfBirth = new DateTime(2001, 2, 15, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0981842524",
                Email = "phamthutuyet@gmail.com",
                Address = "146 Nam Kỳ Khởi Nghĩa, Phường 8, Bình Thạnh, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 53,
                UserId = 64,
                FullName = "Lý Phương Bích",
                DateOfBirth = new DateTime(2000, 12, 28, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0978481188",
                Email = "lyphuongbich@gmail.com",
                Address = "496 Trần Hưng Đạo, Phường 5, Q3, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 54,
                UserId = 65,
                FullName = "Lý Quỳnh Ngọc",
                DateOfBirth = new DateTime(2005, 2, 23, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0935606980",
                Email = "lyquynhngoc@gmail.com",
                Address = "302 Nguyễn Trãi, Phường 5, Q5, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 55,
                UserId = 66,
                FullName = "Bùi Ngọc Linh",
                DateOfBirth = new DateTime(2004, 12, 5, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0924879923",
                Email = "buingoclinh@gmail.com",
                Address = "733 Nguyễn Trãi, Phường 2, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 56,
                UserId = 67,
                FullName = "Vũ Quỳnh Hoa",
                DateOfBirth = new DateTime(2001, 7, 13, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0972044645",
                Email = "vuquynhhoa@gmail.com",
                Address = "936 Nam Kỳ Khởi Nghĩa, Phường 10, Bình Thạnh, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 57,
                UserId = 68,
                FullName = "Lý Ngọc Kiên",
                DateOfBirth = new DateTime(2003, 5, 25, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0981098919",
                Email = "lyngockien@gmail.com",
                Address = "409 Nam Kỳ Khởi Nghĩa, Phường 15, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 58,
                UserId = 69,
                FullName = "Đỗ Đức Đạt",
                DateOfBirth = new DateTime(1998, 7, 11, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0979147720",
                Email = "doducdat@gmail.com",
                Address = "694 Điện Biên Phủ, Phường 13, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 59,
                UserId = 70,
                FullName = "Đỗ Quốc Phúc",
                DateOfBirth = new DateTime(1998, 2, 21, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0907610562",
                Email = "doquocphuc@gmail.com",
                Address = "448 Nguyễn Trãi, Phường 14, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 60,
                UserId = 71,
                FullName = "Nguyễn Hoàng Kiên",
                DateOfBirth = new DateTime(2005, 6, 11, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0964551070",
                Email = "nguyenhoangkien@gmail.com",
                Address = "789 Trần Hưng Đạo, Phường 5, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 61,
                UserId = 72,
                FullName = "Trần Phương Mai",
                DateOfBirth = new DateTime(2001, 11, 3, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0906871360",
                Email = "tranphuongmai@gmail.com",
                Address = "809 Điện Biên Phủ, Phường 1, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 62,
                UserId = 73,
                FullName = "Phạm Văn Phúc",
                DateOfBirth = new DateTime(2000, 8, 22, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0925002120",
                Email = "phamvanphuc@gmail.com",
                Address = "127 Cách Mạng Tháng 8, Phường 4, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 63,
                UserId = 74,
                FullName = "Lý Trúc Trang",
                DateOfBirth = new DateTime(2002, 2, 19, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0913747534",
                Email = "lytructrang@gmail.com",
                Address = "36 Hai Bà Trưng, Phường 10, Bình Thạnh, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 64,
                UserId = 75,
                FullName = "Phan Hồng Phương",
                DateOfBirth = new DateTime(2002, 11, 20, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0932709620",
                Email = "phanhongphuong@gmail.com",
                Address = "834 Lê Duẩn, Phường 13, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 65,
                UserId = 76,
                FullName = "Vũ Thành Nam",
                DateOfBirth = new DateTime(2003, 1, 28, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0962156902",
                Email = "vuthanhnam@gmail.com",
                Address = "440 Nam Kỳ Khởi Nghĩa, Phường 8, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 66,
                UserId = 77,
                FullName = "Vũ Phương Linh",
                DateOfBirth = new DateTime(2002, 10, 26, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0973955087",
                Email = "vuphuonglinh@gmail.com",
                Address = "951 Cách Mạng Tháng 8, Phường 13, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 67,
                UserId = 78,
                FullName = "Phan Thanh Hạnh",
                DateOfBirth = new DateTime(1999, 5, 15, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0965118725",
                Email = "phanthanhhanh@gmail.com",
                Address = "259 Nam Kỳ Khởi Nghĩa, Phường 8, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 68,
                UserId = 79,
                FullName = "Vũ Thị Ngọc",
                DateOfBirth = new DateTime(2005, 4, 12, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0964050766",
                Email = "vuthingoc@gmail.com",
                Address = "826 Hai Bà Trưng, Phường 6, Q5, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 69,
                UserId = 80,
                FullName = "Bùi Thị Tuyết",
                DateOfBirth = new DateTime(2001, 12, 14, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0932436347",
                Email = "buithituyet@gmail.com",
                Address = "510 Cách Mạng Tháng 8, Phường 13, Q3, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 70,
                UserId = 81,
                FullName = "Ngô Phương Hoa",
                DateOfBirth = new DateTime(2002, 4, 13, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0902561176",
                Email = "ngophuonghoa@gmail.com",
                Address = "718 Nguyễn Trãi, Phường 5, Bình Thạnh, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 71,
                UserId = 82,
                FullName = "Đỗ Quỳnh Tuyết",
                DateOfBirth = new DateTime(2003, 6, 23, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0968138769",
                Email = "doquynhtuyet@gmail.com",
                Address = "474 Hai Bà Trưng, Phường 5, Q5, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 72,
                UserId = 83,
                FullName = "Trần Đức Phong",
                DateOfBirth = new DateTime(2000, 4, 7, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0919990305",
                Email = "tranducphong@gmail.com",
                Address = "766 Trần Hưng Đạo, Phường 5, Bình Thạnh, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 73,
                UserId = 84,
                FullName = "Trần Hồng Quỳnh",
                DateOfBirth = new DateTime(2000, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0937054579",
                Email = "tranhongquynh1@gmail.com",
                Address = "735 Cách Mạng Tháng 8, Phường 3, Q5, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 74,
                UserId = 85,
                FullName = "Nguyễn Thành Tùng",
                DateOfBirth = new DateTime(1999, 1, 19, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0929235577",
                Email = "nguyenthanhtung@gmail.com",
                Address = "301 Trần Hưng Đạo, Phường 8, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 75,
                UserId = 86,
                FullName = "Lê Thị Hạnh",
                DateOfBirth = new DateTime(1999, 7, 16, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0982914064",
                Email = "lethihanh@gmail.com",
                Address = "85 Cách Mạng Tháng 8, Phường 11, Bình Thạnh, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 76,
                UserId = 87,
                FullName = "Lê Quốc Bách",
                DateOfBirth = new DateTime(2001, 2, 18, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0942429098",
                Email = "lequocbach@gmail.com",
                Address = "792 Trần Hưng Đạo, Phường 10, Tân Bình, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 77,
                UserId = 88,
                FullName = "Lý Thành Kiên",
                DateOfBirth = new DateTime(2002, 10, 14, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0988427625",
                Email = "lythanhkien@gmail.com",
                Address = "322 Cách Mạng Tháng 8, Phường 10, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 78,
                UserId = 89,
                FullName = "Lý Đức Thịnh",
                DateOfBirth = new DateTime(1999, 3, 8, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0935440025",
                Email = "lyducthinh@gmail.com",
                Address = "187 Cách Mạng Tháng 8, Phường 2, Q3, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 79,
                UserId = 90,
                FullName = "Phan Anh Phúc",
                DateOfBirth = new DateTime(1998, 4, 10, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0985886520",
                Email = "phananhphuc@gmail.com",
                Address = "733 Hai Bà Trưng, Phường 12, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 80,
                UserId = 91,
                FullName = "Ngô Đức Đạt",
                DateOfBirth = new DateTime(1999, 9, 8, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0938132648",
                Email = "ngoducdat@gmail.com",
                Address = "673 Nguyễn Trãi, Phường 15, Q5, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 81,
                UserId = 92,
                FullName = "Trần Văn Hùng",
                DateOfBirth = new DateTime(2005, 2, 15, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0945842082",
                Email = "tranvanhung@gmail.com",
                Address = "715 Hai Bà Trưng, Phường 12, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 82,
                UserId = 93,
                FullName = "Bùi Quỳnh Ngọc",
                DateOfBirth = new DateTime(1998, 7, 24, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0982349590",
                Email = "buiquynhngoc@gmail.com",
                Address = "340 Cách Mạng Tháng 8, Phường 5, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 83,
                UserId = 94,
                FullName = "Phạm Ngọc Bách",
                DateOfBirth = new DateTime(1998, 3, 16, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0905521644",
                Email = "phamngocbach@gmail.com",
                Address = "541 Điện Biên Phủ, Phường 8, Q5, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 84,
                UserId = 95,
                FullName = "Đặng Hữu Thịnh",
                DateOfBirth = new DateTime(2005, 6, 14, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0982530793",
                Email = "danghuuthinh@gmail.com",
                Address = "351 Hai Bà Trưng, Phường 11, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 85,
                UserId = 96,
                FullName = "Vũ Hữu Tuấn",
                DateOfBirth = new DateTime(1998, 8, 3, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0947718473",
                Email = "vuhuutuan@gmail.com",
                Address = "332 Hai Bà Trưng, Phường 6, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 86,
                UserId = 97,
                FullName = "Bùi Thị Dung",
                DateOfBirth = new DateTime(1998, 4, 17, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0987933143",
                Email = "buithidung@gmail.com",
                Address = "380 Cách Mạng Tháng 8, Phường 13, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 87,
                UserId = 98,
                FullName = "Lý Thị Thảo",
                DateOfBirth = new DateTime(2002, 8, 23, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nữ",
                Phone = "0943198037",
                Email = "lythithao@gmail.com",
                Address = "506 Lê Duẩn, Phường 1, Bình Thạnh, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 88,
                UserId = 99,
                FullName = "Dương Quốc Tùng",
                DateOfBirth = new DateTime(1999, 4, 27, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0907844947",
                Email = "duongquoctung@gmail.com",
                Address = "944 Lê Duẩn, Phường 8, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 89,
                UserId = 100,
                FullName = "Đỗ Hoàng Huy",
                DateOfBirth = new DateTime(2005, 8, 8, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0947970771",
                Email = "dohoanghuy@gmail.com",
                Address = "477 Cách Mạng Tháng 8, Phường 3, Q10, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 90,
                UserId = 101,
                FullName = "Đặng Thành Quân",
                DateOfBirth = new DateTime(2004, 6, 26, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0915634662",
                Email = "dangthanhquan@gmail.com",
                Address = "529 Hai Bà Trưng, Phường 14, Q1, TP.HCM",
                CreatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        var enrollments = new List<Enrollment>
        {
            new Enrollment { EnrollmentId = 1, StudentId = 1, ClassId = 1, Status = "DangHoc", EnrolledAt = new DateTime(2026, 2, 20, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 2, StudentId = 2, ClassId = 1, Status = "DangHoc", EnrolledAt = new DateTime(2026, 2, 22, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 3, StudentId = 3, ClassId = 1, Status = "DangHoc", EnrolledAt = new DateTime(2026, 2, 20, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 4, StudentId = 4, ClassId = 1, Status = "DangHoc", EnrolledAt = new DateTime(2026, 2, 25, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 5, StudentId = 5, ClassId = 1, Status = "DangHoc", EnrolledAt = new DateTime(2026, 2, 25, 0, 0, 0, DateTimeKind.Utc) },

            new Enrollment { EnrollmentId = 6, StudentId = 6, ClassId = 2, Status = "DangHoc", EnrolledAt = new DateTime(2026, 6, 15, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 7, StudentId = 7, ClassId = 2, Status = "DangHoc", EnrolledAt = new DateTime(2026, 6, 16, 0, 0, 0, DateTimeKind.Utc) },

            new Enrollment { EnrollmentId = 8, StudentId = 1, ClassId = 3, Status = "DangHoc", EnrolledAt = new DateTime(2026, 3, 20, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 9, StudentId = 3, ClassId = 3, Status = "DangHoc", EnrolledAt = new DateTime(2026, 3, 20, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 10, StudentId = 5, ClassId = 3, Status = "DangHoc", EnrolledAt = new DateTime(2026, 3, 22, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 11, StudentId = 8, ClassId = 3, Status = "DangHoc", EnrolledAt = new DateTime(2026, 3, 25, 0, 0, 0, DateTimeKind.Utc) },

            new Enrollment { EnrollmentId = 12, StudentId = 2, ClassId = 4, Status = "DangHoc", EnrolledAt = new DateTime(2026, 4, 20, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 13, StudentId = 4, ClassId = 4, Status = "DangHoc", EnrolledAt = new DateTime(2026, 4, 22, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 14, StudentId = 6, ClassId = 4, Status = "DangHoc", EnrolledAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc) },

            new Enrollment { EnrollmentId = 15, StudentId = 7, ClassId = 5, Status = "DangHoc", EnrolledAt = new DateTime(2026, 5, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 16, StudentId = 2, ClassId = 5, Status = "DangHoc", EnrolledAt = new DateTime(2026, 5, 2, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 17, StudentId = 3, ClassId = 5, Status = "DangHoc", EnrolledAt = new DateTime(2026, 5, 3, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 18, StudentId = 4, ClassId = 5, Status = "DangHoc", EnrolledAt = new DateTime(2026, 5, 4, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 19, StudentId = 5, ClassId = 5, Status = "DangHoc", EnrolledAt = new DateTime(2026, 5, 5, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 20, StudentId = 6, ClassId = 5, Status = "DangHoc", EnrolledAt = new DateTime(2026, 5, 6, 0, 0, 0, DateTimeKind.Utc) },

            new Enrollment { EnrollmentId = 21, StudentId = 7, ClassId = 6, Status = "DangHoc", EnrolledAt = new DateTime(2026, 6, 25, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 22, StudentId = 8, ClassId = 6, Status = "DangHoc", EnrolledAt = new DateTime(2026, 6, 26, 0, 0, 0, DateTimeKind.Utc) },

            new Enrollment { EnrollmentId = 23, StudentId = 3, ClassId = 7, Status = "DangHoc", EnrolledAt = new DateTime(2026, 7, 15, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 24, StudentId = 4, ClassId = 7, Status = "DangHoc", EnrolledAt = new DateTime(2026, 7, 16, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 25, StudentId = 5, ClassId = 7, Status = "DangHoc", EnrolledAt = new DateTime(2026, 7, 17, 0, 0, 0, DateTimeKind.Utc) },

            new Enrollment { EnrollmentId = 26, StudentId = 6, ClassId = 8, Status = "DangHoc", EnrolledAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 27, StudentId = 7, ClassId = 8, Status = "DangHoc", EnrolledAt = new DateTime(2026, 4, 26, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 28, StudentId = 8, ClassId = 8, Status = "DangHoc", EnrolledAt = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 38, StudentId = 7, ClassId = 28, Status = "DangHoc", EnrolledAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc) },
            new Enrollment { EnrollmentId = 39, StudentId = 8, ClassId = 28, Status = "DangHoc", EnrolledAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc) }
        };

        int enrollmentIdCounter = 100;
        for (int i = 1; i <= 20; i++)
        {
            int classId = 200 + i;
            for (int s = 1; s <= 15; s++)
            {
                int studentId = ((i - 1) * 15 + (s - 1)) % 90 + 1;
                if (studentId == 1)
                {
                    studentId = 20;
                }
                enrollments.Add(new Enrollment
                {
                    EnrollmentId = enrollmentIdCounter++,
                    StudentId = studentId,
                    ClassId = classId,
                    Status = "DangHoc",
                    EnrolledAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc)
                });
            }
        }

        // Seed enrollments for classes 101 to 120 (to cover all teachers in March and June 2026)
        int enrollmentIdCounter2 = 10000;
        for (int i = 1; i <= 20; i++)
        {
            int classId = 100 + i;
            for (int s = 1; s <= 10; s++)
            {
                int studentId = ((i - 1) * 10 + (s - 1)) % 90 + 1;
                if (studentId == 1)
                {
                    studentId = 20;
                }
                enrollments.Add(new Enrollment
                {
                    EnrollmentId = enrollmentIdCounter2++,
                    StudentId = studentId,
                    ClassId = classId,
                    Status = "DangHoc",
                    EnrolledAt = new DateTime(2026, 2, 25, 0, 0, 0, DateTimeKind.Utc)
                });
            }
        }

        modelBuilder.Entity<Enrollment>().HasData(enrollments.ToArray());


        // Seed attendance records
        var attendances = new List<Attendance>
        {
            new Attendance { AttendanceId = 1, EnrollmentId = 1, SessionDate = new DateTime(2026, 3, 4, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 3, 4, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 2, EnrollmentId = 1, SessionDate = new DateTime(2026, 3, 6, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 3, 6, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 3, EnrollmentId = 1, SessionDate = new DateTime(2026, 3, 8, 0, 0, 0, DateTimeKind.Utc), Status = "DiTre", Note = "Trễ 10 phút", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 3, 8, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 4, EnrollmentId = 2, SessionDate = new DateTime(2026, 3, 4, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 3, 4, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 5, EnrollmentId = 2, SessionDate = new DateTime(2026, 3, 6, 0, 0, 0, DateTimeKind.Utc), Status = "Vang", Note = "Không phép", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 3, 6, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 6, EnrollmentId = 3, SessionDate = new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 7, EnrollmentId = 3, SessionDate = new DateTime(2026, 4, 4, 0, 0, 0, DateTimeKind.Utc), Status = "CoPhep", Note = "Xin phép nghỉ", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 4, 4, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 8, EnrollmentId = 8, SessionDate = new DateTime(2026, 6, 3, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 3, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 9, EnrollmentId = 9, SessionDate = new DateTime(2026, 6, 3, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 3, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 10, EnrollmentId = 10, SessionDate = new DateTime(2026, 6, 3, 0, 0, 0, DateTimeKind.Utc), Status = "DiTre", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 3, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 11, EnrollmentId = 11, SessionDate = new DateTime(2026, 6, 3, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 3, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 12, EnrollmentId = 8, SessionDate = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 13, EnrollmentId = 9, SessionDate = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc), Status = "Vang", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 14, EnrollmentId = 10, SessionDate = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 15, EnrollmentId = 11, SessionDate = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 16, EnrollmentId = 8, SessionDate = new DateTime(2026, 6, 17, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 17, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 17, EnrollmentId = 9, SessionDate = new DateTime(2026, 6, 17, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 17, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 18, EnrollmentId = 10, SessionDate = new DateTime(2026, 6, 17, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 17, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 19, EnrollmentId = 11, SessionDate = new DateTime(2026, 6, 17, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 17, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 20, EnrollmentId = 8, SessionDate = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 21, EnrollmentId = 9, SessionDate = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 22, EnrollmentId = 10, SessionDate = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), Status = "DiTre", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 23, EnrollmentId = 11, SessionDate = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), Status = "Vang", MarkedByTeacherId = 2, CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 24, EnrollmentId = 38, SessionDate = new DateTime(2026, 7, 20, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 5, CreatedAt = new DateTime(2026, 7, 20, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 25, EnrollmentId = 39, SessionDate = new DateTime(2026, 7, 20, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 5, CreatedAt = new DateTime(2026, 7, 20, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 26, EnrollmentId = 38, SessionDate = new DateTime(2026, 7, 27, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 5, CreatedAt = new DateTime(2026, 7, 27, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 27, EnrollmentId = 39, SessionDate = new DateTime(2026, 7, 27, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 5, CreatedAt = new DateTime(2026, 7, 27, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 28, EnrollmentId = 12, SessionDate = new DateTime(2026, 6, 4, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 4, CreatedAt = new DateTime(2026, 6, 4, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 29, EnrollmentId = 13, SessionDate = new DateTime(2026, 6, 4, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 4, CreatedAt = new DateTime(2026, 6, 4, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 30, EnrollmentId = 14, SessionDate = new DateTime(2026, 6, 4, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 4, CreatedAt = new DateTime(2026, 6, 4, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 31, EnrollmentId = 12, SessionDate = new DateTime(2026, 6, 11, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 4, CreatedAt = new DateTime(2026, 6, 11, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 32, EnrollmentId = 13, SessionDate = new DateTime(2026, 6, 11, 0, 0, 0, DateTimeKind.Utc), Status = "DiTre", MarkedByTeacherId = 4, CreatedAt = new DateTime(2026, 6, 11, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 33, EnrollmentId = 14, SessionDate = new DateTime(2026, 6, 11, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 4, CreatedAt = new DateTime(2026, 6, 11, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 34, EnrollmentId = 12, SessionDate = new DateTime(2026, 6, 18, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 4, CreatedAt = new DateTime(2026, 6, 18, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 35, EnrollmentId = 13, SessionDate = new DateTime(2026, 6, 18, 0, 0, 0, DateTimeKind.Utc), Status = "Vang", MarkedByTeacherId = 4, CreatedAt = new DateTime(2026, 6, 18, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 36, EnrollmentId = 14, SessionDate = new DateTime(2026, 6, 18, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 4, CreatedAt = new DateTime(2026, 6, 18, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 37, EnrollmentId = 12, SessionDate = new DateTime(2026, 6, 25, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 4, CreatedAt = new DateTime(2026, 6, 25, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 38, EnrollmentId = 13, SessionDate = new DateTime(2026, 6, 25, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 4, CreatedAt = new DateTime(2026, 6, 25, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 39, EnrollmentId = 14, SessionDate = new DateTime(2026, 6, 25, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 4, CreatedAt = new DateTime(2026, 6, 25, 0, 0, 0, DateTimeKind.Utc) }
        };

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

        int attendanceIdCounter = 1000;
        for (int i = 1; i <= 20; i++)
        {
            int classId = 100 + i;
            int teacherId = teachers[i % teachers.Length].Id;
            var months = new[] { 3, 6 };
            var days = new[] { 4, 11, 18, 25 };

            for (int m = 0; m < months.Length; m++)
            {
                int month = months[m];
                for (int d = 0; d < days.Length; d++)
                {
                    int day = days[d];
                    var sessionDate = new DateTime(2026, month, day, 0, 0, 0, DateTimeKind.Utc);
                    for (int s = 0; s < 10; s++)
                    {
                        int enrollmentId = 10000 + (i - 1) * 10 + s;
                        attendances.Add(new Attendance
                        {
                            AttendanceId = attendanceIdCounter++,
                            EnrollmentId = enrollmentId,
                            SessionDate = sessionDate,
                            Status = "CoMat",
                            MarkedByTeacherId = teacherId,
                            CreatedAt = sessionDate
                        });
                    }
                }
            }
        }

        modelBuilder.Entity<Attendance>().HasData(attendances.ToArray());

        // Seed exam results
        modelBuilder.Entity<ExamResult>().HasData(
            new ExamResult { ResultId = 1, EnrollmentId = 1, ExamType = "KiemTra", Score = 8.5m, Note = "Bài kiểm tra 15 phút", GradedByTeacherId = 2, ExamDate = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc) },
            new ExamResult { ResultId = 2, EnrollmentId = 1, ExamType = "GiuaKy", Score = 7.0m, Note = "Kiểm tra giữa kỳ", GradedByTeacherId = 2, ExamDate = new DateTime(2026, 4, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 4, 15, 0, 0, 0, DateTimeKind.Utc) },
            new ExamResult { ResultId = 3, EnrollmentId = 2, ExamType = "KiemTra", Score = 9.0m, GradedByTeacherId = 2, ExamDate = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc) },
            new ExamResult { ResultId = 4, EnrollmentId = 3, ExamType = "KiemTra", Score = 6.5m, Note = "Cần cải thiện phần Listening", GradedByTeacherId = 2, ExamDate = new DateTime(2026, 4, 20, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 4, 20, 0, 0, 0, DateTimeKind.Utc) },
            new ExamResult { ResultId = 5, EnrollmentId = 4, ExamType = "KiemTra", Score = 9.5m, Note = "Xuất sắc", GradedByTeacherId = 2, ExamDate = new DateTime(2026, 5, 10, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 5, 10, 0, 0, 0, DateTimeKind.Utc) }
        );

        // Seed teacher evaluations
        modelBuilder.Entity<TeacherEvaluation>().HasData(
            new TeacherEvaluation
            {
                Id = 1,
                StudentId = 1,
                ClassId = 1,
                TeacherId = 2,
                TeachingQualityRating = 5,
                SupportRating = 5,
                CurriculumRating = 4,
                PunctualityRating = 5,
                Rating = 4.75,
                Comment = "Thầy dạy rất hay và nhiệt tình, tài liệu đầy đủ.",
                CreatedAt = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TeacherEvaluation
            {
                Id = 2,
                StudentId = 2,
                ClassId = 1,
                TeacherId = 2,
                TeachingQualityRating = 4,
                SupportRating = 5,
                CurriculumRating = 5,
                PunctualityRating = 4,
                Rating = 4.50,
                Comment = "Thầy hỗ trợ nhiệt tình sau giờ học.",
                CreatedAt = new DateTime(2026, 6, 2, 0, 0, 0, DateTimeKind.Utc)
            },
            new TeacherEvaluation
            {
                Id = 3,
                StudentId = 3,
                ClassId = 1,
                TeacherId = 2,
                TeachingQualityRating = 5,
                SupportRating = 4,
                CurriculumRating = 4,
                PunctualityRating = 5,
                Rating = 4.50,
                Comment = "Tác phong thầy rất chuyên nghiệp, đúng giờ.",
                CreatedAt = new DateTime(2026, 6, 3, 0, 0, 0, DateTimeKind.Utc)
            },
            new TeacherEvaluation
            {
                Id = 4,
                StudentId = 6,
                ClassId = 2,
                TeacherId = 3,
                TeachingQualityRating = 4,
                SupportRating = 4,
                CurriculumRating = 4,
                PunctualityRating = 4,
                Rating = 4.00,
                Comment = "Cô Bình dạy dễ hiểu, chuẩn bị bài kỹ lưỡng.",
                CreatedAt = new DateTime(2026, 6, 4, 0, 0, 0, DateTimeKind.Utc)
            },
            new TeacherEvaluation
            {
                Id = 5,
                StudentId = 7,
                ClassId = 2,
                TeacherId = 3,
                TeachingQualityRating = 5,
                SupportRating = 5,
                CurriculumRating = 5,
                PunctualityRating = 5,
                Rating = 5.00,
                Comment = "Lớp học rất sôi nổi, cô hỗ trợ nhiệt tình.",
                CreatedAt = new DateTime(2026, 6, 5, 0, 0, 0, DateTimeKind.Utc)
            },
            new TeacherEvaluation
            {
                Id = 6,
                StudentId = 1,
                ClassId = 3,
                TeacherId = 2,
                TeachingQualityRating = 5,
                SupportRating = 5,
                CurriculumRating = 5,
                PunctualityRating = 5,
                Rating = 5.00,
                Comment = "Khóa học TOEIC rất chất lượng, thầy An truyền thụ nhiều mẹo thi thực tế.",
                CreatedAt = new DateTime(2026, 6, 6, 0, 0, 0, DateTimeKind.Utc)
            },
            new TeacherEvaluation
            {
                Id = 7,
                StudentId = 5,
                ClassId = 3,
                TeacherId = 2,
                TeachingQualityRating = 4,
                SupportRating = 4,
                CurriculumRating = 4,
                PunctualityRating = 5,
                Rating = 4.25,
                Comment = "Lịch học đúng giờ, tài liệu phong phú.",
                CreatedAt = new DateTime(2026, 6, 7, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
