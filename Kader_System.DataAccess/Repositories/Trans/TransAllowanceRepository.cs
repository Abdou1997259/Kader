using Kader_System.Domain.DTOs.Response.Trans;

namespace Kader_System.DataAccess.Repositories.Trans;

public class TransAllowanceRepository(KaderDbContext context) : BaseRepository<TransAllowance>(context), ITransAllowanceRepository
{

    public List<TransAllowanceData> GetTransAllowanceInfo(
     Expression<Func<TransAllowance, bool>> filter,
     Expression<Func<TransAllowanceData, bool>> filterSearch,
     int? skip = null,
     int? take = null, string lang = "ar"
    )
    {

        var query = from trans in context.TransAllowances.Where(filter)
                    join employee in context.Employees on trans.EmployeeId equals employee.Id into empGroup
                    from employee in empGroup.DefaultIfEmpty()
                    join u in context.Users on trans.Added_by equals u.Id into userGroup
                    from u in userGroup.DefaultIfEmpty()
                    join allowance in context.Allowances on trans.AllowanceId equals allowance.Id into benefitGroup
                    from allowance in benefitGroup.DefaultIfEmpty()
                    join salary in context.TransSalaryEffects on trans.SalaryEffectId equals salary.Id into salaryGroup
                    from salary in salaryGroup.DefaultIfEmpty()
                    
                    select new TransAllowanceData()
                    {
                        ActionMonth = trans.ActionMonth,
                        AddedBy = u.UserName,
                        AddedOn = trans.Add_date,
                        Amount = trans.Amount,
                        AllowanceId = trans.AllowanceId,
                        AllowanceName = lang == Localization.Arabic ? allowance.Name_ar : allowance.Name_en,
                        EmployeeId = trans.EmployeeId,
                        EmployeeName = lang == Localization.Arabic ? employee.FullNameAr : employee.FullNameEn,
                        Id = trans.Id,
                        Notes = trans.Notes,
                        SalaryEffect = lang == Localization.Arabic ? salary.Name : salary.NameInEnglish,
                        SalaryEffectId = trans.SalaryEffectId,
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
