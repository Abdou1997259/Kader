
using Kader_System.Domain.DTOs.Response.HR;
using Microsoft.EntityFrameworkCore.Internal;

namespace Kader_System.DataAccess.Repositories.HR;

public class VacationRepository(KaderDbContext context) : BaseRepository<HrVacation>(context), IVacationRepository

{


    public List<VacationData> GetVacationInfo(
        Expression<Func<HrVacation, bool>> vacationFilter,
    int? skip = null,
    int? take = null 
        ,string lang="ar")
    {
        var query = from vac in context.Vacations.Where(vacationFilter)
            join vacationType in context.VacationTypes on vac.VacationTypeId equals vacationType.Id into vtGroup
            from vt in vtGroup.DefaultIfEmpty()
            join user in context.Users on vac.Added_by equals user.Id into userGroup
            from u in userGroup.DefaultIfEmpty()
            join employeeCount in context.Employees.GroupBy(e => e.VacationId).Select(g => new { VacationId = g.Key, Count = g.Count() })
                on vac.Id equals employeeCount.VacationId into ecGroup
            from ec in ecGroup.DefaultIfEmpty()
            select new VacationData()
            {
                Id = vac.Id,
                Name = lang == Localization.Arabic ? vac.NameAr : vac.NameEn,
                ApplyAfterMonth = vac.ApplyAfterMonth,
                CanTransfer = vac.CanTransfer,
                TotalBalance = vac.TotalBalance,
                VacationType = vt != null ? (lang == Localization.Arabic ? vt.Name : vt.NameInEnglish) : null,
                AddedBy = u != null ? u.UserName : null,
                EmployeesCount = ec != null ? ec.Count : 0
            };

        var result = query.ToList();

        if (skip.HasValue)
            result = result.Skip(skip.Value).ToList();

        if (take.HasValue)
            result = result.Take(take.Value).ToList();

        return result;

    }




}




