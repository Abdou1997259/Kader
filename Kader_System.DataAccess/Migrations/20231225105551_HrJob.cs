using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class HrJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Has_additional_time",
                table: "Hr_Qualifications");

            migrationBuilder.RenameColumn(
                name: "Name_en",
                table: "Hr_Qualifications",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Name_ar",
                table: "Hr_Qualifications",
                newName: "NameAr");

            migrationBuilder.RenameColumn(
                name: "Name_en",
                table: "Hr_Jobs",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "Name_ar",
                table: "Hr_Jobs",
                newName: "NameAr");

            migrationBuilder.RenameColumn(
                name: "Has_need_license",
                table: "Hr_Jobs",
                newName: "HasNeedLicense");

            migrationBuilder.RenameColumn(
                name: "Has_additional_time",
                table: "Hr_Jobs",
                newName: "HasAdditionalTime");

            migrationBuilder.AddColumn<long>(
                name: "AccountNo",
                table: "Hr_Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AccountNo",
                table: "Hr_Deductions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AccountNo",
                table: "Hr_Benefits",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AccountNo",
                table: "Hr_Allowances",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7bd824a4-8c91-460d-ab24-24fc421be276", "AQAAAAIAAYagAAAAEF7LgMPPEsZII0THCCw16jKd4rtYXHHm6xwRN+5Q83AVLA0+gPsWWqYj1q+d+oKu4w==", "3190e7b7-d321-4e33-b827-ee815f967950" });

            migrationBuilder.UpdateData(
                table: "St_Actions",
                keyColumn: "Id",
                keyValue: 6,
                column: "NameInEnglish",
                value: "Print");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Hr_Employees");

            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Hr_Deductions");

            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Hr_Benefits");

            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Hr_Allowances");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Hr_Qualifications",
                newName: "Name_en");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                table: "Hr_Qualifications",
                newName: "Name_ar");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Hr_Jobs",
                newName: "Name_en");

            migrationBuilder.RenameColumn(
                name: "NameAr",
                table: "Hr_Jobs",
                newName: "Name_ar");

            migrationBuilder.RenameColumn(
                name: "HasNeedLicense",
                table: "Hr_Jobs",
                newName: "Has_need_license");

            migrationBuilder.RenameColumn(
                name: "HasAdditionalTime",
                table: "Hr_Jobs",
                newName: "Has_additional_time");

            migrationBuilder.AddColumn<bool>(
                name: "Has_additional_time",
                table: "Hr_Qualifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17611151-ef11-49c1-905a-d86a58fd850b", "AQAAAAIAAYagAAAAEB/lqjbW9yTUejtnMLlnjTYt0/J6N6I8/0QIhrpLJqJ8tceLTJyw2NK9MzudKI8WfA==", "eb449e34-d3f8-4dd5-9bf1-7fe6ff161682" });

            migrationBuilder.UpdateData(
                table: "St_Actions",
                keyColumn: "Id",
                keyValue: 6,
                column: "NameInEnglish",
                value: "Ptint");
        }
    }
}
