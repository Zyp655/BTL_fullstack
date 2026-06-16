using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseService.Migrations
{
    /// <inheritdoc />
    public partial class AddSecondaryTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId2",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherName2",
                table: "Classes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 1,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 2,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 3,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 4,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 5,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 6,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 7,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 8,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 28,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 101,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 102,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 103,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 104,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 105,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 106,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 107,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 108,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 109,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 110,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 111,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 112,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 113,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 114,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 115,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 116,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 117,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 118,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 119,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 120,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 121,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 122,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 123,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 124,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 125,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 126,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 127,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 128,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 129,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 130,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 131,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 132,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 133,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 134,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 135,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 136,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 137,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 138,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 139,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 140,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 141,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 142,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 143,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 144,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 145,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 146,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 147,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 148,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 149,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 150,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 151,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 152,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 153,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 154,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 155,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 156,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 157,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 158,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 159,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 160,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 161,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 162,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 163,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 164,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 165,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 166,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 167,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 168,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 169,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 170,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 171,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 172,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 173,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 174,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 175,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 176,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 177,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 178,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 179,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 180,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 181,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 182,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 183,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 184,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 185,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 186,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 187,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 188,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 189,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 190,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 191,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 192,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 193,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 194,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 195,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 196,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 197,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 198,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 199,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 200,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 201,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 202,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 203,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 204,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 205,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 206,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 207,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 208,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 209,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 210,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 211,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 212,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 213,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 214,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 215,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 216,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 217,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 218,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 219,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: 220,
                columns: new[] { "TeacherId2", "TeacherName2" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherId2",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "TeacherName2",
                table: "Classes");
        }
    }
}
