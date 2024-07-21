using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class create_contract_termination_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contract_termination",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contract_termination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contract_termination_Hr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b5b43dd1-34f9-41cc-9a13-515ca1bc3c8d", "AQAAAAIAAYagAAAAEFZLxNJ2Y28FygXwJQPybwV1AYuoRgcIdz7nRrKYGhonjQqawRUJcW2/xWmcP2xfrg==", "4d66988d-8a0a-43ba-aa2d-ff008cbc21cf" });

            migrationBuilder.CreateIndex(
                name: "IX_contract_termination_EmployeeId",
                table: "contract_termination",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contract_termination");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "414b7f69-661e-4f13-b5f0-23214e55f0fe", "AQAAAAIAAYagAAAAEKa4NwMpEERmAuemyTCriBxlOCFfhfY2PDY/PEWrcKVV4rwR4XDUjtLjGxasDNqwvg==", "910ab86d-b7a6-4583-b5c1-f4697a7069be" });
        }
    }
}
