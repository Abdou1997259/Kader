using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_hr_allowance_requet_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hr_allowance_requet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    amount = table.Column<double>(type: "float", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    attachment_file_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employe_id = table.Column<int>(type: "int", nullable: false),
                    allowance_id = table.Column<int>(type: "int", nullable: false),
                    allowance_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hr_allowance_requet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hr_allowance_requet_Hr_Allowances_allowance_id",
                        column: x => x.allowance_id,
                        principalTable: "Hr_Allowances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_hr_allowance_requet_Hr_Employees_employe_id",
                        column: x => x.employe_id,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_hr_allowance_requet_Trans_SalaryEffects_allowance_type_id",
                        column: x => x.allowance_type_id,
                        principalTable: "Trans_SalaryEffects",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e4c45fb-35ca-4f13-9c41-61d7c7af5c6b", "AQAAAAIAAYagAAAAEHk46A1FyxoU21hdtrcZqkEoeQWUqw7tvGvxVa332Cx+1Nn5oo7qrS/x0LePoCEHkQ==", "911eee52-d667-43a0-9093-cd3c8bef2bff" });

            migrationBuilder.CreateIndex(
                name: "IX_hr_allowance_requet_allowance_id",
                table: "hr_allowance_requet",
                column: "allowance_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_allowance_requet_allowance_type_id",
                table: "hr_allowance_requet",
                column: "allowance_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_hr_allowance_requet_employe_id",
                table: "hr_allowance_requet",
                column: "employe_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hr_allowance_requet");

            migrationBuilder.DropColumn(
                name: "BasicSalary",
                table: "trans_salary_calculators_details");

            migrationBuilder.DropColumn(
                name: "NetSalary",
                table: "trans_salary_calculators_details");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "trans_salary_calculators_details");

            migrationBuilder.DropColumn(
                name: "TotalAllownces",
                table: "trans_salary_calculators_details");

            migrationBuilder.DropColumn(
                name: "TotalBenefits",
                table: "trans_salary_calculators_details");

            migrationBuilder.DropColumn(
                name: "TransferId",
                table: "trans_salary_calculators_details");

            migrationBuilder.RenameColumn(
                name: "TotalLoans",
                table: "trans_salary_calculators_details",
                newName: "Salary");

            migrationBuilder.RenameColumn(
                name: "TotalDeductions",
                table: "trans_salary_calculators_details",
                newName: "Amount");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "trans_salary_calculators",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "trans_salary_calculators",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManagementId",
                table: "trans_salary_calculators",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74042c78-cd42-4e4a-9c26-5d86c0e60a4d", "AQAAAAIAAYagAAAAEEScV5uInSKmmgB/VXaDme4LcRuw/vsvZHbAtRk7FKozUPY2enZUzD4PZyoHBt4WZg==", "1ae4730a-a5d2-4a36-91bb-7300cf1621be" });
        }
    }
}
