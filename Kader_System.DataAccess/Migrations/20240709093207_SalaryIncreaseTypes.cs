using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SalaryIncreaseTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VacationDayCount",
                table: "SpCaclauateSalaryDetailedTransModel");

            migrationBuilder.DropColumn(
                name: "VacationSum",
                table: "SpCaclauateSalaryDetailedTransModel");

            migrationBuilder.RenameColumn(
                name: "CacluateSalaryId",
                table: "SpCaclauateSalaryDetailedTransModel",
                newName: "CalculateSalaryId");

            migrationBuilder.AddColumn<int>(
                name: "CalculateSalaryDetailsId",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalaryIncreaseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryIncreaseTypes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a3ac21cb-baaa-46fa-9f05-cd571d97cc1d", "AQAAAAIAAYagAAAAEHkwgWKr4dNLhOxCbS02Ej4iPzZ8hCKFa+NpVUxKaxtfV7pOIsknkjcwdvTrY8N/aw==", "95d8ef79-abd0-4ef2-b3ae-79bc5e368477" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryIncreaseTypes");

            migrationBuilder.DropColumn(
                name: "CalculateSalaryDetailsId",
                table: "SpCaclauateSalaryDetailedTransModel");

            migrationBuilder.RenameColumn(
                name: "CalculateSalaryId",
                table: "SpCaclauateSalaryDetailedTransModel",
                newName: "CacluateSalaryId");

            migrationBuilder.AddColumn<double>(
                name: "VacationDayCount",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VacationSum",
                table: "SpCaclauateSalaryDetailedTransModel",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "765dd646-1952-4b97-ab01-d0c8bcaf5922", "AQAAAAIAAYagAAAAENh4C2TArejVqEzCavvXnJHYpfG9mpwmYnL9ALoQ7VeDOayj1pcLt+10aGlCSHyHrA==", "c6d8c69e-c8f3-4806-82b3-fbaf2357afb9" });
        }
    }
}
