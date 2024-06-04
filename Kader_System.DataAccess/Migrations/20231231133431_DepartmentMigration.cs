using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameInEnglish",
                table: "Hr_Departments",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Hr_Departments",
                newName: "NameAr");

            migrationBuilder.AddColumn<int>(
                name: "ManagementId",
                table: "Hr_Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Hr_Departments",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "898c4bc8-9f66-4c37-92a8-1f73e8508fb8", "AQAAAAIAAYagAAAAEEwIJdUzFZKE9IyhjWjyfdMKBYe0ccoKkFvb/psh4h0fPbNQ0I0FA/uN/alO4RkCQw==", "6b6406a1-7431-44f5-ae2e-56d9b7ca5c8a" });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Departments_ManagementId",
                table: "Hr_Departments",
                column: "ManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Departments_ManagerId",
                table: "Hr_Departments",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Departments_Hr_Employees_ManagerId",
                table: "Hr_Departments",
                column: "ManagerId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Departments_Hr_Managements_ManagementId",
                table: "Hr_Departments",
                column: "ManagementId",
                principalTable: "Hr_Managements",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Departments_Hr_Employees_ManagerId",
                table: "Hr_Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Departments_Hr_Managements_ManagementId",
                table: "Hr_Departments");

            migrationBuilder.DropIndex(
                name: "IX_Hr_Departments_ManagementId",
                table: "Hr_Departments");

            migrationBuilder.DropIndex(
                name: "IX_Hr_Departments_ManagerId",
                table: "Hr_Departments");

            migrationBuilder.DropColumn(
                name: "ManagementId",
                table: "Hr_Departments");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Hr_Departments");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Hr_Departments",
                newName: "NameInEnglish");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                table: "Hr_Departments",
                newName: "Name");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f9ea9d66-1ec5-4eb1-96e5-f50f65869a0e", "AQAAAAIAAYagAAAAEIRLhJ/JxBhEAGIoOstPrp3RhrmtDHjA8qiZ4l7SZB95z7ApGD+gfaDa7VeoXklaKA==", "8719d160-159a-47b6-aec1-20b8106915b6" });
        }
    }
}
