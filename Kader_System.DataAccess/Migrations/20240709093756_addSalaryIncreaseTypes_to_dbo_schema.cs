using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addSalaryIncreaseTypes_to_dbo_schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SalaryIncreaseTypes",
                newName: "SalaryIncreaseTypes",
                newSchema: "dbo");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eaed6b65-5b03-45cf-9025-3a768ba9c388", "AQAAAAIAAYagAAAAEK4vaaNtz3VTB3Kwf8aaZnGYU6o5hKLhlR6ilLKhbzqDdaxJbi0yIpTIoy7UkhRG6g==", "dc9cb540-95ae-40c9-8dc4-56827ab6bb00" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SalaryIncreaseTypes",
                schema: "dbo",
                newName: "SalaryIncreaseTypes");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3ac21cb-baaa-46fa-9f05-cd571d97cc1d", "AQAAAAIAAYagAAAAEHkwgWKr4dNLhOxCbS02Ej4iPzZ8hCKFa+NpVUxKaxtfV7pOIsknkjcwdvTrY8N/aw==", "95d8ef79-abd0-4ef2-b3ae-79bc5e368477" });
        }
    }
}
