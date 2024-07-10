using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addpropertyincalcdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "VacationDayCount",
            //    table: "SpCaclauateSalaryDetailedTransModel");

            //migrationBuilder.DropColumn(
            //    name: "VacationSum",
            //    table: "SpCaclauateSalaryDetailedTransModel");

            //migrationBuilder.RenameColumn(
            //    name: "CacluateSalaryId",
            //    table: "SpCaclauateSalaryDetailedTransModel",
            //    newName: "CalculateSalaryId");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "trans_salary_calculators_details",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            //migrationBuilder.AddColumn<int>(
            //    name: "CalculateSalaryDetailsId",
            //    table: "SpCaclauateSalaryDetailedTransModel",
            //    type: "int",
            //    nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0befd46e-4d8e-4f8c-8960-e2ecbe2f7987", "AQAAAAIAAYagAAAAEJW9fZnBFgUbMfKhT6je67DfMrDKJOsmYbEkLFm+NUbHvR2l2ZZS3GRZI+PuYakEIQ==", "09487add-d943-4ff2-a662-6d958c4822bf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "trans_salary_calculators_details");

            //migrationBuilder.DropColumn(
            //    name: "CalculateSalaryDetailsId",
            //    table: "SpCaclauateSalaryDetailedTransModel");

            //migrationBuilder.RenameColumn(
            //    name: "CalculateSalaryId",
            //    table: "SpCaclauateSalaryDetailedTransModel",
            //    newName: "CacluateSalaryId");

            //migrationBuilder.AddColumn<double>(
            //    name: "VacationDayCount",
            //    table: "SpCaclauateSalaryDetailedTransModel",
            //    type: "float",
            //    nullable: true);

            //migrationBuilder.AddColumn<double>(
            //    name: "VacationSum",
            //    table: "SpCaclauateSalaryDetailedTransModel",
            //    type: "float",
            //    nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "765dd646-1952-4b97-ab01-d0c8bcaf5922", "AQAAAAIAAYagAAAAENh4C2TArejVqEzCavvXnJHYpfG9mpwmYnL9ALoQ7VeDOayj1pcLt+10aGlCSHyHrA==", "c6d8c69e-c8f3-4806-82b3-fbaf2357afb9" });
        }
    }
}
