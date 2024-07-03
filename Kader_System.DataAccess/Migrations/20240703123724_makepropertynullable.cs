using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class makepropertynullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CacluateSalaryId",
                table: "SpCacluateSalariesModel");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SpCacluateSalariesModel");

            migrationBuilder.DropColumn(
                name: "JournalDate",
                table: "SpCacluateSalariesModel");

            migrationBuilder.DropColumn(
                name: "JournalType",
                table: "SpCacluateSalariesModel");

            migrationBuilder.AlterColumn<int>(
                name: "ManagementId",
                table: "trans_salary_calculators",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "trans_salary_calculators",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "trans_salary_calculators",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "TotalSalary",
                table: "SpCacluateSalariesModel",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "SpCaclauateSalaryDetailedTransModel",
                columns: table => new
                {
                    TransId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    JournalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    JournalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalculatedSalary = table.Column<double>(type: "float", nullable: false),
                    CacluateSalaryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SpCaclauateSalaryDetailsModel",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    JournalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalculatedSalary = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c448634-93b5-43b4-b08d-dd0d9ddc7193", "AQAAAAIAAYagAAAAEM/zLectsqF8NaNibyH2SKIy++2u6MzW4aQHABHkpAKMA91AZUzTUZfAuyUB7qpwlA==", "f5061761-85d4-47ba-885d-3c6ebec6b496" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpCaclauateSalaryDetailedTransModel");

            migrationBuilder.DropTable(
                name: "SpCaclauateSalaryDetailsModel");

            migrationBuilder.DropColumn(
                name: "TotalSalary",
                table: "SpCacluateSalariesModel");

            migrationBuilder.AlterColumn<int>(
                name: "ManagementId",
                table: "trans_salary_calculators",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "trans_salary_calculators",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchId",
                table: "trans_salary_calculators",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CacluateSalaryId",
                table: "SpCacluateSalariesModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SpCacluateSalariesModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "JournalDate",
                table: "SpCacluateSalariesModel",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "JournalType",
                table: "SpCacluateSalariesModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd7a49c0-e114-418f-a054-003984a90797", "AQAAAAIAAYagAAAAEEas8rD7V8ruo7W6E9ZVvbebS7K4aHVVCzL6dcpnkdShKdcKMc0vuiRzOLY7NpEjjw==", "66c02608-3d43-47a7-8be4-726419ffa670" });
        }
    }
}
