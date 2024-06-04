using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class transBenefitMig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Benefits_Trans_AmountTypes_ValueTypeId",
                table: "Trans_Benefits");

            migrationBuilder.RenameColumn(
                name: "ValueTypeId",
                table: "Trans_Benefits",
                newName: "AmountTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Benefits_ValueTypeId",
                table: "Trans_Benefits",
                newName: "IX_Trans_Benefits_AmountTypeId");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64f3eca5-8800-4a94-a236-7c7734252770", "AQAAAAIAAYagAAAAEBMqWWkfaQ/jXTsIfzllLVv6dIh3RiOXgnm9+aO32kzWbKlNOSvxnqoWItUfW4x1ow==", "4a47ce5e-680e-4c38-a6d7-8f70ff778cb8" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Benefits_Trans_AmountTypes_AmountTypeId",
                table: "Trans_Benefits",
                column: "AmountTypeId",
                principalTable: "Trans_AmountTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Benefits_Trans_AmountTypes_AmountTypeId",
                table: "Trans_Benefits");

            migrationBuilder.RenameColumn(
                name: "AmountTypeId",
                table: "Trans_Benefits",
                newName: "ValueTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Benefits_AmountTypeId",
                table: "Trans_Benefits",
                newName: "IX_Trans_Benefits_ValueTypeId");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e9500a9-8f99-4b30-808e-706868458d1a", "AQAAAAIAAYagAAAAEATDg8OBL3A4V6q1F36xlTxL3MBIAIbMwQ0/0dYW58H2CuoZmL0dZ/g3/MxSAccQMA==", "62d577a5-e6d6-4ccd-be37-6fe1e89c5c92" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Benefits_Trans_AmountTypes_ValueTypeId",
                table: "Trans_Benefits",
                column: "ValueTypeId",
                principalTable: "Trans_AmountTypes",
                principalColumn: "Id");
        }
    }
}
