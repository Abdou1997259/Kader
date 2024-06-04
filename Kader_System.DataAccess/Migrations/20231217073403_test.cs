using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17611151-ef11-49c1-905a-d86a58fd850b", "AQAAAAIAAYagAAAAEB/lqjbW9yTUejtnMLlnjTYt0/J6N6I8/0QIhrpLJqJ8tceLTJyw2NK9MzudKI8WfA==", "eb449e34-d3f8-4dd5-9bf1-7fe6ff161682" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d64c8836-9919-484c-b883-7c2e35ad90de", "AQAAAAIAAYagAAAAEBdc3TQE87jCh1KsEn3Fur3RUzXOpFxd2kuzjg8BiQiRTn8EVkHrb8XlXMXXHx81jA==", "c01d7e93-2bf0-4b9a-a8f9-2e23f6637588" });
        }
    }
}
