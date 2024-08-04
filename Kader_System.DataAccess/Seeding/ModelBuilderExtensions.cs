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
                CurrentTitleId=1,
                CompanyId="1",
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
           new() { Id = 5, Name = "حذف نهائى", NameInEnglish = ActionsEnums.ForceDelete.ToString() },
           new() { Id = 6, Name = "طباعة", NameInEnglish = ActionsEnums.Print.ToString() }
           );
        modelBuilder.Entity<StMainScreen>().HasData(
            new StMainScreen { Id = 1, Screen_main_title_ar = "الموارد البشريه", Screen_main_title_en = "Human Resources" }


        );
        modelBuilder.Entity<StMainScreenCat>().HasData(


         new StMainScreenCat { Id = 1, MainScreenId = 1, Screen_cat_title_ar = "الاعدادات", Screen_cat_title_en = "Setting" },
         new StMainScreenCat { Id = 2, MainScreenId = 1, Screen_cat_title_ar = "اكواد", Screen_cat_title_en = "Codes" },
         new StMainScreenCat { Id = 3, MainScreenId = 1, Screen_cat_title_ar = "طلبات", Screen_cat_title_en = "Request" },
         new StMainScreenCat { Id = 4, MainScreenId = 1, Screen_cat_title_ar = "حركات", Screen_cat_title_en = "Transcation" },
        new StMainScreenCat { Id = 5, MainScreenId = 1, Screen_cat_title_ar = "تقارير", Screen_cat_title_en = "Reports" },


         new StMainScreenCat { Id = 6, MainScreenId = 1, Screen_cat_title_ar = "توظيف", Screen_cat_title_en = "Hiring" },
         new StMainScreenCat { Id = 7, MainScreenId = 1, Screen_cat_title_ar = "الاعدادات HR", Screen_cat_title_en = "Hr Setting" }

      );

