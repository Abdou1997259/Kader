using Kader_System.Domain.DTOs.Response.HR;
using Kader_System.Domain.DTOs.Response.Trans;

namespace Kader_System.DataAccess.Repositories.Trans;

public class TransBenefitRepository(KaderDbContext context) : BaseRepository<TransBenefit>(context), ITransBenefitRepository
{

    public List<TransBenefitData> GetTransBenefitInfo(
       Expression<Func<TransBenefit, bool>> filter,
       Expression<Func<TransBenefitData, bool>> filterSearch,
       int? skip = null,
       int? take = null, string lang = "ar"
      )
    {

        var transBenefits = context.TransBenefits.Where(filter);


        var query = from trans in transBenefits
                    join employee in context.Employees on trans.EmployeeId equals employee.Id into empGroup
                    from employee in empGroup.DefaultIfEmpty()
                    join u in context.Users on trans.Added_by equals u.Id into userGroup
                    from u in userGroup.DefaultIfEmpty()
                    join benefit in context.Benefits on trans.BenefitId equals benefit.Id into benefitGroup
                    from benefit in benefitGroup.DefaultIfEmpty()
                    join salary in context.TransSalaryEffects on trans.SalaryEffectId equals salary.Id into salaryGroup
                    from salary in salaryGroup.DefaultIfEmpty()
                    join amountType in context.TransAmountTypes on trans.AmountTypeId equals amountType.Id into amountTypeGroup
                    from amountType in amountTypeGroup.DefaultIfEmpty()
            
                    select new TransBenefitData()
                    {
                       ActionMonth = trans.ActionMonth,
                       AddedBy = u.UserName,
                       AddedOn = trans.Add_date,
                       Amount = trans.Amount,
                       AmountTypeId = trans.AmountTypeId,
                       BenefitId = trans.BenefitId,
                       BenefitName = lang==Localization.Arabic? benefit.Name_ar:benefit.Name_en,
                       EmployeeId = trans.EmployeeId,
                       EmployeeName = lang == Localization.Arabic ? employee.FullNameAr : employee.FullNameEn,
                       Id = trans.Id,
                       Notes = trans.Notes,
                       SalaryEffect = lang == Localization.Arabic ? salary.Name : salary.NameInEnglish,
                       SalaryEffectId = trans.SalaryEffectId,
                       ValueTypeName = lang == Localization.Arabic ? amountType.Name : amountType.NameInEnglish,
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
