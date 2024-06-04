using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ManagementMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Sections_Hr_Companies_Company_id",
                table: "Hr_Sections");

            migrationBuilder.RenameColumn(
                name: "Company_id",
                table: "Hr_Sections",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Sections_Company_id",
                table: "Hr_Sections",
                newName: "IX_Hr_Sections_CompanyId");

            migrationBuilder.CreateTable(
                name: "Hr_Managements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_Managements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_Managements_Hr_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Hr_Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Managements_Hr_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f9ea9d66-1ec5-4eb1-96e5-f50f65869a0e", "AQAAAAIAAYagAAAAEIRLhJ/JxBhEAGIoOstPrp3RhrmtDHjA8qiZ4l7SZB95z7ApGD+gfaDa7VeoXklaKA==", "8719d160-159a-47b6-aec1-20b8106915b6" });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Managements_CompanyId",
                table: "Hr_Managements",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Managements_ManagerId",
                table: "Hr_Managements",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Sections_Hr_Companies_CompanyId",
                table: "Hr_Sections",
                column: "CompanyId",
                principalTable: "Hr_Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Sections_Hr_Companies_CompanyId",
                table: "Hr_Sections");

            migrationBuilder.DropTable(
                name: "Hr_Managements");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Hr_Sections",
                newName: "Company_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Sections_CompanyId",
                table: "Hr_Sections",
                newName: "IX_Hr_Sections_Company_id");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f923f54-81c7-40fc-b526-c1e3d24bd319", "AQAAAAIAAYagAAAAEMhatGDvYe7hYNOf1Wsjis/9//eoIgTrL9nKSrGwllG7LAstLnx2t9Mqo91+nGQQ4A==", "92bed638-a681-463c-a9fb-13afdd30bb48" });

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Sections_Hr_Companies_Company_id",
                table: "Hr_Sections",
                column: "Company_id",
                principalTable: "Hr_Companies",
                principalColumn: "Id");
        }
    }
}
