using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TransVacationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Vacations_Hr_Employees_Employee_id",
                table: "Trans_Vacations");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Vacations_Hr_Vacations_Vacation_system_d_id",
                table: "Trans_Vacations");

            migrationBuilder.RenameColumn(
                name: "Vacation_system_d_id",
                table: "Trans_Vacations",
                newName: "VacationId");

            migrationBuilder.RenameColumn(
                name: "Start_date",
                table: "Trans_Vacations",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Employee_id",
                table: "Trans_Vacations",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "Days_count",
                table: "Trans_Vacations",
                newName: "DaysCount");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Vacations_Vacation_system_d_id",
                table: "Trans_Vacations",
                newName: "IX_Trans_Vacations_VacationId");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Vacations_Employee_id",
                table: "Trans_Vacations",
                newName: "IX_Trans_Vacations_EmployeeId");

            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "Trans_Vacations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentExtension",
                table: "Trans_Vacations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96007219-6e1f-4c01-9693-f438c2dcc7ad", "AQAAAAIAAYagAAAAEPX4swT+V/B7qIkV6QiAOU30XBRilFMqicYQeeoXeHe6G57aKrvYlUSZd/I/cn5PiQ==", "0c2c2143-398f-4be6-9ca4-990b499ed177" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Vacations_Hr_Employees_EmployeeId",
                table: "Trans_Vacations",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Vacations_Hr_Vacations_VacationId",
                table: "Trans_Vacations",
                column: "VacationId",
                principalTable: "Hr_Vacations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Vacations_Hr_Employees_EmployeeId",
                table: "Trans_Vacations");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Vacations_Hr_Vacations_VacationId",
                table: "Trans_Vacations");

            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Trans_Vacations");

            migrationBuilder.DropColumn(
                name: "AttachmentExtension",
                table: "Trans_Vacations");

            migrationBuilder.RenameColumn(
                name: "VacationId",
                table: "Trans_Vacations",
                newName: "Vacation_system_d_id");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Trans_Vacations",
                newName: "Start_date");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Trans_Vacations",
                newName: "Employee_id");

            migrationBuilder.RenameColumn(
                name: "DaysCount",
                table: "Trans_Vacations",
                newName: "Days_count");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Vacations_VacationId",
                table: "Trans_Vacations",
                newName: "IX_Trans_Vacations_Vacation_system_d_id");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Vacations_EmployeeId",
                table: "Trans_Vacations",
                newName: "IX_Trans_Vacations_Employee_id");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70e24642-3b39-4684-94f0-a786cc8b8135", "AQAAAAIAAYagAAAAEJjxoy/ceEpB98cqdmVOgV3Dl94fAsf1hCyW6y0Rg7wmPBFbH1O95V6PXK0Fw7xBSA==", "3706f049-1ecf-4aac-aa2b-673d31856a25" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Vacations_Hr_Employees_Employee_id",
                table: "Trans_Vacations",
                column: "Employee_id",
                principalTable: "Hr_Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Vacations_Hr_Vacations_Vacation_system_d_id",
                table: "Trans_Vacations",
                column: "Vacation_system_d_id",
                principalTable: "Hr_Vacations",
                principalColumn: "Id");
        }
    }
}
