using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FirstMid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Auth_Roles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title_name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auth_Users",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    VisiblePassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Com_Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogType = table.Column<int>(type: "int", nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExceptionPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExceptionInnerPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObjJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InnerException = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Com_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_AccountingWays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_AccountingWays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_Allowances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Hr_Allowances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_Benefits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Hr_Benefits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_CompanyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_CompanyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_Deductions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Hr_Deductions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_EmployeeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_EmployeeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Has_need_license = table.Column<bool>(type: "bit", nullable: false),
                    Has_additional_time = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Hr_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_MaritalStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_MaritalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_MilitaryStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_MilitaryStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_Nationalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_Nationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_Qualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Has_additional_time = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Hr_Qualifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_SalaryPaymentWays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_SalaryPaymentWays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start_shift = table.Column<TimeOnly>(type: "time", nullable: false),
                    End_shift = table.Column<TimeOnly>(type: "time", nullable: false),
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
                    table.PrimaryKey("PK_Hr_Shifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_VacationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_VacationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hr_ValueTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_ValueTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HrRelegion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrRelegion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "St_Actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_St_Actions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "St_MainScreenCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Screen_main_title_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Screen_main_title_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Screen_main_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_St_MainScreenCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trans_AmountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trans_AmountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trans_SalaryEffects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trans_SalaryEffects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auth_RoleClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auth_RoleClaims_Auth_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Auth_Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Auth_RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    User_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_Auth_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auth_RefreshTokens_Auth_Users_User_Id",
                        column: x => x.User_Id,
                        principalSchema: "dbo",
                        principalTable: "Auth_Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Auth_UserClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auth_UserClaims_Auth_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Auth_Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Auth_UserDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_Auth_UserDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auth_UserDevices_Auth_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Auth_Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Auth_UserLogins",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_Auth_UserLogins_Auth_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Auth_Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Auth_UserRoles",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_Auth_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Auth_UserRoles_Auth_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Auth_Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Auth_UserRoles_Auth_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Auth_Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Auth_UserTokens",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_Auth_UserTokens_Auth_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Auth_Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company_owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company_licenses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company_licenses_extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company_type = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_Companies_Hr_CompanyTypes_Company_type",
                        column: x => x.Company_type,
                        principalTable: "Hr_CompanyTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_Vacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apply_months = table.Column<int>(type: "int", nullable: false),
                    Total_vacation = table.Column<int>(type: "int", nullable: false),
                    Transfer_vacation = table.Column<bool>(type: "bit", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vacation_type = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_Vacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_Vacations_Hr_VacationTypes_Vacation_type",
                        column: x => x.Vacation_type,
                        principalTable: "Hr_VacationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "St_MainScreens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Screen_cat_title_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Screen_cat_title_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Screen_cat_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_St_MainScreens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_St_MainScreens_St_MainScreenCategories_Screen_cat_id",
                        column: x => x.Screen_cat_id,
                        principalTable: "St_MainScreenCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_CompanyContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company_contracts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company_contracts_extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_CompanyContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_CompanyContracts_Hr_Companies_Company_id",
                        column: x => x.Company_id,
                        principalTable: "Hr_Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_FingerPrints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<int>(type: "int", nullable: false),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_FingerPrints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_FingerPrints_Hr_Companies_Company_id",
                        column: x => x.Company_id,
                        principalTable: "Hr_Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company_id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameInEnglish = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_Sections_Hr_Companies_Company_id",
                        column: x => x.Company_id,
                        principalTable: "Hr_Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Employee_name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Father_name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Father_name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grand_father_name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grand_father_name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Family_name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Family_name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fixed_salary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hiring_date = table.Column<DateOnly>(type: "date", nullable: false),
                    Immediately_date = table.Column<DateOnly>(type: "date", nullable: false),
                    Total_salary = table.Column<double>(type: "float", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Children_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Employee_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_image_extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender_id = table.Column<int>(type: "int", nullable: false),
                    Relegion_id = table.Column<int>(type: "int", nullable: false),
                    SalaryPaymentWay_id = table.Column<int>(type: "int", nullable: false),
                    Shift_id = table.Column<int>(type: "int", nullable: false),
                    Department_id = table.Column<int>(type: "int", nullable: false),
                    Nationality_id = table.Column<int>(type: "int", nullable: false),
                    Qualification_id = table.Column<int>(type: "int", nullable: false),
                    AccountingWay_id = table.Column<int>(type: "int", nullable: false),
                    Vacation_id = table.Column<int>(type: "int", nullable: false),
                    EmployeeType_id = table.Column<int>(type: "int", nullable: false),
                    Job_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_Employees_HrRelegion_Relegion_id",
                        column: x => x.Relegion_id,
                        principalTable: "HrRelegion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_AccountingWays_AccountingWay_id",
                        column: x => x.AccountingWay_id,
                        principalTable: "Hr_AccountingWays",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_Departments_Department_id",
                        column: x => x.Department_id,
                        principalTable: "Hr_Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_EmployeeTypes_EmployeeType_id",
                        column: x => x.EmployeeType_id,
                        principalTable: "Hr_EmployeeTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_Genders_Gender_id",
                        column: x => x.Gender_id,
                        principalTable: "Hr_Genders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_Jobs_Job_id",
                        column: x => x.Job_id,
                        principalTable: "Hr_Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_Nationalities_Nationality_id",
                        column: x => x.Nationality_id,
                        principalTable: "Hr_Nationalities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_Qualifications_Qualification_id",
                        column: x => x.Qualification_id,
                        principalTable: "Hr_Qualifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_SalaryPaymentWays_SalaryPaymentWay_id",
                        column: x => x.SalaryPaymentWay_id,
                        principalTable: "Hr_SalaryPaymentWays",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_Shifts_Shift_id",
                        column: x => x.Shift_id,
                        principalTable: "Hr_Shifts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_Vacations_Vacation_id",
                        column: x => x.Vacation_id,
                        principalTable: "Hr_Vacations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_VacationDistributions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Days_count = table.Column<int>(type: "int", nullable: false),
                    AccountingWay_id = table.Column<int>(type: "int", nullable: false),
                    Vacation_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_VacationDistributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_VacationDistributions_Hr_AccountingWays_AccountingWay_id",
                        column: x => x.AccountingWay_id,
                        principalTable: "Hr_AccountingWays",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_VacationDistributions_Hr_Vacations_Vacation_id",
                        column: x => x.Vacation_id,
                        principalTable: "Hr_Vacations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "St_SubMainScreens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Screen_sub_title_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Screen_sub_title_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Screen_main_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_St_SubMainScreens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_St_SubMainScreens_St_MainScreens_Screen_main_id",
                        column: x => x.Screen_main_id,
                        principalTable: "St_MainScreens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_SectionDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Section_id = table.Column<int>(type: "int", nullable: false),
                    Department_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_SectionDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_SectionDepartments_Hr_Departments_Department_id",
                        column: x => x.Department_id,
                        principalTable: "Hr_Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_SectionDepartments_Hr_Sections_Section_id",
                        column: x => x.Section_id,
                        principalTable: "Hr_Sections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Salary_total = table.Column<double>(type: "float", nullable: false),
                    Salary_fixed = table.Column<double>(type: "float", nullable: false),
                    Start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    End_date = table.Column<DateOnly>(type: "date", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Employee_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_Contracts_Hr_Employees_Employee_id",
                        column: x => x.Employee_id,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_EmployeeAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Employee_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_EmployeeAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_EmployeeAttachments_Hr_Employees_Employee_id",
                        column: x => x.Employee_id,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trans_Allowances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action_month = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Salary_effect_id = table.Column<int>(type: "int", nullable: false),
                    Employee_id = table.Column<int>(type: "int", nullable: false),
                    Allowance_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Trans_Allowances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trans_Allowances_Hr_Allowances_Allowance_id",
                        column: x => x.Allowance_id,
                        principalTable: "Hr_Allowances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Allowances_Hr_Employees_Employee_id",
                        column: x => x.Employee_id,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Allowances_Trans_SalaryEffects_Salary_effect_id",
                        column: x => x.Salary_effect_id,
                        principalTable: "Trans_SalaryEffects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trans_Benefits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action_month = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value_type = table.Column<int>(type: "int", nullable: false),
                    Salary_effect_id = table.Column<int>(type: "int", nullable: false),
                    Employee_id = table.Column<int>(type: "int", nullable: false),
                    Benefit_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Trans_Benefits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trans_Benefits_Hr_Benefits_Benefit_id",
                        column: x => x.Benefit_id,
                        principalTable: "Hr_Benefits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Benefits_Hr_Employees_Employee_id",
                        column: x => x.Employee_id,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Benefits_Trans_AmountTypes_Value_type",
                        column: x => x.Value_type,
                        principalTable: "Trans_AmountTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Benefits_Trans_SalaryEffects_Salary_effect_id",
                        column: x => x.Salary_effect_id,
                        principalTable: "Trans_SalaryEffects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trans_Covenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Covenant_amount = table.Column<double>(type: "float", nullable: false),
                    Employee_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Trans_Covenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trans_Covenants_Hr_Employees_Employee_id",
                        column: x => x.Employee_id,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trans_Deductions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action_month = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value_type = table.Column<int>(type: "int", nullable: false),
                    Salary_effect_id = table.Column<int>(type: "int", nullable: false),
                    Employee_id = table.Column<int>(type: "int", nullable: false),
                    Deduction_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Trans_Deductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trans_Deductions_Hr_Deductions_Deduction_id",
                        column: x => x.Deduction_id,
                        principalTable: "Hr_Deductions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Deductions_Hr_Employees_Employee_id",
                        column: x => x.Employee_id,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Deductions_Trans_AmountTypes_Value_type",
                        column: x => x.Value_type,
                        principalTable: "Trans_AmountTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Deductions_Trans_SalaryEffects_Salary_effect_id",
                        column: x => x.Salary_effect_id,
                        principalTable: "Trans_SalaryEffects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trans_SalaryIncreases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Increase_type = table.Column<int>(type: "int", nullable: false),
                    Employee_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Trans_SalaryIncreases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trans_SalaryIncreases_Hr_Employees_Employee_id",
                        column: x => x.Employee_id,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_SalaryIncreases_Hr_ValueTypes_Increase_type",
                        column: x => x.Increase_type,
                        principalTable: "Hr_ValueTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trans_Vacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    Days_count = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee_id = table.Column<int>(type: "int", nullable: false),
                    Vacation_system_d_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Trans_Vacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trans_Vacations_Hr_Employees_Employee_id",
                        column: x => x.Employee_id,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Vacations_Hr_Vacations_Vacation_system_d_id",
                        column: x => x.Vacation_system_d_id,
                        principalTable: "Hr_Vacations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "St_SubMainScreenActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubMainScreenId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_St_SubMainScreenActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_St_SubMainScreenActions_St_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "St_Actions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_St_SubMainScreenActions_St_SubMainScreens_SubMainScreenId",
                        column: x => x.SubMainScreenId,
                        principalTable: "St_SubMainScreens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_ContractAllowancesDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Value_type = table.Column<int>(type: "int", nullable: false),
                    Salary_effect_id = table.Column<int>(type: "int", nullable: false),
                    Contract_id = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_ContractAllowancesDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_ContractAllowancesDetails_Hr_Allowances_Salary_effect_id",
                        column: x => x.Salary_effect_id,
                        principalTable: "Hr_Allowances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_ContractAllowancesDetails_Hr_Contracts_Contract_id",
                        column: x => x.Contract_id,
                        principalTable: "Hr_Contracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_ContractAllowancesDetails_Hr_ValueTypes_Value_type",
                        column: x => x.Value_type,
                        principalTable: "Hr_ValueTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Auth_Roles",
                columns: new[] { "Id", "Add_date", "Added_by", "ConcurrencyStamp", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NormalizedName", "Title_name_ar", "UpdateBy", "UpdateDate" },
                values: new object[] { "fab4fac1-c546-41de-aebc-a14da68957ab1", null, null, "1", null, null, true, false, "Superadmin", "SUPERADMIN", "سوبر أدمن", null, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Auth_Users",
                columns: new[] { "Id", "AccessFailedCount", "Add_date", "Added_by", "ConcurrencyStamp", "DeleteBy", "DeleteDate", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdateBy", "UpdateDate", "UserName", "VisiblePassword" },
                values: new object[] { "b74ddd14-6340-4840-95c2-db12554843e5basb1", 0, null, null, "d64c8836-9919-484c-b883-7c2e35ad90de", null, null, "mohammed88@gmail.com", true, true, false, false, null, "MOHAMMED88@GMAIL.COM", "MR_MOHAMMED", "AQAAAAIAAYagAAAAEBdc3TQE87jCh1KsEn3Fur3RUzXOpFxd2kuzjg8BiQiRTn8EVkHrb8XlXMXXHx81jA==", null, false, "c01d7e93-2bf0-4b9a-a8f9-2e23f6637588", false, null, null, "Mr_Mohammed", "Mohammed88" });

            migrationBuilder.InsertData(
                table: "HrRelegion",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "مسلم", "Muslim", null, null },
                    { 2, null, null, null, null, true, false, "مسيحى", "Christian", null, null },
                    { 3, null, null, null, null, true, false, "غير ذلك", "Otherwise", null, null }
                });

            migrationBuilder.InsertData(
                table: "Hr_AccountingWays",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "كل الاتب", "All salary", null, null },
                    { 2, null, null, null, null, true, false, "الراتب الرئيسى", "Main salary", null, null },
                    { 3, null, null, null, null, true, false, "بدون راتب", "Without salary", null, null }
                });

            migrationBuilder.InsertData(
                table: "Hr_CompanyTypes",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "شركة", "Company", null, null },
                    { 2, null, null, null, null, true, false, "مؤسسة", "Corporate", null, null }
                });

            migrationBuilder.InsertData(
                table: "Hr_EmployeeTypes",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "مقيم", "Resident", null, null },
                    { 2, null, null, null, null, true, false, "مواطن", "Citizen", null, null }
                });

            migrationBuilder.InsertData(
                table: "Hr_Genders",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "ذكر", "Male", null, null },
                    { 2, null, null, null, null, true, false, "أنثى", "Female", null, null }
                });

            migrationBuilder.InsertData(
                table: "Hr_MaritalStatus",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "أعزب", "Single", null, null },
                    { 2, null, null, null, null, true, false, "خاطب", "Engaged", null, null },
                    { 3, null, null, null, null, true, false, "متزوج", "Married", null, null },
                    { 4, null, null, null, null, true, false, "مطللق", "Divorced", null, null }
                });

            migrationBuilder.InsertData(
                table: "Hr_MilitaryStatus",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "معفى", "Exempt", null, null },
                    { 2, null, null, null, null, true, false, "مؤجل", "Delayed", null, null },
                    { 3, null, null, null, null, true, false, "انهى الخدمة", "Completed", null, null }
                });

            migrationBuilder.InsertData(
                table: "Hr_Nationalities",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "مصرى", "Egyptian ", null, null },
                    { 2, null, null, null, null, true, false, "سعودى", "Saudian", null, null }
                });

            migrationBuilder.InsertData(
                table: "Hr_SalaryPaymentWays",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "بنكى", "Bank", null, null },
                    { 2, null, null, null, null, true, false, "نقدى", "Cash", null, null },
                    { 3, null, null, null, null, true, false, "حوالة مالية", "Money transfer", null, null }
                });

            migrationBuilder.InsertData(
                table: "Hr_VacationTypes",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "عام كامل", "Full year", null, null },
                    { 2, null, null, null, null, true, false, "من تاريخ التعيين", "From hiring date", null, null },
                    { 3, null, null, null, null, true, false, "من تاريخ الاستحقاق", "After hiring days", null, null }
                });

            migrationBuilder.InsertData(
                table: "Hr_ValueTypes",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "مبلغ", "Percent", null, null },
                    { 2, null, null, null, null, true, false, "نسبة", "Amount", null, null }
                });

            migrationBuilder.InsertData(
                table: "St_Actions",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "إظهار", "View", null, null },
                    { 2, null, null, null, null, true, false, "اضافة", "Add", null, null },
                    { 3, null, null, null, null, true, false, "تعديل", "Edit", null, null },
                    { 4, null, null, null, null, true, false, "حذف", "Delete", null, null },
                    { 5, null, null, null, null, true, false, "حذف نهائى", "ForceDelete", null, null },
                    { 6, null, null, null, null, true, false, "طباعة", "Ptint", null, null }
                });

            migrationBuilder.InsertData(
                table: "Trans_AmountTypes",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "ساعة", "Hour", null, null },
                    { 2, null, null, null, null, true, false, "أيام عمل", "Work days", null, null },
                    { 3, null, null, null, null, true, false, "القيمة", "Value", null, null }
                });

            migrationBuilder.InsertData(
                table: "Trans_SalaryEffects",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "قطعى", "On time", null, null },
                    { 2, null, null, null, null, true, false, "شهرى", "Monthly", null, null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Auth_UserRoles",
                columns: new[] { "RoleId", "UserId", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "UpdateBy", "UpdateDate" },
                values: new object[] { "fab4fac1-c546-41de-aebc-a14da68957ab1", "b74ddd14-6340-4840-95c2-db12554843e5basb1", null, null, null, null, true, false, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Auth_RefreshTokens_User_Id",
                table: "Auth_RefreshTokens",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Auth_RoleClaims_RoleId",
                schema: "dbo",
                table: "Auth_RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "Auth_Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Auth_UserClaims_UserId",
                schema: "dbo",
                table: "Auth_UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Auth_UserDevices_UserId",
                table: "Auth_UserDevices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Auth_UserLogins_UserId",
                schema: "dbo",
                table: "Auth_UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Auth_UserRoles_RoleId",
                schema: "dbo",
                table: "Auth_UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "Auth_Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "Auth_Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Companies_Company_type",
                table: "Hr_Companies",
                column: "Company_type");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_CompanyContracts_Company_id",
                table: "Hr_CompanyContracts",
                column: "Company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_ContractAllowancesDetails_Contract_id",
                table: "Hr_ContractAllowancesDetails",
                column: "Contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_ContractAllowancesDetails_Salary_effect_id",
                table: "Hr_ContractAllowancesDetails",
                column: "Salary_effect_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_ContractAllowancesDetails_Value_type",
                table: "Hr_ContractAllowancesDetails",
                column: "Value_type");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Contracts_Employee_id",
                table: "Hr_Contracts",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_EmployeeAttachments_Employee_id",
                table: "Hr_EmployeeAttachments",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_AccountingWay_id",
                table: "Hr_Employees",
                column: "AccountingWay_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_Department_id",
                table: "Hr_Employees",
                column: "Department_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_EmployeeType_id",
                table: "Hr_Employees",
                column: "EmployeeType_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_Gender_id",
                table: "Hr_Employees",
                column: "Gender_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_Job_id",
                table: "Hr_Employees",
                column: "Job_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_Nationality_id",
                table: "Hr_Employees",
                column: "Nationality_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_Qualification_id",
                table: "Hr_Employees",
                column: "Qualification_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_Relegion_id",
                table: "Hr_Employees",
                column: "Relegion_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_SalaryPaymentWay_id",
                table: "Hr_Employees",
                column: "SalaryPaymentWay_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_Shift_id",
                table: "Hr_Employees",
                column: "Shift_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_Vacation_id",
                table: "Hr_Employees",
                column: "Vacation_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_FingerPrints_Company_id",
                table: "Hr_FingerPrints",
                column: "Company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_SectionDepartments_Department_id",
                table: "Hr_SectionDepartments",
                column: "Department_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_SectionDepartments_Section_id",
                table: "Hr_SectionDepartments",
                column: "Section_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Sections_Company_id",
                table: "Hr_Sections",
                column: "Company_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_VacationDistributions_AccountingWay_id",
                table: "Hr_VacationDistributions",
                column: "AccountingWay_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_VacationDistributions_Vacation_id",
                table: "Hr_VacationDistributions",
                column: "Vacation_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Vacations_Vacation_type",
                table: "Hr_Vacations",
                column: "Vacation_type");

            migrationBuilder.CreateIndex(
                name: "IX_St_MainScreens_Screen_cat_id",
                table: "St_MainScreens",
                column: "Screen_cat_id");

            migrationBuilder.CreateIndex(
                name: "IX_St_SubMainScreenActions_ActionId",
                table: "St_SubMainScreenActions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_St_SubMainScreenActions_SubMainScreenId",
                table: "St_SubMainScreenActions",
                column: "SubMainScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_St_SubMainScreens_Screen_main_id",
                table: "St_SubMainScreens",
                column: "Screen_main_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Allowances_Allowance_id",
                table: "Trans_Allowances",
                column: "Allowance_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Allowances_Employee_id",
                table: "Trans_Allowances",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Allowances_Salary_effect_id",
                table: "Trans_Allowances",
                column: "Salary_effect_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Benefits_Benefit_id",
                table: "Trans_Benefits",
                column: "Benefit_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Benefits_Employee_id",
                table: "Trans_Benefits",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Benefits_Salary_effect_id",
                table: "Trans_Benefits",
                column: "Salary_effect_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Benefits_Value_type",
                table: "Trans_Benefits",
                column: "Value_type");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Covenants_Employee_id",
                table: "Trans_Covenants",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Deductions_Deduction_id",
                table: "Trans_Deductions",
                column: "Deduction_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Deductions_Employee_id",
                table: "Trans_Deductions",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Deductions_Salary_effect_id",
                table: "Trans_Deductions",
                column: "Salary_effect_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Deductions_Value_type",
                table: "Trans_Deductions",
                column: "Value_type");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_SalaryIncreases_Employee_id",
                table: "Trans_SalaryIncreases",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_SalaryIncreases_Increase_type",
                table: "Trans_SalaryIncreases",
                column: "Increase_type");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Vacations_Employee_id",
                table: "Trans_Vacations",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Vacations_Vacation_system_d_id",
                table: "Trans_Vacations",
                column: "Vacation_system_d_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auth_RefreshTokens");

            migrationBuilder.DropTable(
                name: "Auth_RoleClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Auth_UserClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Auth_UserDevices");

            migrationBuilder.DropTable(
                name: "Auth_UserLogins",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Auth_UserRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Auth_UserTokens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Com_Logs");

            migrationBuilder.DropTable(
                name: "Hr_CompanyContracts");

            migrationBuilder.DropTable(
                name: "Hr_ContractAllowancesDetails");

            migrationBuilder.DropTable(
                name: "Hr_EmployeeAttachments");

            migrationBuilder.DropTable(
                name: "Hr_FingerPrints");

            migrationBuilder.DropTable(
                name: "Hr_MaritalStatus");

            migrationBuilder.DropTable(
                name: "Hr_MilitaryStatus");

            migrationBuilder.DropTable(
                name: "Hr_SectionDepartments");

            migrationBuilder.DropTable(
                name: "Hr_VacationDistributions");

            migrationBuilder.DropTable(
                name: "St_SubMainScreenActions");

            migrationBuilder.DropTable(
                name: "Trans_Allowances");

            migrationBuilder.DropTable(
                name: "Trans_Benefits");

            migrationBuilder.DropTable(
                name: "Trans_Covenants");

            migrationBuilder.DropTable(
                name: "Trans_Deductions");

            migrationBuilder.DropTable(
                name: "Trans_SalaryIncreases");

            migrationBuilder.DropTable(
                name: "Trans_Vacations");

            migrationBuilder.DropTable(
                name: "Auth_Roles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Auth_Users",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Hr_Contracts");

            migrationBuilder.DropTable(
                name: "Hr_Sections");

            migrationBuilder.DropTable(
                name: "St_Actions");

            migrationBuilder.DropTable(
                name: "St_SubMainScreens");

            migrationBuilder.DropTable(
                name: "Hr_Allowances");

            migrationBuilder.DropTable(
                name: "Hr_Benefits");

            migrationBuilder.DropTable(
                name: "Hr_Deductions");

            migrationBuilder.DropTable(
                name: "Trans_AmountTypes");

            migrationBuilder.DropTable(
                name: "Trans_SalaryEffects");

            migrationBuilder.DropTable(
                name: "Hr_ValueTypes");

            migrationBuilder.DropTable(
                name: "Hr_Employees");

            migrationBuilder.DropTable(
                name: "Hr_Companies");

            migrationBuilder.DropTable(
                name: "St_MainScreens");

            migrationBuilder.DropTable(
                name: "HrRelegion");

            migrationBuilder.DropTable(
                name: "Hr_AccountingWays");

            migrationBuilder.DropTable(
                name: "Hr_Departments");

            migrationBuilder.DropTable(
                name: "Hr_EmployeeTypes");

            migrationBuilder.DropTable(
                name: "Hr_Genders");

            migrationBuilder.DropTable(
                name: "Hr_Jobs");

            migrationBuilder.DropTable(
                name: "Hr_Nationalities");

            migrationBuilder.DropTable(
                name: "Hr_Qualifications");

            migrationBuilder.DropTable(
                name: "Hr_SalaryPaymentWays");

            migrationBuilder.DropTable(
                name: "Hr_Shifts");

            migrationBuilder.DropTable(
                name: "Hr_Vacations");

            migrationBuilder.DropTable(
                name: "Hr_CompanyTypes");

            migrationBuilder.DropTable(
                name: "St_MainScreenCategories");

            migrationBuilder.DropTable(
                name: "Hr_VacationTypes");
        }
    }
}
