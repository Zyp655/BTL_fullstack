using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseService.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomToSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "Schedules",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 1,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 2,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 3,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 4,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 5,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 6,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 7,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 8,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 9,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 10,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 11,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 12,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 13,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 14,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 15,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 16,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 47,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 48,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 100,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 101,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 102,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 103,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 104,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 105,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 106,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 107,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 108,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 109,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 110,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 111,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 112,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 113,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 114,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 115,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 116,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 117,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 118,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 119,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 120,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 121,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 122,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 123,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 124,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 125,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 126,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 127,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 128,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 129,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 130,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 131,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 132,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 133,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 134,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 135,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 136,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 137,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 138,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 139,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 140,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 141,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 142,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 143,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 144,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 145,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 146,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 147,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 148,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 149,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 150,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 151,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 152,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 153,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 154,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 155,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 156,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 157,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 158,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 159,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 160,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 161,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 162,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 163,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 164,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 165,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 166,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 167,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 168,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 169,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 170,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 171,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 172,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 173,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 174,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 175,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 176,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 177,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 178,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 179,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 180,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 181,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 182,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 183,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 184,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 185,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 186,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 187,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 188,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 189,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 190,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 191,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 192,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 193,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 194,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 195,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 196,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 197,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 198,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 199,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 200,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 201,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 202,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 203,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 204,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 205,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 206,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 207,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 208,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 209,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 210,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 211,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 212,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 213,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 214,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 215,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 216,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 217,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 218,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 219,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 220,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 221,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 222,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 223,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 224,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 225,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 226,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 227,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 228,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 229,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 230,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 231,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 232,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 233,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 234,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 235,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 236,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 237,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 238,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 239,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 240,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 241,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 242,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 243,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 244,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 245,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 246,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 247,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 248,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 249,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 250,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 251,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 252,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 253,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 254,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 255,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 256,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 257,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 258,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 259,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 260,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 261,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 262,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 263,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 264,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 265,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 266,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 267,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 268,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 269,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 270,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 271,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 272,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 273,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 274,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 275,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 276,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 277,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 278,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 279,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 280,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 281,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 282,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 283,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 284,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 285,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 286,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 287,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 288,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 289,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 290,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 291,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 292,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 293,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 294,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 295,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 296,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 297,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 298,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 299,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 300,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 301,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 302,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 303,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 304,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 305,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 306,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 307,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 308,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 309,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 310,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 311,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 312,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 313,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 314,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 315,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 316,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 317,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 318,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 319,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 320,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 321,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 322,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 323,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 324,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 325,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 326,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 327,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 328,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 329,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 330,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 331,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 332,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 333,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 334,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 335,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 336,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 337,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 338,
                column: "Room",
                value: null);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "ScheduleId",
                keyValue: 339,
                column: "Room",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Room",
                table: "Schedules");
        }
    }
}
