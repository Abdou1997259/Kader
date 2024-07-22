using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removehrloan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hr_loan_request_Hr_Employees_EmployeeId",
                table: "hr_loan_request");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hr_loan_request",
                table: "hr_loan_request");

            migrationBuilder.RenameTable(
                name: "hr_loan_request",
                newName: "hr_loan_request");

            migrationBuilder.RenameIndex(
                name: "IX_hr_loan_request_EmployeeId",
                table: "hr_loan_request",
                newName: "IX_hr_loan_request_EmployeeId");

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
                values: new object[] { "cf0ab5e6-ddd4-42f8-98a6-e985def3b851", "AQAAAAIAAYagAAAAEM3k6RRxPxDtTOMQnfQDoI+6H7Gcrw4iTZorwdqO2v8z6tRG1xduhM0iyHj7NXQ7hg==", "7feb8e20-83ef-425f-bc63-145e8c291e94" });

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
                newName: "hr_loan_request");

            migrationBuilder.RenameIndex(
                name: "IX_hr_loan_request_EmployeeId",
                table: "hr_loan_request",
                newName: "IX_hr_loan_request_EmployeeId");

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
                values: new object[] { "414b7f69-661e-4f13-b5f0-23214e55f0fe", "AQAAAAIAAYagAAAAEKa4NwMpEERmAuemyTCriBxlOCFfhfY2PDY/PEWrcKVV4rwR4XDUjtLjGxasDNqwvg==", "910ab86d-b7a6-4583-b5c1-f4697a7069be" });
 
        }
    }
}
