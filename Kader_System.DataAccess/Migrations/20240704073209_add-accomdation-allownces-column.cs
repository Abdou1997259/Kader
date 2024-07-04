using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addaccomdationallowncescolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AccommodationAllowance",
                table: "Hr_Employees",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0fd05a5c-50e7-4875-8121-4a877c6fc07d", "AQAAAAIAAYagAAAAEIBvb+w9og1KvaSBuSTLA4wo7Xg4lajC3ndY7HUjAn5BZyZFmbXG0/89UpM7sRjiwA==", "b212b13c-33cb-4c31-a988-cea2d883cc91" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccommodationAllowance",
                table: "Hr_Employees");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c448634-93b5-43b4-b08d-dd0d9ddc7193", "AQAAAAIAAYagAAAAEM/zLectsqF8NaNibyH2SKIy++2u6MzW4aQHABHkpAKMA91AZUzTUZfAuyUB7qpwlA==", "f5061761-85d4-47ba-885d-3c6ebec6b496" });
        }
    }
}
