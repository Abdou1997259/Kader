using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class transBenefitMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Benefits_Hr_Benefits_Benefit_id",
                table: "Trans_Benefits");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Benefits_Hr_Employees_Employee_id",
                table: "Trans_Benefits");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Benefits_Trans_AmountTypes_Value_type",
                table: "Trans_Benefits");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Benefits_Trans_SalaryEffects_Salary_effect_id",
                table: "Trans_Benefits");

            migrationBuilder.RenameColumn(
                name: "Value_type",
                table: "Trans_Benefits",
                newName: "ValueTypeId");

            migrationBuilder.RenameColumn(
                name: "Salary_effect_id",
                table: "Trans_Benefits",
                newName: "SalaryEffectId");

            migrationBuilder.RenameColumn(
                name: "Employee_id",
                table: "Trans_Benefits",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "Benefit_id",
                table: "Trans_Benefits",
                newName: "BenefitId");

            migrationBuilder.RenameColumn(
                name: "Action_month",
                table: "Trans_Benefits",
                newName: "ActionMonth");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Benefits_Value_type",
                table: "Trans_Benefits",
                newName: "IX_Trans_Benefits_ValueTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Benefits_Salary_effect_id",
                table: "Trans_Benefits",
                newName: "IX_Trans_Benefits_SalaryEffectId");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Benefits_Employee_id",
                table: "Trans_Benefits",
                newName: "IX_Trans_Benefits_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Benefits_Benefit_id",
                table: "Trans_Benefits",
                newName: "IX_Trans_Benefits_BenefitId");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Trans_Benefits",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e9500a9-8f99-4b30-808e-706868458d1a", "AQAAAAIAAYagAAAAEATDg8OBL3A4V6q1F36xlTxL3MBIAIbMwQ0/0dYW58H2CuoZmL0dZ/g3/MxSAccQMA==", "62d577a5-e6d6-4ccd-be37-6fe1e89c5c92" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Benefits_Hr_Benefits_BenefitId",
                table: "Trans_Benefits",
                column: "BenefitId",
                principalTable: "Hr_Benefits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Benefits_Hr_Employees_EmployeeId",
                table: "Trans_Benefits",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Benefits_Trans_AmountTypes_ValueTypeId",
                table: "Trans_Benefits",
                column: "ValueTypeId",
                principalTable: "Trans_AmountTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Benefits_Trans_SalaryEffects_SalaryEffectId",
                table: "Trans_Benefits",
                column: "SalaryEffectId",
                principalTable: "Trans_SalaryEffects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Benefits_Hr_Benefits_BenefitId",
                table: "Trans_Benefits");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Benefits_Hr_Employees_EmployeeId",
                table: "Trans_Benefits");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Benefits_Trans_AmountTypes_ValueTypeId",
                table: "Trans_Benefits");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Benefits_Trans_SalaryEffects_SalaryEffectId",
                table: "Trans_Benefits");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Trans_Benefits");

            migrationBuilder.RenameColumn(
                name: "ValueTypeId",
                table: "Trans_Benefits",
                newName: "Value_type");

            migrationBuilder.RenameColumn(
                name: "SalaryEffectId",
                table: "Trans_Benefits",
                newName: "Salary_effect_id");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Trans_Benefits",
                newName: "Employee_id");

            migrationBuilder.RenameColumn(
                name: "BenefitId",
                table: "Trans_Benefits",
                newName: "Benefit_id");

            migrationBuilder.RenameColumn(
                name: "ActionMonth",
                table: "Trans_Benefits",
                newName: "Action_month");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Benefits_ValueTypeId",
                table: "Trans_Benefits",
                newName: "IX_Trans_Benefits_Value_type");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Benefits_SalaryEffectId",
                table: "Trans_Benefits",
                newName: "IX_Trans_Benefits_Salary_effect_id");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Benefits_EmployeeId",
                table: "Trans_Benefits",
                newName: "IX_Trans_Benefits_Employee_id");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Benefits_BenefitId",
                table: "Trans_Benefits",
                newName: "IX_Trans_Benefits_Benefit_id");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8fbca25-f683-4ed9-8075-deb9d3130b50", "AQAAAAIAAYagAAAAEKPMRT6ujdVGymq05IKs7JvgO9ruxPnvtOMrmcNi25Mvz6SaRLgIZDgrBp4+/tIfvA==", "14d31e9d-3e5d-4785-ba3e-be3ccd87c76f" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Benefits_Hr_Benefits_Benefit_id",
                table: "Trans_Benefits",
                column: "Benefit_id",
                principalTable: "Hr_Benefits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Benefits_Hr_Employees_Employee_id",
                table: "Trans_Benefits",
                column: "Employee_id",
                principalTable: "Hr_Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Benefits_Trans_AmountTypes_Value_type",
                table: "Trans_Benefits",
                column: "Value_type",
                principalTable: "Trans_AmountTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Benefits_Trans_SalaryEffects_Salary_effect_id",
                table: "Trans_Benefits",
                column: "Salary_effect_id",
                principalTable: "Trans_SalaryEffects",
                principalColumn: "Id");
        }
    }
}
