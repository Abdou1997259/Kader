using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FingerPrintMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_ContractAllowancesDetails_Hr_Contracts_ContractId",
                table: "Hr_ContractAllowancesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_FingerPrints_Hr_Companies_Company_id",
                table: "Hr_FingerPrints");

            migrationBuilder.DropIndex(
                name: "IX_Hr_FingerPrints_Company_id",
                table: "Hr_FingerPrints");

            migrationBuilder.DropColumn(
                name: "Company_id",
                table: "Hr_FingerPrints");

            migrationBuilder.RenameColumn(
                name: "Name_en",
                table: "Hr_FingerPrints",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Name_ar",
                table: "Hr_FingerPrints",
                newName: "NameAr");

            migrationBuilder.RenameColumn(
                name: "Ip",
                table: "Hr_FingerPrints",
                newName: "CompanyId");

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "Hr_FingerPrints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "086dbd12-e961-4d4a-9bd4-e349ad1f3b4d", "AQAAAAIAAYagAAAAEAd6bNU2k6rtpbTzA1sweH/wqUYSI5YHicbbTHrVlEDuCgfMIkHMf5eQZI9BhK+/MA==", "6b11c3d5-0bc3-483c-835a-9536e56dc7e2" });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_FingerPrints_CompanyId",
                table: "Hr_FingerPrints",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_ContractAllowancesDetails_Hr_Contracts_ContractId",
                table: "Hr_ContractAllowancesDetails",
                column: "ContractId",
                principalTable: "Hr_Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_FingerPrints_Hr_Companies_CompanyId",
                table: "Hr_FingerPrints",
                column: "CompanyId",
                principalTable: "Hr_Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_ContractAllowancesDetails_Hr_Contracts_ContractId",
                table: "Hr_ContractAllowancesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_FingerPrints_Hr_Companies_CompanyId",
                table: "Hr_FingerPrints");

            migrationBuilder.DropIndex(
                name: "IX_Hr_FingerPrints_CompanyId",
                table: "Hr_FingerPrints");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "Hr_FingerPrints");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Hr_FingerPrints",
                newName: "Name_en");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                table: "Hr_FingerPrints",
                newName: "Name_ar");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Hr_FingerPrints",
                newName: "Ip");

            migrationBuilder.AddColumn<int>(
                name: "Company_id",
                table: "Hr_FingerPrints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa1fb72f-be7f-4837-bf91-5d9565346f46", "AQAAAAIAAYagAAAAEMbyD8CaPLTfozrB8HyclFNhXfu07Celvnt3zpUnQGc1ED/OSH7pabnJ9yhKzJpTBQ==", "483408f6-70d8-4cc1-820b-6a5f9a2016e6" });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_FingerPrints_Company_id",
                table: "Hr_FingerPrints",
                column: "Company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_ContractAllowancesDetails_Hr_Contracts_ContractId",
                table: "Hr_ContractAllowancesDetails",
                column: "ContractId",
                principalTable: "Hr_Contracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_FingerPrints_Hr_Companies_Company_id",
                table: "Hr_FingerPrints",
                column: "Company_id",
                principalTable: "Hr_Companies",
                principalColumn: "Id");
        }
    }
}
