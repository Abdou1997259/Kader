using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editContractTerminationRequest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "hr_contract_termination");

            migrationBuilder.RenameColumn(
                name: "AttachmentFileName",
                table: "hr_contract_termination",
                newName: "AttachmentPath");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e9bc4b69-f3ee-4db0-8c0b-6a91fa7c0e96", "AQAAAAIAAYagAAAAEHstNydckieU/OvHalIwXGOoyc5WVz1s4wxE0Yx0X06Vs02Tepqz+uUNKQZLSGx0/g==", "c05acebd-dc39-4b03-b1ea-d7ecc8789e7f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachmentPath",
                table: "hr_contract_termination",
                newName: "AttachmentFileName");

            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "hr_contract_termination",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e5a2228d-489f-4508-8056-c891be048d6b", "AQAAAAIAAYagAAAAEKL4/Y5W02sovRaJpCYmxQ/TXT24SRYUo10kE5YF6yZpHQG3HZ6pVklsct0yYISgdw==", "91a2609f-08a9-4dc7-9ab5-b5d3b1524017" });
        }
    }
}
