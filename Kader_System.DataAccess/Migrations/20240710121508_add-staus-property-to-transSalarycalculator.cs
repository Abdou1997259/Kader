using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addstauspropertytotransSalarycalculator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "trans_salary_calculators",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "trans_salary_calculators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActive", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName", "VisiblePassword" },
                values: new object[] { "ec993647-dc80-4b11-91f2-51a310f0d307", "mohammed88@gmail.com", true, true, "MOHAMMED88@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEAlAOf7mWLh8jMa+D5BFAfhqSorzLdFJO47yZPlGH1UJc4Gez7uMy3si5N3lLIL+xg==", "406aa7ba-bd0b-4ffb-82e8-5eea3ddc51a2", "admin", "123456" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "trans_salary_calculators");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "trans_salary_calculators");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActive", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName", "VisiblePassword" },
                values: new object[] { "0befd46e-4d8e-4f8c-8960-e2ecbe2f7987", null, false, false, null, null, "AQAAAAIAAYagAAAAEJW9fZnBFgUbMfKhT6je67DfMrDKJOsmYbEkLFm+NUbHvR2l2ZZS3GRZI+PuYakEIQ==", "09487add-d943-4ff2-a662-6d958c4822bf", null, null });
        }
    }
}
