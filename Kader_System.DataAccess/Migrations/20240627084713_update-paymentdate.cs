using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatepaymentdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "PaymentDate",
                table: "Trans_Loan_Details",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d4d40f3-8dbb-41ee-add9-c3f774ae13b8", "AQAAAAIAAYagAAAAEAhd1MKfXlHIjWZq5HWBp01442UUAPfB3RygYmZBR2571xG1E671/AOsIHTpvw7sbw==", "ca237424-c497-432e-93e7-d4aab4090f40" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "PaymentDate",
                table: "Trans_Loan_Details",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce134075-0fe8-4811-be20-f9a926d6a727", "AQAAAAIAAYagAAAAEE1WQ9Af01NaRD04tcCgpyCeegSlQ7vWfBvodDgVYu+Mhh0noZdKH7XDaLgxEE2YEw==", "bd0f42db-66ca-4e04-bc8e-81b54d223e05" });
        }
    }
}
