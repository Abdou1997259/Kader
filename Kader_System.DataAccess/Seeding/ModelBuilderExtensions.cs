using Kader_System.Domain.Constants.Enums;
using Kader_System.Domain.Models.HR;
using Kader_System.Domain.Models.Setting;
using Kader_System.Domain.Models.Trans;

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
            },new ApplicationRole()
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

        modelBuilder.Entity<StAction>()
            .HasData(
           new() { Id = 1, Name = "إظهار", NameInEnglish = ActionsEnums.View.ToString()},
           new() { Id = 2, Name = "اضافة", NameInEnglish = ActionsEnums.Add.ToString()},
           new() { Id = 3, Name = "تعديل", NameInEnglish = ActionsEnums.Edit.ToString()},
           new() { Id = 4, Name = "حذف", NameInEnglish = ActionsEnums.Delete.ToString()},
           new() { Id = 5, Name = "حذف نهائى", NameInEnglish = ActionsEnums.ForceDelete.ToString()},   
           new() { Id = 6, Name = "طباعة", NameInEnglish = ActionsEnums.Print.ToString()}
           );

        modelBuilder.Entity<HrVacationType>()
            .HasData(
           new() { Id = 1, Name = "عام كامل", NameInEnglish = "Full year"},
           new() { Id = 2, Name = "من تاريخ التعيين", NameInEnglish = "From hiring date"},
           new() { Id = 3, Name = "من تاريخ الاستحقاق", NameInEnglish = "After hiring days"}
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
