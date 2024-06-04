using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TransactionVacationFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Vacations_Hr_Vacations_VacationId",
                table: "Trans_Vacations");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e3501005-1d8c-480d-bad6-f24d8ea95c89", "AQAAAAIAAYagAAAAEMuKljuOsDrurK+FDiTaQvESBSOcl/IfSglKeiRLzOyiXNc29dk2qrx4EkJlRaYIHQ==", "3e768c73-afce-446c-8db2-94277be8a1da" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Vacations_Hr_VacationDistributions_VacationId",
                table: "Trans_Vacations",
                column: "VacationId",
                principalTable: "Hr_VacationDistributions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Vacations_Hr_VacationDistributions_VacationId",
                table: "Trans_Vacations");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60aceadc-5b79-4752-8c67-2bf38d508bb8", "AQAAAAIAAYagAAAAEPms30ecl1gX2+QK7E4gblKDCgVb7b4XzlNx0gSUnRFvTx7Wjj9Bzfl66zjuEjkJtQ==", "2981d044-a7ef-48a2-9b16-ad7655ba1cdd" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Vacations_Hr_Vacations_VacationId",
                table: "Trans_Vacations",
                column: "VacationId",
                principalTable: "Hr_Vacations",
                principalColumn: "Id");
        }
    }
}
