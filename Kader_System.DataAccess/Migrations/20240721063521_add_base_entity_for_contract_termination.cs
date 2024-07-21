using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_base_entity_for_contract_termination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contract_termination_Hr_Employees_EmployeeId",
                table: "contract_termination");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contract_termination",
                table: "contract_termination");

            migrationBuilder.DropColumn(
                name: "allowance_request_date",
                table: "hr_allowance_request");

            migrationBuilder.RenameTable(
                name: "contract_termination",
                newName: "hr_contract_termination");

            migrationBuilder.RenameIndex(
                name: "IX_contract_termination_EmployeeId",
                table: "hr_contract_termination",
                newName: "IX_hr_contract_termination_EmployeeId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Add_date",
                table: "hr_allowance_request",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Added_by",
                table: "hr_allowance_request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "hr_allowance_request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "hr_allowance_request",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "hr_allowance_request",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "hr_allowance_request",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdateBy",
                table: "hr_allowance_request",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "hr_allowance_request",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Add_date",
                table: "hr_contract_termination",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Added_by",
                table: "hr_contract_termination",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "hr_contract_termination",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "hr_contract_termination",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "hr_contract_termination",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "hr_contract_termination",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdateBy",
                table: "hr_contract_termination",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "hr_contract_termination",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_hr_contract_termination",
                table: "hr_contract_termination",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c94572e2-9a5a-4bdf-b1b0-da070445bbf3", "AQAAAAIAAYagAAAAEI9L9uHaS/AHfZlgfZB/cMRtyVkVzF0IClQMdbXqOwURImOnVEo1skfEKoo3qwcVHA==", "d136d2d0-25ef-43c8-a235-a4f1cdcdbfe9" });

            migrationBuilder.AddForeignKey(
                name: "FK_hr_contract_termination_Hr_Employees_EmployeeId",
                table: "hr_contract_termination",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hr_contract_termination_Hr_Employees_EmployeeId",
                table: "hr_contract_termination");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hr_contract_termination",
                table: "hr_contract_termination");

            migrationBuilder.DropColumn(
                name: "Add_date",
                table: "hr_allowance_request");

            migrationBuilder.DropColumn(
                name: "Added_by",
                table: "hr_allowance_request");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "hr_allowance_request");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "hr_allowance_request");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "hr_allowance_request");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "hr_allowance_request");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "hr_allowance_request");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "hr_allowance_request");

            migrationBuilder.DropColumn(
                name: "Add_date",
                table: "hr_contract_termination");

            migrationBuilder.DropColumn(
                name: "Added_by",
                table: "hr_contract_termination");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "hr_contract_termination");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "hr_contract_termination");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "hr_contract_termination");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "hr_contract_termination");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "hr_contract_termination");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "hr_contract_termination");

            migrationBuilder.RenameTable(
                name: "hr_contract_termination",
                newName: "contract_termination");

            migrationBuilder.RenameIndex(
                name: "IX_hr_contract_termination_EmployeeId",
                table: "contract_termination",
                newName: "IX_contract_termination_EmployeeId");

            migrationBuilder.AddColumn<DateTime>(
                name: "allowance_request_date",
                table: "hr_allowance_request",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_contract_termination",
                table: "contract_termination",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b5b43dd1-34f9-41cc-9a13-515ca1bc3c8d", "AQAAAAIAAYagAAAAEFZLxNJ2Y28FygXwJQPybwV1AYuoRgcIdz7nRrKYGhonjQqawRUJcW2/xWmcP2xfrg==", "4d66988d-8a0a-43ba-aa2d-ff008cbc21cf" });

            migrationBuilder.AddForeignKey(
                name: "FK_contract_termination_Hr_Employees_EmployeeId",
                table: "contract_termination",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");
        }
    }
}
