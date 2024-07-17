using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addLeavePermessionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hr_LeavePermissionRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    BackTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_LeavePermissionRequest", x => x.Id);
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f25faff9-b1b5-45db-88b1-77c75432d40e", "AQAAAAIAAYagAAAAEIQwOqrW9lAYNlZxVmnK7VpDn+OLE05jX/+7x5JH04hzz/Acnt0h/jPAWapkTwJEPQ==", "740debde-1aa4-4b52-8e5d-2ae2f23bcb0d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hr_LeavePermissionRequest");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a9f9fd7-2c36-4a28-8ade-8c06fb937e66", "AQAAAAIAAYagAAAAENR8auSyHYxFELEpA9ZjXzN8RT+Nw4NhxGRkOpif68rGeerGAEwpQgq1QnUh+MjLKQ==", "a1cabf16-3f3e-46e7-bcc3-7f405180fb17" });
        }
    }
}
