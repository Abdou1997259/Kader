using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editVacation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachmentFileName",
                table: "hr_vacation_requests",
                newName: "AttachmentPath");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d59ca47d-1b5d-448b-9d3b-f5055543d46b", "AQAAAAIAAYagAAAAEB49zfX8LUsGWz7l21NU9/KZn+HygEsO2OsA3U9ZjBHMOFjq1wKKPYtZ1NkwRqyjLg==", "2ed13847-b35c-46ba-8114-1277c3bade6a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachmentPath",
                table: "hr_vacation_requests",
                newName: "AttachmentFileName");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b720f5bf-6721-48c0-a174-e04377d67a5b", "AQAAAAIAAYagAAAAECG9JJnmMzEyZteabODHpe0Kqzb1GiSAVxe0oARf0rdzWCC7nIDOfde2iM6I9u1Gzg==", "38b8a546-ee53-44e9-b258-c630bc35fd81" });
        }
    }
}
