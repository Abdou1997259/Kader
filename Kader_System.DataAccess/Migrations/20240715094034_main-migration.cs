using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kader_System.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mainmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Advanced_Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvanceTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advanced_Types", x => x.Id);
                });

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
                name: "Hr_Allowances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNo = table.Column<long>(type: "bigint", nullable: true),
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
                    AccountNo = table.Column<long>(type: "bigint", nullable: true),
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
                    AccountNo = table.Column<long>(type: "bigint", nullable: true),
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
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasNeedLicense = table.Column<bool>(type: "bit", nullable: false),
                    HasAdditionalTime = table.Column<bool>(type: "bit", nullable: false),
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
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "Hr_SalaryCalculators",
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
                    table.PrimaryKey("PK_Hr_SalaryCalculators", x => x.Id);
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
                name: "MainScreenTrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenTitleEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreenTitleAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_MainScreenTrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainScreenTrees_MainScreenTrees_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MainScreenTrees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Screens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    ScreenType = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Screens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screens_Screens_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Screens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpCaclauateSalaryDetailedTransModel",
                columns: table => new
                {
                    TransId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    FullNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JournalDate = table.Column<DateOnly>(type: "date", nullable: false),
                    JournalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccommodationAllowance = table.Column<double>(type: "float", nullable: true),
                    CalculatedSalary = table.Column<double>(type: "float", nullable: false),
                    CalculateSalaryId = table.Column<int>(type: "int", nullable: true),
                    CalculateSalaryDetailsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SpCaclauateSalaryDetailsModel",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    JournalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalculatedSalary = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SpCacluateSalariesModel",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    FullNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccommodationAllowance = table.Column<double>(type: "float", nullable: false),
                    CalculatedSalary = table.Column<double>(type: "float", nullable: false),
                    TotalSalary = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
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
                name: "St_MainScreens",
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
                    table.PrimaryKey("PK_St_MainScreens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Titles", x => x.Id);
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
                name: "trans_salary_calculators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalculationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    ManagementId = table.Column<int>(type: "int", nullable: true),
                    IsMigrated = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_trans_salary_calculators", x => x.Id);
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
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyOwner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company_licenses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Company_licenses_extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyTypeId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Hr_Companies_Hr_CompanyTypes_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalTable: "Hr_CompanyTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_Vacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplyAfterMonth = table.Column<int>(type: "int", nullable: false),
                    TotalBalance = table.Column<int>(type: "int", nullable: false),
                    CanTransfer = table.Column<bool>(type: "bit", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VacationTypeId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Hr_Vacations_Hr_VacationTypes_VacationTypeId",
                        column: x => x.VacationTypeId,
                        principalTable: "Hr_VacationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StScreenAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_StScreenAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StScreenAction_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StScreenAction_St_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "St_Actions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "St_MainScreenCats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Screen_cat_title_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Screen_cat_title_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainScreenId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_St_MainScreenCats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_St_MainScreenCats_St_MainScreens_MainScreenId",
                        column: x => x.MainScreenId,
                        principalTable: "St_MainScreens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "trans_salary_calculators_details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    TransSalaryCalculatorsId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_trans_salary_calculators_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trans_salary_calculators_details_trans_salary_calculators_TransSalaryCalculatorsId",
                        column: x => x.TransSalaryCalculatorsId,
                        principalTable: "trans_salary_calculators",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_CompanyContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyContracts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyContractsExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Hr_CompanyContracts_Hr_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Hr_Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_CompanyLicenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_CompanyLicenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_CompanyLicenses_Hr_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Hr_Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_FingerPrints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Hr_FingerPrints_Hr_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Hr_Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Hr_Sections_Hr_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Hr_Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_VacationDistributions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaysCount = table.Column<int>(type: "int", nullable: false),
                    SalaryCalculatorId = table.Column<int>(type: "int", nullable: false),
                    VacationId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Hr_VacationDistributions_Hr_SalaryCalculators_SalaryCalculatorId",
                        column: x => x.SalaryCalculatorId,
                        principalTable: "Hr_SalaryCalculators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_VacationDistributions_Hr_Vacations_VacationId",
                        column: x => x.VacationId,
                        principalTable: "Hr_Vacations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "St_ScreensSubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Screen_sub_title_en = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Screen_sub_title_ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreenCatId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_St_ScreensSubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_St_ScreensSubs_St_MainScreenCats_ScreenCatId",
                        column: x => x.ScreenCatId,
                        principalTable: "St_MainScreenCats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrandFatherNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrandFatherNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccommodationAllowance = table.Column<double>(type: "float", nullable: false),
                    FullNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false, computedColumnSql: "[FirstNameAr]+' '+[FatherNameAr]+' '+[GrandFatherNameAr]+' '+[FamilyNameAr]"),
                    FullNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false, computedColumnSql: "[FirstNameEn]+' '+[FatherNameEn]+' '+[GrandFatherNameEn]+' '+[FamilyNameEn]"),
                    MaritalStatusId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FixedSalary = table.Column<double>(type: "float", nullable: false),
                    HiringDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ImmediatelyDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TotalSalary = table.Column<double>(type: "float", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReligionId = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalaryPaymentWayId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChildrenNumber = table.Column<int>(type: "int", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    FingerPrintId = table.Column<int>(type: "int", nullable: true),
                    FingerPrintCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeImageExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagementId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    NationalityId = table.Column<int>(type: "int", nullable: false),
                    QualificationId = table.Column<int>(type: "int", nullable: false),
                    VacationId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    EmployeeTypeId = table.Column<int>(type: "int", nullable: false),
                    AccountNo = table.Column<long>(type: "bigint", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Add_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Added_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleteBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Auth_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Auth_Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_HrRelegion_ReligionId",
                        column: x => x.ReligionId,
                        principalTable: "HrRelegion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Hr_Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_EmployeeTypes_EmployeeTypeId",
                        column: x => x.EmployeeTypeId,
                        principalTable: "Hr_EmployeeTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_FingerPrints_FingerPrintId",
                        column: x => x.FingerPrintId,
                        principalTable: "Hr_FingerPrints",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Hr_Genders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Hr_Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_MaritalStatus_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "Hr_MaritalStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Hr_Nationalities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_Qualifications_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Hr_Qualifications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_SalaryPaymentWays_SalaryPaymentWayId",
                        column: x => x.SalaryPaymentWayId,
                        principalTable: "Hr_SalaryPaymentWays",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Hr_Shifts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Employees_Hr_Vacations_VacationId",
                        column: x => x.VacationId,
                        principalTable: "Hr_Vacations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "St_SubMainScreenActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenSubId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    MainScreenTreeId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_St_SubMainScreenActions_MainScreenTrees_MainScreenTreeId",
                        column: x => x.MainScreenTreeId,
                        principalTable: "MainScreenTrees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_St_SubMainScreenActions_St_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "St_Actions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_St_SubMainScreenActions_St_ScreensSubs_ScreenSubId",
                        column: x => x.ScreenSubId,
                        principalTable: "St_ScreensSubs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TitlePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleId = table.Column<int>(type: "int", nullable: false),
                    SubScreenId = table.Column<int>(type: "int", nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitlePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TitlePermissions_St_ScreensSubs_SubScreenId",
                        column: x => x.SubScreenId,
                        principalTable: "St_ScreensSubs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TitlePermissions_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalSalary = table.Column<double>(type: "float", nullable: false),
                    FixedSalary = table.Column<double>(type: "float", nullable: false),
                    HousingAllowance = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Hr_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_Contracts_Hr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
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
                    table.PrimaryKey("PK_Hr_EmployeeAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_EmployeeAttachments_Hr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_Managements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_Managements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_Managements_Hr_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Hr_Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Managements_Hr_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trans_Allowances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionMonth = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    SalaryEffectId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AllowanceId = table.Column<int>(type: "int", nullable: false),
                    CalculateSalaryId = table.Column<int>(type: "int", nullable: true),
                    CalculateSalaryDetailsId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_Trans_Allowances_Hr_Allowances_AllowanceId",
                        column: x => x.AllowanceId,
                        principalTable: "Hr_Allowances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Allowances_Hr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Allowances_Trans_SalaryEffects_SalaryEffectId",
                        column: x => x.SalaryEffectId,
                        principalTable: "Trans_SalaryEffects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trans_Benefits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionMonth = table.Column<DateOnly>(type: "date", nullable: false),
                    AmountTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    SalaryEffectId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    BenefitId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalculateSalaryId = table.Column<int>(type: "int", nullable: true),
                    CalculateSalaryDetailsId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_Trans_Benefits_Hr_Benefits_BenefitId",
                        column: x => x.BenefitId,
                        principalTable: "Hr_Benefits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Benefits_Hr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Benefits_Trans_AmountTypes_AmountTypeId",
                        column: x => x.AmountTypeId,
                        principalTable: "Trans_AmountTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Benefits_Trans_SalaryEffects_SalaryEffectId",
                        column: x => x.SalaryEffectId,
                        principalTable: "Trans_SalaryEffects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trans_Covenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        name: "FK_Trans_Covenants_Hr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trans_Deductions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionMonth = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    AmountTypeId = table.Column<int>(type: "int", nullable: false),
                    SalaryEffectId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DeductionId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalculateSalaryId = table.Column<int>(type: "int", nullable: true),
                    CalculateSalaryDetailsId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_Trans_Deductions_Hr_Deductions_DeductionId",
                        column: x => x.DeductionId,
                        principalTable: "Hr_Deductions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Deductions_Hr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Deductions_Trans_AmountTypes_AmountTypeId",
                        column: x => x.AmountTypeId,
                        principalTable: "Trans_AmountTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Deductions_Trans_SalaryEffects_SalaryEffectId",
                        column: x => x.SalaryEffectId,
                        principalTable: "Trans_SalaryEffects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trans_Loan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartLoanDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDoDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DocumentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AdvanceType = table.Column<short>(type: "smallint", nullable: false),
                    MonthlyDeducted = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PrevDedcutedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    InstallmentCount = table.Column<int>(type: "int", nullable: false),
                    LoanType = table.Column<short>(type: "smallint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartCalculationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndCalculationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CalculateSalaryId = table.Column<int>(type: "int", nullable: true),
                    CalculateSalaryDetailsId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Trans_Loan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trans_Loan_Hr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trans_SalaryIncreases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    salaryAfterIncrease = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DaysCount = table.Column<double>(type: "float", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    VacationId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalculateSalaryId = table.Column<int>(type: "int", nullable: true),
                    CalculateSalaryDetailsId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_Trans_Vacations_Hr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trans_Vacations_Hr_VacationDistributions_VacationId",
                        column: x => x.VacationId,
                        principalTable: "Hr_VacationDistributions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hr_ContractAllowancesDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllowanceId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    IsPercent = table.Column<bool>(type: "bit", nullable: false),
                    ContractId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Hr_ContractAllowancesDetails_Hr_Allowances_AllowanceId",
                        column: x => x.AllowanceId,
                        principalTable: "Hr_Allowances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_ContractAllowancesDetails_Hr_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Hr_Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hr_Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    ManagementId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Hr_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hr_Departments_Hr_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Hr_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hr_Departments_Hr_Managements_ManagementId",
                        column: x => x.ManagementId,
                        principalTable: "Hr_Managements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trans_Loan_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeductionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DelayCount = table.Column<int>(type: "int", nullable: false),
                    TransLoanId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Trans_Loan_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trans_Loan_Details_Trans_Loan_TransLoanId",
                        column: x => x.TransLoanId,
                        principalTable: "Trans_Loan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                table: "Advanced_Types",
                columns: new[] { "Id", "AdvanceTypeName" },
                values: new object[,]
                {
                    { 1, "سلفة" },
                    { 2, "مخالفة مرورية" },
                    { 3, "حوادث وأصلاحات" },
                    { 4, "نقل خدمات" },
                    { 5, "مخالفة مرورية" },
                    { 6, "تأمينات" },
                    { 7, "تسويات اخري" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Auth_Roles",
                columns: new[] { "Id", "Add_date", "Added_by", "ConcurrencyStamp", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NormalizedName", "Title_name_ar", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { "0ffa8112-ba0d-4416-b0ed-992897ac896e", null, null, "1", null, null, true, false, "User", "USER", "مستخدم", null, null },
                    { "fab4fac1-c546-41de-aebc-a14da68957ab1", null, null, "1", null, null, true, false, "Superadmin", "SUPERADMIN", "سوبر أدمن", null, null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Auth_Users",
                columns: new[] { "Id", "AccessFailedCount", "Add_date", "Added_by", "ConcurrencyStamp", "DeleteBy", "DeleteDate", "Email", "EmailConfirmed", "IsActive", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdateBy", "UpdateDate", "UserName", "VisiblePassword" },
                values: new object[] { "b74ddd14-6340-4840-95c2-db12554843e5basb1", 0, null, null, "c76649a9-ddfa-445d-9f7f-848db3851372", null, null, "mohammed88@gmail.com", true, true, false, false, null, "MOHAMMED88@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEJej//H+xGlSgfa+otcdJS9vT/0QVnG3VpsaSy/dcsObxlxiD4iOT7GwHaju5Tynfw==", null, false, "aa56a45c-c884-4435-bb18-5369b734a656", false, null, null, "admin", "123456" });

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
                table: "Hr_SalaryCalculators",
                columns: new[] { "Id", "Add_date", "Added_by", "DeleteBy", "DeleteDate", "IsActive", "IsDeleted", "Name", "NameInEnglish", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, null, null, null, true, false, "كل الاتب", "All salary", null, null },
                    { 2, null, null, null, null, true, false, "الراتب الرئيسى", "Main salary", null, null },
                    { 3, null, null, null, null, true, false, "بدون راتب", "Without salary", null, null }
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
                    { 6, null, null, null, null, true, false, "طباعة", "Print", null, null }
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
                name: "IX_Hr_Companies_CompanyTypeId",
                table: "Hr_Companies",
                column: "CompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_CompanyContracts_CompanyId",
                table: "Hr_CompanyContracts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_CompanyLicenses_CompanyId",
                table: "Hr_CompanyLicenses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_ContractAllowancesDetails_AllowanceId",
                table: "Hr_ContractAllowancesDetails",
                column: "AllowanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_ContractAllowancesDetails_ContractId",
                table: "Hr_ContractAllowancesDetails",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Contracts_EmployeeId",
                table: "Hr_Contracts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Departments_ManagementId",
                table: "Hr_Departments",
                column: "ManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Departments_ManagerId",
                table: "Hr_Departments",
                column: "ManagerId",
                unique: true,
                filter: "[ManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_EmployeeAttachments_EmployeeId",
                table: "Hr_EmployeeAttachments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_CompanyId",
                table: "Hr_Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_EmployeeTypeId",
                table: "Hr_Employees",
                column: "EmployeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_FingerPrintId",
                table: "Hr_Employees",
                column: "FingerPrintId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_GenderId",
                table: "Hr_Employees",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_JobId",
                table: "Hr_Employees",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_MaritalStatusId",
                table: "Hr_Employees",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_NationalityId",
                table: "Hr_Employees",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_QualificationId",
                table: "Hr_Employees",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_ReligionId",
                table: "Hr_Employees",
                column: "ReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_SalaryPaymentWayId",
                table: "Hr_Employees",
                column: "SalaryPaymentWayId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_ShiftId",
                table: "Hr_Employees",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_UserId",
                table: "Hr_Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Employees_VacationId",
                table: "Hr_Employees",
                column: "VacationId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_FingerPrints_CompanyId",
                table: "Hr_FingerPrints",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Managements_CompanyId",
                table: "Hr_Managements",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Managements_ManagerId",
                table: "Hr_Managements",
                column: "ManagerId",
                unique: true,
                filter: "[ManagerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_SectionDepartments_Department_id",
                table: "Hr_SectionDepartments",
                column: "Department_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_SectionDepartments_Section_id",
                table: "Hr_SectionDepartments",
                column: "Section_id");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Sections_CompanyId",
                table: "Hr_Sections",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_VacationDistributions_SalaryCalculatorId",
                table: "Hr_VacationDistributions",
                column: "SalaryCalculatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_VacationDistributions_VacationId",
                table: "Hr_VacationDistributions",
                column: "VacationId");

            migrationBuilder.CreateIndex(
                name: "IX_Hr_Vacations_VacationTypeId",
                table: "Hr_Vacations",
                column: "VacationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MainScreenTrees_ParentId",
                table: "MainScreenTrees",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Screens_ParentId",
                table: "Screens",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_St_MainScreenCats_MainScreenId",
                table: "St_MainScreenCats",
                column: "MainScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_St_ScreensSubs_ScreenCatId",
                table: "St_ScreensSubs",
                column: "ScreenCatId");

            migrationBuilder.CreateIndex(
                name: "IX_St_SubMainScreenActions_ActionId",
                table: "St_SubMainScreenActions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_St_SubMainScreenActions_MainScreenTreeId",
                table: "St_SubMainScreenActions",
                column: "MainScreenTreeId");

            migrationBuilder.CreateIndex(
                name: "IX_St_SubMainScreenActions_ScreenSubId",
                table: "St_SubMainScreenActions",
                column: "ScreenSubId");

            migrationBuilder.CreateIndex(
                name: "IX_StScreenAction_ActionId",
                table: "StScreenAction",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_StScreenAction_ScreenId",
                table: "StScreenAction",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_TitlePermissions_SubScreenId",
                table: "TitlePermissions",
                column: "SubScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_TitlePermissions_TitleId",
                table: "TitlePermissions",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Allowances_AllowanceId",
                table: "Trans_Allowances",
                column: "AllowanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Allowances_EmployeeId",
                table: "Trans_Allowances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Allowances_SalaryEffectId",
                table: "Trans_Allowances",
                column: "SalaryEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Benefits_AmountTypeId",
                table: "Trans_Benefits",
                column: "AmountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Benefits_BenefitId",
                table: "Trans_Benefits",
                column: "BenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Benefits_EmployeeId",
                table: "Trans_Benefits",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Benefits_SalaryEffectId",
                table: "Trans_Benefits",
                column: "SalaryEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Covenants_EmployeeId",
                table: "Trans_Covenants",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Deductions_AmountTypeId",
                table: "Trans_Deductions",
                column: "AmountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Deductions_DeductionId",
                table: "Trans_Deductions",
                column: "DeductionId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Deductions_EmployeeId",
                table: "Trans_Deductions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Deductions_SalaryEffectId",
                table: "Trans_Deductions",
                column: "SalaryEffectId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Loan_EmployeeId",
                table: "Trans_Loan",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Loan_Details_TransLoanId",
                table: "Trans_Loan_Details",
                column: "TransLoanId");

            migrationBuilder.CreateIndex(
                name: "IX_trans_salary_calculators_details_TransSalaryCalculatorsId",
                table: "trans_salary_calculators_details",
                column: "TransSalaryCalculatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_SalaryIncreases_Employee_id",
                table: "Trans_SalaryIncreases",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_SalaryIncreases_Increase_type",
                table: "Trans_SalaryIncreases",
                column: "Increase_type");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Vacations_EmployeeId",
                table: "Trans_Vacations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trans_Vacations_VacationId",
                table: "Trans_Vacations",
                column: "VacationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advanced_Types");

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
                name: "Hr_CompanyLicenses");

            migrationBuilder.DropTable(
                name: "Hr_ContractAllowancesDetails");

            migrationBuilder.DropTable(
                name: "Hr_EmployeeAttachments");

            migrationBuilder.DropTable(
                name: "Hr_MilitaryStatus");

            migrationBuilder.DropTable(
                name: "Hr_SectionDepartments");

            migrationBuilder.DropTable(
                name: "SpCaclauateSalaryDetailedTransModel");

            migrationBuilder.DropTable(
                name: "SpCaclauateSalaryDetailsModel");

            migrationBuilder.DropTable(
                name: "SpCacluateSalariesModel");

            migrationBuilder.DropTable(
                name: "St_SubMainScreenActions");

            migrationBuilder.DropTable(
                name: "StScreenAction");

            migrationBuilder.DropTable(
                name: "TitlePermissions");

            migrationBuilder.DropTable(
                name: "Trans_Allowances");

            migrationBuilder.DropTable(
                name: "Trans_Benefits");

            migrationBuilder.DropTable(
                name: "Trans_Covenants");

            migrationBuilder.DropTable(
                name: "Trans_Deductions");

            migrationBuilder.DropTable(
                name: "Trans_Loan_Details");

            migrationBuilder.DropTable(
                name: "trans_salary_calculators_details");

            migrationBuilder.DropTable(
                name: "Trans_SalaryIncreases");

            migrationBuilder.DropTable(
                name: "Trans_Vacations");

            migrationBuilder.DropTable(
                name: "Auth_Roles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Hr_Contracts");

            migrationBuilder.DropTable(
                name: "Hr_Departments");

            migrationBuilder.DropTable(
                name: "Hr_Sections");

            migrationBuilder.DropTable(
                name: "MainScreenTrees");

            migrationBuilder.DropTable(
                name: "Screens");

            migrationBuilder.DropTable(
                name: "St_Actions");

            migrationBuilder.DropTable(
                name: "St_ScreensSubs");

            migrationBuilder.DropTable(
                name: "Titles");

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
                name: "Trans_Loan");

            migrationBuilder.DropTable(
                name: "trans_salary_calculators");

            migrationBuilder.DropTable(
                name: "Hr_ValueTypes");

            migrationBuilder.DropTable(
                name: "Hr_VacationDistributions");

            migrationBuilder.DropTable(
                name: "Hr_Managements");

            migrationBuilder.DropTable(
                name: "St_MainScreenCats");

            migrationBuilder.DropTable(
                name: "Hr_SalaryCalculators");

            migrationBuilder.DropTable(
                name: "Hr_Employees");

            migrationBuilder.DropTable(
                name: "St_MainScreens");

            migrationBuilder.DropTable(
                name: "Auth_Users",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "HrRelegion");

            migrationBuilder.DropTable(
                name: "Hr_EmployeeTypes");

            migrationBuilder.DropTable(
                name: "Hr_FingerPrints");

            migrationBuilder.DropTable(
                name: "Hr_Genders");

            migrationBuilder.DropTable(
                name: "Hr_Jobs");

            migrationBuilder.DropTable(
                name: "Hr_MaritalStatus");

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
                name: "Hr_Companies");

            migrationBuilder.DropTable(
                name: "Hr_VacationTypes");

            migrationBuilder.DropTable(
                name: "Hr_CompanyTypes");
        }
    }
}
