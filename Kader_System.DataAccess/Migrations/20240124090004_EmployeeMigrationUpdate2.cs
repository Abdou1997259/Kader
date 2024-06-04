using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeMigrationUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Hr_Employees");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Hr_Employees");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Hr_Employees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5183c63-dcd3-4e33-ad79-1eb0abd1961f", "AQAAAAIAAYagAAAAECy+kARK7bfBCqVD4PVg/23G8VJSyPXtQKkbjhSz1nZqYpH/n2rk+6o2x9067TXIYA==", "2a7ef96b-f1b1-42dd-933b-fb90555b4527" });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_UserId",
                table: "Hr_Employees",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Auth_Users_UserId",
                table: "Hr_Employees",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "Auth_Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Auth_Users_UserId",
                table: "Hr_Employees");

            migrationBuilder.DropIndex(
                name: "IX_Hr_Employees_UserId",
                table: "Hr_Employees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Hr_Employees");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Hr_Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
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
                values: new object[] { "2f6fd8ad-2d2b-4158-8e40-5320b02306a6", "AQAAAAIAAYagAAAAEBwp0gQbLwoa0BWXhVy1+Ru7d+ZTMNK0tvwW4XZe3/0BOgx5S+MOcGJcWJmx/BC79g==", "f349df16-3849-4254-8154-d447d9906cfa" });
        }
    }
}
