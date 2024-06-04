using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Departments_DepartmentId",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Managements_ManagementId",
                table: "Hr_Employees");

            migrationBuilder.DropIndex(
                name: "IX_Hr_Managements_ManagerId",
                table: "Hr_Managements");

            migrationBuilder.DropIndex(
                name: "IX_Hr_Employees_DepartmentId",
                table: "Hr_Employees");

            migrationBuilder.DropIndex(
                name: "IX_Hr_Employees_ManagementId",
                table: "Hr_Employees");

            migrationBuilder.DropIndex(
                name: "IX_Hr_Departments_ManagerId",
                table: "Hr_Departments");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Auth_Roles",
                columns: new[] { "Id", "Add_date", "Added_by", "ConcurrencyStamp", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NormalizedName", "Title_name_ar", "UpdateBy", "UpdateDate" },
                values: new object[] { "0ffa8112-ba0d-4416-b0ed-992897ac896e", null, null, "1", null, null, true, false, "User", "USER", "مستخدم", null, null });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f012b540-a775-402b-a1c5-690c42bb6c1f", "AQAAAAIAAYagAAAAEDQ784B/5kCn8EEPbPVRS02rkGYzm3R020Pv9Z9J4XxeM/0SQc+kkXCdYwd32/ig8g==", "3d64bcf8-5c36-4e7f-b9bb-618775132d4e" });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Managements_ManagerId",
                table: "Hr_Managements",
                column: "ManagerId",
                unique: true,
                filter: "[ManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Departments_ManagerId",
                table: "Hr_Departments",
                column: "ManagerId",
                unique: true,
                filter: "[ManagerId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hr_Managements_ManagerId",
                table: "Hr_Managements");

            migrationBuilder.DropIndex(
                name: "IX_Hr_Departments_ManagerId",
                table: "Hr_Departments");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Auth_Roles",
                keyColumn: "Id",
                keyValue: "0ffa8112-ba0d-4416-b0ed-992897ac896e");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "086dbd12-e961-4d4a-9bd4-e349ad1f3b4d", "AQAAAAIAAYagAAAAEAd6bNU2k6rtpbTzA1sweH/wqUYSI5YHicbbTHrVlEDuCgfMIkHMf5eQZI9BhK+/MA==", "6b11c3d5-0bc3-483c-835a-9536e56dc7e2" });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Managements_ManagerId",
                table: "Hr_Managements",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_DepartmentId",
                table: "Hr_Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_ManagementId",
                table: "Hr_Employees",
                column: "ManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Departments_ManagerId",
                table: "Hr_Departments",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Departments_DepartmentId",
                table: "Hr_Employees",
                column: "DepartmentId",
                principalTable: "Hr_Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Managements_ManagementId",
                table: "Hr_Employees",
                column: "ManagementId",
                principalTable: "Hr_Managements",
                principalColumn: "Id");
        }
    }
}
