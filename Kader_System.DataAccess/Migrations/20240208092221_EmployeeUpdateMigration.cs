using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeUpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Hr_Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName", "VisiblePassword" },
                values: new object[] { "60aceadc-5b79-4752-8c67-2bf38d508bb8", "ADMIN", "AQAAAAIAAYagAAAAEPms30ecl1gX2+QK7E4gblKDCgVb7b4XzlNx0gSUnRFvTx7Wjj9Bzfl66zjuEjkJtQ==", "2981d044-a7ef-48a2-9b16-ad7655ba1cdd", "admin", "123456" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Hr_Employees");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName", "VisiblePassword" },
                values: new object[] { "392bba27-3406-4c5c-bbae-c51b48b9d9e5", "MR_MOHAMMED", "AQAAAAIAAYagAAAAEGNWMOQ3I8OihhBgG4OmvpO7rbjQxffEbeP4vK2MAKT2eeLYpEDyxT078YkKYl4xbA==", "9bda98e6-c47b-4699-8501-37dea6cdd6d6", "Mr_Mohammed", "Mohammed88" });
        }
    }
}
