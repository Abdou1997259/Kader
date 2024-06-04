using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TransCovenantMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Covenants_Hr_Employees_Employee_id",
                table: "Trans_Covenants");

            migrationBuilder.RenameColumn(
                name: "Name_en",
                table: "Trans_Covenants",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Name_ar",
                table: "Trans_Covenants",
                newName: "NameAr");

            migrationBuilder.RenameColumn(
                name: "Employee_id",
                table: "Trans_Covenants",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "Covenant_amount",
                table: "Trans_Covenants",
                newName: "Amount");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Covenants_Employee_id",
                table: "Trans_Covenants",
                newName: "IX_Trans_Covenants_EmployeeId");

            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "Trans_Covenants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttachmentExtension",
                table: "Trans_Covenants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f6fd8ad-2d2b-4158-8e40-5320b02306a6", "AQAAAAIAAYagAAAAEBwp0gQbLwoa0BWXhVy1+Ru7d+ZTMNK0tvwW4XZe3/0BOgx5S+MOcGJcWJmx/BC79g==", "f349df16-3849-4254-8154-d447d9906cfa" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Covenants_Hr_Employees_EmployeeId",
                table: "Trans_Covenants",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Covenants_Hr_Employees_EmployeeId",
                table: "Trans_Covenants");

            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Trans_Covenants");

            migrationBuilder.DropColumn(
                name: "AttachmentExtension",
                table: "Trans_Covenants");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Trans_Covenants",
                newName: "Name_en");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                table: "Trans_Covenants",
                newName: "Name_ar");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Trans_Covenants",
                newName: "Employee_id");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Trans_Covenants",
                newName: "Covenant_amount");

            migrationBuilder.RenameIndex(
                name: "IX_Trans_Covenants_EmployeeId",
                table: "Trans_Covenants",
                newName: "IX_Trans_Covenants_Employee_id");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96007219-6e1f-4c01-9693-f438c2dcc7ad", "AQAAAAIAAYagAAAAEPX4swT+V/B7qIkV6QiAOU30XBRilFMqicYQeeoXeHe6G57aKrvYlUSZd/I/cn5PiQ==", "0c2c2143-398f-4be6-9ca4-990b499ed177" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Covenants_Hr_Employees_Employee_id",
                table: "Trans_Covenants",
                column: "Employee_id",
                principalTable: "Hr_Employees",
                principalColumn: "Id");
        }
    }
}
