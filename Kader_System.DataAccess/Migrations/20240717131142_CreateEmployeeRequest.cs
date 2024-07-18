using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateEmployeeRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Hr_DelayPermission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_DelayPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_DelayPermission_Hr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_LoanRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstallmentsCount = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    AtachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_LoanRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_LoanRequest_Hr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_ResignationRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_ResignationRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_ResignationRequest_Hr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_VacationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayCounts = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VacationTypeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_VacationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_VacationRequests_Hr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_VacationRequests_Hr_VacationTypes_VacationTypeId",
                        column: x => x.VacationTypeId,
                        principalTable: "Hr_VacationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74042c78-cd42-4e4a-9c26-5d86c0e60a4d", "AQAAAAIAAYagAAAAEEScV5uInSKmmgB/VXaDme4LcRuw/vsvZHbAtRk7FKozUPY2enZUzD4PZyoHBt4WZg==", "1ae4730a-a5d2-4a36-91bb-7300cf1621be" });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_DelayPermission_EmployeeId",
                table: "Hr_DelayPermission",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_LoanRequest_EmployeeId",
                table: "Hr_LoanRequest",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_ResignationRequest_EmployeeId",
                table: "Hr_ResignationRequest",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_VacationRequests_EmployeeId",
                table: "Hr_VacationRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_VacationRequests_VacationTypeId",
                table: "Hr_VacationRequests",
                column: "VacationTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hr_DelayPermission");

            migrationBuilder.DropTable(
                name: "Hr_LoanRequest");

            migrationBuilder.DropTable(
                name: "Hr_ResignationRequest");

            migrationBuilder.DropTable(
                name: "Hr_VacationRequests");

            migrationBuilder.DropColumn(
                name: "TransNameAr",
                table: "SpCaclauateSalaryDetailedTransModel");

            migrationBuilder.DropColumn(
                name: "TransNameEn",
                table: "SpCaclauateSalaryDetailedTransModel");

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
