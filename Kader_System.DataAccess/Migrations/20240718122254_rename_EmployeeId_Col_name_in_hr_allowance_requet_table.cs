using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class rename_EmployeeId_Col_name_in_hr_allowance_requet_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hr_allowance_requet_Hr_Allowances_allowance_id",
                table: "hr_allowance_requet");

            migrationBuilder.DropForeignKey(
                name: "FK_hr_allowance_requet_Hr_Employees_employe_id",
                table: "hr_allowance_requet");

            migrationBuilder.DropForeignKey(
                name: "FK_hr_allowance_requet_Trans_SalaryEffects_allowance_type_id",
                table: "hr_allowance_requet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hr_allowance_requet",
                table: "hr_allowance_requet");

            migrationBuilder.RenameTable(
                name: "hr_allowance_requet",
                newName: "hr_allowance_request");

            migrationBuilder.RenameColumn(
                name: "employe_id",
                table: "hr_allowance_request",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_hr_allowance_requet_employe_id",
                table: "hr_allowance_request",
                newName: "IX_hr_allowance_request_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_hr_allowance_requet_allowance_type_id",
                table: "hr_allowance_request",
                newName: "IX_hr_allowance_request_allowance_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_hr_allowance_requet_allowance_id",
                table: "hr_allowance_request",
                newName: "IX_hr_allowance_request_allowance_id");

            migrationBuilder.AlterColumn<string>(
                name: "TransNameEn",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TransNameAr",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FullNameEn",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FullNameAr",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hr_allowance_request",
                table: "hr_allowance_request",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "588cb136-f4f3-42c9-94c1-36c7a23df9ce", "AQAAAAIAAYagAAAAENDMGGJrG3XzUNjIcG2PYlH4nwde6AMz6JnzktsxJvz5Bd/dOzQ60Q09GpGpLbTeBw==", "0a3ac72a-8ae5-46b3-81fd-1924c9234a7e" });

            migrationBuilder.AddForeignKey(
                name: "FK_hr_allowance_request_Hr_Allowances_allowance_id",
                table: "hr_allowance_request",
                column: "allowance_id",
                principalTable: "Hr_Allowances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_hr_allowance_request_Hr_Employees_EmployeeId",
                table: "hr_allowance_request",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_hr_allowance_request_Trans_SalaryEffects_allowance_type_id",
                table: "hr_allowance_request",
                column: "allowance_type_id",
                principalTable: "Trans_SalaryEffects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hr_allowance_request_Hr_Allowances_allowance_id",
                table: "hr_allowance_request");

            migrationBuilder.DropForeignKey(
                name: "FK_hr_allowance_request_Hr_Employees_EmployeeId",
                table: "hr_allowance_request");

            migrationBuilder.DropForeignKey(
                name: "FK_hr_allowance_request_Trans_SalaryEffects_allowance_type_id",
                table: "hr_allowance_request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hr_allowance_request",
                table: "hr_allowance_request");

            migrationBuilder.RenameTable(
                name: "hr_allowance_request",
                newName: "hr_allowance_requet");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "hr_allowance_requet",
                newName: "employe_id");

            migrationBuilder.RenameIndex(
                name: "IX_hr_allowance_request_EmployeeId",
                table: "hr_allowance_requet",
                newName: "IX_hr_allowance_requet_employe_id");

            migrationBuilder.RenameIndex(
                name: "IX_hr_allowance_request_allowance_type_id",
                table: "hr_allowance_requet",
                newName: "IX_hr_allowance_requet_allowance_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_hr_allowance_request_allowance_id",
                table: "hr_allowance_requet",
                newName: "IX_hr_allowance_requet_allowance_id");

            migrationBuilder.AlterColumn<string>(
                name: "TransNameEn",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransNameAr",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullNameEn",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullNameAr",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_hr_allowance_requet",
                table: "hr_allowance_requet",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e722ee1d-e06a-49c9-9e33-2dff6afa74ca", "AQAAAAIAAYagAAAAELczig+/lly8viPKn+S/L697Cf30q9A17oWIJ8/aagPvh/kMS5KwG1BQXTXzDHdNkw==", "03796de7-0dbf-462d-891e-9748ed2cd0ad" });

            migrationBuilder.AddForeignKey(
                name: "FK_hr_allowance_requet_Hr_Allowances_allowance_id",
                table: "hr_allowance_requet",
                column: "allowance_id",
                principalTable: "Hr_Allowances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_hr_allowance_requet_Hr_Employees_employe_id",
                table: "hr_allowance_requet",
                column: "employe_id",
                principalTable: "Hr_Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_hr_allowance_requet_Trans_SalaryEffects_allowance_type_id",
                table: "hr_allowance_requet",
                column: "allowance_type_id",
                principalTable: "Trans_SalaryEffects",
                principalColumn: "Id");
        }
    }
}
