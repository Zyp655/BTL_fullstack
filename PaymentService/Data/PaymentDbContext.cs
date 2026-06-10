using Microsoft.EntityFrameworkCore;
using PaymentService.Models;

namespace PaymentService.Data;

public class PaymentDbContext : DbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
    }

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
                PasswordHash = "$2a$11$7wHWG7dZnyt5WV1Gwe6PK.weH5mleI3aSegpTfqGNr5UdMvha6URC",
                FullName = "Quản trị viên",
                Email = "admin@trainingcenter.vn",
                Phone = "0901000001",
                Role = "Admin",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
new User
            {
                UserId = 2,
                Username = "nguyenvanan",
                PasswordHash = "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2",
                FullName = "Nguyễn Văn An",
                Email = "nguyenvanan@trainingcenter.vn",
                Phone = "0901000002",
                Role = "GiaoVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 3,
                Username = "tranthibinh",
                PasswordHash = "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2",
                FullName = "Trần Thị Bình",
                Email = "tranthibinh@trainingcenter.vn",
                Phone = "0901000003",
                Role = "GiaoVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 4,
                Username = "levancuong",
                PasswordHash = "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2",
                FullName = "Lê Văn Cường",
                Email = "levancuong@trainingcenter.vn",
                Phone = "0901000004",
                Role = "GiaoVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 5,
                Username = "lethihoa",
                PasswordHash = "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2",
                FullName = "Lê Thị Hoa",
                Email = "lethihoa@trainingcenter.vn",
                Phone = "0901000008",
                Role = "GiaoVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 6,
                Username = "phamvankhanh",
                PasswordHash = "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2",
                FullName = "Phạm Văn Khánh",
                Email = "phamvankhanh@trainingcenter.vn",
                Phone = "0901000009",
                Role = "GiaoVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 7,
                Username = "tranthilan",
                PasswordHash = "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2",
                FullName = "Trần Thị Lan",
                Email = "tranthilan@trainingcenter.vn",
                Phone = "0901000010",
                Role = "GiaoVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 8,
                Username = "nguyenhoangnam",
                PasswordHash = "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2",
                FullName = "Nguyễn Hoàng Nam",
                Email = "nguyenhoangnam@trainingcenter.vn",
                Phone = "0901000021",
                Role = "GiaoVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 9,
                Username = "tranthimai",
                PasswordHash = "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2",
                FullName = "Trần Thị Mai",
                Email = "tranthimai@trainingcenter.vn",
                Phone = "0901000022",
                Role = "GiaoVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 10,
                Username = "phamvietanh",
                PasswordHash = "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2",
                FullName = "Phạm Việt Anh",
                Email = "phamvietanh@trainingcenter.vn",
                Phone = "0901000023",
                Role = "GiaoVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 11,
                Username = "hoangducduy",
                PasswordHash = "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2",
                FullName = "Hoàng Đức Duy",
                Email = "hoangducduy@trainingcenter.vn",
                Phone = "0901000024",
                Role = "GiaoVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 12,
                Username = "phamvandung",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phạm Văn Dũng",
                Email = "phamvandung@gmail.com",
                Phone = "0901000005",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 13,
                Username = "hoangthimai",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Hoàng Thị Mai",
                Email = "hoangthimai@gmail.com",
                Phone = "0901000006",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 14,
                Username = "vuvangiang",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Vũ Văn Giang",
                Email = "vuvangiang@gmail.com",
                Phone = "0901000007",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 15,
                Username = "nguyenvanminh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Nguyễn Văn Minh",
                Email = "nguyenvanminh@gmail.com",
                Phone = "0901000011",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 16,
                Username = "hoangthinga",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Hoàng Thị Nga",
                Email = "hoangthinga@gmail.com",
                Phone = "0901000012",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 17,
                Username = "vuvanhai",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Vũ Văn Hải",
                Email = "vuvanhai@gmail.com",
                Phone = "0901000013",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 18,
                Username = "lethiphuong",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lê Thị Phương",
                Email = "lethiphuong@gmail.com",
                Phone = "0901000014",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 19,
                Username = "tranquocquan",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Trần Quốc Quân",
                Email = "tranquocquan@gmail.com",
                Phone = "0901000015",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 20,
                Username = "nguyenhoangtien",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Nguyễn Hoàng Tiến",
                Email = "nguyenhoangtien@gmail.com",
                Phone = "0933341057",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 21,
                Username = "nguyenthiphuong",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Nguyễn Thị Phương",
                Email = "nguyenthiphuong@gmail.com",
                Phone = "0934903402",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 22,
                Username = "phamphuongchi",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phạm Phương Chi",
                Email = "phamphuongchi@gmail.com",
                Phone = "0941109031",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 23,
                Username = "tranngocnhi",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Trần Ngọc Nhi",
                Email = "tranngocnhi@gmail.com",
                Phone = "0917022674",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 24,
                Username = "phanminhlong",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phan Minh Long",
                Email = "phanminhlong@gmail.com",
                Phone = "0947067228",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 25,
                Username = "phamminhkien",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phạm Minh Kiên",
                Email = "phamminhkien@gmail.com",
                Phone = "0948606962",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 26,
                Username = "dangquoclong",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Đặng Quốc Long",
                Email = "dangquoclong@gmail.com",
                Phone = "0933741438",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 27,
                Username = "ngovietgiang",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Ngô Việt Giang",
                Email = "ngovietgiang@gmail.com",
                Phone = "0931538552",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 28,
                Username = "phamphuongnhi",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phạm Phương Nhi",
                Email = "phamphuongnhi@gmail.com",
                Phone = "0983396987",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 29,
                Username = "dangkhanhlan",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Đặng Khánh Lan",
                Email = "dangkhanhlan@gmail.com",
                Phone = "0933320821",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 30,
                Username = "lyhuuphuc",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lý Hữu Phúc",
                Email = "lyhuuphuc@gmail.com",
                Phone = "0917455324",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 31,
                Username = "ngothanhdat",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Ngô Thành Đạt",
                Email = "ngothanhdat@gmail.com",
                Phone = "0962871534",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 32,
                Username = "buithutuyet",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Bùi Thu Tuyết",
                Email = "buithutuyet@gmail.com",
                Phone = "0916007072",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 33,
                Username = "dangviettuan",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Đặng Việt Tuấn",
                Email = "dangviettuan@gmail.com",
                Phone = "0902876828",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 34,
                Username = "trananhminh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Trần Anh Minh",
                Email = "trananhminh@gmail.com",
                Phone = "0923154051",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 35,
                Username = "phamquynhthao",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phạm Quỳnh Thảo",
                Email = "phamquynhthao@gmail.com",
                Phone = "0947693979",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 36,
                Username = "vuvanbach",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Vũ Văn Bách",
                Email = "vuvanbach@gmail.com",
                Phone = "0934694634",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 37,
                Username = "vuminhhuy",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Vũ Minh Huy",
                Email = "vuminhhuy@gmail.com",
                Phone = "0935672073",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 38,
                Username = "phamphuonganh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phạm Phương Anh",
                Email = "phamphuonganh@gmail.com",
                Phone = "0932582524",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 39,
                Username = "ngominhgiang",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Ngô Minh Giang",
                Email = "ngominhgiang@gmail.com",
                Phone = "0976692553",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 40,
                Username = "lekimhoa",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lê Kim Hoa",
                Email = "lekimhoa@gmail.com",
                Phone = "0932264748",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 41,
                Username = "lyduchung",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lý Đức Hùng",
                Email = "lyduchung@gmail.com",
                Phone = "0979147706",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 42,
                Username = "phanhoangviet",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phan Hoàng Việt",
                Email = "phanhoangviet@gmail.com",
                Phone = "0948096887",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 43,
                Username = "duongvietgiang",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Dương Việt Giang",
                Email = "duongvietgiang@gmail.com",
                Phone = "0908999183",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 44,
                Username = "ngoduckien",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Ngô Đức Kiên",
                Email = "ngoduckien@gmail.com",
                Phone = "0915130808",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 45,
                Username = "hoanghongtrinh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Hoàng Hồng Trinh",
                Email = "hoanghongtrinh@gmail.com",
                Phone = "0935456272",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 46,
                Username = "nguyenanhphuc",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Nguyễn Anh Phúc",
                Email = "nguyenanhphuc@gmail.com",
                Phone = "0912229110",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 47,
                Username = "vuhoanghung",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Vũ Hoàng Hùng",
                Email = "vuhoanghung@gmail.com",
                Phone = "0986075395",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 48,
                Username = "lehoanghai",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lê Hoàng Hải",
                Email = "lehoanghai@gmail.com",
                Phone = "0913607983",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 49,
                Username = "buiphuonghanh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Bùi Phương Hạnh",
                Email = "buiphuonghanh@gmail.com",
                Phone = "0902548511",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 50,
                Username = "lephuongdung",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lê Phương Dung",
                Email = "lephuongdung@gmail.com",
                Phone = "0971162230",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 51,
                Username = "dangquynhlinh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Đặng Quỳnh Linh",
                Email = "dangquynhlinh@gmail.com",
                Phone = "0973138181",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 52,
                Username = "phamhongvy",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phạm Hồng Vy",
                Email = "phamhongvy@gmail.com",
                Phone = "0967817881",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 53,
                Username = "nguyenthutrinh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Nguyễn Thu Trinh",
                Email = "nguyenthutrinh@gmail.com",
                Phone = "0975163555",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 54,
                Username = "phamhonghoa",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phạm Hồng Hoa",
                Email = "phamhonghoa@gmail.com",
                Phone = "0966120253",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 55,
                Username = "trankimlan",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Trần Kim Lan",
                Email = "trankimlan@gmail.com",
                Phone = "0979996207",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 56,
                Username = "nguyenngocbich",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Nguyễn Ngọc Bích",
                Email = "nguyenngocbich@gmail.com",
                Phone = "0976799667",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 57,
                Username = "hoangvannam",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Hoàng Văn Nam",
                Email = "hoangvannam@gmail.com",
                Phone = "0909722815",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 58,
                Username = "ngongocquynh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Ngô Ngọc Quỳnh",
                Email = "ngongocquynh@gmail.com",
                Phone = "0947851696",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 59,
                Username = "ngokhanhtrang",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Ngô Khánh Trang",
                Email = "ngokhanhtrang@gmail.com",
                Phone = "0947812810",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 60,
                Username = "dophuonghoa",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Đỗ Phương Hoa",
                Email = "dophuonghoa@gmail.com",
                Phone = "0939576076",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 61,
                Username = "tranhongquynh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Trần Hồng Quỳnh",
                Email = "tranhongquynh@gmail.com",
                Phone = "0934340845",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 62,
                Username = "phanthanhthao",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phan Thanh Thảo",
                Email = "phanthanhthao@gmail.com",
                Phone = "0979294272",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 63,
                Username = "phamthutuyet",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phạm Thu Tuyết",
                Email = "phamthutuyet@gmail.com",
                Phone = "0981842524",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 64,
                Username = "lyphuongbich",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lý Phương Bích",
                Email = "lyphuongbich@gmail.com",
                Phone = "0978481188",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 65,
                Username = "lyquynhngoc",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lý Quỳnh Ngọc",
                Email = "lyquynhngoc@gmail.com",
                Phone = "0935606980",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 66,
                Username = "buingoclinh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Bùi Ngọc Linh",
                Email = "buingoclinh@gmail.com",
                Phone = "0924879923",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 67,
                Username = "vuquynhhoa",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Vũ Quỳnh Hoa",
                Email = "vuquynhhoa@gmail.com",
                Phone = "0972044645",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 68,
                Username = "lyngockien",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lý Ngọc Kiên",
                Email = "lyngockien@gmail.com",
                Phone = "0981098919",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 69,
                Username = "doducdat",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Đỗ Đức Đạt",
                Email = "doducdat@gmail.com",
                Phone = "0979147720",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 70,
                Username = "doquocphuc",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Đỗ Quốc Phúc",
                Email = "doquocphuc@gmail.com",
                Phone = "0907610562",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 71,
                Username = "nguyenhoangkien",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Nguyễn Hoàng Kiên",
                Email = "nguyenhoangkien@gmail.com",
                Phone = "0964551070",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 72,
                Username = "tranphuongmai",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Trần Phương Mai",
                Email = "tranphuongmai@gmail.com",
                Phone = "0906871360",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 73,
                Username = "phamvanphuc",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phạm Văn Phúc",
                Email = "phamvanphuc@gmail.com",
                Phone = "0925002120",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 74,
                Username = "lytructrang",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lý Trúc Trang",
                Email = "lytructrang@gmail.com",
                Phone = "0913747534",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 75,
                Username = "phanhongphuong",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phan Hồng Phương",
                Email = "phanhongphuong@gmail.com",
                Phone = "0932709620",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 76,
                Username = "vuthanhnam",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Vũ Thành Nam",
                Email = "vuthanhnam@gmail.com",
                Phone = "0962156902",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 77,
                Username = "vuphuonglinh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Vũ Phương Linh",
                Email = "vuphuonglinh@gmail.com",
                Phone = "0973955087",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 78,
                Username = "phanthanhhanh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phan Thanh Hạnh",
                Email = "phanthanhhanh@gmail.com",
                Phone = "0965118725",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 79,
                Username = "vuthingoc",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Vũ Thị Ngọc",
                Email = "vuthingoc@gmail.com",
                Phone = "0964050766",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 80,
                Username = "buithituyet",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Bùi Thị Tuyết",
                Email = "buithituyet@gmail.com",
                Phone = "0932436347",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 81,
                Username = "ngophuonghoa",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Ngô Phương Hoa",
                Email = "ngophuonghoa@gmail.com",
                Phone = "0902561176",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 82,
                Username = "doquynhtuyet",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Đỗ Quỳnh Tuyết",
                Email = "doquynhtuyet@gmail.com",
                Phone = "0968138769",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 83,
                Username = "tranducphong",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Trần Đức Phong",
                Email = "tranducphong@gmail.com",
                Phone = "0919990305",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 84,
                Username = "tranhongquynh1",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Trần Hồng Quỳnh",
                Email = "tranhongquynh1@gmail.com",
                Phone = "0937054579",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 85,
                Username = "nguyenthanhtung",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Nguyễn Thành Tùng",
                Email = "nguyenthanhtung@gmail.com",
                Phone = "0929235577",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 86,
                Username = "lethihanh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lê Thị Hạnh",
                Email = "lethihanh@gmail.com",
                Phone = "0982914064",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 87,
                Username = "lequocbach",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lê Quốc Bách",
                Email = "lequocbach@gmail.com",
                Phone = "0942429098",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 88,
                Username = "lythanhkien",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lý Thành Kiên",
                Email = "lythanhkien@gmail.com",
                Phone = "0988427625",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 89,
                Username = "lyducthinh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lý Đức Thịnh",
                Email = "lyducthinh@gmail.com",
                Phone = "0935440025",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 90,
                Username = "phananhphuc",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phan Anh Phúc",
                Email = "phananhphuc@gmail.com",
                Phone = "0985886520",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 91,
                Username = "ngoducdat",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Ngô Đức Đạt",
                Email = "ngoducdat@gmail.com",
                Phone = "0938132648",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 92,
                Username = "tranvanhung",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Trần Văn Hùng",
                Email = "tranvanhung@gmail.com",
                Phone = "0945842082",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 93,
                Username = "buiquynhngoc",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Bùi Quỳnh Ngọc",
                Email = "buiquynhngoc@gmail.com",
                Phone = "0982349590",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 94,
                Username = "phamngocbach",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Phạm Ngọc Bách",
                Email = "phamngocbach@gmail.com",
                Phone = "0905521644",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 95,
                Username = "danghuuthinh",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Đặng Hữu Thịnh",
                Email = "danghuuthinh@gmail.com",
                Phone = "0982530793",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 96,
                Username = "vuhuutuan",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Vũ Hữu Tuấn",
                Email = "vuhuutuan@gmail.com",
                Phone = "0947718473",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 97,
                Username = "buithidung",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Bùi Thị Dung",
                Email = "buithidung@gmail.com",
                Phone = "0987933143",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 98,
                Username = "lythithao",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Lý Thị Thảo",
                Email = "lythithao@gmail.com",
                Phone = "0943198037",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 99,
                Username = "duongquoctung",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Dương Quốc Tùng",
                Email = "duongquoctung@gmail.com",
                Phone = "0907844947",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 100,
                Username = "dohoanghuy",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Đỗ Hoàng Huy",
                Email = "dohoanghuy@gmail.com",
                Phone = "0947970771",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                UserId = 101,
                Username = "dangthanhquan",
                PasswordHash = "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS",
                FullName = "Đặng Thành Quân",
                Email = "dangthanhquan@gmail.com",
                Phone = "0915634662",
                Role = "HocVien",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // Seed payments
        modelBuilder.Entity<Payment>().HasData(
            new Payment { PaymentId = 1, StudentUserId = 12, ClassId = 1, TotalAmount = 2500000, PaidAmount = 2500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 2, 20, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 2, StudentUserId = 13, ClassId = 1, TotalAmount = 2500000, PaidAmount = 2500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 2, 22, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 3, 5, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 3, StudentUserId = 14, ClassId = 1, TotalAmount = 2500000, PaidAmount = 2500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 2, 20, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 3, 10, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 4, StudentUserId = 15, ClassId = 1, TotalAmount = 2500000, PaidAmount = 2500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 2, 25, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 3, 12, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 5, StudentUserId = 16, ClassId = 1, TotalAmount = 2500000, PaidAmount = 2500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 3, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 2, 25, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc) },

            new Payment { PaymentId = 6, StudentUserId = 17, ClassId = 2, TotalAmount = 2500000, PaidAmount = 2500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 7, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 6, 15, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 9, 5, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 7, StudentUserId = 18, ClassId = 2, TotalAmount = 2500000, PaidAmount = 2500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 7, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 6, 16, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 10, 2, 0, 0, 0, DateTimeKind.Utc) },

            new Payment { PaymentId = 8, StudentUserId = 12, ClassId = 3, TotalAmount = 3500000, PaidAmount = 3500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 4, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 3, 20, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 4, 2, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 9, StudentUserId = 14, ClassId = 3, TotalAmount = 3500000, PaidAmount = 3500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 4, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 3, 20, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 4, 5, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 10, StudentUserId = 16, ClassId = 3, TotalAmount = 3500000, PaidAmount = 3500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 4, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 3, 22, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 4, 2, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 11, StudentUserId = 19, ClassId = 3, TotalAmount = 3500000, PaidAmount = 0, RemainingAmount = 3500000, Status = "ChuaTT", DueDate = new DateTime(2026, 4, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 3, 25, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 3, 25, 0, 0, 0, DateTimeKind.Utc) },

            new Payment { PaymentId = 12, StudentUserId = 13, ClassId = 4, TotalAmount = 3000000, PaidAmount = 3000000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 5, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 4, 20, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 5, 2, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 13, StudentUserId = 15, ClassId = 4, TotalAmount = 3000000, PaidAmount = 3000000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 5, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 4, 22, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 5, 5, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 14, StudentUserId = 17, ClassId = 4, TotalAmount = 3000000, PaidAmount = 3000000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 5, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 6, 5, 0, 0, 0, DateTimeKind.Utc) },

            new Payment { PaymentId = 15, StudentUserId = 12, ClassId = 5, TotalAmount = 4500000, PaidAmount = 4500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 5, 30, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 5, 1, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 5, 10, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 16, StudentUserId = 13, ClassId = 5, TotalAmount = 4500000, PaidAmount = 4500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 5, 30, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 5, 2, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 5, 12, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 17, StudentUserId = 14, ClassId = 5, TotalAmount = 4500000, PaidAmount = 4500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 5, 30, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 5, 3, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 5, 14, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 18, StudentUserId = 15, ClassId = 5, TotalAmount = 4500000, PaidAmount = 0, RemainingAmount = 4500000, Status = "ChuaTT", DueDate = new DateTime(2026, 5, 30, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 5, 4, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 5, 4, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 19, StudentUserId = 16, ClassId = 5, TotalAmount = 4500000, PaidAmount = 4500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 5, 30, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 5, 5, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 5, 10, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 20, StudentUserId = 17, ClassId = 5, TotalAmount = 4500000, PaidAmount = 0, RemainingAmount = 4500000, Status = "ChuaTT", DueDate = new DateTime(2026, 5, 30, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 5, 6, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 5, 6, 0, 0, 0, DateTimeKind.Utc) },

            new Payment { PaymentId = 21, StudentUserId = 18, ClassId = 6, TotalAmount = 1500000, PaidAmount = 1500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 7, 25, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 6, 25, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 7, 5, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 22, StudentUserId = 19, ClassId = 6, TotalAmount = 1500000, PaidAmount = 1500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 7, 25, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 6, 26, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc) },

            new Payment { PaymentId = 23, StudentUserId = 14, ClassId = 7, TotalAmount = 2800000, PaidAmount = 2800000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 8, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 7, 15, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 8, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 24, StudentUserId = 15, ClassId = 7, TotalAmount = 2800000, PaidAmount = 2800000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 8, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 7, 16, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 8, 3, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 25, StudentUserId = 16, ClassId = 7, TotalAmount = 2800000, PaidAmount = 2800000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 8, 15, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 7, 17, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 8, 20, 0, 0, 0, DateTimeKind.Utc) },

            new Payment { PaymentId = 26, StudentUserId = 17, ClassId = 8, TotalAmount = 3800000, PaidAmount = 3800000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 5, 25, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 4, 25, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 5, 11, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 27, StudentUserId = 18, ClassId = 8, TotalAmount = 3800000, PaidAmount = 3800000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 5, 25, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 4, 26, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 5, 13, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 28, StudentUserId = 19, ClassId = 8, TotalAmount = 3800000, PaidAmount = 0, RemainingAmount = 3800000, Status = "ChuaTT", DueDate = new DateTime(2026, 5, 25, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 4, 27, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 30, StudentUserId = 18, ClassId = 28, TotalAmount = 4500000, PaidAmount = 4500000, RemainingAmount = 0, Status = "HoanTat", DueDate = new DateTime(2026, 7, 30, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc) },
            new Payment { PaymentId = 31, StudentUserId = 19, ClassId = 28, TotalAmount = 4500000, PaidAmount = 0, RemainingAmount = 4500000, Status = "ChuaTT", DueDate = new DateTime(2026, 7, 30, 0, 0, 0, DateTimeKind.Utc), CreatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc), UpdatedAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc) }
        );

        // Seed transactions
        modelBuilder.Entity<PaymentTransaction>().HasData(
            new PaymentTransaction { TransactionId = 1, PaymentId = 1, Amount = 2500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí TA-CB-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 2, PaymentId = 2, Amount = 2500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí TA-CB-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 3, 5, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 3, PaymentId = 3, Amount = 2500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí TA-CB-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 3, 10, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 4, PaymentId = 4, Amount = 2500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí TA-CB-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 3, 12, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 5, PaymentId = 6, Amount = 2500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí TA-CB-02", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 9, 5, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 6, PaymentId = 7, Amount = 2500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí TA-CB-02", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 10, 2, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 7, PaymentId = 8, Amount = 3500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí TOEIC-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 4, 2, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 8, PaymentId = 9, Amount = 3500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí TOEIC-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 4, 5, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 9, PaymentId = 12, Amount = 3000000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí PY-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 5, 2, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 10, PaymentId = 13, Amount = 3000000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí PY-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 5, 5, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 11, PaymentId = 14, Amount = 3000000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí PY-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 6, 5, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 12, PaymentId = 15, Amount = 4500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí FS-REACT-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 5, 10, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 13, PaymentId = 16, Amount = 4500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí FS-REACT-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 5, 12, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 14, PaymentId = 17, Amount = 4500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí FS-REACT-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 5, 14, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 15, PaymentId = 21, Amount = 1500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí KN-QLTG-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 7, 5, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 16, PaymentId = 22, Amount = 1500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí KN-QLTG-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 7, 10, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 17, PaymentId = 23, Amount = 2800000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí JP-N5-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 8, 1, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 18, PaymentId = 24, Amount = 2800000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí JP-N5-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 8, 3, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 19, PaymentId = 25, Amount = 2800000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí JP-N5-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 8, 20, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 20, PaymentId = 26, Amount = 3800000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí VUEJS-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 5, 11, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 21, PaymentId = 27, Amount = 3800000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí VUEJS-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 5, 13, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 22, PaymentId = 5, Amount = 2500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí Lập trình Web với React & Node.js", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 23, PaymentId = 10, Amount = 3500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí TOEIC 600+", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 4, 2, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 24, PaymentId = 19, Amount = 4500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí Tiếng Anh giao tiếp cơ bản", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 5, 10, 0, 0, 0, DateTimeKind.Utc) },
            new PaymentTransaction { TransactionId = 28, PaymentId = 30, Amount = 4500000, PaymentMethod = "ChuyenKhoan", Note = "Thanh toán học phí CS-NC-01", ReceivedByUserId = 1, PaidAt = new DateTime(2026, 6, 10, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
