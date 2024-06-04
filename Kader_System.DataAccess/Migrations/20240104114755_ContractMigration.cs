using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ContractMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_ContractAllowancesDetails_Hr_Allowances_Salary_effect_id",
                table: "Hr_ContractAllowancesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_ContractAllowancesDetails_Hr_Contracts_Contract_id",
                table: "Hr_ContractAllowancesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_ContractAllowancesDetails_Hr_ValueTypes_Value_type",
                table: "Hr_ContractAllowancesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Contracts_Hr_Employees_Employee_id",
                table: "Hr_Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Hr_ContractAllowancesDetails_Contract_id",
                table: "Hr_ContractAllowancesDetails");

            migrationBuilder.DropColumn(
                name: "Contract_id",
                table: "Hr_ContractAllowancesDetails");

            migrationBuilder.RenameColumn(
                name: "Start_date",
                table: "Hr_Contracts",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Salary_total",
                table: "Hr_Contracts",
                newName: "TotalSalary");

            migrationBuilder.RenameColumn(
                name: "Salary_fixed",
                table: "Hr_Contracts",
                newName: "HousingAllowance");

            migrationBuilder.RenameColumn(
                name: "End_date",
                table: "Hr_Contracts",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "Employee_id",
                table: "Hr_Contracts",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Contracts_Employee_id",
                table: "Hr_Contracts",
                newName: "IX_Hr_Contracts_EmployeeId");

            migrationBuilder.RenameColumn(
                name: "Value_type",
                table: "Hr_ContractAllowancesDetails",
                newName: "ContractId");

            migrationBuilder.RenameColumn(
                name: "Salary_effect_id",
                table: "Hr_ContractAllowancesDetails",
                newName: "AllowanceId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_ContractAllowancesDetails_Value_type",
                table: "Hr_ContractAllowancesDetails",
                newName: "IX_Hr_ContractAllowancesDetails_ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_ContractAllowancesDetails_Salary_effect_id",
                table: "Hr_ContractAllowancesDetails",
                newName: "IX_Hr_ContractAllowancesDetails_AllowanceId");

            migrationBuilder.AddColumn<double>(
                name: "FixedSalary",
                table: "Hr_Contracts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPercent",
                table: "Hr_ContractAllowancesDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa1fb72f-be7f-4837-bf91-5d9565346f46", "AQAAAAIAAYagAAAAEMbyD8CaPLTfozrB8HyclFNhXfu07Celvnt3zpUnQGc1ED/OSH7pabnJ9yhKzJpTBQ==", "483408f6-70d8-4cc1-820b-6a5f9a2016e6" });

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_ContractAllowancesDetails_Hr_Allowances_AllowanceId",
                table: "Hr_ContractAllowancesDetails",
                column: "AllowanceId",
                principalTable: "Hr_Allowances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_ContractAllowancesDetails_Hr_Contracts_ContractId",
                table: "Hr_ContractAllowancesDetails",
                column: "ContractId",
                principalTable: "Hr_Contracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Contracts_Hr_Employees_EmployeeId",
                table: "Hr_Contracts",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_ContractAllowancesDetails_Hr_Allowances_AllowanceId",
                table: "Hr_ContractAllowancesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_ContractAllowancesDetails_Hr_Contracts_ContractId",
                table: "Hr_ContractAllowancesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Contracts_Hr_Employees_EmployeeId",
                table: "Hr_Contracts");

            migrationBuilder.DropColumn(
                name: "FixedSalary",
                table: "Hr_Contracts");

            migrationBuilder.DropColumn(
                name: "IsPercent",
                table: "Hr_ContractAllowancesDetails");

            migrationBuilder.RenameColumn(
                name: "TotalSalary",
                table: "Hr_Contracts",
                newName: "Salary_total");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Hr_Contracts",
                newName: "Start_date");

            migrationBuilder.RenameColumn(
                name: "HousingAllowance",
                table: "Hr_Contracts",
                newName: "Salary_fixed");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Hr_Contracts",
                newName: "End_date");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Hr_Contracts",
                newName: "Employee_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Contracts_EmployeeId",
                table: "Hr_Contracts",
                newName: "IX_Hr_Contracts_Employee_id");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                table: "Hr_ContractAllowancesDetails",
                newName: "Value_type");

            migrationBuilder.RenameColumn(
                name: "AllowanceId",
                table: "Hr_ContractAllowancesDetails",
                newName: "Salary_effect_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_ContractAllowancesDetails_ContractId",
                table: "Hr_ContractAllowancesDetails",
                newName: "IX_Hr_ContractAllowancesDetails_Value_type");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_ContractAllowancesDetails_AllowanceId",
                table: "Hr_ContractAllowancesDetails",
                newName: "IX_Hr_ContractAllowancesDetails_Salary_effect_id");

            migrationBuilder.AddColumn<int>(
                name: "Contract_id",
                table: "Hr_ContractAllowancesDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "181ebc35-bff6-47f5-b093-22591891286d", "AQAAAAIAAYagAAAAECv+1nnWNSuZakRrXqU9FoZZDlLOFDrnEfou+APRLWJqcNKieCgAMNMBh5AH5+z2wA==", "4f5c9f7f-35d8-4f84-8e76-1d02686af434" });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_ContractAllowancesDetails_Contract_id",
                table: "Hr_ContractAllowancesDetails",
                column: "Contract_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_ContractAllowancesDetails_Hr_Allowances_Salary_effect_id",
                table: "Hr_ContractAllowancesDetails",
                column: "Salary_effect_id",
                principalTable: "Hr_Allowances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_ContractAllowancesDetails_Hr_Contracts_Contract_id",
                table: "Hr_ContractAllowancesDetails",
                column: "Contract_id",
                principalTable: "Hr_Contracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_ContractAllowancesDetails_Hr_ValueTypes_Value_type",
                table: "Hr_ContractAllowancesDetails",
                column: "Value_type",
                principalTable: "Hr_ValueTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Contracts_Hr_Employees_Employee_id",
                table: "Hr_Contracts",
                column: "Employee_id",
                principalTable: "Hr_Employees",
                principalColumn: "Id");
        }
    }
}
