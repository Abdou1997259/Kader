using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddHrLoanRequest : Migration
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
                values: new object[] { "dc5f7787-1add-40ff-93e3-30aa555a3a95", "AQAAAAIAAYagAAAAEFjSqANr4qp/L6SyD1GUrvvo3BiaJ3gWaKYbswgi3IxkFWgIKEFtc/ft4+69UKWT8Q==", "1d0e2fc2-ede3-41e7-a238-20ce38b76fa2" });
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
                values: new object[] { "cf0ab5e6-ddd4-42f8-98a6-e985def3b851", "AQAAAAIAAYagAAAAEM3k6RRxPxDtTOMQnfQDoI+6H7Gcrw4iTZorwdqO2v8z6tRG1xduhM0iyHj7NXQ7hg==", "7feb8e20-83ef-425f-bc63-145e8c291e94" });
        }
    }
}
