using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeMigrationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullNameAr",
                table: "Hr_Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullNameEn",
                table: "Hr_Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ebdc0c1f-dd11-4ef0-a31a-fa400b1a4b33", "AQAAAAIAAYagAAAAEIbGTdqDHugMnYz7MfGM7Uxs268EwhGU2nPN89pz8Oxoc07hXgzzHFZCI8yFL0Srgg==", "ce46d35b-340e-47e3-84ae-45e9fae2a3e1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullNameAr",
                table: "Hr_Employees");

            migrationBuilder.DropColumn(
                name: "FullNameEn",
                table: "Hr_Employees");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d7e8189-2c58-4961-8dbb-62248a896c9d", "AQAAAAIAAYagAAAAEAgwsEtp8VUfWdG3zejBEl/OiynFrbRe3UYXUFYlB/QCPRnvTiafWfw8qklyyZQFiQ==", "25f1075f-c2d5-47a1-bad8-26a28b1c9041" });
        }
    }
}
