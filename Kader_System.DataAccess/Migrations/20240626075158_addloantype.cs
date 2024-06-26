using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addloantype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeductedFromSalary",
                table: "Trans_Loan");

            migrationBuilder.DropColumn(
                name: "MakePaymentJournal",
                table: "Trans_Loan");

            migrationBuilder.AddColumn<short>(
                name: "LoanType",
                table: "Trans_Loan",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a037c520-8820-4966-a7ba-dac4203a8a72", "AQAAAAIAAYagAAAAEJJ4YU1PW3k8sAyJ6oPRYskCdY0DWRo/OxkK/ZmlxSxHFZb3wfBgj+QaLrQkJ9ogyA==", "66bac253-4254-414d-af87-f43d429578cc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoanType",
                table: "Trans_Loan");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeductedFromSalary",
                table: "Trans_Loan",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MakePaymentJournal",
                table: "Trans_Loan",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "53d57b16-5535-41aa-9356-f13c21b7683d", "AQAAAAIAAYagAAAAEJ4kXJAbKhLnb/s2kWcb4PBqmXYHywi+xirzTw/jOQnLnSNcYbje94g614a/Y7QXMA==", "4bb0673e-e3a9-4a2d-9737-097037b11bad" });
        }
    }
}
