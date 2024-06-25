using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update_user_data : Migration
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
                values: new object[] { "28c3504f-bc4f-4f83-9a73-5a8d140bcd61", "AQAAAAIAAYagAAAAEGbxuJ0QgnLqk8Va3OsoM/avHkvEfX2v5oD5vNYgwO5nExixnq/HA9XN4gp5XdTFIg==", "d7c9af0f-0c60-45f3-ae83-57b04146ed93" });
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
