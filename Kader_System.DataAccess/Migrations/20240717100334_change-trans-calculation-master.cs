using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changetranscalculationmaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "trans_salary_calculators");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "trans_salary_calculators");

            migrationBuilder.DropColumn(
                name: "ManagementId",
                table: "trans_salary_calculators");

            //migrationBuilder.AddColumn<string>(
            //    name: "TransNameAr",
            //    table: "SpCaclauateSalaryDetailedTransModel",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "TransNameEn",
            //    table: "SpCaclauateSalaryDetailedTransModel",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e19f1155-2db5-4f73-a984-23ffe6a11137", "AQAAAAIAAYagAAAAEIB+onz3//hUUtGDXo6BM4fp8qpQSHyff+VmJYZHUmUe9M7tDz5bjfdPqOsgz67g9g==", "106dea95-8987-4796-9d16-656e9fea2c0b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransNameAr",
                table: "SpCaclauateSalaryDetailedTransModel");

            migrationBuilder.DropColumn(
                name: "TransNameEn",
                table: "SpCaclauateSalaryDetailedTransModel");

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
                values: new object[] { "13e684d1-20d1-434a-824e-83d3208c827f", "AQAAAAIAAYagAAAAEG3nXyqBXXuutWlCw+QKdDgW2nk5Q7VJ4wC2K8Fr1N5MktisEqH6lO8t8090FDvbMg==", "a6150849-8f90-4ac3-9247-5eb86e14173b" });
        }
    }
}
