using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addCacluateSalarytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cacluate_Salary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    EmpolyeeId = table.Column<int>(type: "int", nullable: false),
                    ActionDate = table.Column<DateOnly>(type: "date", nullable: false),
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
                    table.PrimaryKey("PK_Cacluate_Salary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cacluate_Salary_Hr_Employees_EmpolyeeId",
                        column: x => x.EmpolyeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35516848-a162-4211-834d-c5d5fec6ff39", "AQAAAAIAAYagAAAAEP/XzM0O03zl2JOTX/eemrEzLzllNxEDFYpqLNlFIOsazIqpyPG9zQxA54G2Uu8SJQ==", "ec2411aa-3572-4e58-8df1-0c9b1c21e922" });

            migrationBuilder.CreateIndex(
                name: "IX_Cacluate_Salary_EmpolyeeId",
                table: "Cacluate_Salary",
                column: "EmpolyeeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cacluate_Salary");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9faa613-e6f3-4ef8-9337-598f92ec22ab", "AQAAAAIAAYagAAAAENHzH/jeHNHqpIDROOKWgyT5m83+x4IrfURQPpdXN1iLxiVQdl8OXFVQezc03jlJhw==", "3ce9c571-701c-4c82-8853-e87b9157acdb" });
        }
    }
}
