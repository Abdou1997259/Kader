using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addrenamecalcid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CacluateSalaryId",
                table: "Trans_Loan",
                newName: "CalculateSalaryId");

            migrationBuilder.RenameColumn(
                name: "CacluateSalaryId",
                table: "Trans_Deductions",
                newName: "CalculateSalaryId");

            migrationBuilder.RenameColumn(
                name: "CacluateSalaryId",
                table: "Trans_Benefits",
                newName: "CalculateSalaryId");

            migrationBuilder.RenameColumn(
                name: "CacluateSalaryId",
                table: "Trans_Allowances",
                newName: "CalculateSalaryId");

            migrationBuilder.AddColumn<int>(
                name: "CalculateSalaryDetailsId",
                table: "Trans_Vacations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalculateSalaryId",
                table: "Trans_Vacations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalculateSalaryDetailsId",
                table: "Trans_Loan",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalculateSalaryDetailsId",
                table: "Trans_Deductions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalculateSalaryDetailsId",
                table: "Trans_Benefits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalculateSalaryDetailsId",
                table: "Trans_Allowances",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AccommodationAllowance",
                table: "SpCacluateSalariesModel",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "FullNameAr",
                table: "SpCacluateSalariesModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullNameEn",
                table: "SpCacluateSalariesModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "AccommodationAllowance",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullNameAr",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullNameEn",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "VacationDayCount",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VacationSum",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "765dd646-1952-4b97-ab01-d0c8bcaf5922", "AQAAAAIAAYagAAAAENh4C2TArejVqEzCavvXnJHYpfG9mpwmYnL9ALoQ7VeDOayj1pcLt+10aGlCSHyHrA==", "c6d8c69e-c8f3-4806-82b3-fbaf2357afb9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculateSalaryDetailsId",
                table: "Trans_Vacations");

            migrationBuilder.DropColumn(
                name: "CalculateSalaryId",
                table: "Trans_Vacations");

            migrationBuilder.DropColumn(
                name: "CalculateSalaryDetailsId",
                table: "Trans_Loan");

            migrationBuilder.DropColumn(
                name: "CalculateSalaryDetailsId",
                table: "Trans_Deductions");

            migrationBuilder.DropColumn(
                name: "CalculateSalaryDetailsId",
                table: "Trans_Benefits");

            migrationBuilder.DropColumn(
                name: "CalculateSalaryDetailsId",
                table: "Trans_Allowances");

            migrationBuilder.DropColumn(
                name: "AccommodationAllowance",
                table: "SpCacluateSalariesModel");

            migrationBuilder.DropColumn(
                name: "FullNameAr",
                table: "SpCacluateSalariesModel");

            migrationBuilder.DropColumn(
                name: "FullNameEn",
                table: "SpCacluateSalariesModel");

            migrationBuilder.DropColumn(
                name: "AccommodationAllowance",
                table: "SpCaclauateSalaryDetailedTransModel");

            migrationBuilder.DropColumn(
                name: "FullNameAr",
                table: "SpCaclauateSalaryDetailedTransModel");

            migrationBuilder.DropColumn(
                name: "FullNameEn",
                table: "SpCaclauateSalaryDetailedTransModel");

            migrationBuilder.DropColumn(
                name: "VacationDayCount",
                table: "SpCaclauateSalaryDetailedTransModel");

            migrationBuilder.DropColumn(
                name: "VacationSum",
                table: "SpCaclauateSalaryDetailedTransModel");

            migrationBuilder.RenameColumn(
                name: "CalculateSalaryId",
                table: "Trans_Loan",
                newName: "CacluateSalaryId");

            migrationBuilder.RenameColumn(
                name: "CalculateSalaryId",
                table: "Trans_Deductions",
                newName: "CacluateSalaryId");

            migrationBuilder.RenameColumn(
                name: "CalculateSalaryId",
                table: "Trans_Benefits",
                newName: "CacluateSalaryId");

            migrationBuilder.RenameColumn(
                name: "CalculateSalaryId",
                table: "Trans_Allowances",
                newName: "CacluateSalaryId");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0fd05a5c-50e7-4875-8121-4a877c6fc07d", "AQAAAAIAAYagAAAAEIBvb+w9og1KvaSBuSTLA4wo7Xg4lajC3ndY7HUjAn5BZyZFmbXG0/89UpM7sRjiwA==", "b212b13c-33cb-4c31-a988-cea2d883cc91" });
        }
    }
}
