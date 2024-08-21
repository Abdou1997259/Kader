using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editDelayPermessionAttachmentRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AtachmentPath",
                table: "hr_delay_permission",
                newName: "AttachmentPath");

            migrationBuilder.RenameColumn(
                name: "attachment_file_name",
                table: "hr_allowance_request",
                newName: "AttachmentPath");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a2589db-2f93-4cdc-8487-5caaf04f99b9", "AQAAAAIAAYagAAAAEAztk2C7O5ha+/Ve3b7X5ligkhh35r2qNbEhBxbGLEpm/1UTEpZBXRcZF89Ci8yuZA==", "c1d2324d-b70a-4209-ba5f-47dfd1e930f8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachmentPath",
                table: "hr_delay_permission",
                newName: "AtachmentPath");

            migrationBuilder.RenameColumn(
                name: "AttachmentPath",
                table: "hr_allowance_request",
                newName: "attachment_file_name");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9bc4b69-f3ee-4db0-8c0b-6a91fa7c0e96", "AQAAAAIAAYagAAAAEHstNydckieU/OvHalIwXGOoyc5WVz1s4wxE0Yx0X06Vs02Tepqz+uUNKQZLSGx0/g==", "c05acebd-dc39-4b03-b1ea-d7ecc8789e7f" });
        }
    }
}
