using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addnotrequriredToLoanRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Loan_Details_Trans_Loan_TransLoanId",
                table: "Trans_Loan_Details");

            migrationBuilder.AlterColumn<int>(
                name: "TransLoanId",
                table: "Trans_Loan_Details",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MainScreenTreeId",
                table: "St_SubMainScreenActions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MainScreenTrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreenTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_MainScreenTrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainScreenTrees_MainScreenTrees_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MainScreenTrees",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9faa613-e6f3-4ef8-9337-598f92ec22ab", "AQAAAAIAAYagAAAAENHzH/jeHNHqpIDROOKWgyT5m83+x4IrfURQPpdXN1iLxiVQdl8OXFVQezc03jlJhw==", "3ce9c571-701c-4c82-8853-e87b9157acdb" });

            migrationBuilder.CreateIndex(
                name: "IX_St_SubMainScreenActions_MainScreenTreeId",
                table: "St_SubMainScreenActions",
                column: "MainScreenTreeId");

            migrationBuilder.CreateIndex(
                name: "IX_MainScreenTrees_ParentId",
                table: "MainScreenTrees",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_St_SubMainScreenActions_MainScreenTrees_MainScreenTreeId",
                table: "St_SubMainScreenActions",
                column: "MainScreenTreeId",
                principalTable: "MainScreenTrees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Loan_Details_Trans_Loan_TransLoanId",
                table: "Trans_Loan_Details",
                column: "TransLoanId",
                principalTable: "Trans_Loan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_St_SubMainScreenActions_MainScreenTrees_MainScreenTreeId",
                table: "St_SubMainScreenActions");

            migrationBuilder.DropForeignKey(
                name: "FK_Trans_Loan_Details_Trans_Loan_TransLoanId",
                table: "Trans_Loan_Details");

            migrationBuilder.DropTable(
                name: "MainScreenTrees");

            migrationBuilder.DropIndex(
                name: "IX_St_SubMainScreenActions_MainScreenTreeId",
                table: "St_SubMainScreenActions");

            migrationBuilder.DropColumn(
                name: "MainScreenTreeId",
                table: "St_SubMainScreenActions");

            migrationBuilder.AlterColumn<int>(
                name: "TransLoanId",
                table: "Trans_Loan_Details",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4d4d40f3-8dbb-41ee-add9-c3f774ae13b8", "AQAAAAIAAYagAAAAEAhd1MKfXlHIjWZq5HWBp01442UUAPfB3RygYmZBR2571xG1E671/AOsIHTpvw7sbw==", "ca237424-c497-432e-93e7-d4aab4090f40" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trans_Loan_Details_Trans_Loan_TransLoanId",
                table: "Trans_Loan_Details",
                column: "TransLoanId",
                principalTable: "Trans_Loan",
                principalColumn: "Id");
        }
    }
}
