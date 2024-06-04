using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TitlesAndPermissionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TitlePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleId = table.Column<int>(type: "int", nullable: false),
                    SubScreenId = table.Column<int>(type: "int", nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitlePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TitlePermissions_St_ScreensSubs_SubScreenId",
                        column: x => x.SubScreenId,
                        principalTable: "St_ScreensSubs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TitlePermissions_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "392bba27-3406-4c5c-bbae-c51b48b9d9e5", "AQAAAAIAAYagAAAAEGNWMOQ3I8OihhBgG4OmvpO7rbjQxffEbeP4vK2MAKT2eeLYpEDyxT078YkKYl4xbA==", "9bda98e6-c47b-4699-8501-37dea6cdd6d6" });

            migrationBuilder.CreateIndex(
                name: "IX_TitlePermissions_SubScreenId",
                table: "TitlePermissions",
                column: "SubScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_TitlePermissions_TitleId",
                table: "TitlePermissions",
                column: "TitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TitlePermissions");

            migrationBuilder.DropTable(
                name: "Titles");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8f0f88a-a379-4cfc-8145-684e081708cf", "AQAAAAIAAYagAAAAEDjAGDKdVVkL/lZLCVQAXmh12c9RKWMRiT4K9C0aoHQVoZ/B3RBbMReXoT9kNjUYjw==", "789fe5ae-b6be-4e27-9693-9af5b0b2abc6" });
        }
    }
}
