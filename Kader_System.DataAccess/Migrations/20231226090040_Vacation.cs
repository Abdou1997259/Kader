using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Vacation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_AccountingWays_AccountingWay_id",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_VacationDistributions_Hr_AccountingWays_AccountingWay_id",
                table: "Hr_VacationDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_VacationDistributions_Hr_Vacations_Vacation_id",
                table: "Hr_VacationDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Vacations_Hr_VacationTypes_Vacation_type",
                table: "Hr_Vacations");

            migrationBuilder.DropTable(
                name: "Hr_AccountingWays");

            migrationBuilder.DropIndex(
                name: "IX_Hr_VacationDistributions_AccountingWay_id",
                table: "Hr_VacationDistributions");

            migrationBuilder.RenameColumn(
                name: "Vacation_type",
                table: "Hr_Vacations",
                newName: "VacationTypeId");

            migrationBuilder.RenameColumn(
                name: "Transfer_vacation",
                table: "Hr_Vacations",
                newName: "CanTransfer");

            migrationBuilder.RenameColumn(
                name: "Total_vacation",
                table: "Hr_Vacations",
                newName: "TotalBalance");

            migrationBuilder.RenameColumn(
                name: "Name_en",
                table: "Hr_Vacations",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Name_ar",
                table: "Hr_Vacations",
                newName: "NameAr");

            migrationBuilder.RenameColumn(
                name: "Apply_months",
                table: "Hr_Vacations",
                newName: "ApplyAfterMonth");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Vacations_Vacation_type",
                table: "Hr_Vacations",
                newName: "IX_Hr_Vacations_VacationTypeId");

            migrationBuilder.RenameColumn(
                name: "Vacation_id",
                table: "Hr_VacationDistributions",
                newName: "VacationId");

            migrationBuilder.RenameColumn(
                name: "Name_en",
                table: "Hr_VacationDistributions",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Name_ar",
                table: "Hr_VacationDistributions",
                newName: "NameAr");

            migrationBuilder.RenameColumn(
                name: "Days_count",
                table: "Hr_VacationDistributions",
                newName: "SalaryCalculatorId");

            migrationBuilder.RenameColumn(
                name: "AccountingWay_id",
                table: "Hr_VacationDistributions",
                newName: "DaysCount");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_VacationDistributions_Vacation_id",
                table: "Hr_VacationDistributions",
                newName: "IX_Hr_VacationDistributions_VacationId");

            migrationBuilder.CreateTable(
                name: "Hr_SalaryCalculators",
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
                    table.PrimaryKey("PK_Hr_SalaryCalculators", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "501183de-1fd5-426c-8a6e-39f637791a10", "AQAAAAIAAYagAAAAEE/Uf15qfVPVLXklliH0wHPzcKFfRcvsL8P5dTIoCuNUY5IjITlb2WU3ES2BwfP2fQ==", "36e51f60-350e-4911-9764-dbf4d1d79d9a" });

            migrationBuilder.InsertData(
                table: "Hr_SalaryCalculators",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "كل الاتب", "All salary", null, null },
                    { 2, null, null, null, null, true, false, "الراتب الرئيسى", "Main salary", null, null },
                    { 3, null, null, null, null, true, false, "بدون راتب", "Without salary", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_VacationDistributions_SalaryCalculatorId",
                table: "Hr_VacationDistributions",
                column: "SalaryCalculatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_SalaryCalculators_AccountingWay_id",
                table: "Hr_Employees",
                column: "AccountingWay_id",
                principalTable: "Hr_SalaryCalculators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_VacationDistributions_Hr_SalaryCalculators_SalaryCalculatorId",
                table: "Hr_VacationDistributions",
                column: "SalaryCalculatorId",
                principalTable: "Hr_SalaryCalculators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_VacationDistributions_Hr_Vacations_VacationId",
                table: "Hr_VacationDistributions",
                column: "VacationId",
                principalTable: "Hr_Vacations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Vacations_Hr_VacationTypes_VacationTypeId",
                table: "Hr_Vacations",
                column: "VacationTypeId",
                principalTable: "Hr_VacationTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_SalaryCalculators_AccountingWay_id",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_VacationDistributions_Hr_SalaryCalculators_SalaryCalculatorId",
                table: "Hr_VacationDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_VacationDistributions_Hr_Vacations_VacationId",
                table: "Hr_VacationDistributions");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Vacations_Hr_VacationTypes_VacationTypeId",
                table: "Hr_Vacations");

            migrationBuilder.DropTable(
                name: "Hr_SalaryCalculators");

            migrationBuilder.DropIndex(
                name: "IX_Hr_VacationDistributions_SalaryCalculatorId",
                table: "Hr_VacationDistributions");

            migrationBuilder.RenameColumn(
                name: "VacationTypeId",
                table: "Hr_Vacations",
                newName: "Vacation_type");

            migrationBuilder.RenameColumn(
                name: "TotalBalance",
                table: "Hr_Vacations",
                newName: "Total_vacation");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Hr_Vacations",
                newName: "Name_en");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                table: "Hr_Vacations",
                newName: "Name_ar");

            migrationBuilder.RenameColumn(
                name: "CanTransfer",
                table: "Hr_Vacations",
                newName: "Transfer_vacation");

            migrationBuilder.RenameColumn(
                name: "ApplyAfterMonth",
                table: "Hr_Vacations",
                newName: "Apply_months");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Vacations_VacationTypeId",
                table: "Hr_Vacations",
                newName: "IX_Hr_Vacations_Vacation_type");

            migrationBuilder.RenameColumn(
                name: "VacationId",
                table: "Hr_VacationDistributions",
                newName: "Vacation_id");

            migrationBuilder.RenameColumn(
                name: "SalaryCalculatorId",
                table: "Hr_VacationDistributions",
                newName: "Days_count");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Hr_VacationDistributions",
                newName: "Name_en");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                table: "Hr_VacationDistributions",
                newName: "Name_ar");

            migrationBuilder.RenameColumn(
                name: "DaysCount",
                table: "Hr_VacationDistributions",
                newName: "AccountingWay_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_VacationDistributions_VacationId",
                table: "Hr_VacationDistributions",
                newName: "IX_Hr_VacationDistributions_Vacation_id");

            migrationBuilder.CreateTable(
                name: "Hr_AccountingWays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_AccountingWays", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7bd824a4-8c91-460d-ab24-24fc421be276", "AQAAAAIAAYagAAAAEF7LgMPPEsZII0THCCw16jKd4rtYXHHm6xwRN+5Q83AVLA0+gPsWWqYj1q+d+oKu4w==", "3190e7b7-d321-4e33-b827-ee815f967950" });

            migrationBuilder.InsertData(
                table: "Hr_AccountingWays",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "كل الاتب", "All salary", null, null },
                    { 2, null, null, null, null, true, false, "الراتب الرئيسى", "Main salary", null, null },
                    { 3, null, null, null, null, true, false, "بدون راتب", "Without salary", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_VacationDistributions_AccountingWay_id",
                table: "Hr_VacationDistributions",
                column: "AccountingWay_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_AccountingWays_AccountingWay_id",
                table: "Hr_Employees",
                column: "AccountingWay_id",
                principalTable: "Hr_AccountingWays",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_VacationDistributions_Hr_AccountingWays_AccountingWay_id",
                table: "Hr_VacationDistributions",
                column: "AccountingWay_id",
                principalTable: "Hr_AccountingWays",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_VacationDistributions_Hr_Vacations_Vacation_id",
                table: "Hr_VacationDistributions",
                column: "Vacation_id",
                principalTable: "Hr_Vacations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Vacations_Hr_VacationTypes_Vacation_type",
                table: "Hr_Vacations",
                column: "Vacation_type",
                principalTable: "Hr_VacationTypes",
                principalColumn: "Id");
        }
    }
}
