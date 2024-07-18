using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class trans_salary_calculators_detailsupdating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "trans_salary_calculators_details",
                newName: "TotalLoans");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "trans_salary_calculators_details",
                newName: "TotalDeductions");

            migrationBuilder.AddColumn<double>(
                name: "BasicSalary",
                table: "trans_salary_calculators_details",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NetSalary",
                table: "trans_salary_calculators_details",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "trans_salary_calculators_details",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalAllownces",
                table: "trans_salary_calculators_details",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalBenefits",
                table: "trans_salary_calculators_details",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TransferId",
                table: "trans_salary_calculators_details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8c6c504-5244-4a9c-9fd7-126d77c69e84", "AQAAAAIAAYagAAAAEAU3TSt/2EoV/daCCkalKmtR8oZcDLd/IGVyePu1DUClQmPIeymgBLW6cFac8+Cgjg==", "c7410776-4a09-4b4a-be65-dd8ff13dc2b7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasicSalary",
                table: "trans_salary_calculators_details");

            migrationBuilder.DropColumn(
                name: "NetSalary",
                table: "trans_salary_calculators_details");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "trans_salary_calculators_details");

            migrationBuilder.DropColumn(
                name: "TotalAllownces",
                table: "trans_salary_calculators_details");

            migrationBuilder.DropColumn(
                name: "TotalBenefits",
                table: "trans_salary_calculators_details");

            migrationBuilder.DropColumn(
                name: "TransferId",
                table: "trans_salary_calculators_details");

            migrationBuilder.RenameColumn(
                name: "TotalLoans",
                table: "trans_salary_calculators_details",
                newName: "Salary");

            migrationBuilder.RenameColumn(
                name: "TotalDeductions",
                table: "trans_salary_calculators_details",
                newName: "Amount");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e19f1155-2db5-4f73-a984-23ffe6a11137", "AQAAAAIAAYagAAAAEIB+onz3//hUUtGDXo6BM4fp8qpQSHyff+VmJYZHUmUe9M7tDz5bjfdPqOsgz67g9g==", "106dea95-8987-4796-9d16-656e9fea2c0b" });
        }
    }
}
