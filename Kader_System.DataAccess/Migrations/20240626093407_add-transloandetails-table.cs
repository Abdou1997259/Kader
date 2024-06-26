using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addtransloandetailstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Loan_Hr_Employees_EmpolyeeId",
                table: "Trans_Loan");

            migrationBuilder.RenameColumn(
                name: "EmpolyeeId",
                table: "Trans_Loan",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "DocumentType",
                table: "Trans_Loan",
                newName: "AdvanceType");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Loan_EmpolyeeId",
                table: "Trans_Loan",
                newName: "IX_Trans_Loan_EmployeeId");

            migrationBuilder.CreateTable(
                name: "Trans_Loan_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeductionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DelayCount = table.Column<int>(type: "int", nullable: false),
                    TransLoanId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trans_Loan_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trans_Loan_Details_Trans_Loan_TransLoanId",
                        column: x => x.TransLoanId,
                        principalTable: "Trans_Loan",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "192713cd-278e-43d7-8875-550431cf8e18", "AQAAAAIAAYagAAAAEEX64RtKFOf6qnSOVc9Uw3uIvCQjbz2NfwqiYjhz5HMMzsiEIvAD9ITNZMjDO870xQ==", "4ed9bd09-6b88-438d-8e69-6bbc8ad8060e" });

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Loan_Details_TransLoanId",
                table: "Trans_Loan_Details",
                column: "TransLoanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Loan_Hr_Employees_EmployeeId",
                table: "Trans_Loan",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Loan_Hr_Employees_EmployeeId",
                table: "Trans_Loan");

            migrationBuilder.DropTable(
                name: "Trans_Loan_Details");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Trans_Loan",
                newName: "EmpolyeeId");

            migrationBuilder.RenameColumn(
                name: "AdvanceType",
                table: "Trans_Loan",
                newName: "DocumentType");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Loan_EmployeeId",
                table: "Trans_Loan",
                newName: "IX_Trans_Loan_EmpolyeeId");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e9ae668-32c8-4962-a0f1-a274936186e1", "AQAAAAIAAYagAAAAEHhn2QXPv+M+8unyH7Urd1EYEDW5LmKh4X11ce6GrmthnCkkRKJuBufhve6ATaE20Q==", "8341afc9-b466-48d8-a0e6-0b9891fec279" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Loan_Hr_Employees_EmpolyeeId",
                table: "Trans_Loan",
                column: "EmpolyeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");
        }
    }
}
