using Kader_System.Domain.DTOs.Response.Trans;

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

            var transVacations = context.TransVacations.Where(filter).OrderByDescending(
                v => v.id);


            var query = from trans in transVacations
                        join employee in context.Employees on trans.employee_id equals employee.Id into empGroup
                        from employee in empGroup.DefaultIfEmpty()
                        join u in context.Users on trans.Added_by equals u.Id into userGroup
                        from u in userGroup.DefaultIfEmpty()
                        join vacation in context.Vacations on employee.VacationId equals vacation.Id into vacationGroup
                        from vacation in vacationGroup.DefaultIfEmpty()
                        join vacationType in context.VacationDistributions on trans.vacation_id
                        equals vacationType.Id into vacationTypeGroup
                        from vacationType in vacationTypeGroup.DefaultIfEmpty()


                        select new TransVacationData()
                        {
                            StartDate = trans.start_date,
                            AddedBy = u.UserName,
                            DaysCount = trans.days_count,
                            VacationId = trans.vacation_id,
                            VacationName = lang == Localization.Arabic ? vacation.NameAr : vacation.NameEn,
                            EmployeeId = trans.employee_id,
                            EmployeeName = lang == Localization.Arabic ? employee.FullNameAr : employee.FullNameEn,
                            Id = trans.id,
                            Notes = trans.notes,
                            EndDate = trans.start_date.AddDays((int)trans.days_count - 1),
                            VacationType = lang == Localization.Arabic ?
                            vacationType.NameAr : vacationType.NameEn,
                            AddedDate = trans.Add_date,
                            TotalBalance = vacation.TotalBalance - (int)trans.days_count



                        };

            if (filterSearch != null)
                query = query.Where(filterSearch);

            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);


            return query.ToList();

        }

        public async Task<GetTransVacationById> GetTransVacationByIdAsync(int id, string lang, int companyId)
        {
            var query = from trans in context.TransVacations
                        join emp in context.Employees on trans.employee_id equals emp.Id
                        join vac in context.Vacations on emp.VacationId equals vac.Id
                        join vacType in context.VacationDistributions on trans.vacation_id
                        equals vacType.Id
                        where trans.id == id && trans.company_id == companyId
                        select new GetTransVacationById()
                        {
                            DaysCount = trans.days_count,
                            EmployeeId = trans.employee_id,
                            EmployeeName = lang == Localization.Arabic ? emp!.FullNameAr : emp!.FullNameEn,
                            StartDate = trans.start_date,
                            Id = trans.id,
                            Notes = trans.notes,
                            VacationId = trans.vacation_id,
                            VacationName = lang ==

                            Localization.Arabic ? vac!.NameAr : vac!.NameEn,
                            VacationType = lang ==
                            Localization.Arabic ? vacType.NameAr : vacType.NameEn
                        };

            return await query!.FirstOrDefaultAsync();
        }
        public async Task<Response<TransVacationLookUpsData>> GetTransVacationLookUpsData(string lang, int companyId)
        {
            try
            {
                var employees = await context.Employees.Where(e => !e.IsDeleted &&
                e.IsActive && e.CompanyId == companyId)
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
                                used_days = context.TransVacations.Where(c => c.vacation_id
                                == v.Id && c.employee_id == x.Id && !c.IsDeleted)
                                .Sum(d => d.days_count)
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

        public async Task<double> GetVacationDaysUsedByEmployee(int empId, int vacationId, int companyId)
        {
            return await context.TransVacations.Where(v => v.employee_id == empId &&
            v.company_id == companyId && v.vacation_id == vacationId
                && !v.IsDeleted)
                  .SumAsync(c => c.days_count);
        }

        public async Task<double> GetVacationTotalBalance(int vacationId, int companyId)
        {
            return await context.VacationDistributions.Where(v => v.Id == vacationId)
                .SumAsync(c => c.DaysCount);
        }
    }
}
