using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class renameadvancetypename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ADvancesTypeName",
                table: "Advanced_Types",
                newName: "AdvanceTypeName");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e9ae668-32c8-4962-a0f1-a274936186e1", "AQAAAAIAAYagAAAAEHhn2QXPv+M+8unyH7Urd1EYEDW5LmKh4X11ce6GrmthnCkkRKJuBufhve6ATaE20Q==", "8341afc9-b466-48d8-a0e6-0b9891fec279" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdvanceTypeName",
                table: "Advanced_Types",
                newName: "ADvancesTypeName");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a037c520-8820-4966-a7ba-dac4203a8a72", "AQAAAAIAAYagAAAAEJJ4YU1PW3k8sAyJ6oPRYskCdY0DWRo/OxkK/ZmlxSxHFZb3wfBgj+QaLrQkJ9ogyA==", "66bac253-4254-414d-af87-f43d429578cc" });
        }
    }
}
