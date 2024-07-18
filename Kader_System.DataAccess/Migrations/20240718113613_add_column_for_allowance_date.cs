using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_column_for_allowance_date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "allowance_request_date",
                table: "hr_allowance_requet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e722ee1d-e06a-49c9-9e33-2dff6afa74ca", "AQAAAAIAAYagAAAAELczig+/lly8viPKn+S/L697Cf30q9A17oWIJ8/aagPvh/kMS5KwG1BQXTXzDHdNkw==", "03796de7-0dbf-462d-891e-9748ed2cd0ad" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "allowance_request_date",
                table: "hr_allowance_requet");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e4c45fb-35ca-4f13-9c41-61d7c7af5c6b", "AQAAAAIAAYagAAAAEHk46A1FyxoU21hdtrcZqkEoeQWUqw7tvGvxVa332Cx+1Nn5oo7qrS/x0LePoCEHkQ==", "911eee52-d667-43a0-9093-cd3c8bef2bff" });
        }
    }
}
