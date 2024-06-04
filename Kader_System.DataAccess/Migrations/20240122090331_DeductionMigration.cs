using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DeductionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Deductions_Hr_Deductions_Deduction_id",
                table: "Trans_Deductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Deductions_Hr_Employees_Employee_id",
                table: "Trans_Deductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Deductions_Trans_AmountTypes_Value_type",
                table: "Trans_Deductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Deductions_Trans_SalaryEffects_Salary_effect_id",
                table: "Trans_Deductions");

            migrationBuilder.RenameColumn(
                name: "Value_type",
                table: "Trans_Deductions",
                newName: "SalaryEffectId");

            migrationBuilder.RenameColumn(
                name: "Salary_effect_id",
                table: "Trans_Deductions",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "Employee_id",
                table: "Trans_Deductions",
                newName: "DeductionId");

            migrationBuilder.RenameColumn(
                name: "Deduction_id",
                table: "Trans_Deductions",
                newName: "AmountTypeId");

            migrationBuilder.RenameColumn(
                name: "Action_month",
                table: "Trans_Deductions",
                newName: "ActionMonth");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Deductions_Value_type",
                table: "Trans_Deductions",
                newName: "IX_Trans_Deductions_SalaryEffectId");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Deductions_Salary_effect_id",
                table: "Trans_Deductions",
                newName: "IX_Trans_Deductions_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Deductions_Employee_id",
                table: "Trans_Deductions",
                newName: "IX_Trans_Deductions_DeductionId");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Deductions_Deduction_id",
                table: "Trans_Deductions",
                newName: "IX_Trans_Deductions_AmountTypeId");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Trans_Deductions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70e24642-3b39-4684-94f0-a786cc8b8135", "AQAAAAIAAYagAAAAEJjxoy/ceEpB98cqdmVOgV3Dl94fAsf1hCyW6y0Rg7wmPBFbH1O95V6PXK0Fw7xBSA==", "3706f049-1ecf-4aac-aa2b-673d31856a25" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Deductions_Hr_Deductions_DeductionId",
                table: "Trans_Deductions",
                column: "DeductionId",
                principalTable: "Hr_Deductions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Deductions_Hr_Employees_EmployeeId",
                table: "Trans_Deductions",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Deductions_Trans_AmountTypes_AmountTypeId",
                table: "Trans_Deductions",
                column: "AmountTypeId",
                principalTable: "Trans_AmountTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Deductions_Trans_SalaryEffects_SalaryEffectId",
                table: "Trans_Deductions",
                column: "SalaryEffectId",
                principalTable: "Trans_SalaryEffects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Deductions_Hr_Deductions_DeductionId",
                table: "Trans_Deductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Deductions_Hr_Employees_EmployeeId",
                table: "Trans_Deductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Deductions_Trans_AmountTypes_AmountTypeId",
                table: "Trans_Deductions");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Deductions_Trans_SalaryEffects_SalaryEffectId",
                table: "Trans_Deductions");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Trans_Deductions");

            migrationBuilder.RenameColumn(
                name: "SalaryEffectId",
                table: "Trans_Deductions",
                newName: "Value_type");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Trans_Deductions",
                newName: "Salary_effect_id");

            migrationBuilder.RenameColumn(
                name: "DeductionId",
                table: "Trans_Deductions",
                newName: "Employee_id");

            migrationBuilder.RenameColumn(
                name: "AmountTypeId",
                table: "Trans_Deductions",
                newName: "Deduction_id");

            migrationBuilder.RenameColumn(
                name: "ActionMonth",
                table: "Trans_Deductions",
                newName: "Action_month");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Deductions_SalaryEffectId",
                table: "Trans_Deductions",
                newName: "IX_Trans_Deductions_Value_type");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Deductions_EmployeeId",
                table: "Trans_Deductions",
                newName: "IX_Trans_Deductions_Salary_effect_id");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Deductions_DeductionId",
                table: "Trans_Deductions",
                newName: "IX_Trans_Deductions_Employee_id");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Deductions_AmountTypeId",
                table: "Trans_Deductions",
                newName: "IX_Trans_Deductions_Deduction_id");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64f3eca5-8800-4a94-a236-7c7734252770", "AQAAAAIAAYagAAAAEBMqWWkfaQ/jXTsIfzllLVv6dIh3RiOXgnm9+aO32kzWbKlNOSvxnqoWItUfW4x1ow==", "4a47ce5e-680e-4c38-a6d7-8f70ff778cb8" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Deductions_Hr_Deductions_Deduction_id",
                table: "Trans_Deductions",
                column: "Deduction_id",
                principalTable: "Hr_Deductions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Deductions_Hr_Employees_Employee_id",
                table: "Trans_Deductions",
                column: "Employee_id",
                principalTable: "Hr_Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Deductions_Trans_AmountTypes_Value_type",
                table: "Trans_Deductions",
                column: "Value_type",
                principalTable: "Trans_AmountTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Deductions_Trans_SalaryEffects_Salary_effect_id",
                table: "Trans_Deductions",
                column: "Salary_effect_id",
                principalTable: "Trans_SalaryEffects",
                principalColumn: "Id");
        }
    }
}
