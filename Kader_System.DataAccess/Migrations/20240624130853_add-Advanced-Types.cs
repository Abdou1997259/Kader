using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addAdvancedTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advanced_Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ADvancesTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advanced_Types", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Advanced_Types",
                columns: new[] { "Id", "ADvancesTypeName" },
                values: new object[,]
                {
                    { 1, "سلفة" },
                    { 2, "مخالفة مرورية" },
                    { 3, "حوادث وأصلاحات" },
                    { 4, "نقل خدمات" },
                    { 5, "مخالفة مرورية" },
                    { 6, "تأمينات" },
                    { 7, "تسويات اخري" }
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ebc8de4-8137-42e6-b234-1d03a6aec086", "AQAAAAIAAYagAAAAEDeZf+1YqXPgfg/0LAL2YVWC31bD64bTQ0f3ui4Rq/kz9OM5D/CDqy2QV8HskZ/9EA==", "e37f25ab-d290-49b6-a0e8-2f2fe7a62aff" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advanced_Types");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f474006-ac21-4807-acb6-a4c6b5e00559", "AQAAAAIAAYagAAAAEJdwEpRfQj5pxkTH99x+YwksrSvPXWiiD5+Hn7/I99DsTYi0iCHI0qxCbO6pAO7S5Q==", "322913ae-dc4c-4b08-bbf6-37747cd56d5c" });
        }
    }
}
