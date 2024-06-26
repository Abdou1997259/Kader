using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addtwocolumnstotransloantable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndCalculationDate",
                table: "Trans_Loan",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartCalculationDate",
                table: "Trans_Loan",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "53d57b16-5535-41aa-9356-f13c21b7683d", "AQAAAAIAAYagAAAAEJ4kXJAbKhLnb/s2kWcb4PBqmXYHywi+xirzTw/jOQnLnSNcYbje94g614a/Y7QXMA==", "4bb0673e-e3a9-4a2d-9737-097037b11bad" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndCalculationDate",
                table: "Trans_Loan");

            migrationBuilder.DropColumn(
                name: "StartCalculationDate",
                table: "Trans_Loan");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a45c3fc2-1492-4ec3-b5d0-e163b100fdcc", "AQAAAAIAAYagAAAAEK1TT8KgcWidBKQr7QfpbzFSSVazwIkN6TrQhABstI6FOXOinxPDH56PyR3MpyTxHA==", "1dd06bf4-057c-498e-b747-90b1a3825685" });
        }
    }
}
