using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class remove_relation_nyazy : Migration
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
                values: new object[] { "5a9f9fd7-2c36-4a28-8ade-8c06fb937e66", "AQAAAAIAAYagAAAAENR8auSyHYxFELEpA9ZjXzN8RT+Nw4NhxGRkOpif68rGeerGAEwpQgq1QnUh+MjLKQ==", "a1cabf16-3f3e-46e7-bcc3-7f405180fb17" });
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
                values: new object[] { "8d483b46-2131-4fb9-9850-93387ee41788", "AQAAAAIAAYagAAAAEG9vlaiW4naa6F31tTgwfZ9FbiwHeqHPN+G0Ytlb1kcbb3CTQjeNEHWRbtHK4o+Qrg==", "6601bd34-a8f9-4043-b8a6-b18bbc7db8a4" });
        }
    }
}
