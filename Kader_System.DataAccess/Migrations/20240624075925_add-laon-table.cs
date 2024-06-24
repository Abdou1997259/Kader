using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addlaontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hr_Loan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartLoanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDoDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentType = table.Column<short>(type: "smallint", nullable: false),
                    MonthlyDeducted = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrevDedcutedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmpolyeeId = table.Column<int>(type: "int", nullable: false),
                    InstallmentCount = table.Column<int>(type: "int", nullable: false),
                    MakePaymentJournal = table.Column<bool>(type: "bit", nullable: false),
                    IsDeductedFromSalary = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hr_Loan");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94ef0d60-7f5e-45aa-bf7a-c7b4b752f9e5", "AQAAAAIAAYagAAAAENIYkfrLh0vQYbOdIOVkphgaQIkLKWMh671PE/IWrf4EanUqyb8+heswDvWCUQItqA==", "35287338-41b8-41a0-b53c-47c20a65ab44" });
        }
    }
}
