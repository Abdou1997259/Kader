using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeMigrationUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullNameEn",
                table: "Hr_Employees",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "[FirstNameEn]+' '+[FatherNameEn]+' '+[GrandFatherNameEn]+' '+[FamilyNameEn]",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FullNameAr",
                table: "Hr_Employees",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "[FirstNameAr]+' '+[FatherNameAr]+' '+[GrandFatherNameAr]+' '+[FamilyNameAr]",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "181ebc35-bff6-47f5-b093-22591891286d", "AQAAAAIAAYagAAAAECv+1nnWNSuZakRrXqU9FoZZDlLOFDrnEfou+APRLWJqcNKieCgAMNMBh5AH5+z2wA==", "4f5c9f7f-35d8-4f84-8e76-1d02686af434" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullNameEn",
                table: "Hr_Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "[FirstNameEn]+' '+[FatherNameEn]+' '+[GrandFatherNameEn]+' '+[FamilyNameEn]");

            migrationBuilder.AlterColumn<string>(
                name: "FullNameAr",
                table: "Hr_Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComputedColumnSql: "[FirstNameAr]+' '+[FatherNameAr]+' '+[GrandFatherNameAr]+' '+[FamilyNameAr]");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ebdc0c1f-dd11-4ef0-a31a-fa400b1a4b33", "AQAAAAIAAYagAAAAEIbGTdqDHugMnYz7MfGM7Uxs268EwhGU2nPN89pz8Oxoc07hXgzzHFZCI8yFL0Srgg==", "ce46d35b-340e-47e3-84ae-45e9fae2a3e1" });
        }
    }
}
