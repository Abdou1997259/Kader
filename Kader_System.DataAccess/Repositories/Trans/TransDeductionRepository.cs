using Kader_System.Domain.DTOs.Response.Trans;

namespace Kader_System.DataAccess.Repositories.Trans;

public class TransDeductionRepository(KaderDbContext context) : BaseRepository<TransDeduction>(context), ITransDeductionRepository
{
    public List<TransDeductionData> GetTransDeductionInfo(
      Expression<Func<TransDeduction, bool>> filter,
      Expression<Func<TransDeductionData, bool>> filterSearch,
      int? skip = null,
      int? take = null, string lang = "ar"
     )
    {

        var transBenefits = context.TransDeductions.Where(filter);
        var test = transBenefits.Count();

        var query = from trans in transBenefits
                    join employee in context.Employees on trans.employee_id equals employee.Id into empGroup
                    from employee in empGroup.DefaultIfEmpty()
                    join u in context.Users on trans.Added_by equals u.Id into userGroup
                    from u in userGroup.DefaultIfEmpty()
                    join benefit in context.Benefits on trans.deduction_id
                    equals benefit.Id into benefitGroup
                    from benefit in benefitGroup.DefaultIfEmpty()
                    join salary in context.TransSalaryEffects on trans.salary_effect_id
                    equals salary.Id into salaryGroup
                    from salary in salaryGroup.DefaultIfEmpty()
                    join amountType in context.TransAmountTypes on trans.amount_type_id equals amountType.Id into amountTypeGroup
                    from amountType in amountTypeGroup.DefaultIfEmpty()

                    select new TransDeductionData()
                    {
                        ActionMonth = trans.action_month,
                        AddedBy = u.UserName,
                        AddedOn = DateOnly.FromDateTime(trans.Add_date.Value),
                        Amount = trans.amount,
                        AmountTypeId = trans.amount_type_id,
                        DeductionId = trans.deduction_id,
                        DeductionName = lang == Localization.Arabic ? benefit.Name_ar : benefit.Name_en,
                        EmployeeId = trans.employee_id,
                        EmployeeName = lang == Localization.Arabic ? employee.FullNameAr : employee.FullNameEn,
                        Id = trans.id,
                        Notes = trans.notes,
                        SalaryEffect = lang ==
                        Localization.Arabic ? salary.Name : salary.NameInEnglish,
                        SalaryEffectId = trans.salary_effect_id,
                        DiscountType = lang == Localization.Arabic ?
                        amountType.Name : amountType.NameInEnglish,
                    };

        if (filterSearch != null)
            query = query.Where(filterSearch);

        if (skip.HasValue)
            query = query.Skip(skip.Value);
        if (take.HasValue)
            query = query.Take(take.Value);


        return query.ToList();

    }
}
