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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
        // Seed students (matching UserIds from PaymentService)
        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                StudentId = 1,
                UserId = 5, // Phạm Văn D
                FullName = "Phạm Văn D",
                DateOfBirth = new DateTime(2000, 5, 15, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0901000005",
                Email = "phamvand@gmail.com",
                Address = "123 Nguyễn Huệ, Q1, TP.HCM",
                CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 2,
                UserId = 6, // Hoàng Thị E
                FullName = "Hoàng Thị E",
                DateOfBirth = new DateTime(2001, 8, 20, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nu",
                Phone = "0901000006",
                Email = "hoangthie@gmail.com",
                Address = "456 Lê Lợi, Q3, TP.HCM",
                CreatedAt = new DateTime(2024, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 20, 0, 0, 0, DateTimeKind.Utc)
            },
            new Student
            {
                StudentId = 3,
                UserId = 7, // Vũ Văn G
                FullName = "Vũ Văn G",
                DateOfBirth = new DateTime(1999, 12, 1, 0, 0, 0, DateTimeKind.Utc),
                Gender = "Nam",
                Phone = "0901000007",
                Email = "vuvang@gmail.com",
                Address = "789 Trần Hưng Đạo, Q5, TP.HCM",
                CreatedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // Seed enrollments (matching ClassIds from CourseService)
        modelBuilder.Entity<Enrollment>().HasData(
            new Enrollment
            {
                EnrollmentId = 1,
                StudentId = 1, // Phạm Văn D
                ClassId = 1,   // TA-CB-01
                Status = "DangHoc",
                EnrolledAt = new DateTime(2024, 2, 20, 0, 0, 0, DateTimeKind.Utc)
            },
            new Enrollment
            {
                EnrollmentId = 2,
                StudentId = 2, // Hoàng Thị E
                ClassId = 1,   // TA-CB-01
                Status = "DangHoc",
                EnrolledAt = new DateTime(2024, 2, 22, 0, 0, 0, DateTimeKind.Utc)
            },
            new Enrollment
            {
                EnrollmentId = 3,
                StudentId = 3, // Vũ Văn G
                ClassId = 3,   // TOEIC-01
                Status = "DangHoc",
                EnrolledAt = new DateTime(2024, 3, 20, 0, 0, 0, DateTimeKind.Utc)
            },
            new Enrollment
            {
                EnrollmentId = 4,
                StudentId = 1, // Phạm Văn D
                ClassId = 4,   // PY-01
                Status = "DangHoc",
                EnrolledAt = new DateTime(2024, 4, 20, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // Seed attendance records
        modelBuilder.Entity<Attendance>().HasData(
            new Attendance { AttendanceId = 1, EnrollmentId = 1, SessionDate = new DateTime(2024, 3, 4, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2024, 3, 4, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 2, EnrollmentId = 1, SessionDate = new DateTime(2024, 3, 6, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2024, 3, 6, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 3, EnrollmentId = 1, SessionDate = new DateTime(2024, 3, 8, 0, 0, 0, DateTimeKind.Utc), Status = "DiTre", Note = "Trễ 10 phút", MarkedByTeacherId = 2, CreatedAt = new DateTime(2024, 3, 8, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 4, EnrollmentId = 2, SessionDate = new DateTime(2024, 3, 4, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2024, 3, 4, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 5, EnrollmentId = 2, SessionDate = new DateTime(2024, 3, 6, 0, 0, 0, DateTimeKind.Utc), Status = "Vang", Note = "Không phép", MarkedByTeacherId = 2, CreatedAt = new DateTime(2024, 3, 6, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 6, EnrollmentId = 3, SessionDate = new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc), Status = "CoMat", MarkedByTeacherId = 2, CreatedAt = new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Attendance { AttendanceId = 7, EnrollmentId = 3, SessionDate = new DateTime(2024, 4, 4, 0, 0, 0, DateTimeKind.Utc), Status = "CoPhep", Note = "Xin phép nghỉ", MarkedByTeacherId = 2, CreatedAt = new DateTime(2024, 4, 4, 0, 0, 0, DateTimeKind.Utc) }
        );

        // Seed exam results
        modelBuilder.Entity<ExamResult>().HasData(
            new ExamResult { ResultId = 1, EnrollmentId = 1, ExamType = "KiemTra", Score = 8.5m, Note = "Bài kiểm tra 15 phút", GradedByTeacherId = 2, ExamDate = new DateTime(2024, 3, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2024, 3, 15, 0, 0, 0, DateTimeKind.Utc) },
            new ExamResult { ResultId = 2, EnrollmentId = 1, ExamType = "GiuaKy", Score = 7.0m, Note = "Kiểm tra giữa kỳ", GradedByTeacherId = 2, ExamDate = new DateTime(2024, 4, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2024, 4, 15, 0, 0, 0, DateTimeKind.Utc) },
            new ExamResult { ResultId = 3, EnrollmentId = 2, ExamType = "KiemTra", Score = 9.0m, GradedByTeacherId = 2, ExamDate = new DateTime(2024, 3, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2024, 3, 15, 0, 0, 0, DateTimeKind.Utc) },
            new ExamResult { ResultId = 4, EnrollmentId = 3, ExamType = "KiemTra", Score = 6.5m, Note = "Cần cải thiện phần Listening", GradedByTeacherId = 2, ExamDate = new DateTime(2024, 4, 20, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2024, 4, 20, 0, 0, 0, DateTimeKind.Utc) },
            new ExamResult { ResultId = 5, EnrollmentId = 4, ExamType = "KiemTra", Score = 9.5m, Note = "Xuất sắc", GradedByTeacherId = 3, ExamDate = new DateTime(2024, 5, 10, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2024, 5, 10, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
