using Kader_System.Domain.DTOs.Response;
using Kader_System.Domain.DTOs.Response.Trans;
using Kader_System.Domain.Interfaces.EmployeeRequest;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;

namespace Kader_System.DataAccess.Repositories.EmployeeRequests
{
    public class EmployeeRequestsRepository(KaderDbContext context) : BaseRepository<LeavePermissionRequest>(context), IEmployeeRequestsRepository
    {
        public async Task<Response<EmployeeRequestsLookUpsData>> GetEmployeeRequestsLookUpsData(string lang)
        {
            try
            {
                var employees = await (from q in context.Employees.AsNoTracking()
                                       where !q.IsDeleted && q.IsActive
                                       select new
                                       {
                                           id = q.Id,
                                           name = lang == Localization.Arabic ? q.FullNameAr : q.FullNameEn,
                                       }).ToArrayAsync();

                var allowance = await (from q in context.Allowances.AsNoTracking()
                                       where !q.IsDeleted && q.IsActive
                                       select new
                                       {
                                           id = q.Id,
                                           name = lang == Localization.Arabic ? q.Name_ar : q.Name_en,
                                       }).ToArrayAsync();

                var vacation_type = await (from q in context.VacationTypes.AsNoTracking()
                                           where !q.IsDeleted && q.IsActive
                                           select new
                                           {
                                               id = q.Id,
                                               name = lang == Localization.Arabic ? q.Name : q.NameInEnglish,
                                           }).ToArrayAsync();


                return new Response<EmployeeRequestsLookUpsData>()
                {
                    Check = true,
                    IsActive = true,
                    Error = "",
                    Msg = "",
                    Data = new EmployeeRequestsLookUpsData()
                    {
                        employees = employees,
                        allowances = allowance,
                        vacation_types = vacation_type,
                    }
                };
            }
            catch (Exception exception)
            {
                return new Response<EmployeeRequestsLookUpsData>()
                {
                    Error = exception.InnerException != null ? exception.InnerException.Message : exception.Message,
                    Msg = "Can not able to Get Data",
                    Check = false,
                    Data = null,
                    IsActive = false
                };
            }
        }
    }
}
