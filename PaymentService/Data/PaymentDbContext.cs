using Microsoft.EntityFrameworkCore;
using PaymentService.Models;

namespace PaymentService.Data;

public class PaymentDbContext : DbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<PaymentTransaction> PaymentTransactions => Set<PaymentTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Role);
            entity.HasIndex(e => e.IsActive);
        });

        // Payment
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasIndex(e => e.StudentUserId);
            entity.HasIndex(e => e.ClassId);
            entity.HasIndex(e => e.Status);

            entity.HasOne(e => e.StudentUser)
                  .WithMany(u => u.Payments)
                  .HasForeignKey(e => e.StudentUserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // PaymentTransaction
        modelBuilder.Entity<PaymentTransaction>(entity =>
        {
            entity.HasIndex(e => e.PaymentId);

            entity.HasOne(e => e.Payment)
                  .WithMany(p => p.Transactions)
                  .HasForeignKey(e => e.PaymentId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Admin account (password: admin123)
        modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                FullName = "Quản trị viên",
                Email = "admin@trainingcenter.vn",
                Phone = "0901000001",
                Role = "Admin",
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 2,
                Username = "nguyenvana",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("teacher123"),
                FullName = "Nguyễn Văn A",
                Email = "nguyenvana@trainingcenter.vn",
                Phone = "0901000002",
                Role = "GiaoVien",
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 3,
                Username = "tranthib",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("teacher123"),
                FullName = "Trần Thị B",
                Email = "tranthib@trainingcenter.vn",
                Phone = "0901000003",
                Role = "GiaoVien",
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 4,
                Username = "levanc",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("teacher123"),
                FullName = "Lê Văn C",
                Email = "levanc@trainingcenter.vn",
                Phone = "0901000004",
                Role = "GiaoVien",
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 5,
                Username = "phamvand",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("student123"),
                FullName = "Phạm Văn D",
                Email = "phamvand@gmail.com",
                Phone = "0901000005",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 6,
                Username = "hoangthie",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("student123"),
                FullName = "Hoàng Thị E",
                Email = "hoangthie@gmail.com",
                Phone = "0901000006",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 7,
                Username = "vuvang",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("student123"),
                FullName = "Vũ Văn G",
                Email = "vuvang@gmail.com",
                Phone = "0901000007",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // Seed payments
        modelBuilder.Entity<Payment>().HasData(
            new Payment
            {
                PaymentId = 1,
                StudentUserId = 5,
                ClassId = 1,
                TotalAmount = 2500000,
                PaidAmount = 2500000,
                RemainingAmount = 0,
                Status = "HoanTat",
                DueDate = new DateTime(2024, 3, 15, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 2, 20, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                PaymentId = 2,
                StudentUserId = 6,
                ClassId = 1,
                TotalAmount = 2500000,
                PaidAmount = 1500000,
                RemainingAmount = 1000000,
                Status = "DangTT",
                DueDate = new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 2, 25, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 3, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                PaymentId = 3,
                StudentUserId = 7,
                ClassId = 3,
                TotalAmount = 3500000,
                PaidAmount = 0,
                RemainingAmount = 3500000,
                Status = "ChuaTT",
                DueDate = new DateTime(2024, 4, 15, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 3, 20, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 3, 20, 0, 0, 0, DateTimeKind.Utc)
            },
            new Payment
            {
                PaymentId = 4,
                StudentUserId = 5,
                ClassId = 4,
                TotalAmount = 3000000,
                PaidAmount = 3000000,
                RemainingAmount = 0,
                Status = "HoanTat",
                DueDate = new DateTime(2024, 5, 15, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = new DateTime(2024, 4, 20, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 5, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // Seed transactions
        modelBuilder.Entity<PaymentTransaction>().HasData(
            new PaymentTransaction
            {
                TransactionId = 1,
                PaymentId = 1,
                Amount = 2500000,
                PaymentMethod = "ChuyenKhoan",
                Note = "Thanh toán đầy đủ",
                ReceivedByUserId = 1,
                PaidAt = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new PaymentTransaction
            {
                TransactionId = 2,
                PaymentId = 2,
                Amount = 1000000,
                PaymentMethod = "TienMat",
                Note = "Đợt 1",
                ReceivedByUserId = 1,
                PaidAt = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new PaymentTransaction
            {
                TransactionId = 3,
                PaymentId = 2,
                Amount = 500000,
                PaymentMethod = "ChuyenKhoan",
                Note = "Đợt 2",
                ReceivedByUserId = 1,
                PaidAt = new DateTime(2024, 3, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new PaymentTransaction
            {
                TransactionId = 4,
                PaymentId = 4,
                Amount = 3000000,
                PaymentMethod = "TheTD",
                Note = "Thanh toán đầy đủ bằng thẻ",
                ReceivedByUserId = 1,
                PaidAt = new DateTime(2024, 5, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
