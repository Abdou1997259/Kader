using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SettingScreenMigrationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_St_MainScreens_St_MainScreenCategories_Screen_cat_id",
                table: "St_MainScreens");

            migrationBuilder.DropForeignKey(
                name: "FK_St_SubMainScreenActions_St_SubMainScreens_SubMainScreenId",
                table: "St_SubMainScreenActions");

            migrationBuilder.DropTable(
                name: "St_MainScreenCategories");

            migrationBuilder.DropTable(
                name: "St_SubMainScreens");

            migrationBuilder.DropIndex(
                name: "IX_St_MainScreens_Screen_cat_id",
                table: "St_MainScreens");

            migrationBuilder.DropColumn(
                name: "Screen_cat_id",
                table: "St_MainScreens");

            migrationBuilder.RenameColumn(
                name: "SubMainScreenId",
                table: "St_SubMainScreenActions",
                newName: "ScreenSubId");

            migrationBuilder.RenameIndex(
                name: "IX_St_SubMainScreenActions_SubMainScreenId",
                table: "St_SubMainScreenActions",
                newName: "IX_St_SubMainScreenActions_ScreenSubId");

            migrationBuilder.RenameColumn(
                name: "Screen_cat_title_en",
                table: "St_MainScreens",
                newName: "Screen_main_title_en");

            migrationBuilder.RenameColumn(
                name: "Screen_cat_title_ar",
                table: "St_MainScreens",
                newName: "Screen_main_title_ar");

            migrationBuilder.AddColumn<string>(
                name: "ImageExtension",
                table: "St_MainScreens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Screen_main_image",
                table: "St_MainScreens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "St_MainScreenCats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Screen_cat_title_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Screen_cat_title_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainScreenId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_St_MainScreenCats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_St_MainScreenCats_St_MainScreens_MainScreenId",
                        column: x => x.MainScreenId,
                        principalTable: "St_MainScreens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "St_ScreensSubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Screen_sub_title_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Screen_sub_title_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreenCatId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_St_ScreensSubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_St_ScreensSubs_St_MainScreenCats_ScreenCatId",
                        column: x => x.ScreenCatId,
                        principalTable: "St_MainScreenCats",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8f0f88a-a379-4cfc-8145-684e081708cf", "AQAAAAIAAYagAAAAEDjAGDKdVVkL/lZLCVQAXmh12c9RKWMRiT4K9C0aoHQVoZ/B3RBbMReXoT9kNjUYjw==", "789fe5ae-b6be-4e27-9693-9af5b0b2abc6" });

            migrationBuilder.CreateIndex(
                name: "IX_St_MainScreenCats_MainScreenId",
                table: "St_MainScreenCats",
                column: "MainScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_St_ScreensSubs_ScreenCatId",
                table: "St_ScreensSubs",
                column: "ScreenCatId");

            migrationBuilder.AddForeignKey(
                name: "FK_St_SubMainScreenActions_St_ScreensSubs_ScreenSubId",
                table: "St_SubMainScreenActions",
                column: "ScreenSubId",
                principalTable: "St_ScreensSubs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_St_SubMainScreenActions_St_ScreensSubs_ScreenSubId",
                table: "St_SubMainScreenActions");

            migrationBuilder.DropTable(
                name: "St_ScreensSubs");

            migrationBuilder.DropTable(
                name: "St_MainScreenCats");

            migrationBuilder.DropColumn(
                name: "ImageExtension",
                table: "St_MainScreens");

            migrationBuilder.DropColumn(
                name: "Screen_main_image",
                table: "St_MainScreens");

            migrationBuilder.RenameColumn(
                name: "ScreenSubId",
                table: "St_SubMainScreenActions",
                newName: "SubMainScreenId");

            migrationBuilder.RenameIndex(
                name: "IX_St_SubMainScreenActions_ScreenSubId",
                table: "St_SubMainScreenActions",
                newName: "IX_St_SubMainScreenActions_SubMainScreenId");

            migrationBuilder.RenameColumn(
                name: "Screen_main_title_en",
                table: "St_MainScreens",
                newName: "Screen_cat_title_en");

            migrationBuilder.RenameColumn(
                name: "Screen_main_title_ar",
                table: "St_MainScreens",
                newName: "Screen_cat_title_ar");

            migrationBuilder.AddColumn<int>(
                name: "Screen_cat_id",
                table: "St_MainScreens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "St_MainScreenCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImageExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Screen_main_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Screen_main_title_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Screen_main_title_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_St_MainScreenCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "St_SubMainScreens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Screen_main_id = table.Column<int>(type: "int", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Screen_sub_title_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Screen_sub_title_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_St_SubMainScreens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_St_SubMainScreens_St_MainScreens_Screen_main_id",
                        column: x => x.Screen_main_id,
                        principalTable: "St_MainScreens",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5183c63-dcd3-4e33-ad79-1eb0abd1961f", "AQAAAAIAAYagAAAAECy+kARK7bfBCqVD4PVg/23G8VJSyPXtQKkbjhSz1nZqYpH/n2rk+6o2x9067TXIYA==", "2a7ef96b-f1b1-42dd-933b-fb90555b4527" });

            migrationBuilder.CreateIndex(
                name: "IX_St_MainScreens_Screen_cat_id",
                table: "St_MainScreens",
                column: "Screen_cat_id");

            migrationBuilder.CreateIndex(
                name: "IX_St_SubMainScreens_Screen_main_id",
                table: "St_SubMainScreens",
                column: "Screen_main_id");

            migrationBuilder.AddForeignKey(
                name: "FK_St_MainScreens_St_MainScreenCategories_Screen_cat_id",
                table: "St_MainScreens",
                column: "Screen_cat_id",
                principalTable: "St_MainScreenCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_St_SubMainScreenActions_St_SubMainScreens_SubMainScreenId",
                table: "St_SubMainScreenActions",
                column: "SubMainScreenId",
                principalTable: "St_SubMainScreens",
                principalColumn: "Id");
        }
    }
}
