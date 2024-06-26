using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateuser : Migration
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
                values: new object[] { "a45c3fc2-1492-4ec3-b5d0-e163b100fdcc", "AQAAAAIAAYagAAAAEK1TT8KgcWidBKQr7QfpbzFSSVazwIkN6TrQhABstI6FOXOinxPDH56PyR3MpyTxHA==", "1dd06bf4-057c-498e-b747-90b1a3825685" });
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
                values: new object[] { "9ebc8de4-8137-42e6-b234-1d03a6aec086", "AQAAAAIAAYagAAAAEDeZf+1YqXPgfg/0LAL2YVWC31bD64bTQ0f3ui4Rq/kz9OM5D/CDqy2QV8HskZ/9EA==", "e37f25ab-d290-49b6-a0e8-2f2fe7a62aff" });
        }
    }
}
