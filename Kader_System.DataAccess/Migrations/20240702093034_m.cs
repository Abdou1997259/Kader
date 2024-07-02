using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class m : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionDate",
                table: "Hr_SalaryCalculators");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Hr_SalaryCalculators");

            migrationBuilder.AlterColumn<int>(
                name: "CacluateSalaryId",
                table: "SpCacluateSalariesModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9397e603-63c0-497a-b4d9-accbe3248e7a", "AQAAAAIAAYagAAAAECr0w8x10qNvkMek0Ea185RwVX6R13Ho/Pfdym2yijH2GSTg8y39/ezysdvHXTy2iw==", "f37b7674-f801-4942-8d87-9d474a6e63e1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CacluateSalaryId",
                table: "SpCacluateSalariesModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ActionDate",
                table: "Hr_SalaryCalculators",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<double>(
                name: "Salary",
                table: "Hr_SalaryCalculators",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4edb0e2-8ab9-4bd8-8978-66c788aadd0c", "AQAAAAIAAYagAAAAEJwFeds/eWnbKFz4bq49Cz4nKSUOWep+6WcRPurkM05CTYBJxP0bYS9jCY8W8rg2Qw==", "9a82cd3e-fc0a-4bce-a6ff-68aef0689a9a" });

            migrationBuilder.UpdateData(
                table: "Hr_SalaryCalculators",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActionDate", "Salary" },
                values: new object[] { new DateOnly(1, 1, 1), 0.0 });

            migrationBuilder.UpdateData(
                table: "Hr_SalaryCalculators",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActionDate", "Salary" },
                values: new object[] { new DateOnly(1, 1, 1), 0.0 });

            migrationBuilder.UpdateData(
                table: "Hr_SalaryCalculators",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActionDate", "Salary" },
                values: new object[] { new DateOnly(1, 1, 1), 0.0 });
        }
    }
}
