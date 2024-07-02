using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class detailsforcalc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryCalculator");

            migrationBuilder.RenameColumn(
                name: "JournalDate",
                table: "SpCacluateSalariesModel",
                newName: "StartCalculationDate");

            migrationBuilder.CreateTable(
                name: "trans_salary_calculators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    ManagementId = table.Column<int>(type: "int", nullable: false),
                    IsMigrated = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_trans_salary_calculators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trans_salary_calculators_details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransSalaryCalculatorsId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_trans_salary_calculators_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trans_salary_calculators_details_trans_salary_calculators_TransSalaryCalculatorsId",
                        column: x => x.TransSalaryCalculatorsId,
                        principalTable: "trans_salary_calculators",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ee7d6b7-40e0-4c80-8f44-da1de182fe07", "AQAAAAIAAYagAAAAEPUDdaiQGzKHp8lXlgLwj+1C8epgFyg1l05nsj+pcEsAWFmRjjpljyCcLDv9r2iI6w==", "73dc9956-8955-46d3-8be6-c2220f4bd9ff" });

            migrationBuilder.CreateIndex(
                name: "IX_trans_salary_calculators_details_TransSalaryCalculatorsId",
                table: "trans_salary_calculators_details",
                column: "TransSalaryCalculatorsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trans_salary_calculators_details");

            migrationBuilder.DropTable(
                name: "trans_salary_calculators");

            migrationBuilder.RenameColumn(
                name: "StartCalculationDate",
                table: "SpCacluateSalariesModel",
                newName: "JournalDate");

            migrationBuilder.CreateTable(
                name: "SalaryCalculator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryCalculator", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "acd9e8dc-a9c5-4bdd-a480-cf11e268ceb7", "AQAAAAIAAYagAAAAEIX0wJIvwS7WU+tLBEggfmsqW7Olm8T4A/vi4jCfi4UA6wBuurmvLvQHUtDNRPtZig==", "67ce5671-6d28-4dd6-9b4a-8bb3d02b9df9" });
        }
    }
}
