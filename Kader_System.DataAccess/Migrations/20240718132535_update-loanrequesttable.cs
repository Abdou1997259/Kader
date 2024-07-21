using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateloanrequesttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_LoanRequest_Hr_Employees_EmployeeId",
                table: "Hr_LoanRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hr_LoanRequest",
                table: "Hr_LoanRequest");

            migrationBuilder.RenameTable(
                name: "Hr_LoanRequest",
                newName: "hr_loan_request");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_LoanRequest_EmployeeId",
                table: "hr_loan_request",
                newName: "IX_hr_loan_request_EmployeeId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "hr_loan_request",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hr_loan_request",
                table: "hr_loan_request",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d21c7e02-554a-4d9a-9e51-25261ef9655b", "AQAAAAIAAYagAAAAEFNbYcNvSlIKRNsKyppml7JgHA01wOUc5bIq5lI+N/EY2V4UnYsIGyoMTnaIJL/pjQ==", "92c5424f-58d3-4cec-a90e-e0ae63481b08" });

            migrationBuilder.AddForeignKey(
                name: "FK_hr_loan_request_Hr_Employees_EmployeeId",
                table: "hr_loan_request",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hr_loan_request_Hr_Employees_EmployeeId",
                table: "hr_loan_request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hr_loan_request",
                table: "hr_loan_request");

            migrationBuilder.RenameTable(
                name: "hr_loan_request",
                newName: "Hr_LoanRequest");

            migrationBuilder.RenameIndex(
                name: "IX_hr_loan_request_EmployeeId",
                table: "Hr_LoanRequest",
                newName: "IX_Hr_LoanRequest_EmployeeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Hr_LoanRequest",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hr_LoanRequest",
                table: "Hr_LoanRequest",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff7e09cb-f52b-44de-911a-aae8d1f2b5f9", "AQAAAAIAAYagAAAAEOYPycitq68CAQsCHpYJv17NlOI4C9dXMGKdCbVt7SSzYpHAmX2GkuHjVj0Z1Er/8Q==", "b394dcf5-14fc-427d-a201-7c2807136311" });

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_LoanRequest_Hr_Employees_EmployeeId",
                table: "Hr_LoanRequest",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");
        }
    }
}
