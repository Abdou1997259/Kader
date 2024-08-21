using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editSalaryIncreaseRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachmentFileName",
                table: "Hr_SalaryIncreaseRequest",
                newName: "AttachmentPath");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b720f5bf-6721-48c0-a174-e04377d67a5b", "AQAAAAIAAYagAAAAECG9JJnmMzEyZteabODHpe0Kqzb1GiSAVxe0oARf0rdzWCC7nIDOfde2iM6I9u1Gzg==", "38b8a546-ee53-44e9-b258-c630bc35fd81" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachmentPath",
                table: "Hr_SalaryIncreaseRequest",
                newName: "AttachmentFileName");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92aa665b-1f7f-4471-91af-fc4c48da2e11", "AQAAAAIAAYagAAAAEOop2IAqswVdffYUOtfRe63VKELcAjBqVDoz/pa3f/M8GUQMpgkrITZ9JNi9rG58bw==", "1b53a270-63a9-490c-b6e0-d46d3db6f77a" });
        }
    }
}
