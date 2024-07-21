using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_StatusRequests_cols_in_all_requests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApporvalStatus",
                table: "Hr_VacationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "Hr_VacationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "Hr_VacationRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Hr_VacationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StatusMessage",
                table: "Hr_VacationRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ApporvalStatus",
                table: "Hr_SalaryIncreaseRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "Hr_SalaryIncreaseRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "Hr_SalaryIncreaseRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Hr_SalaryIncreaseRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StatusMessage",
                table: "Hr_SalaryIncreaseRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ApporvalStatus",
                table: "hr_resignation_request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "hr_resignation_request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "hr_resignation_request",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "hr_resignation_request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StatusMessage",
                table: "hr_resignation_request",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ApporvalStatus",
                table: "hr_loan_request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "hr_loan_request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "hr_loan_request",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "hr_loan_request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StatusMessage",
                table: "hr_loan_request",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ApporvalStatus",
                table: "Hr_LeavePermissionRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "Hr_LeavePermissionRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "Hr_LeavePermissionRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Hr_LeavePermissionRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StatusMessage",
                table: "Hr_LeavePermissionRequest",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ApporvalStatus",
                table: "Hr_DelayPermission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "Hr_DelayPermission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "Hr_DelayPermission",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Hr_DelayPermission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StatusMessage",
                table: "Hr_DelayPermission",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ApporvalStatus",
                table: "hr_contract_termination",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "hr_contract_termination",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "hr_contract_termination",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "hr_contract_termination",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StatusMessage",
                table: "hr_contract_termination",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ApporvalStatus",
                table: "hr_allowance_request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "hr_allowance_request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "hr_allowance_request",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "hr_allowance_request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StatusMessage",
                table: "hr_allowance_request",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5fefec6-f5d8-4c39-89ec-e0f1327ce754", "AQAAAAIAAYagAAAAEF43j+29TDM9N557it0MmEvlmvEUtM/lrAyRFS6wMqJt1zo86LiygImUfQ9lTi/KFw==", "89e348b1-7d98-486f-8993-24dfb3f6fbe8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApporvalStatus",
                table: "Hr_VacationRequests");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Hr_VacationRequests");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "Hr_VacationRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Hr_VacationRequests");

            migrationBuilder.DropColumn(
                name: "StatusMessage",
                table: "Hr_VacationRequests");

            migrationBuilder.DropColumn(
                name: "ApporvalStatus",
                table: "Hr_SalaryIncreaseRequest");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Hr_SalaryIncreaseRequest");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "Hr_SalaryIncreaseRequest");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Hr_SalaryIncreaseRequest");

            migrationBuilder.DropColumn(
                name: "StatusMessage",
                table: "Hr_SalaryIncreaseRequest");

            migrationBuilder.DropColumn(
                name: "ApporvalStatus",
                table: "hr_resignation_request");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "hr_resignation_request");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "hr_resignation_request");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "hr_resignation_request");

            migrationBuilder.DropColumn(
                name: "StatusMessage",
                table: "hr_resignation_request");

            migrationBuilder.DropColumn(
                name: "ApporvalStatus",
                table: "hr_loan_request");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "hr_loan_request");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "hr_loan_request");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "hr_loan_request");

            migrationBuilder.DropColumn(
                name: "StatusMessage",
                table: "hr_loan_request");

            migrationBuilder.DropColumn(
                name: "ApporvalStatus",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropColumn(
                name: "StatusMessage",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropColumn(
                name: "ApporvalStatus",
                table: "Hr_DelayPermission");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Hr_DelayPermission");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "Hr_DelayPermission");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Hr_DelayPermission");

            migrationBuilder.DropColumn(
                name: "StatusMessage",
                table: "Hr_DelayPermission");

            migrationBuilder.DropColumn(
                name: "ApporvalStatus",
                table: "hr_contract_termination");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "hr_contract_termination");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "hr_contract_termination");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "hr_contract_termination");

            migrationBuilder.DropColumn(
                name: "StatusMessage",
                table: "hr_contract_termination");

            migrationBuilder.DropColumn(
                name: "ApporvalStatus",
                table: "hr_allowance_request");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "hr_allowance_request");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "hr_allowance_request");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "hr_allowance_request");

            migrationBuilder.DropColumn(
                name: "StatusMessage",
                table: "hr_allowance_request");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78d64132-d694-419c-b42a-5b653457dafb", "AQAAAAIAAYagAAAAEMTgQSeJ476BFh0iOCwbQEGXCOBtyo4YSy3yd3NhlCV2DkOWMmP1MCDcIllt15jb6w==", "d606ba70-92c3-4dc7-b873-3fd6145bcc7f" });
        }
    }
}
