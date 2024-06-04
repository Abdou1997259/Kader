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


        var query = from trans in transBenefits
                    join employee in context.Employees on trans.EmployeeId equals employee.Id into empGroup
                    from employee in empGroup.DefaultIfEmpty()
                    join u in context.Users on trans.Added_by equals u.Id into userGroup
                    from u in userGroup.DefaultIfEmpty()
                    join benefit in context.Benefits on trans.DeductionId equals benefit.Id into benefitGroup
                    from benefit in benefitGroup.DefaultIfEmpty()
                    join salary in context.TransSalaryEffects on trans.SalaryEffectId equals salary.Id into salaryGroup
                    from salary in salaryGroup.DefaultIfEmpty()
                    join amountType in context.TransAmountTypes on trans.AmountTypeId equals amountType.Id into amountTypeGroup
                    from amountType in amountTypeGroup.DefaultIfEmpty()

                    select new TransDeductionData()
                    {
                        ActionMonth = trans.ActionMonth,
                        AddedBy = u.UserName,
                        AddedOn = trans.Add_date,
                        Amount = trans.Amount,
                        AmountTypeId = trans.AmountTypeId,
                        DeductionId = trans.DeductionId,
                        DeductionName = lang == Localization.Arabic ? benefit.Name_ar : benefit.Name_en,
                        EmployeeId = trans.EmployeeId,
                        EmployeeName = lang == Localization.Arabic ? employee.FullNameAr : employee.FullNameEn,
                        Id = trans.Id,
                        Notes = trans.Notes,
                        SalaryEffect = lang == Localization.Arabic ? salary.Name : salary.NameInEnglish,
                        SalaryEffectId = trans.SalaryEffectId,
                        DiscountType = lang == Localization.Arabic ? amountType.Name : amountType.NameInEnglish,
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
