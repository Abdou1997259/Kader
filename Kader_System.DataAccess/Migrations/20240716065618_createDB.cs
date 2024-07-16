using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class createDB : Migration
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
                values: new object[] { "df1761d5-623f-4882-bf00-91d85f86080a", "AQAAAAIAAYagAAAAEN63Q6Arg4KpUUf/dg9IS4WuMh95w94OhpcC+LUSJ4B4FezyeAGpc7Ye1Ql0BTmqsQ==", "fa54c01d-7b3f-4d5a-9c7f-395bde06ada7" });
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
                values: new object[] { "c76649a9-ddfa-445d-9f7f-848db3851372", "AQAAAAIAAYagAAAAEJej//H+xGlSgfa+otcdJS9vT/0QVnG3VpsaSy/dcsObxlxiD4iOT7GwHaju5Tynfw==", "aa56a45c-c884-4435-bb18-5369b734a656" });
        }
    }
}
