using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class transAllowance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Allowances_Hr_Allowances_Allowance_id",
                table: "Trans_Allowances");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Allowances_Hr_Employees_Employee_id",
                table: "Trans_Allowances");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Allowances_Trans_SalaryEffects_Salary_effect_id",
                table: "Trans_Allowances");

            migrationBuilder.RenameColumn(
                name: "Salary_effect_id",
                table: "Trans_Allowances",
                newName: "SalaryEffectId");

            migrationBuilder.RenameColumn(
                name: "Employee_id",
                table: "Trans_Allowances",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "Allowance_id",
                table: "Trans_Allowances",
                newName: "AllowanceId");

            migrationBuilder.RenameColumn(
                name: "Action_month",
                table: "Trans_Allowances",
                newName: "ActionMonth");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Allowances_Salary_effect_id",
                table: "Trans_Allowances",
                newName: "IX_Trans_Allowances_SalaryEffectId");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Allowances_Employee_id",
                table: "Trans_Allowances",
                newName: "IX_Trans_Allowances_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Allowances_Allowance_id",
                table: "Trans_Allowances",
                newName: "IX_Trans_Allowances_AllowanceId");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8fbca25-f683-4ed9-8075-deb9d3130b50", "AQAAAAIAAYagAAAAEKPMRT6ujdVGymq05IKs7JvgO9ruxPnvtOMrmcNi25Mvz6SaRLgIZDgrBp4+/tIfvA==", "14d31e9d-3e5d-4785-ba3e-be3ccd87c76f" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Allowances_Hr_Allowances_AllowanceId",
                table: "Trans_Allowances",
                column: "AllowanceId",
                principalTable: "Hr_Allowances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Allowances_Hr_Employees_EmployeeId",
                table: "Trans_Allowances",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Allowances_Trans_SalaryEffects_SalaryEffectId",
                table: "Trans_Allowances",
                column: "SalaryEffectId",
                principalTable: "Trans_SalaryEffects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Allowances_Hr_Allowances_AllowanceId",
                table: "Trans_Allowances");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Allowances_Hr_Employees_EmployeeId",
                table: "Trans_Allowances");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Allowances_Trans_SalaryEffects_SalaryEffectId",
                table: "Trans_Allowances");

            migrationBuilder.RenameColumn(
                name: "SalaryEffectId",
                table: "Trans_Allowances",
                newName: "Salary_effect_id");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Trans_Allowances",
                newName: "Employee_id");

            migrationBuilder.RenameColumn(
                name: "AllowanceId",
                table: "Trans_Allowances",
                newName: "Allowance_id");

            migrationBuilder.RenameColumn(
                name: "ActionMonth",
                table: "Trans_Allowances",
                newName: "Action_month");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Allowances_SalaryEffectId",
                table: "Trans_Allowances",
                newName: "IX_Trans_Allowances_Salary_effect_id");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Allowances_EmployeeId",
                table: "Trans_Allowances",
                newName: "IX_Trans_Allowances_Employee_id");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Allowances_AllowanceId",
                table: "Trans_Allowances",
                newName: "IX_Trans_Allowances_Allowance_id");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f012b540-a775-402b-a1c5-690c42bb6c1f", "AQAAAAIAAYagAAAAEDQ784B/5kCn8EEPbPVRS02rkGYzm3R020Pv9Z9J4XxeM/0SQc+kkXCdYwd32/ig8g==", "3d64bcf8-5c36-4e7f-b9bb-618775132d4e" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Allowances_Hr_Allowances_Allowance_id",
                table: "Trans_Allowances",
                column: "Allowance_id",
                principalTable: "Hr_Allowances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Allowances_Hr_Employees_Employee_id",
                table: "Trans_Allowances",
                column: "Employee_id",
                principalTable: "Hr_Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Allowances_Trans_SalaryEffects_Salary_effect_id",
                table: "Trans_Allowances",
                column: "Salary_effect_id",
                principalTable: "Trans_SalaryEffects",
                principalColumn: "Id");
        }
    }
}
