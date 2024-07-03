using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartCalculationDate",
                table: "SpCacluateSalariesModel",
                newName: "JournalDate");

            migrationBuilder.AlterColumn<double>(
                name: "Salary",
                table: "trans_salary_calculators_details",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cd7a49c0-e114-418f-a054-003984a90797", "AQAAAAIAAYagAAAAEEas8rD7V8ruo7W6E9ZVvbebS7K4aHVVCzL6dcpnkdShKdcKMc0vuiRzOLY7NpEjjw==", "66c02608-3d43-47a7-8be4-726419ffa670" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JournalDate",
                table: "SpCacluateSalariesModel",
                newName: "StartCalculationDate");

            migrationBuilder.AlterColumn<string>(
                name: "Salary",
                table: "trans_salary_calculators_details",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ee7d6b7-40e0-4c80-8f44-da1de182fe07", "AQAAAAIAAYagAAAAEPUDdaiQGzKHp8lXlgLwj+1C8epgFyg1l05nsj+pcEsAWFmRjjpljyCcLDv9r2iI6w==", "73dc9956-8955-46d3-8be6-c2220f4bd9ff" });
        }
    }
}
