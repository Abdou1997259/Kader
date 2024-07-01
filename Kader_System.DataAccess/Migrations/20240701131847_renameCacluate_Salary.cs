using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class renameCacluate_Salary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cacluate_Salary_Hr_Employees_EmpolyeeId",
                table: "Cacluate_Salary");

            migrationBuilder.RenameColumn(
                name: "EmpolyeeId",
                table: "Cacluate_Salary",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Cacluate_Salary_EmpolyeeId",
                table: "Cacluate_Salary",
                newName: "IX_Cacluate_Salary_EmployeeId");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79c06291-e027-4478-901d-e9ef2599af44", "AQAAAAIAAYagAAAAEPOgr2bA5S4At5XLYqCWlvpvMGXeAYEaFLXsmKmh0nprNfkSPU4nxbbMRzcykoZXLQ==", "8045f7a4-eddd-4bb5-80d4-2b779bc6d335" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cacluate_Salary_Hr_Employees_EmployeeId",
                table: "Cacluate_Salary",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cacluate_Salary_Hr_Employees_EmployeeId",
                table: "Cacluate_Salary");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Cacluate_Salary",
                newName: "EmpolyeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Cacluate_Salary_EmployeeId",
                table: "Cacluate_Salary",
                newName: "IX_Cacluate_Salary_EmpolyeeId");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "98060a0f-e20f-4163-9061-4b801d4f7d9b", "AQAAAAIAAYagAAAAECftS27LsWXJAMb5La5XymZEqIfE4qUL1W2+c8PA56yMgn6j0zpsXCFF4NV7vrHJyA==", "86e0cebd-a1aa-4778-ab46-51b2d7d1bb63" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cacluate_Salary_Hr_Employees_EmpolyeeId",
                table: "Cacluate_Salary",
                column: "EmpolyeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");
        }
    }
}
