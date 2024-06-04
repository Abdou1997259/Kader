using Kader_System.Domain.DTOs.Response.Trans;
using Kader_System.Domain.Interfaces;

namespace Kader_System.DataAccess.Repositories.Trans
{
    public class TransVacationRepository(KaderDbContext context) : BaseRepository<TransVacation>(context), ITransVacationRepository
    { 
            public List<TransVacationData> GetTransVacationInfo(
         Expression<Func<TransVacation, bool>> filter,
         Expression<Func<TransVacationData, bool>> filterSearch,
         int? skip = null,
         int? take = null, string lang = "ar"
          )
        {

            var transVacations = context.TransVacations.Where(filter).OrderByDescending(v=>v.Id);


            var query = from trans in transVacations
                        join employee in context.Employees on trans.EmployeeId equals employee.Id into empGroup
                        from employee in empGroup.DefaultIfEmpty()
                        join u in context.Users on trans.Added_by equals u.Id into userGroup
                        from u in userGroup.DefaultIfEmpty()
                        join vacation in context.Vacations on employee.VacationId equals vacation.Id into vacationGroup
                        from vacation in vacationGroup.DefaultIfEmpty()
                        join vacationType in context.VacationDistributions on trans.VacationId equals vacationType.Id into vacationTypeGroup
                        from vacationType in vacationTypeGroup.DefaultIfEmpty()

                         
                        select new TransVacationData()
                        {
                            StartDate = trans.StartDate,
                            AddedBy = u.UserName,
                            DaysCount = trans.DaysCount,
                            VacationId = trans.VacationId,
                            VacationName = lang == Localization.Arabic ? vacation.NameAr : vacation.NameEn,
                            EmployeeId = trans.EmployeeId,
                            EmployeeName = lang == Localization.Arabic ? employee.FullNameAr : employee.FullNameEn,
                            Id = trans.Id,
                            Notes = trans.Notes,
                            EndDate = trans.StartDate.AddDays((int)trans.DaysCount - 1),
                            VacationType = lang==Localization.Arabic ? vacationType.NameAr: vacationType.NameEn,
                            AddedDate = trans.Add_date

                        };

            if (filterSearch != null)
                query = query.Where(filterSearch);

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);


            return query.ToList();

        }

        public async Task<GetTransVacationById> GetTransVacationByIdAsync(int id, string lang)
        {
            var query = from trans in context.TransVacations
                join emp in context.Employees on trans.EmployeeId equals emp.Id
                join vac in context.Vacations on emp.VacationId equals vac.Id
                join vacType in context.VacationDistributions on trans.VacationId equals vacType.Id
                where trans.Id == id
                select new GetTransVacationById()
                {
                    DaysCount = trans.DaysCount,
                    EmployeeId = trans.EmployeeId,
                    EmployeeName = lang == Localization.Arabic ? emp!.FullNameAr : emp!.FullNameEn,
                    StartDate = trans.StartDate,
                    Id = trans.Id,
                    Notes = trans.Notes,
                    VacationId = trans.VacationId,
                    VacationName = lang == Localization.Arabic ? vac!.NameAr : vac!.NameEn,
                    VacationType = lang==Localization.Arabic ? vacType.NameAr : vacType.NameEn
                };

            return await query!.FirstOrDefaultAsync();
        }
        public async Task<Response<TransVacationLookUpsData>> GetTransVacationLookUpsData(string lang)
        {
            try
            {
                var employees = await context.Employees.Where(e => !e.IsDeleted && e.IsActive)
                    .Select(x => new
                    {
                        id = x.Id,
                        name = lang == Localization.Arabic ? x.FullNameAr : x.FullNameEn,
                        vacations = context.VacationDistributions.Where(v => v.VacationId == x.VacationId && !v.IsDeleted)
                            .Select(v => new
                            {
                                id = v.Id,
                                name = lang == Localization.Arabic ? v.NameAr : v.NameEn,
                                vacation_id = v.VacationId,
                                total_days = v.DaysCount,
                                used_days = context.TransVacations.Where(c => c.VacationId == v.Id && c.EmployeeId == x.Id && !c.IsDeleted).Sum(d => d.DaysCount)
                            }).ToList()
                    }).ToArrayAsync();


                return new Response<TransVacationLookUpsData>()
                {
                    Check = true,
                    IsActive = true,
                    Error = "",
                    Msg = "",
                    Data = new TransVacationLookUpsData()
                    {
                        employees = employees,

                    }
                };
            }
            catch (Exception exception)
            {
                return new Response<TransVacationLookUpsData>()
                {
                    Error = exception.InnerException != null ? exception.InnerException.Message : exception.Message,
                    Msg = "Can not able to Get Data",
                    Check = false,
                    Data = null,
                    IsActive = false
                };
            }

        }

        public async Task<double> GetVacationDaysUsedByEmployee(int empId, int vacationId)
        {
          return await context.TransVacations.Where(v => v.EmployeeId == empId && v.VacationId == vacationId
              && !v.IsDeleted)
                .SumAsync(c => c.DaysCount);
        }

        public async Task<double> GetVacationTotalBalance( int vacationId)
        {
            return await context.VacationDistributions.Where(v => v.Id == vacationId)
                .SumAsync(c => c.DaysCount);
        }
    }
}
