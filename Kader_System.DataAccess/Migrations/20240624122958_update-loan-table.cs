using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateloantable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hr_Loan");

            migrationBuilder.CreateTable(
                name: "Trans_Loan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartLoanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentType = table.Column<short>(type: "smallint", nullable: false),
                    MonthlyDeducted = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PrevDedcutedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InstallmentCount = table.Column<int>(type: "int", nullable: false),
                    MakePaymentJournal = table.Column<bool>(type: "bit", nullable: false),
                    IsDeductedFromSalary = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpolyeeId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Trans_Loan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trans_Loan_Hr_Employees_EmpolyeeId",
                        column: x => x.EmpolyeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f474006-ac21-4807-acb6-a4c6b5e00559", "AQAAAAIAAYagAAAAEJdwEpRfQj5pxkTH99x+YwksrSvPXWiiD5+Hn7/I99DsTYi0iCHI0qxCbO6pAO7S5Q==", "322913ae-dc4c-4b08-bbf6-37747cd56d5c" });

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Loan_EmpolyeeId",
                table: "Trans_Loan",
                column: "EmpolyeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trans_Loan");

            migrationBuilder.CreateTable(
                name: "Hr_Loan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentType = table.Column<short>(type: "smallint", nullable: false),
                    EmpolyeeId = table.Column<int>(type: "int", nullable: false),
                    EndDoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstallmentCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeductedFromSalary = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MakePaymentJournal = table.Column<bool>(type: "bit", nullable: false),
                    MonthlyDeducted = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrevDedcutedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartLoanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_Loan", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eba158f5-d1f5-4f23-84e9-170e65377e73", "AQAAAAIAAYagAAAAEKKATrxnzHka18hmoTEJ2+u+u6XpigxoFI0iCvP9JbUXOtSXsrTGjE/EQp9CX9425g==", "b8f7fae0-3db8-4c9e-a1d8-1cc230e1c3c7" });
        }
    }
}
