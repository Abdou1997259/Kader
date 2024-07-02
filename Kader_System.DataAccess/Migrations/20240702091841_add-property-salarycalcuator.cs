using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addpropertysalarycalcuator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cacluate_Salary");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ActionDate",
                table: "Hr_SalaryCalculators",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<double>(
                name: "Salary",
                table: "Hr_SalaryCalculators",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "SpCacluateSalariesModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    JournalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalculatedSalary = table.Column<double>(type: "float", nullable: false),
                    JournalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CacluateSalaryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4edb0e2-8ab9-4bd8-8978-66c788aadd0c", "AQAAAAIAAYagAAAAEJwFeds/eWnbKFz4bq49Cz4nKSUOWep+6WcRPurkM05CTYBJxP0bYS9jCY8W8rg2Qw==", "9a82cd3e-fc0a-4bce-a6ff-68aef0689a9a" });

            migrationBuilder.UpdateData(
                table: "Hr_SalaryCalculators",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActionDate", "Salary" },
                values: new object[] { new DateOnly(1, 1, 1), 0.0 });

            migrationBuilder.UpdateData(
                table: "Hr_SalaryCalculators",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActionDate", "Salary" },
                values: new object[] { new DateOnly(1, 1, 1), 0.0 });

            migrationBuilder.UpdateData(
                table: "Hr_SalaryCalculators",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActionDate", "Salary" },
                values: new object[] { new DateOnly(1, 1, 1), 0.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpCacluateSalariesModel");

            migrationBuilder.DropColumn(
                name: "ActionDate",
                table: "Hr_SalaryCalculators");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Hr_SalaryCalculators");

            migrationBuilder.CreateTable(
                name: "Cacluate_Salary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cacluate_Salary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cacluate_Salary_Hr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79c06291-e027-4478-901d-e9ef2599af44", "AQAAAAIAAYagAAAAEPOgr2bA5S4At5XLYqCWlvpvMGXeAYEaFLXsmKmh0nprNfkSPU4nxbbMRzcykoZXLQ==", "8045f7a4-eddd-4bb5-80d4-2b779bc6d335" });

            migrationBuilder.CreateIndex(
                name: "IX_Cacluate_Salary_EmployeeId",
                table: "Cacluate_Salary",
                column: "EmployeeId",
                unique: true);
        }
    }
}
