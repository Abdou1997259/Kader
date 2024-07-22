using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_StatusRequests_cols_in_all_requests3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Hr_VacationRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Hr_SalaryIncreaseRequest");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "hr_resignation_request");


            migrationBuilder.DropColumn(
                name: "Status",
                table: "hr_loan_request");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Hr_LeavePermissionRequest");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Hr_DelayPermission");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "hr_contract_termination");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "hr_allowance_request");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "467eea2b-643e-4f60-8997-8e3cf2e421db", "AQAAAAIAAYagAAAAEEJ6yRO3RcYYjqLACH00Gkar3frchnwdRjhKXxrk5N7qjY/mjFSfLd10M5lHIFDGTg==", "5f08bd33-7168-4186-b463-e5a2f6c12749" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Hr_VacationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Hr_SalaryIncreaseRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "hr_resignation_request",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "hr_loan_request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Hr_LeavePermissionRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Hr_DelayPermission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "hr_contract_termination",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "hr_allowance_request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fa79587-0b8c-4dfa-b6e6-2e57fbfabed6", "AQAAAAIAAYagAAAAECwGRqGZVT7xvJbzOlTkQSx0WNlBThLx4gd5t7bKlDH+/6w3pqz2DkPSTNlFuZAYcg==", "bcdaa41c-1070-482c-bfe7-234d469f0b52" });
        }
    }
}
