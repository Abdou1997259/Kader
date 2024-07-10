using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addTwoColumns_transactionDate_dueDate_in_TransSalaryIncrease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dueDate",
                table: "Trans_SalaryIncreases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "transactionDate",
                table: "Trans_SalaryIncreases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61d064b3-a010-4c24-a4fe-1795b12c04ea", "AQAAAAIAAYagAAAAEMH6PmxTZAL+w7CQZlW6FJskq2PvFnmXoM8cFaP9De0iq9wc1gmTasazzgsexbQKxA==", "3c8b299c-3d6c-4233-8cd2-11ab25ebe9e7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dueDate",
                table: "Trans_SalaryIncreases");

            migrationBuilder.DropColumn(
                name: "transactionDate",
                table: "Trans_SalaryIncreases");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c025ffe5-f890-4761-8463-20e2f378a78c", "AQAAAAIAAYagAAAAEDpjClWeVTTieovxxByISDw0E3cx7SoYW0JUovsFXjWXrHcSx23+FD5xr+Ek8YRnHQ==", "6968dd96-a24e-4778-ad77-fbbd9695ed29" });
        }
    }
}
