using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CacluateSalaryIdToAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CacluateSalaryId",
                table: "Trans_Loan",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CacluateSalaryId",
                table: "Trans_Deductions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CacluateSalaryId",
                table: "Trans_Benefits",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CacluateSalaryId",
                table: "Trans_Allowances",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "98060a0f-e20f-4163-9061-4b801d4f7d9b", "AQAAAAIAAYagAAAAECftS27LsWXJAMb5La5XymZEqIfE4qUL1W2+c8PA56yMgn6j0zpsXCFF4NV7vrHJyA==", "86e0cebd-a1aa-4778-ab46-51b2d7d1bb63" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CacluateSalaryId",
                table: "Trans_Loan");

            migrationBuilder.DropColumn(
                name: "CacluateSalaryId",
                table: "Trans_Deductions");

            migrationBuilder.DropColumn(
                name: "CacluateSalaryId",
                table: "Trans_Benefits");

            migrationBuilder.DropColumn(
                name: "CacluateSalaryId",
                table: "Trans_Allowances");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35516848-a162-4211-834d-c5d5fec6ff39", "AQAAAAIAAYagAAAAEP/XzM0O03zl2JOTX/eemrEzLzllNxEDFYpqLNlFIOsazIqpyPG9zQxA54G2Uu8SJQ==", "ec2411aa-3572-4e58-8df1-0c9b1c21e922" });
        }
    }
}
