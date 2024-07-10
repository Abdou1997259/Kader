using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addComputedColumnSalaryAfterIncrease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "salaryAfterIncrease",
                table: "Trans_SalaryIncreases",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c025ffe5-f890-4761-8463-20e2f378a78c", "AQAAAAIAAYagAAAAEDpjClWeVTTieovxxByISDw0E3cx7SoYW0JUovsFXjWXrHcSx23+FD5xr+Ek8YRnHQ==", "6968dd96-a24e-4778-ad77-fbbd9695ed29" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "salaryAfterIncrease",
                table: "Trans_SalaryIncreases");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "252ea1c3-8e19-4be6-856d-5d7dccabca13", "AQAAAAIAAYagAAAAEMRTwxn8xSQ1G4/dAUA2ilR9QOVNORyuSKULXhxbOk7Ylo3+KwWbmldh70oUzqEv2Q==", "a5a2cfe1-f04f-4f98-ac00-ad6cc8052ecd" });
        }
    }
}
