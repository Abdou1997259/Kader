using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Addsalaryamount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<decimal>(
             name: "SalaryAmount",
             table: "TransVacation",
             type: "decimal(18,2)", // Precision: 18, Scale: 2
             nullable: false,
             defaultValue: 0m);
            // Add SalaryAmount column to trans_vacations table
            migrationBuilder.AddColumn<decimal>(
                name: "SalaryAmount",
                table: "VacationRequests",
                type: "decimal(18,2)", // Precision: 18, Scale: 2
                nullable: false,
                defaultValue: 0m);

            // Add SalaryAmount column to hr_vacation_requests table
         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove SalaryAmount column from trans_vacations table
            //migrationBuilder.DropColumn(
            //    name: "SalaryAmount",
            //    table: "TransVacation");

            //// Remove SalaryAmount column from hr_vacation_requests table
            //migrationBuilder.DropColumn(
            //    name: "SalaryAmount",
            //    table: "VacationRequests");
        }

    }
}
