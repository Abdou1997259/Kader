using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addLeavePermessionTableRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Add_date",
                table: "Hr_LeavePermissionRequest",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Added_by",
                table: "Hr_LeavePermissionRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteBy",
                table: "Hr_LeavePermissionRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Hr_LeavePermissionRequest",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Hr_LeavePermissionRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Hr_LeavePermissionRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdateBy",
                table: "Hr_LeavePermissionRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Hr_LeavePermissionRequest",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e04b919-ac65-417f-8e38-e6e32148848a", "AQAAAAIAAYagAAAAEFl3N/cdtaYaLiDQV8btvrkc6W/Vf9hrb+q2zqDlFfq8wATcq89zBifjm4jWg4Rfhg==", "d382ed47-ad63-41e8-9a3a-5c4afde9693b" });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_LeavePermissionRequest_EmployeeId",
                table: "Hr_LeavePermissionRequest",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_LeavePermissionRequest_Hr_Employees_EmployeeId",
                table: "Hr_LeavePermissionRequest",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_LeavePermissionRequest_Hr_Employees_EmployeeId",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropIndex(
                name: "IX_Hr_LeavePermissionRequest_EmployeeId",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropColumn(
                name: "Add_date",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropColumn(
                name: "Added_by",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f25faff9-b1b5-45db-88b1-77c75432d40e", "AQAAAAIAAYagAAAAEIQwOqrW9lAYNlZxVmnK7VpDn+OLE05jX/+7x5JH04hzz/Acnt0h/jPAWapkTwJEPQ==", "740debde-1aa4-4b52-8e5d-2ae2f23bcb0d" });
        }
    }
}
