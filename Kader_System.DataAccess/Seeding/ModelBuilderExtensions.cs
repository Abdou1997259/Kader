using Kader_System.Domain.Constants.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Abstractions;
using static Kader_System.Domain.Constants.SD.ApiRoutes;
namespace Kader_System.DataAccess.Seeding;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        #region Users and their roles

        modelBuilder.Entity<ApplicationRole>()
            .HasData(
            new ApplicationRole()
            {
                Id = SuperAdmin.RoleId,
                Name = RolesEnums.Superadmin.ToString(),
                Title_name_ar = SuperAdmin.RoleNameInAr,
                ConcurrencyStamp = "1",
                NormalizedName = "SUPERADMIN"
            }, new ApplicationRole()
            {
                Id = UserRole.Id,
                Name = UserRole.RoleNameInEn,
                Title_name_ar = UserRole.RoleNameInAr,
                ConcurrencyStamp = "1",
                NormalizedName = UserRole.RoleNameInEn.ToUpper(),
                IsActive = true
            });

        modelBuilder.Entity<ApplicationUser>()
            .HasData(
            new ApplicationUser()
            {
                Id = SuperAdmin.Id,
                UserName = "admin",

                NormalizedUserName = "ADMIN",
                Email = "mohammed88@gmail.com",
                FullName = "Mohamed abdou",
                ImagePath = "/",
                TitleId = "1",
                PhoneNumber = "1202200",
                FinancialYear = 2013,
                CurrentCompanyId = 3,
                CurrentTitleId = 1,
                CompanyId = "1",
                NormalizedEmail = "MOHAMMED88@GMAIL.COM",
                EmailConfirmed = true,
                IsActive = true,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null!, SuperAdmin.Password),
                VisiblePassword = SuperAdmin.Password
            }
            );

        modelBuilder.Entity<ApplicationUserRole>()
            .HasData(
           new ApplicationUserRole()
           {
               RoleId = SuperAdmin.RoleId,
               UserId = SuperAdmin.Id
           });

        #endregion
        #region screen data

        //modelBuilder.Entity<StMainScreen>()
        //   .HasData(
        //      new StMainScreen() { Id = 1, Screen_main_title_ar = "شئون العاملين", Screen_main_title_en = "HR" }

        //  );


        //modelBuilder.Entity<StMainScreenCat>()
        //  .HasData(
        // new StMainScreenCat() { Id = 1, Screen_cat_title_ar = "الاعدادات", Screen_cat_title_en ="Setting" ,MainScreenId=1},
        // new StMainScreenCat() { Id = 2, Screen_cat_title_ar = "الاكواد", Screen_cat_title_en = "Codes", MainScreenId = 1 },
        // new StMainScreenCat() { Id = 3, Screen_cat_title_ar = "طلبات", Screen_cat_title_en ="Request", MainScreenId = 1 },
        // new StMainScreenCat() { Id = 4, Screen_cat_title_ar = "حركات", Screen_cat_title_en = "Transcation", MainScreenId = 1 },
        // new StMainScreenCat () { Id = 5, Screen_cat_title_ar = "التقارير", Screen_cat_title_en ="Reports", MainScreenId = 1 },
        // new StMainScreenCat() { Id = 6, Screen_cat_title_ar = "طباعة", Screen_cat_title_en = ActionsEnums.Print.ToString(), MainScreenId = 1 }
        // );
        #endregion
        modelBuilder.Entity<StAction>()
            .HasData(
           new() { Id = 1, Name = "إظهار", NameInEnglish = ActionsEnums.View.ToString() },
           new() { Id = 2, Name = "اضافة", NameInEnglish = ActionsEnums.Add.ToString() },
           new() { Id = 3, Name = "تعديل", NameInEnglish = ActionsEnums.Edit.ToString() },
           new() { Id = 4, Name = "حذف", NameInEnglish = ActionsEnums.Delete.ToString() },
           new() { Id = 5, Name = "طباعة", NameInEnglish = ActionsEnums.Print.ToString() }
           );
        modelBuilder.Entity<StMainScreen>().HasData(
            new StMainScreen { Id = 1, Screen_main_title_ar = "الموارد البشريه", Screen_main_title_en = "Human Resources",Order=1 }


        );
        modelBuilder.Entity<StMainScreenCat>().HasData(


         new StMainScreenCat { Id = 1, MainScreenId = 1, Screen_cat_title_ar = "الاعدادات", Screen_cat_title_en = "Setting", Order = 1 },
         new StMainScreenCat { Id = 2, MainScreenId = 1, Screen_cat_title_ar = "اكواد", Screen_cat_title_en = "Codes" , Order = 2 },
         new StMainScreenCat { Id = 3, MainScreenId = 1, Screen_cat_title_ar = "طلبات", Screen_cat_title_en = "Request" , Order = 3 },
         new StMainScreenCat { Id = 4, MainScreenId = 1, Screen_cat_title_ar = "حركات", Screen_cat_title_en = "Transcation" , Order = 4 },
        new StMainScreenCat { Id = 5, MainScreenId = 1, Screen_cat_title_ar = "تقارير", Screen_cat_title_en = "Reports" , Order = 5 },


         new StMainScreenCat { Id = 6, MainScreenId = 1, Screen_cat_title_ar = "توظيف", Screen_cat_title_en = "Hiring" , Order = 6 },
         new StMainScreenCat { Id = 7, MainScreenId = 1, Screen_cat_title_ar = "الاعدادات HR", Screen_cat_title_en = "Hr Setting"  , Order = 7 }

      );

        modelBuilder.Entity<StScreenSub>().HasData(


        new StScreenSub { Id = 1, ScreenCatId = 1, Screen_sub_title_ar = "القائمة الرئيسية", Screen_sub_title_en = "Main Screen", Url = "/main/screen_main", ScreenCode = "01001",Order=1 },
        new StScreenSub { Id = 2, ScreenCatId = 1, Screen_sub_title_ar = "القائمة الفرعية", Screen_sub_title_en = "Sub Cat", Url = "/main/screen_cat", ScreenCode = "01001", Order = 2 },
        new StScreenSub { Id = 3, ScreenCatId = 1, Screen_sub_title_ar = "الشاشات", Screen_sub_title_en = "Screen Sub", Url = "/main/screen_sub", ScreenCode = "01001", Order = 3 },

        new StScreenSub { Id = 4, ScreenCatId = 1, Screen_sub_title_ar = "المسئوليات", Screen_sub_title_en = "Titles", Url = "/main/title", ScreenCode = "01001", Order = 4 },
        new StScreenSub { Id = 5, ScreenCatId = 1, Screen_sub_title_ar = "مستخدمين", Screen_sub_title_en = "Users", Url = "/main/user", ScreenCode = "01001", Order = 5},
        new StScreenSub { Id = 6, ScreenCatId = 1, Screen_sub_title_ar = "صلاحيات المستخدمين", Screen_sub_title_en = "Users Privilege", Url = "/main/user_permission", ScreenCode = "01001", Order = 6 },

        new StScreenSub { Id = 7, ScreenCatId = 1, Screen_sub_title_ar = "اعدادات", Screen_sub_title_en = "Settings", Url = "/main/settings/10", ScreenCode = "01001" , Order = 7       },

        new StScreenSub { Id = 8, ScreenCatId = 2, Screen_sub_title_ar = "شركات", Screen_sub_title_en = "Company", Url = "/codes/company", ScreenCode = "01001",Order = 8 },
        new StScreenSub { Id = 9, ScreenCatId = 2, Screen_sub_title_ar = "الوظائف", Screen_sub_title_en = "Jobs", Url = "/codes/job", ScreenCode = "01001" , Order = 9 },
        new StScreenSub { Id = 10, ScreenCatId = 2, Screen_sub_title_ar = "المؤهلات", Screen_sub_title_en = "Qualifications", Url = "/codes/qualification", ScreenCode = "01001" , Order = 10 },
        new StScreenSub { Id = 11, ScreenCatId = 2, Screen_sub_title_ar = "الهيكل الاداري", Screen_sub_title_en = "Structured Managements", Url = "/codes/admin_structure", ScreenCode = "01001" , Order = 11 },
        new StScreenSub { Id = 12, ScreenCatId = 2, Screen_sub_title_ar = "الاجازات", Screen_sub_title_en = "Vacations", Url = "/codes/vacation", ScreenCode = "01001" , Order = 12 },
        new StScreenSub { Id = 13, ScreenCatId = 2, Screen_sub_title_ar = "الموظفين", Screen_sub_title_en = "Employees", Url = "/codes/employee", ScreenCode = "01001"  , Order = 13 },
        new StScreenSub { Id = 14, ScreenCatId = 2, Screen_sub_title_ar = "بدلات", Screen_sub_title_en = "Allowances", Url = "/codes/allowance", ScreenCode = "01001"   , Order = 14 },
        new StScreenSub { Id = 15, ScreenCatId = 2, Screen_sub_title_ar = "استقطاعات", Screen_sub_title_en = "Deductions", Url = "/codes/deduction", ScreenCode = "01001" , Order = 15 },
        new StScreenSub { Id = 16, ScreenCatId = 2, Screen_sub_title_ar = "استحقاقات", Screen_sub_title_en = "Benefits", Url = "/codes/benefit", ScreenCode = "01001"           , Order = 16 },
        new StScreenSub { Id = 17, ScreenCatId = 2, Screen_sub_title_ar = "العقود", Screen_sub_title_en = "Contracts", Url = "/codes/contract", ScreenCode = "01001"    , Order = 17 },
        new StScreenSub { Id = 18, ScreenCatId = 2, Screen_sub_title_ar = "اجهزة البصمة", Screen_sub_title_en = "Fingerprint Devices", Url = "/codes/fingerprint", ScreenCode = "01001"  , Order =18     },

        new StScreenSub { Id = 19, ScreenCatId = 3, Screen_sub_title_ar = "متابعة الطلبات", Screen_sub_title_en = "Request Tracking", Url = "/requests/follow_request", ScreenCode = "01001", Order = 19},
        new StScreenSub { Id = 20, ScreenCatId = 3, Screen_sub_title_ar = "طلب", Screen_sub_title_en = "Request", Url = "/requests/request", ScreenCode = "01001", Order = 20 },

        new StScreenSub { Id = 21, ScreenCatId = 4, Screen_sub_title_ar = "الاستقطاعات", Screen_sub_title_en = "Deductions", Url = "/transactions/deduction_transaction", ScreenCode = "01001" , Order = 21 },
        new StScreenSub { Id = 22, ScreenCatId = 4, Screen_sub_title_ar = "الاستحقاقات", Screen_sub_title_en = "Benefits", Url = "/transactions/benefit_transaction", ScreenCode = "01001" , Order = 22 },
        new StScreenSub { Id = 23, ScreenCatId = 4, Screen_sub_title_ar = "السلف", Screen_sub_title_en = "Loans", Url = "/transactions/loan_transaction", ScreenCode = "01001" , Order = 23 },
        new StScreenSub { Id = 24, ScreenCatId = 4, Screen_sub_title_ar = "البدلات", Screen_sub_title_en = "Allowances Transactions", Url = "/transactions/allowance_transaction", ScreenCode = "01001"     , Order = 24 },
        new StScreenSub { Id = 25, ScreenCatId = 4, Screen_sub_title_ar = "الاجازات", Screen_sub_title_en = "Vacations Transactions", Url = "/transactions/vacation_transaction", ScreenCode = "01001" , Order = 25 },
        new StScreenSub { Id = 26, ScreenCatId = 4, Screen_sub_title_ar = "حساب الرواتب", Screen_sub_title_en = "Salary Calculator", Url = "/transactions/salary_calculator_transaction", ScreenCode = "01001"      , Order = 26 },
        new StScreenSub { Id = 27, ScreenCatId = 4, Screen_sub_title_ar = "الصرف", Screen_sub_title_en = "Disbursement", Url = "/transactions/disbursement_transaction", ScreenCode = "01001" , Order = 27 },
        new StScreenSub { Id = 28, ScreenCatId = 4, Screen_sub_title_ar = "زيادة المرتبات", Screen_sub_title_en = "Salary Increase", Url = "/transactions/salary_increase", ScreenCode = "01001" , Order = 28 },
        new StScreenSub { Id = 29, ScreenCatId = 4, Screen_sub_title_ar = "العهد العينية", Screen_sub_title_en = "Covenants", Url = "/transactions/covenant_transaction", ScreenCode = "01001"  , Order = 29 },

        new StScreenSub { Id = 30, ScreenCatId = 2, Screen_sub_title_ar = "الدوام", Screen_sub_title_en = "Shifts", Url = "/codes/shift", ScreenCode = "01001"  , Order = 30 }





             );





        modelBuilder.Entity<StSubMainScreenAction>().HasData(
    new StSubMainScreenAction { Id = 1, ActionId = 1, ScreenSubId = 1 },
    new StSubMainScreenAction { Id = 2, ActionId = 2, ScreenSubId = 1 },
    new StSubMainScreenAction { Id = 3, ActionId = 3, ScreenSubId = 1 },
    new StSubMainScreenAction { Id = 4, ActionId = 4, ScreenSubId = 1 },
    new StSubMainScreenAction { Id = 5, ActionId = 5, ScreenSubId = 1 },
    new StSubMainScreenAction { Id = 6, ActionId = 1, ScreenSubId = 2 },
    new StSubMainScreenAction { Id = 7, ActionId = 2, ScreenSubId = 2 },
    new StSubMainScreenAction { Id = 8, ActionId = 3, ScreenSubId = 2 },
    new StSubMainScreenAction { Id = 9, ActionId = 4, ScreenSubId = 2 },
    new StSubMainScreenAction { Id = 10, ActionId = 5, ScreenSubId = 2 },
    new StSubMainScreenAction { Id = 11, ActionId = 1, ScreenSubId = 3 },
    new StSubMainScreenAction { Id = 12, ActionId = 2, ScreenSubId = 3 },
    new StSubMainScreenAction { Id = 13, ActionId = 3, ScreenSubId = 3 },
    new StSubMainScreenAction { Id = 14, ActionId = 4, ScreenSubId = 3 },
    new StSubMainScreenAction { Id = 15, ActionId = 5, ScreenSubId = 3 },
    new StSubMainScreenAction { Id = 16, ActionId = 1, ScreenSubId = 4 },
    new StSubMainScreenAction { Id = 17, ActionId = 2, ScreenSubId = 4 },
    new StSubMainScreenAction { Id = 18, ActionId = 3, ScreenSubId = 4 },
    new StSubMainScreenAction { Id = 19, ActionId = 4, ScreenSubId = 4 },
    new StSubMainScreenAction { Id = 20, ActionId = 5, ScreenSubId = 4 },
    new StSubMainScreenAction { Id = 21, ActionId = 1, ScreenSubId = 5 },
    new StSubMainScreenAction { Id = 22, ActionId = 2, ScreenSubId = 5 },
    new StSubMainScreenAction { Id = 23, ActionId = 3, ScreenSubId = 5 },
    new StSubMainScreenAction { Id = 24, ActionId = 4, ScreenSubId = 5 },
    new StSubMainScreenAction { Id = 25, ActionId = 5, ScreenSubId = 5 },
    new StSubMainScreenAction { Id = 26, ActionId = 1, ScreenSubId = 6 },
    new StSubMainScreenAction { Id = 27, ActionId = 2, ScreenSubId = 6 },
    new StSubMainScreenAction { Id = 28, ActionId = 3, ScreenSubId = 6 },
    new StSubMainScreenAction { Id = 29, ActionId = 4, ScreenSubId = 6 },
    new StSubMainScreenAction { Id = 30, ActionId = 5, ScreenSubId = 6 },
    new StSubMainScreenAction { Id = 31, ActionId = 1, ScreenSubId = 7 },
    new StSubMainScreenAction { Id = 32, ActionId = 2, ScreenSubId = 7 },
    new StSubMainScreenAction { Id = 33, ActionId = 3, ScreenSubId = 7 },
    new StSubMainScreenAction { Id = 34, ActionId = 4, ScreenSubId = 7 },
    new StSubMainScreenAction { Id = 35, ActionId = 5, ScreenSubId = 7 },
    new StSubMainScreenAction { Id = 36, ActionId = 1, ScreenSubId = 8 },
    new StSubMainScreenAction { Id = 37, ActionId = 2, ScreenSubId = 8 },
    new StSubMainScreenAction { Id = 38, ActionId = 3, ScreenSubId = 8 },
    new StSubMainScreenAction { Id = 39, ActionId = 4, ScreenSubId = 8 },
    new StSubMainScreenAction { Id = 40, ActionId = 5, ScreenSubId = 8 },
    new StSubMainScreenAction { Id = 41, ActionId = 1, ScreenSubId = 9 },
    new StSubMainScreenAction { Id = 42, ActionId = 2, ScreenSubId = 9 },
    new StSubMainScreenAction { Id = 43, ActionId = 3, ScreenSubId = 9 },
    new StSubMainScreenAction { Id = 44, ActionId = 4, ScreenSubId = 9 },
    new StSubMainScreenAction { Id = 45, ActionId = 5, ScreenSubId = 9 },
    new StSubMainScreenAction { Id = 46, ActionId = 1, ScreenSubId = 10 },
    new StSubMainScreenAction { Id = 47, ActionId = 2, ScreenSubId = 10 },
    new StSubMainScreenAction { Id = 48, ActionId = 3, ScreenSubId = 10 },
    new StSubMainScreenAction { Id = 49, ActionId = 4, ScreenSubId = 10 },
    new StSubMainScreenAction { Id = 50, ActionId = 5, ScreenSubId = 10 },
    new StSubMainScreenAction { Id = 51, ActionId = 1, ScreenSubId = 11 },
    new StSubMainScreenAction { Id = 52, ActionId = 2, ScreenSubId = 11 },
    new StSubMainScreenAction { Id = 53, ActionId = 3, ScreenSubId = 11 },
    new StSubMainScreenAction { Id = 54, ActionId = 4, ScreenSubId = 11 },
    new StSubMainScreenAction { Id = 55, ActionId = 5, ScreenSubId = 11 },
    new StSubMainScreenAction { Id = 56, ActionId = 1, ScreenSubId = 12 },
    new StSubMainScreenAction { Id = 57, ActionId = 2, ScreenSubId = 12 },
    new StSubMainScreenAction { Id = 58, ActionId = 3, ScreenSubId = 12 },
    new StSubMainScreenAction { Id = 59, ActionId = 4, ScreenSubId = 12 },
    new StSubMainScreenAction { Id = 60, ActionId = 5, ScreenSubId = 12 },
    new StSubMainScreenAction { Id = 61, ActionId = 1, ScreenSubId = 13 },
    new StSubMainScreenAction { Id = 62, ActionId = 2, ScreenSubId = 13 },
    new StSubMainScreenAction { Id = 63, ActionId = 3, ScreenSubId = 13 },
    new StSubMainScreenAction { Id = 64, ActionId = 4, ScreenSubId = 13 },
    new StSubMainScreenAction { Id = 65, ActionId = 5, ScreenSubId = 13 },
    new StSubMainScreenAction { Id = 66, ActionId = 1, ScreenSubId = 14 },
    new StSubMainScreenAction { Id = 67, ActionId = 2, ScreenSubId = 14 },
    new StSubMainScreenAction { Id = 68, ActionId = 3, ScreenSubId = 14 },
    new StSubMainScreenAction { Id = 69, ActionId = 4, ScreenSubId = 14 },
    new StSubMainScreenAction { Id = 70, ActionId = 5, ScreenSubId = 14 },
    new StSubMainScreenAction { Id = 71, ActionId = 1, ScreenSubId = 15 },
    new StSubMainScreenAction { Id = 72, ActionId = 2, ScreenSubId = 15 },
    new StSubMainScreenAction { Id = 73, ActionId = 3, ScreenSubId = 15 },
    new StSubMainScreenAction { Id = 74, ActionId = 4, ScreenSubId = 15 },
    new StSubMainScreenAction { Id = 75, ActionId = 5, ScreenSubId = 15 },
    new StSubMainScreenAction { Id = 76, ActionId = 1, ScreenSubId = 16 },
    new StSubMainScreenAction { Id = 77, ActionId = 2, ScreenSubId = 16 },
    new StSubMainScreenAction { Id = 78, ActionId = 3, ScreenSubId = 16 },
    new StSubMainScreenAction { Id = 79, ActionId = 4, ScreenSubId = 16 },
    new StSubMainScreenAction { Id = 80, ActionId = 5, ScreenSubId = 16 },
    new StSubMainScreenAction { Id = 81, ActionId = 1, ScreenSubId = 17 },
    new StSubMainScreenAction { Id = 82, ActionId = 2, ScreenSubId = 17 },
    new StSubMainScreenAction { Id = 83, ActionId = 3, ScreenSubId = 17 },
    new StSubMainScreenAction { Id = 84, ActionId = 4, ScreenSubId = 17 },
    new StSubMainScreenAction { Id = 85, ActionId = 5, ScreenSubId = 17 },
    new StSubMainScreenAction { Id = 86, ActionId = 1, ScreenSubId = 18 },
    new StSubMainScreenAction { Id = 87, ActionId = 2, ScreenSubId = 18 },
    new StSubMainScreenAction { Id = 88, ActionId = 3, ScreenSubId = 18 },
    new StSubMainScreenAction { Id = 89, ActionId = 4, ScreenSubId = 18 },
    new StSubMainScreenAction { Id = 90, ActionId = 5, ScreenSubId = 18 },
    new StSubMainScreenAction { Id = 91, ActionId = 1, ScreenSubId = 19 },
    new StSubMainScreenAction { Id = 92, ActionId = 2, ScreenSubId = 19 },
    new StSubMainScreenAction { Id = 93, ActionId = 3, ScreenSubId = 19 },
    new StSubMainScreenAction { Id = 94, ActionId = 4, ScreenSubId = 19 },
    new StSubMainScreenAction { Id = 95, ActionId = 5, ScreenSubId = 19 },
    new StSubMainScreenAction { Id = 96, ActionId = 1, ScreenSubId = 20 },
    new StSubMainScreenAction { Id = 97, ActionId = 2, ScreenSubId = 20 },
    new StSubMainScreenAction { Id = 98, ActionId = 3, ScreenSubId = 20 },
    new StSubMainScreenAction { Id = 99, ActionId = 4, ScreenSubId = 20 },
    new StSubMainScreenAction { Id = 100, ActionId = 5, ScreenSubId = 20 },
    new StSubMainScreenAction { Id = 101, ActionId = 1, ScreenSubId = 21 },
    new StSubMainScreenAction { Id = 102, ActionId = 2, ScreenSubId = 21 },
    new StSubMainScreenAction { Id = 103, ActionId = 3, ScreenSubId = 21 },
    new StSubMainScreenAction { Id = 104, ActionId = 4, ScreenSubId = 21 },
    new StSubMainScreenAction { Id = 105, ActionId = 5, ScreenSubId = 21 },
    new StSubMainScreenAction { Id = 106, ActionId = 1, ScreenSubId = 22 },
    new StSubMainScreenAction { Id = 107, ActionId = 2, ScreenSubId = 22 },
    new StSubMainScreenAction { Id = 108, ActionId = 3, ScreenSubId = 22 },
    new StSubMainScreenAction { Id = 109, ActionId = 4, ScreenSubId = 22 },
    new StSubMainScreenAction { Id = 110, ActionId = 5, ScreenSubId = 22 },
    new StSubMainScreenAction { Id = 111, ActionId = 1, ScreenSubId = 23 },
    new StSubMainScreenAction { Id = 112, ActionId = 2, ScreenSubId = 23 },
    new StSubMainScreenAction { Id = 113, ActionId = 3, ScreenSubId = 23 },
    new StSubMainScreenAction { Id = 114, ActionId = 4, ScreenSubId = 23 },
    new StSubMainScreenAction { Id = 115, ActionId = 5, ScreenSubId = 23 },
    new StSubMainScreenAction { Id = 116, ActionId = 1, ScreenSubId = 24 },
    new StSubMainScreenAction { Id = 117, ActionId = 2, ScreenSubId = 24 },
    new StSubMainScreenAction { Id = 118, ActionId = 3, ScreenSubId = 24 },
    new StSubMainScreenAction { Id = 119, ActionId = 4, ScreenSubId = 24 },
    new StSubMainScreenAction { Id = 120, ActionId = 5, ScreenSubId = 24 },
    new StSubMainScreenAction { Id = 121, ActionId = 1, ScreenSubId = 25 },
    new StSubMainScreenAction { Id = 122, ActionId = 2, ScreenSubId = 25 },
    new StSubMainScreenAction { Id = 123, ActionId = 3, ScreenSubId = 25 },
    new StSubMainScreenAction { Id = 124, ActionId = 4, ScreenSubId = 25 },
    new StSubMainScreenAction { Id = 125, ActionId = 5, ScreenSubId = 25 },
    new StSubMainScreenAction { Id = 126, ActionId = 1, ScreenSubId = 26 },
    new StSubMainScreenAction { Id = 127, ActionId = 2, ScreenSubId = 26 },
    new StSubMainScreenAction { Id = 128, ActionId = 3, ScreenSubId = 26 },
    new StSubMainScreenAction { Id = 129, ActionId = 4, ScreenSubId = 26 },
    new StSubMainScreenAction { Id = 130, ActionId = 5, ScreenSubId = 26 },
    new StSubMainScreenAction { Id = 131, ActionId = 1, ScreenSubId = 27 },
    new StSubMainScreenAction { Id = 132, ActionId = 2, ScreenSubId = 27 },
    new StSubMainScreenAction { Id = 133, ActionId = 3, ScreenSubId = 27 },
    new StSubMainScreenAction { Id = 134, ActionId = 4, ScreenSubId = 27 },
    new StSubMainScreenAction { Id = 135, ActionId = 5, ScreenSubId = 27 },
    new StSubMainScreenAction { Id = 136, ActionId = 1, ScreenSubId = 28 },
    new StSubMainScreenAction { Id = 137, ActionId = 2, ScreenSubId = 28 },
    new StSubMainScreenAction { Id = 138, ActionId = 3, ScreenSubId = 28 },
    new StSubMainScreenAction { Id = 139, ActionId = 4, ScreenSubId = 28 },
    new StSubMainScreenAction { Id = 140, ActionId = 5, ScreenSubId = 28 },
    new StSubMainScreenAction { Id = 141, ActionId = 1, ScreenSubId = 29 },
    new StSubMainScreenAction { Id = 142, ActionId = 2, ScreenSubId = 29 },
    new StSubMainScreenAction { Id = 143, ActionId = 3, ScreenSubId = 29 },
    new StSubMainScreenAction { Id = 144, ActionId = 4, ScreenSubId = 29 },
    new StSubMainScreenAction { Id = 145, ActionId = 5, ScreenSubId = 29 },
    new StSubMainScreenAction { Id = 146, ActionId = 1, ScreenSubId = 30 },
    new StSubMainScreenAction { Id = 147, ActionId = 2, ScreenSubId = 30 },
    new StSubMainScreenAction { Id = 148, ActionId = 3, ScreenSubId = 30 },
    new StSubMainScreenAction { Id = 149, ActionId = 4, ScreenSubId = 30 },
    new StSubMainScreenAction { Id = 150, ActionId = 5, ScreenSubId = 30 }
);


        modelBuilder.Entity<Kader_System.Domain.Models.Title>().HasData(
                  new Kader_System.Domain.Models.Title { Id = 1, TitleNameAr = "مدير عام", TitleNameEn = "maanger" }

            );


        modelBuilder.Entity<TitlePermission>().HasData(
                  new TitlePermission { Id = 1, TitleId = 1, Permissions = "1,2,3", SubScreenId = 4 }

            );

        modelBuilder.Entity<HrVacationType>()
            .HasData(
           new() { Id = 1, Name = "عام كامل", NameInEnglish = "Full year" },
           new() { Id = 2, Name = "من تاريخ التعيين", NameInEnglish = "From hiring date" },
           new() { Id = 3, Name = "من تاريخ الاستحقاق", NameInEnglish = "After hiring days" }
           );

        modelBuilder.Entity<HrSalaryCalculator>()
            .HasData(
           new() { Id = 1, Name = "كل الاتب", NameInEnglish = "All salary" },
           new() { Id = 2, Name = "الراتب الرئيسى", NameInEnglish = "Main salary" },
           new() { Id = 3, Name = "بدون راتب", NameInEnglish = "Without salary" }
           );

        modelBuilder.Entity<HrCompanyType>()
            .HasData(
           new() { Id = 1, Name = "شركة", NameInEnglish = "Company" },
           new() { Id = 2, Name = "مؤسسة", NameInEnglish = "Corporate" }
           );

        modelBuilder.Entity<HrGender>()
            .HasData(
            new() { Id = 1, Name = "ذكر", NameInEnglish = "Male" },
            new() { Id = 2, Name = "أنثى", NameInEnglish = "Female" }
           );

        modelBuilder.Entity<HrNationality>()
            .HasData(
            new() { Id = 1, Name = "مصرى", NameInEnglish = "Egyptian " },
            new() { Id = 2, Name = "سعودى", NameInEnglish = "Saudian" }
           );

        modelBuilder.Entity<HrMilitaryStatus>()
            .HasData(
                new() { Id = 1, Name = "معفى", NameInEnglish = "Exempt" },
                new() { Id = 2, Name = "مؤجل", NameInEnglish = "Delayed" },
                new() { Id = 3, Name = "انهى الخدمة", NameInEnglish = "Completed" }
            );

        modelBuilder.Entity<HrMaritalStatus>()
            .HasData(
                new() { Id = 1, Name = "أعزب", NameInEnglish = "Single" },
                new() { Id = 2, Name = "خاطب", NameInEnglish = "Engaged" },
                new() { Id = 3, Name = "متزوج", NameInEnglish = "Married" },
                new() { Id = 4, Name = "مطللق", NameInEnglish = "Divorced" }
            );

        modelBuilder.Entity<HrRelegion>()
            .HasData(
                new() { Id = 1, Name = "مسلم", NameInEnglish = "Muslim" },
                new() { Id = 2, Name = "مسيحى", NameInEnglish = "Christian" },
                new() { Id = 3, Name = "غير ذلك", NameInEnglish = "Otherwise" }
            );

        modelBuilder.Entity<HrSalaryPaymentWay>()
            .HasData(
                new() { Id = 1, Name = "بنكى", NameInEnglish = "Bank" },
                new() { Id = 2, Name = "نقدى", NameInEnglish = "Cash" },
                new() { Id = 3, Name = "حوالة مالية", NameInEnglish = "Money transfer" }
            );

        modelBuilder.Entity<HrEmployeeType>()
            .HasData(
                new() { Id = 1, Name = "مقيم", NameInEnglish = "Resident" },
                new() { Id = 2, Name = "مواطن", NameInEnglish = "Citizen" }
           );
        modelBuilder.Entity<AdvancedType>()
         .HasData(
             new() { Id = 1, AdvanceTypeName = "سلفة" },
             new() { Id = 2, AdvanceTypeName = "مخالفة مرورية" },
             new() { Id = 3, AdvanceTypeName = "حوادث وأصلاحات" },
             new() { Id = 4, AdvanceTypeName = "نقل خدمات" },
             new() { Id = 5, AdvanceTypeName = "مخالفة مرورية" },
             new() { Id = 6, AdvanceTypeName = "تأمينات" },
             new() { Id = 7, AdvanceTypeName = "تسويات اخري" }



        );

        modelBuilder.Entity<HrValueType>()
            .HasData(
                new() { Id = 1, Name = "مبلغ", NameInEnglish = "Percent" },
                new() { Id = 2, Name = "نسبة", NameInEnglish = "Amount" }
           );

        modelBuilder.Entity<TransSalaryEffect>()
            .HasData(
                new() { Id = 1, Name = "قطعى", NameInEnglish = "On time" },
                new() { Id = 2, Name = "شهرى", NameInEnglish = "Monthly" }
           );

        modelBuilder.Entity<TransAmountType>()
            .HasData(
                new() { Id = 1, Name = "ساعة", NameInEnglish = "Hour" },
                new() { Id = 2, Name = "أيام عمل", NameInEnglish = "Work days" },
                new() { Id = 3, Name = "القيمة", NameInEnglish = "Value" }
            );


        modelBuilder.Entity<UserPermission>().HasData(
         new UserPermission { Id = 1, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 1, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 2, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 2, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 3, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 3, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 4, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 4, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 5, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 5, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 6, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 6, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 7, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 7, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 8, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 8, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 9, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 9, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 10, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 10, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 11, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 11, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 12, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 12, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 13, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 13, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 14, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 14, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 15, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 15, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 16, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 16, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 17, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 17, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 18, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 18, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 19, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 19, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 20, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 20, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 21, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 21, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 22, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 22, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 23, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 23, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 24, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 24, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 25, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 25, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 26, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 26, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 27, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 27, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 28, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 28, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 29, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 29, Permission = "1,2,3,4,5" },
         new UserPermission { Id = 30, UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", TitleId = 1, SubScreenId = 30, Permission = "1,2,3,4,5" }
     );

        modelBuilder.Entity<HrCompany>().HasData(
            new HrCompany { Id = 3, NameEn = "Kader", NameAr = "كادر", CompanyOwner = "Sallem", Company_licenses = "dad56ad1323", Company_licenses_extension = "234adad", CompanyTypeId = 1, IsDeleted = false, IsActive = true }
            );

        modelBuilder.Entity<HrManagement>().HasData(
            new HrManagement { Id = 4, NameEn = "Programming Mangement", NameAr = "ادارة البرمجة", CompanyId = 3, IsDeleted = false, IsActive = true }
            );
        modelBuilder.Entity<HrDepartment>().HasData(
            new HrDepartment { Id = 5, NameEn = "ASP Dep", NameAr = "ASP قسم ال", ManagementId = 4, IsDeleted = false, IsActive = true }
            );
        modelBuilder.Entity<HrShift>().HasData(
            new HrShift { Id = 1,Name_ar = "شيفت مسائى",Name_en = "evening shifts",IsDeleted = false,IsActive = true} 
            );
        modelBuilder.Entity<HrQualification>().HasData(
            new HrQualification { Id = 1,NameAr = "بكالريوس حاسبات ومعلومات",NameEn = "Bachelor's degree in computers and information", IsDeleted = false,IsActive = true} 
            );
        modelBuilder.Entity<HrVacation>().HasData(
           new HrVacation { Id = 1, NameAr = "اجازة", NameEn = "Vacation",VacationTypeId = 1,ApplyAfterMonth = 1,TotalBalance = 3000,CanTransfer = true, IsDeleted = false, IsActive = true }
           ); 
        modelBuilder.Entity<HrJob>().HasData(
           new HrJob { Id = 1, NameAr = "وظيفة", NameEn = "job",HasAdditionalTime = false,HasNeedLicense = false, IsDeleted = false, IsActive = true }
           );
        modelBuilder.Entity<HrEmployee>().HasData(
               new HrEmployee
               {
                   Id = 1,
                   FirstNameAr = "أحمد",
                   FirstNameEn = "Ahmed",
                   FatherNameAr = "محمد",
                   FatherNameEn = "Mohammed",
                   GrandFatherNameAr = "علي",
                   GrandFatherNameEn = "Ali",
                   FamilyNameAr = "السعود",
                   FamilyNameEn = "Al-Saud",
                   AccommodationAllowance = 1500.00,
                   MaritalStatusId = 2, 
                   Address = "Riyadh, Saudi Arabia",
                   FixedSalary = 8000.00,
                   HiringDate = new DateOnly(2020, 01, 01),
                   ImmediatelyDate = new DateOnly(2020, 01, 02),
                   IsActive = true,
                   TotalSalary = 9500.00,
                   GenderId = 1,
                   BirthDate = new DateOnly(1990, 05, 20),
                   ReligionId = 1, 
                   Phone = "0551234567",
                   Email = "ahmed@example.com",
                   NationalId = "1234567890",
                   JobNumber = "E1234",
                   SalaryPaymentWayId = 1, 
                   UserId = "b74ddd14-6340-4840-95c2-db12554843e5basb1", 
                   ChildrenNumber = 2,
                   ShiftId = 1,
                   CompanyId = 3, 
                   FingerPrintCode = "FP1234",
                   EmployeeImage = "image.png",
                   EmployeeImageExtension = ".png",
                   ManagementId = 4, 
                   DepartmentId = 5,
                   NationalityId = 1, 
                   QualificationId = 1, 
                   VacationId = 1, 
                   JobId = 1, 
                   EmployeeTypeId = 1, 
                   AccountNo = 1234567890,
                   Note = "No notes",
               }
           );
    }

    public static void AddQueryFilterToAllEntitiesAssignableFrom<T>(this ModelBuilder modelBuilder,
     Expression<Func<T, bool>> expression)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(T).IsAssignableFrom(entityType.ClrType))
                continue;

            var parameterType = Expression.Parameter(entityType.ClrType);
            var expressionFilter = ReplacingExpressionVisitor.Replace(
                expression.Parameters.Single(), parameterType, expression.Body);

            var currentQueryFilter = entityType.GetQueryFilter();
            if (currentQueryFilter != null)
            {
                var currentExpressionFilter = ReplacingExpressionVisitor.Replace(
                    currentQueryFilter.Parameters.Single(), parameterType, currentQueryFilter.Body);
                expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
            }

            var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
            entityType.SetQueryFilter(lambdaExpression);
        }
    }
}
