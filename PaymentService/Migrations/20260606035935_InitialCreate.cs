using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PaymentService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentUserId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Users_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReceivedByUserId = table.Column<int>(type: "int", nullable: true),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "Phone", "Role", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@trainingcenter.vn", "Quản trị viên", true, "$2a$11$7wHWG7dZnyt5WV1Gwe6PK.weH5mleI3aSegpTfqGNr5UdMvha6URC", "0901000001", "Admin", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenvanan@trainingcenter.vn", "Nguyễn Văn An", true, "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2", "0901000002", "GiaoVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenvanan" },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranthibinh@trainingcenter.vn", "Trần Thị Bình", true, "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2", "0901000003", "GiaoVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranthibinh" },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "levancuong@trainingcenter.vn", "Lê Văn Cường", true, "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2", "0901000004", "GiaoVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "levancuong" },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lethihoa@trainingcenter.vn", "Lê Thị Hoa", true, "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2", "0901000008", "GiaoVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lethihoa" },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamvankhanh@trainingcenter.vn", "Phạm Văn Khánh", true, "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2", "0901000009", "GiaoVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamvankhanh" },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranthilan@trainingcenter.vn", "Trần Thị Lan", true, "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2", "0901000010", "GiaoVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranthilan" },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenhoangnam@trainingcenter.vn", "Nguyễn Hoàng Nam", true, "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2", "0901000021", "GiaoVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenhoangnam" },
                    { 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranthimai@trainingcenter.vn", "Trần Thị Mai", true, "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2", "0901000022", "GiaoVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranthimai" },
                    { 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamvietanh@trainingcenter.vn", "Phạm Việt Anh", true, "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2", "0901000023", "GiaoVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamvietanh" },
                    { 11, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hoangducduy@trainingcenter.vn", "Hoàng Đức Duy", true, "$2a$11$hjgbS3EJUerHIvpo7pRUnu9OHI3aGgqwJn/vypDh18juRtuUj3fI2", "0901000024", "GiaoVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hoangducduy" },
                    { 12, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamvandung@gmail.com", "Phạm Văn Dũng", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0901000005", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamvandung" },
                    { 13, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hoangthimai@gmail.com", "Hoàng Thị Mai", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0901000006", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hoangthimai" },
                    { 14, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuvangiang@gmail.com", "Vũ Văn Giang", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0901000007", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuvangiang" },
                    { 15, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenvanminh@gmail.com", "Nguyễn Văn Minh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0901000011", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenvanminh" },
                    { 16, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hoangthinga@gmail.com", "Hoàng Thị Nga", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0901000012", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hoangthinga" },
                    { 17, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuvanhai@gmail.com", "Vũ Văn Hải", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0901000013", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuvanhai" },
                    { 18, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lethiphuong@gmail.com", "Lê Thị Phương", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0901000014", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lethiphuong" },
                    { 19, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranquocquan@gmail.com", "Trần Quốc Quân", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0901000015", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranquocquan" },
                    { 20, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenhoangtien@gmail.com", "Nguyễn Hoàng Tiến", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0933341057", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenhoangtien" },
                    { 21, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenthiphuong@gmail.com", "Nguyễn Thị Phương", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0934903402", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenthiphuong" },
                    { 22, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamphuongchi@gmail.com", "Phạm Phương Chi", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0941109031", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamphuongchi" },
                    { 23, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranngocnhi@gmail.com", "Trần Ngọc Nhi", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0917022674", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranngocnhi" },
                    { 24, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phanminhlong@gmail.com", "Phan Minh Long", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0947067228", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phanminhlong" },
                    { 25, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamminhkien@gmail.com", "Phạm Minh Kiên", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0948606962", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamminhkien" },
                    { 26, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dangquoclong@gmail.com", "Đặng Quốc Long", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0933741438", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dangquoclong" },
                    { 27, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngovietgiang@gmail.com", "Ngô Việt Giang", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0931538552", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngovietgiang" },
                    { 28, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamphuongnhi@gmail.com", "Phạm Phương Nhi", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0983396987", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamphuongnhi" },
                    { 29, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dangkhanhlan@gmail.com", "Đặng Khánh Lan", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0933320821", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dangkhanhlan" },
                    { 30, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lyhuuphuc@gmail.com", "Lý Hữu Phúc", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0917455324", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lyhuuphuc" },
                    { 31, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngothanhdat@gmail.com", "Ngô Thành Đạt", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0962871534", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngothanhdat" },
                    { 32, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "buithutuyet@gmail.com", "Bùi Thu Tuyết", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0916007072", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "buithutuyet" },
                    { 33, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dangviettuan@gmail.com", "Đặng Việt Tuấn", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0902876828", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dangviettuan" },
                    { 34, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "trananhminh@gmail.com", "Trần Anh Minh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0923154051", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "trananhminh" },
                    { 35, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamquynhthao@gmail.com", "Phạm Quỳnh Thảo", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0947693979", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamquynhthao" },
                    { 36, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuvanbach@gmail.com", "Vũ Văn Bách", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0934694634", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuvanbach" },
                    { 37, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuminhhuy@gmail.com", "Vũ Minh Huy", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0935672073", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuminhhuy" },
                    { 38, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamphuonganh@gmail.com", "Phạm Phương Anh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0932582524", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamphuonganh" },
                    { 39, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngominhgiang@gmail.com", "Ngô Minh Giang", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0976692553", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngominhgiang" },
                    { 40, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lekimhoa@gmail.com", "Lê Kim Hoa", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0932264748", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lekimhoa" },
                    { 41, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lyduchung@gmail.com", "Lý Đức Hùng", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0979147706", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lyduchung" },
                    { 42, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phanhoangviet@gmail.com", "Phan Hoàng Việt", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0948096887", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phanhoangviet" },
                    { 43, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "duongvietgiang@gmail.com", "Dương Việt Giang", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0908999183", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "duongvietgiang" },
                    { 44, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngoduckien@gmail.com", "Ngô Đức Kiên", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0915130808", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngoduckien" },
                    { 45, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hoanghongtrinh@gmail.com", "Hoàng Hồng Trinh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0935456272", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hoanghongtrinh" },
                    { 46, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenanhphuc@gmail.com", "Nguyễn Anh Phúc", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0912229110", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenanhphuc" },
                    { 47, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuhoanghung@gmail.com", "Vũ Hoàng Hùng", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0986075395", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuhoanghung" },
                    { 48, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lehoanghai@gmail.com", "Lê Hoàng Hải", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0913607983", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lehoanghai" },
                    { 49, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "buiphuonghanh@gmail.com", "Bùi Phương Hạnh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0902548511", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "buiphuonghanh" },
                    { 50, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lephuongdung@gmail.com", "Lê Phương Dung", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0971162230", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lephuongdung" },
                    { 51, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dangquynhlinh@gmail.com", "Đặng Quỳnh Linh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0973138181", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dangquynhlinh" },
                    { 52, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamhongvy@gmail.com", "Phạm Hồng Vy", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0967817881", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamhongvy" },
                    { 53, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenthutrinh@gmail.com", "Nguyễn Thu Trinh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0975163555", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenthutrinh" },
                    { 54, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamhonghoa@gmail.com", "Phạm Hồng Hoa", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0966120253", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamhonghoa" },
                    { 55, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "trankimlan@gmail.com", "Trần Kim Lan", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0979996207", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "trankimlan" },
                    { 56, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenngocbich@gmail.com", "Nguyễn Ngọc Bích", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0976799667", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenngocbich" },
                    { 57, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hoangvannam@gmail.com", "Hoàng Văn Nam", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0909722815", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hoangvannam" },
                    { 58, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngongocquynh@gmail.com", "Ngô Ngọc Quỳnh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0947851696", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngongocquynh" },
                    { 59, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngokhanhtrang@gmail.com", "Ngô Khánh Trang", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0947812810", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngokhanhtrang" },
                    { 60, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dophuonghoa@gmail.com", "Đỗ Phương Hoa", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0939576076", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dophuonghoa" },
                    { 61, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranhongquynh@gmail.com", "Trần Hồng Quỳnh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0934340845", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranhongquynh" },
                    { 62, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phanthanhthao@gmail.com", "Phan Thanh Thảo", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0979294272", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phanthanhthao" },
                    { 63, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamthutuyet@gmail.com", "Phạm Thu Tuyết", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0981842524", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamthutuyet" },
                    { 64, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lyphuongbich@gmail.com", "Lý Phương Bích", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0978481188", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lyphuongbich" },
                    { 65, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lyquynhngoc@gmail.com", "Lý Quỳnh Ngọc", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0935606980", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lyquynhngoc" },
                    { 66, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "buingoclinh@gmail.com", "Bùi Ngọc Linh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0924879923", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "buingoclinh" },
                    { 67, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuquynhhoa@gmail.com", "Vũ Quỳnh Hoa", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0972044645", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuquynhhoa" },
                    { 68, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lyngockien@gmail.com", "Lý Ngọc Kiên", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0981098919", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lyngockien" },
                    { 69, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "doducdat@gmail.com", "Đỗ Đức Đạt", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0979147720", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "doducdat" },
                    { 70, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "doquocphuc@gmail.com", "Đỗ Quốc Phúc", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0907610562", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "doquocphuc" },
                    { 71, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenhoangkien@gmail.com", "Nguyễn Hoàng Kiên", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0964551070", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenhoangkien" },
                    { 72, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranphuongmai@gmail.com", "Trần Phương Mai", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0906871360", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranphuongmai" },
                    { 73, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamvanphuc@gmail.com", "Phạm Văn Phúc", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0925002120", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamvanphuc" },
                    { 74, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lytructrang@gmail.com", "Lý Trúc Trang", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0913747534", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lytructrang" },
                    { 75, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phanhongphuong@gmail.com", "Phan Hồng Phương", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0932709620", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phanhongphuong" },
                    { 76, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuthanhnam@gmail.com", "Vũ Thành Nam", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0962156902", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuthanhnam" },
                    { 77, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuphuonglinh@gmail.com", "Vũ Phương Linh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0973955087", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuphuonglinh" },
                    { 78, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phanthanhhanh@gmail.com", "Phan Thanh Hạnh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0965118725", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phanthanhhanh" },
                    { 79, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuthingoc@gmail.com", "Vũ Thị Ngọc", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0964050766", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuthingoc" },
                    { 80, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "buithituyet@gmail.com", "Bùi Thị Tuyết", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0932436347", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "buithituyet" },
                    { 81, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngophuonghoa@gmail.com", "Ngô Phương Hoa", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0902561176", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngophuonghoa" },
                    { 82, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "doquynhtuyet@gmail.com", "Đỗ Quỳnh Tuyết", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0968138769", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "doquynhtuyet" },
                    { 83, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranducphong@gmail.com", "Trần Đức Phong", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0919990305", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranducphong" },
                    { 84, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranhongquynh1@gmail.com", "Trần Hồng Quỳnh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0937054579", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranhongquynh1" },
                    { 85, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenthanhtung@gmail.com", "Nguyễn Thành Tùng", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0929235577", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nguyenthanhtung" },
                    { 86, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lethihanh@gmail.com", "Lê Thị Hạnh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0982914064", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lethihanh" },
                    { 87, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lequocbach@gmail.com", "Lê Quốc Bách", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0942429098", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lequocbach" },
                    { 88, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lythanhkien@gmail.com", "Lý Thành Kiên", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0988427625", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lythanhkien" },
                    { 89, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lyducthinh@gmail.com", "Lý Đức Thịnh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0935440025", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lyducthinh" },
                    { 90, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phananhphuc@gmail.com", "Phan Anh Phúc", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0985886520", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phananhphuc" },
                    { 91, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngoducdat@gmail.com", "Ngô Đức Đạt", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0938132648", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ngoducdat" },
                    { 92, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranvanhung@gmail.com", "Trần Văn Hùng", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0945842082", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tranvanhung" },
                    { 93, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "buiquynhngoc@gmail.com", "Bùi Quỳnh Ngọc", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0982349590", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "buiquynhngoc" },
                    { 94, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamngocbach@gmail.com", "Phạm Ngọc Bách", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0905521644", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "phamngocbach" },
                    { 95, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "danghuuthinh@gmail.com", "Đặng Hữu Thịnh", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0982530793", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "danghuuthinh" },
                    { 96, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuhuutuan@gmail.com", "Vũ Hữu Tuấn", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0947718473", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "vuhuutuan" },
                    { 97, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "buithidung@gmail.com", "Bùi Thị Dung", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0987933143", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "buithidung" },
                    { 98, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lythithao@gmail.com", "Lý Thị Thảo", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0943198037", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "lythithao" },
                    { 99, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "duongquoctung@gmail.com", "Dương Quốc Tùng", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0907844947", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "duongquoctung" },
                    { 100, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dohoanghuy@gmail.com", "Đỗ Hoàng Huy", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0947970771", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dohoanghuy" },
                    { 101, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dangthanhquan@gmail.com", "Đặng Thành Quân", true, "$2a$11$vOY3o7J6ZuVj1k34/Ne7me.cbXdSO11UfXzIdGkBXU0eSa9RkoFSS", "0915634662", "HocVien", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "dangthanhquan" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "ClassId", "CreatedAt", "DueDate", "PaidAmount", "RemainingAmount", "Status", "StudentUserId", "TotalAmount", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2500000m, 0m, "HoanTat", 5, 2500000m, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2500000m, 0m, "HoanTat", 6, 2500000m, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 1, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2500000m, 0m, "HoanTat", 7, 2500000m, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 1, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2500000m, 0m, "HoanTat", 11, 2500000m, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 1, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, 2500000m, "ChuaTT", 12, 2500000m, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 2, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2500000m, 0m, "HoanTat", 13, 2500000m, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 2, new DateTime(2024, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2500000m, 0m, "HoanTat", 14, 2500000m, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 3, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3500000m, 0m, "HoanTat", 5, 3500000m, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 3, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3500000m, 0m, "HoanTat", 7, 3500000m, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 3, new DateTime(2024, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, 3500000m, "ChuaTT", 12, 3500000m, new DateTime(2024, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 3, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), 0m, 3500000m, "ChuaTT", 15, 3500000m, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 4, new DateTime(2024, 4, 20, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3000000m, 0m, "HoanTat", 6, 3000000m, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, 4, new DateTime(2024, 4, 22, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3000000m, 0m, "HoanTat", 11, 3000000m, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 4, new DateTime(2024, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3000000m, 0m, "HoanTat", 13, 3000000m, new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, 5, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Utc), 4500000m, 0m, "HoanTat", 5, 4500000m, new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 16, 5, new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Utc), 4500000m, 0m, "HoanTat", 6, 4500000m, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 17, 5, new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Utc), 4500000m, 0m, "HoanTat", 7, 4500000m, new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 18, 5, new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Utc), 0m, 4500000m, "ChuaTT", 11, 4500000m, new DateTime(2024, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 19, 5, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Utc), 0m, 4500000m, "ChuaTT", 12, 4500000m, new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 20, 5, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Utc), 0m, 4500000m, "ChuaTT", 13, 4500000m, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 21, 6, new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Utc), 1500000m, 0m, "HoanTat", 14, 1500000m, new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 22, 6, new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Utc), 1500000m, 0m, "HoanTat", 15, 1500000m, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 23, 7, new DateTime(2024, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2800000m, 0m, "HoanTat", 7, 2800000m, new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 24, 7, new DateTime(2024, 7, 16, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2800000m, 0m, "HoanTat", 11, 2800000m, new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 25, 7, new DateTime(2024, 7, 17, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2800000m, 0m, "HoanTat", 12, 2800000m, new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 26, 8, new DateTime(2024, 4, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Utc), 3800000m, 0m, "HoanTat", 13, 3800000m, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 27, 8, new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Utc), 3800000m, 0m, "HoanTat", 14, 3800000m, new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 28, 8, new DateTime(2024, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Utc), 0m, 3800000m, "ChuaTT", 15, 3800000m, new DateTime(2024, 4, 27, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "PaymentTransactions",
                columns: new[] { "TransactionId", "Amount", "Note", "PaidAt", "PaymentId", "PaymentMethod", "ReceivedByUserId" },
                values: new object[,]
                {
                    { 1, 2500000m, "Thanh toán học phí TA-CB-01", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "ChuyenKhoan", 1 },
                    { 2, 2500000m, "Thanh toán học phí TA-CB-01", new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), 2, "ChuyenKhoan", 1 },
                    { 3, 2500000m, "Thanh toán học phí TA-CB-01", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), 3, "ChuyenKhoan", 1 },
                    { 4, 2500000m, "Thanh toán học phí TA-CB-01", new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), 4, "ChuyenKhoan", 1 },
                    { 5, 2500000m, "Thanh toán học phí TA-CB-02", new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), 6, "ChuyenKhoan", 1 },
                    { 6, 2500000m, "Thanh toán học phí TA-CB-02", new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Utc), 7, "ChuyenKhoan", 1 },
                    { 7, 3500000m, "Thanh toán học phí TOEIC-01", new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Utc), 8, "ChuyenKhoan", 1 },
                    { 8, 3500000m, "Thanh toán học phí TOEIC-01", new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc), 9, "ChuyenKhoan", 1 },
                    { 9, 3000000m, "Thanh toán học phí PY-01", new DateTime(2024, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), 12, "ChuyenKhoan", 1 },
                    { 10, 3000000m, "Thanh toán học phí PY-01", new DateTime(2024, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), 13, "ChuyenKhoan", 1 },
                    { 11, 3000000m, "Thanh toán học phí PY-01", new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc), 14, "ChuyenKhoan", 1 },
                    { 12, 4500000m, "Thanh toán học phí FS-REACT-01", new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 15, "ChuyenKhoan", 1 },
                    { 13, 4500000m, "Thanh toán học phí FS-REACT-01", new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), 16, "ChuyenKhoan", 1 },
                    { 14, 4500000m, "Thanh toán học phí FS-REACT-01", new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), 17, "ChuyenKhoan", 1 },
                    { 15, 1500000m, "Thanh toán học phí KN-QLTG-01", new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Utc), 21, "ChuyenKhoan", 1 },
                    { 16, 1500000m, "Thanh toán học phí KN-QLTG-01", new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), 22, "ChuyenKhoan", 1 },
                    { 17, 2800000m, "Thanh toán học phí JP-N5-01", new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 23, "ChuyenKhoan", 1 },
                    { 18, 2800000m, "Thanh toán học phí JP-N5-01", new DateTime(2024, 8, 3, 0, 0, 0, 0, DateTimeKind.Utc), 24, "ChuyenKhoan", 1 },
                    { 19, 2800000m, "Thanh toán học phí JP-N5-01", new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Utc), 25, "ChuyenKhoan", 1 },
                    { 20, 3800000m, "Thanh toán học phí VUEJS-01", new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc), 26, "ChuyenKhoan", 1 },
                    { 21, 3800000m, "Thanh toán học phí VUEJS-01", new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc), 27, "ChuyenKhoan", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClassId",
                table: "Payments",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Status",
                table: "Payments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StudentUserId",
                table: "Payments",
                column: "StudentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_PaymentId",
                table: "PaymentTransactions",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsActive",
                table: "Users",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role",
                table: "Users",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
