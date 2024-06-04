using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_EmployeeAttachments_Hr_Employees_Employee_id",
                table: "Hr_EmployeeAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_HrRelegion_Relegion_id",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Departments_Department_id",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_EmployeeTypes_EmployeeType_id",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Genders_Gender_id",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Jobs_Job_id",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Nationalities_Nationality_id",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Qualifications_Qualification_id",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_SalaryCalculators_AccountingWay_id",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_SalaryPaymentWays_SalaryPaymentWay_id",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Shifts_Shift_id",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Vacations_Vacation_id",
                table: "Hr_Employees");

            migrationBuilder.RenameColumn(
                name: "Vacation_id",
                table: "Hr_Employees",
                newName: "VacationId");

            migrationBuilder.RenameColumn(
                name: "Total_salary",
                table: "Hr_Employees",
                newName: "TotalSalary");

            migrationBuilder.RenameColumn(
                name: "Shift_id",
                table: "Hr_Employees",
                newName: "ShiftId");

            migrationBuilder.RenameColumn(
                name: "SalaryPaymentWay_id",
                table: "Hr_Employees",
                newName: "SalaryPaymentWayId");

            migrationBuilder.RenameColumn(
                name: "Relegion_id",
                table: "Hr_Employees",
                newName: "ReligionId");

            migrationBuilder.RenameColumn(
                name: "Qualification_id",
                table: "Hr_Employees",
                newName: "QualificationId");

            migrationBuilder.RenameColumn(
                name: "Nationality_id",
                table: "Hr_Employees",
                newName: "NationalityId");

            migrationBuilder.RenameColumn(
                name: "Job_id",
                table: "Hr_Employees",
                newName: "MaritalStatusId");

            migrationBuilder.RenameColumn(
                name: "Immediately_date",
                table: "Hr_Employees",
                newName: "ImmediatelyDate");

            migrationBuilder.RenameColumn(
                name: "Hiring_date",
                table: "Hr_Employees",
                newName: "HiringDate");

            migrationBuilder.RenameColumn(
                name: "Grand_father_name_en",
                table: "Hr_Employees",
                newName: "NationalId");

            migrationBuilder.RenameColumn(
                name: "Grand_father_name_ar",
                table: "Hr_Employees",
                newName: "JobNumber");

            migrationBuilder.RenameColumn(
                name: "Gender_id",
                table: "Hr_Employees",
                newName: "ManagementId");

            migrationBuilder.RenameColumn(
                name: "Fixed_salary",
                table: "Hr_Employees",
                newName: "GrandFatherNameEn");

            migrationBuilder.RenameColumn(
                name: "Father_name_en",
                table: "Hr_Employees",
                newName: "GrandFatherNameAr");

            migrationBuilder.RenameColumn(
                name: "Father_name_ar",
                table: "Hr_Employees",
                newName: "FirstNameEn");

            migrationBuilder.RenameColumn(
                name: "Family_name_en",
                table: "Hr_Employees",
                newName: "FirstNameAr");

            migrationBuilder.RenameColumn(
                name: "Family_name_ar",
                table: "Hr_Employees",
                newName: "FatherNameEn");

            migrationBuilder.RenameColumn(
                name: "Employee_name_en",
                table: "Hr_Employees",
                newName: "FatherNameAr");

            migrationBuilder.RenameColumn(
                name: "Employee_name_ar",
                table: "Hr_Employees",
                newName: "FamilyNameEn");

            migrationBuilder.RenameColumn(
                name: "Employee_image_extension",
                table: "Hr_Employees",
                newName: "FingerPrintCode");

            migrationBuilder.RenameColumn(
                name: "Employee_image",
                table: "Hr_Employees",
                newName: "EmployeeImageExtension");

            migrationBuilder.RenameColumn(
                name: "EmployeeType_id",
                table: "Hr_Employees",
                newName: "JobId");

            migrationBuilder.RenameColumn(
                name: "Department_id",
                table: "Hr_Employees",
                newName: "GenderId");

            migrationBuilder.RenameColumn(
                name: "Children_number",
                table: "Hr_Employees",
                newName: "FamilyNameAr");

            migrationBuilder.RenameColumn(
                name: "AccountingWay_id",
                table: "Hr_Employees",
                newName: "EmployeeTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_Vacation_id",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_VacationId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_Shift_id",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_ShiftId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_SalaryPaymentWay_id",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_SalaryPaymentWayId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_Relegion_id",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_ReligionId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_Qualification_id",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_QualificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_Nationality_id",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_NationalityId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_Job_id",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_MaritalStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_Gender_id",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_ManagementId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_EmployeeType_id",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_Department_id",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_GenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_AccountingWay_id",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_EmployeeTypeId");

            migrationBuilder.RenameColumn(
                name: "Employee_id",
                table: "Hr_EmployeeAttachments",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_EmployeeAttachments_Employee_id",
                table: "Hr_EmployeeAttachments",
                newName: "IX_Hr_EmployeeAttachments_EmployeeId");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "Hr_Employees",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "ChildrenNumber",
                table: "Hr_Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Hr_Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Hr_Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeImage",
                table: "Hr_Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FingerPrintId",
                table: "Hr_Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FixedSalary",
                table: "Hr_Employees",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d7e8189-2c58-4961-8dbb-62248a896c9d", "AQAAAAIAAYagAAAAEAgwsEtp8VUfWdG3zejBEl/OiynFrbRe3UYXUFYlB/QCPRnvTiafWfw8qklyyZQFiQ==", "25f1075f-c2d5-47a1-bad8-26a28b1c9041" });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_CompanyId",
                table: "Hr_Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_DepartmentId",
                table: "Hr_Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_FingerPrintId",
                table: "Hr_Employees",
                column: "FingerPrintId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_EmployeeAttachments_Hr_Employees_EmployeeId",
                table: "Hr_EmployeeAttachments",
                column: "EmployeeId",
                principalTable: "Hr_Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_HrRelegion_ReligionId",
                table: "Hr_Employees",
                column: "ReligionId",
                principalTable: "HrRelegion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Companies_CompanyId",
                table: "Hr_Employees",
                column: "CompanyId",
                principalTable: "Hr_Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Departments_DepartmentId",
                table: "Hr_Employees",
                column: "DepartmentId",
                principalTable: "Hr_Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_EmployeeTypes_EmployeeTypeId",
                table: "Hr_Employees",
                column: "EmployeeTypeId",
                principalTable: "Hr_EmployeeTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_FingerPrints_FingerPrintId",
                table: "Hr_Employees",
                column: "FingerPrintId",
                principalTable: "Hr_FingerPrints",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Genders_GenderId",
                table: "Hr_Employees",
                column: "GenderId",
                principalTable: "Hr_Genders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Jobs_JobId",
                table: "Hr_Employees",
                column: "JobId",
                principalTable: "Hr_Jobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Managements_ManagementId",
                table: "Hr_Employees",
                column: "ManagementId",
                principalTable: "Hr_Managements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_MaritalStatus_MaritalStatusId",
                table: "Hr_Employees",
                column: "MaritalStatusId",
                principalTable: "Hr_MaritalStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Nationalities_NationalityId",
                table: "Hr_Employees",
                column: "NationalityId",
                principalTable: "Hr_Nationalities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Qualifications_QualificationId",
                table: "Hr_Employees",
                column: "QualificationId",
                principalTable: "Hr_Qualifications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_SalaryPaymentWays_SalaryPaymentWayId",
                table: "Hr_Employees",
                column: "SalaryPaymentWayId",
                principalTable: "Hr_SalaryPaymentWays",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Shifts_ShiftId",
                table: "Hr_Employees",
                column: "ShiftId",
                principalTable: "Hr_Shifts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Vacations_VacationId",
                table: "Hr_Employees",
                column: "VacationId",
                principalTable: "Hr_Vacations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hr_EmployeeAttachments_Hr_Employees_EmployeeId",
                table: "Hr_EmployeeAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_HrRelegion_ReligionId",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Companies_CompanyId",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Departments_DepartmentId",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_EmployeeTypes_EmployeeTypeId",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_FingerPrints_FingerPrintId",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Genders_GenderId",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Jobs_JobId",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Managements_ManagementId",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_MaritalStatus_MaritalStatusId",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Nationalities_NationalityId",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Qualifications_QualificationId",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_SalaryPaymentWays_SalaryPaymentWayId",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Shifts_ShiftId",
                table: "Hr_Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hr_Employees_Hr_Vacations_VacationId",
                table: "Hr_Employees");

            migrationBuilder.DropIndex(
                name: "IX_Hr_Employees_CompanyId",
                table: "Hr_Employees");

            migrationBuilder.DropIndex(
                name: "IX_Hr_Employees_DepartmentId",
                table: "Hr_Employees");

            migrationBuilder.DropIndex(
                name: "IX_Hr_Employees_FingerPrintId",
                table: "Hr_Employees");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Hr_Employees");

            migrationBuilder.DropColumn(
                name: "ChildrenNumber",
                table: "Hr_Employees");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Hr_Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Hr_Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeImage",
                table: "Hr_Employees");

            migrationBuilder.DropColumn(
                name: "FingerPrintId",
                table: "Hr_Employees");

            migrationBuilder.DropColumn(
                name: "FixedSalary",
                table: "Hr_Employees");

            migrationBuilder.RenameColumn(
                name: "VacationId",
                table: "Hr_Employees",
                newName: "Vacation_id");

            migrationBuilder.RenameColumn(
                name: "TotalSalary",
                table: "Hr_Employees",
                newName: "Total_salary");

            migrationBuilder.RenameColumn(
                name: "ShiftId",
                table: "Hr_Employees",
                newName: "Shift_id");

            migrationBuilder.RenameColumn(
                name: "SalaryPaymentWayId",
                table: "Hr_Employees",
                newName: "SalaryPaymentWay_id");

            migrationBuilder.RenameColumn(
                name: "ReligionId",
                table: "Hr_Employees",
                newName: "Relegion_id");

            migrationBuilder.RenameColumn(
                name: "QualificationId",
                table: "Hr_Employees",
                newName: "Qualification_id");

            migrationBuilder.RenameColumn(
                name: "NationalityId",
                table: "Hr_Employees",
                newName: "Nationality_id");

            migrationBuilder.RenameColumn(
                name: "NationalId",
                table: "Hr_Employees",
                newName: "Grand_father_name_en");

            migrationBuilder.RenameColumn(
                name: "MaritalStatusId",
                table: "Hr_Employees",
                newName: "Job_id");

            migrationBuilder.RenameColumn(
                name: "ManagementId",
                table: "Hr_Employees",
                newName: "Gender_id");

            migrationBuilder.RenameColumn(
                name: "JobNumber",
                table: "Hr_Employees",
                newName: "Grand_father_name_ar");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "Hr_Employees",
                newName: "EmployeeType_id");

            migrationBuilder.RenameColumn(
                name: "ImmediatelyDate",
                table: "Hr_Employees",
                newName: "Immediately_date");

            migrationBuilder.RenameColumn(
                name: "HiringDate",
                table: "Hr_Employees",
                newName: "Hiring_date");

            migrationBuilder.RenameColumn(
                name: "GrandFatherNameEn",
                table: "Hr_Employees",
                newName: "Fixed_salary");

            migrationBuilder.RenameColumn(
                name: "GrandFatherNameAr",
                table: "Hr_Employees",
                newName: "Father_name_en");

            migrationBuilder.RenameColumn(
                name: "GenderId",
                table: "Hr_Employees",
                newName: "Department_id");

            migrationBuilder.RenameColumn(
                name: "FirstNameEn",
                table: "Hr_Employees",
                newName: "Father_name_ar");

            migrationBuilder.RenameColumn(
                name: "FirstNameAr",
                table: "Hr_Employees",
                newName: "Family_name_en");

            migrationBuilder.RenameColumn(
                name: "FingerPrintCode",
                table: "Hr_Employees",
                newName: "Employee_image_extension");

            migrationBuilder.RenameColumn(
                name: "FatherNameEn",
                table: "Hr_Employees",
                newName: "Family_name_ar");

            migrationBuilder.RenameColumn(
                name: "FatherNameAr",
                table: "Hr_Employees",
                newName: "Employee_name_en");

            migrationBuilder.RenameColumn(
                name: "FamilyNameEn",
                table: "Hr_Employees",
                newName: "Employee_name_ar");

            migrationBuilder.RenameColumn(
                name: "FamilyNameAr",
                table: "Hr_Employees",
                newName: "Children_number");

            migrationBuilder.RenameColumn(
                name: "EmployeeTypeId",
                table: "Hr_Employees",
                newName: "AccountingWay_id");

            migrationBuilder.RenameColumn(
                name: "EmployeeImageExtension",
                table: "Hr_Employees",
                newName: "Employee_image");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_VacationId",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_Vacation_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_ShiftId",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_Shift_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_SalaryPaymentWayId",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_SalaryPaymentWay_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_ReligionId",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_Relegion_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_QualificationId",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_Qualification_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_NationalityId",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_Nationality_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_MaritalStatusId",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_Job_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_ManagementId",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_Gender_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_JobId",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_EmployeeType_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_GenderId",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_Department_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_Employees_EmployeeTypeId",
                table: "Hr_Employees",
                newName: "IX_Hr_Employees_AccountingWay_id");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Hr_EmployeeAttachments",
                newName: "Employee_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hr_EmployeeAttachments_EmployeeId",
                table: "Hr_EmployeeAttachments",
                newName: "IX_Hr_EmployeeAttachments_Employee_id");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Auth_Users",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5basb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "898c4bc8-9f66-4c37-92a8-1f73e8508fb8", "AQAAAAIAAYagAAAAEEwIJdUzFZKE9IyhjWjyfdMKBYe0ccoKkFvb/psh4h0fPbNQ0I0FA/uN/alO4RkCQw==", "6b6406a1-7431-44f5-ae2e-56d9b7ca5c8a" });

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_EmployeeAttachments_Hr_Employees_Employee_id",
                table: "Hr_EmployeeAttachments",
                column: "Employee_id",
                principalTable: "Hr_Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_HrRelegion_Relegion_id",
                table: "Hr_Employees",
                column: "Relegion_id",
                principalTable: "HrRelegion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Departments_Department_id",
                table: "Hr_Employees",
                column: "Department_id",
                principalTable: "Hr_Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_EmployeeTypes_EmployeeType_id",
                table: "Hr_Employees",
                column: "EmployeeType_id",
                principalTable: "Hr_EmployeeTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Genders_Gender_id",
                table: "Hr_Employees",
                column: "Gender_id",
                principalTable: "Hr_Genders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Jobs_Job_id",
                table: "Hr_Employees",
                column: "Job_id",
                principalTable: "Hr_Jobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Nationalities_Nationality_id",
                table: "Hr_Employees",
                column: "Nationality_id",
                principalTable: "Hr_Nationalities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Qualifications_Qualification_id",
                table: "Hr_Employees",
                column: "Qualification_id",
                principalTable: "Hr_Qualifications",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_SalaryCalculators_AccountingWay_id",
                table: "Hr_Employees",
                column: "AccountingWay_id",
                principalTable: "Hr_SalaryCalculators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_SalaryPaymentWays_SalaryPaymentWay_id",
                table: "Hr_Employees",
                column: "SalaryPaymentWay_id",
                principalTable: "Hr_SalaryPaymentWays",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Shifts_Shift_id",
                table: "Hr_Employees",
                column: "Shift_id",
                principalTable: "Hr_Shifts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hr_Employees_Hr_Vacations_Vacation_id",
                table: "Hr_Employees",
                column: "Vacation_id",
                principalTable: "Hr_Vacations",
                principalColumn: "Id");
        }
    }
}
