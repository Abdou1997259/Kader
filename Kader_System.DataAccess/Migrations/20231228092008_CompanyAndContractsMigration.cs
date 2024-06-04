using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CompanyAndContractsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Companies_Hr_CompanyTypes_Company_type",
                table: "Hr_Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_CompanyContracts_Hr_Companies_Company_id",
                table: "Hr_CompanyContracts");

            migrationBuilder.RenameColumn(
                name: "Company_id",
                table: "Hr_CompanyContracts",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "Company_contracts_extension",
                table: "Hr_CompanyContracts",
                newName: "CompanyContractsExtension");

            migrationBuilder.RenameColumn(
                name: "Company_contracts",
                table: "Hr_CompanyContracts",
                newName: "CompanyContracts");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_CompanyContracts_Company_id",
                table: "Hr_CompanyContracts",
                newName: "IX_Hr_CompanyContracts_CompanyId");

            migrationBuilder.RenameColumn(
                name: "Name_en",
                table: "Hr_Companies",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Name_ar",
                table: "Hr_Companies",
                newName: "NameAr");

            migrationBuilder.RenameColumn(
                name: "Company_type",
                table: "Hr_Companies",
                newName: "CompanyTypeId");

            migrationBuilder.RenameColumn(
                name: "Company_owner",
                table: "Hr_Companies",
                newName: "CompanyOwner");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Companies_Company_type",
                table: "Hr_Companies",
                newName: "IX_Hr_Companies_CompanyTypeId");

            migrationBuilder.CreateTable(
                name: "Hr_CompanyLicenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Hr_CompanyLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_CompanyLicenses_Hr_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Hr_Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f923f54-81c7-40fc-b526-c1e3d24bd319", "AQAAAAIAAYagAAAAEMhatGDvYe7hYNOf1Wsjis/9//eoIgTrL9nKSrGwllG7LAstLnx2t9Mqo91+nGQQ4A==", "92bed638-a681-463c-a9fb-13afdd30bb48" });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_CompanyLicenses_CompanyId",
                table: "Hr_CompanyLicenses",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Companies_Hr_CompanyTypes_CompanyTypeId",
                table: "Hr_Companies",
                column: "CompanyTypeId",
                principalTable: "Hr_CompanyTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_CompanyContracts_Hr_Companies_CompanyId",
                table: "Hr_CompanyContracts",
                column: "CompanyId",
                principalTable: "Hr_Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Companies_Hr_CompanyTypes_CompanyTypeId",
                table: "Hr_Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_CompanyContracts_Hr_Companies_CompanyId",
                table: "Hr_CompanyContracts");

            migrationBuilder.DropTable(
                name: "Hr_CompanyLicenses");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Hr_CompanyContracts",
                newName: "Company_id");

            migrationBuilder.RenameColumn(
                name: "CompanyContractsExtension",
                table: "Hr_CompanyContracts",
                newName: "Company_contracts_extension");

            migrationBuilder.RenameColumn(
                name: "CompanyContracts",
                table: "Hr_CompanyContracts",
                newName: "Company_contracts");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_CompanyContracts_CompanyId",
                table: "Hr_CompanyContracts",
                newName: "IX_Hr_CompanyContracts_Company_id");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Hr_Companies",
                newName: "Name_en");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                table: "Hr_Companies",
                newName: "Name_ar");

            migrationBuilder.RenameColumn(
                name: "CompanyTypeId",
                table: "Hr_Companies",
                newName: "Company_type");

            migrationBuilder.RenameColumn(
                name: "CompanyOwner",
                table: "Hr_Companies",
                newName: "Company_owner");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Companies_CompanyTypeId",
                table: "Hr_Companies",
                newName: "IX_Hr_Companies_Company_type");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "501183de-1fd5-426c-8a6e-39f637791a10", "AQAAAAIAAYagAAAAEE/Uf15qfVPVLXklliH0wHPzcKFfRcvsL8P5dTIoCuNUY5IjITlb2WU3ES2BwfP2fQ==", "36e51f60-350e-4911-9764-dbf4d1d79d9a" });

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Companies_Hr_CompanyTypes_Company_type",
                table: "Hr_Companies",
                column: "Company_type",
                principalTable: "Hr_CompanyTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_CompanyContracts_Hr_Companies_Company_id",
                table: "Hr_CompanyContracts",
                column: "Company_id",
                principalTable: "Hr_Companies",
                principalColumn: "Id");
        }
    }
}