modelBuilder.Entity<StScreenSub>().HasData(

new StScreenSub { Id = 1, ScreenCatId = 1, Screen_sub_title_ar = "القائمة الرئيسية", Screen_sub_title_en = "Main Screen", Url = "/main/screen_main", ScreenCode="01001"},
new StScreenSub { Id = 2, ScreenCatId = 1, Screen_sub_title_ar = "القائمة الفرعية", Screen_sub_title_en = "Sub Screen", Url = "/main/screen_sub", ScreenCode = "01001" },
new StScreenSub { Id = 3, ScreenCatId = 1, Screen_sub_title_ar = "المسئوليات", Screen_sub_title_en = "Titles", Url = "/main/title", ScreenCode = "01001" },
new StScreenSub { Id = 4, ScreenCatId = 1, Screen_sub_title_ar = "مستخدمين", Screen_sub_title_en = "Users", Url = "/main/user", ScreenCode = "01001" },
new StScreenSub { Id = 5, ScreenCatId = 1, Screen_sub_title_ar = "صلاحيات المستخدمين", Screen_sub_title_en = "Users Privilige", Url = "/main/user_permission", ScreenCode = "01001" },
new StScreenSub { Id = 6, ScreenCatId = 1, Screen_sub_title_ar = "اعدادات", Screen_sub_title_en = "Setting", Url = "/main/settings/10", ScreenCode = "01001" },
new StScreenSub { Id = 7, ScreenCatId = 2, Screen_sub_title_ar = "شركات", Screen_sub_title_en = "Company", Url = "/codes/company", ScreenCode = "01001" },
new StScreenSub { Id = 8, ScreenCatId = 2, Screen_sub_title_ar = "الوظائف", Screen_sub_title_en = "Jobs", Url = "/codes/job", ScreenCode = "01001" },
new StScreenSub { Id = 9, ScreenCatId = 2, Screen_sub_title_ar = "المؤهلات", Screen_sub_title_en = "Qualifications", Url = "/codes/qualification", ScreenCode = "01001" },
new StScreenSub { Id = 10, ScreenCatId = 2, Screen_sub_title_ar = "الهيكل الاداري", Screen_sub_title_en = "Strucutred Mangements", Url = "/codes/admin_structure", ScreenCode = "01001" },
new StScreenSub { Id = 11, ScreenCatId = 2, Screen_sub_title_ar = "الاجازات", Screen_sub_title_en = "Vacations", Url = "/codes/vacation", ScreenCode = "01001" },
new StScreenSub { Id = 12, ScreenCatId = 2, Screen_sub_title_ar = "الموظفين", Screen_sub_title_en = "Employee", Url = "/codes/employee", ScreenCode = "01001" },
new StScreenSub { Id = 13, ScreenCatId = 2, Screen_sub_title_ar = "بدلات", Screen_sub_title_en = "Allowneces", Url = "/codes/allowance'", ScreenCode = "01001" },
new StScreenSub { Id = 14, ScreenCatId = 2, Screen_sub_title_ar = "استقطاعات", Screen_sub_title_en = "Deductions", Url = "/codes/deduction", ScreenCode = "01001" },
new StScreenSub { Id = 15, ScreenCatId = 2, Screen_sub_title_ar = "استحقاقات", Screen_sub_title_en = "Benefits", Url = "/codes/benefit", ScreenCode = "01001" },
new StScreenSub { Id = 16, ScreenCatId = 2, Screen_sub_title_ar = "العقود", Screen_sub_title_en = "Contracts", Url = "/codes/contract", ScreenCode = "01001" },
new StScreenSub { Id = 17, ScreenCatId = 2, Screen_sub_title_ar = "اجهزة البصمة", Screen_sub_title_en = "Print Devices", Url = "/codes/fingerprint", ScreenCode = "01001" },
new StScreenSub { Id = 18, ScreenCatId = 3, Screen_sub_title_ar = "متابعة الطلبات", Screen_sub_title_en = "Request tracking", Url = "/requests/follow_request", ScreenCode = "01001" },
new StScreenSub { Id = 19, ScreenCatId = 3, Screen_sub_title_ar = "طلب", Screen_sub_title_en = "Request", Url = "/requests/request", ScreenCode = "01001" },
new StScreenSub { Id = 20, ScreenCatId = 4, Screen_sub_title_ar = "الاستقطاعات", Screen_sub_title_en = "Vacations", Url = "/transactions/deduction_transaction'", ScreenCode = "01001" },
new StScreenSub { Id = 21, ScreenCatId = 4, Screen_sub_title_ar = "الاستحقاقات", Screen_sub_title_en = "Employee", Url = "/transactions/benefit_transaction", ScreenCode = "01001" },
new StScreenSub { Id = 22, ScreenCatId = 4, Screen_sub_title_ar = "الاجازات", Screen_sub_title_en = "Qualifications", Url = "/transactions/vacation_transaction", ScreenCode = "01001" },
new StScreenSub { Id = 23, ScreenCatId = 4, Screen_sub_title_ar = "البدلات", Screen_sub_title_en = "Allowneces", Url = "/transactions/loan_transaction", ScreenCode = "01001" },
new StScreenSub { Id = 24, ScreenCatId = 4, Screen_sub_title_ar = "العهد العينية", Screen_sub_title_en = "Deductions", Url = "/transactions/covenant_transaction", ScreenCode = "01001" },
new StScreenSub { Id = 25, ScreenCatId = 4, Screen_sub_title_ar = "حساب الرواتب", Screen_sub_title_en = "Benefits", Url = "/transactions/salary_calculator_transaction", ScreenCode = "01001" },
new StScreenSub { Id = 26, ScreenCatId = 4, Screen_sub_title_ar = "الصرف", Screen_sub_title_en = "Contracts", Url = "/transactions/disbursement_transaction", ScreenCode = "01001" },
new StScreenSub { Id = 27, ScreenCatId = 4, Screen_sub_title_ar = "زيادة المرتبات", Screen_sub_title_en = "Print Devices", Url = "/transactions/salary_increase", ScreenCode = "01001" }







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


        modelBuilder.Entity<StScreenAction>()
                 .HasData(
                    new StScreenAction() { Id = 1, ScreenId = 1, ActionId = 1 },
                    new StScreenAction() { Id = 2, ScreenId = 1, ActionId = 2 },
                    new StScreenAction() { Id = 3, ScreenId = 1, ActionId = 3 },
                    new StScreenAction() { Id = 4, ScreenId = 1, ActionId = 4 },
                    new StScreenAction() { Id = 5, ScreenId = 2, ActionId = 1 },
                    new StScreenAction() { Id = 6, ScreenId = 2, ActionId = 2 },
                    new StScreenAction() { Id = 7, ScreenId = 2, ActionId = 3 },
                    new StScreenAction() { Id = 8, ScreenId = 3, ActionId = 1 },
                    new StScreenAction() { Id = 9, ScreenId = 3, ActionId = 2 },
                    new StScreenAction() { Id = 10, ScreenId = 3, ActionId = 2 }


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
