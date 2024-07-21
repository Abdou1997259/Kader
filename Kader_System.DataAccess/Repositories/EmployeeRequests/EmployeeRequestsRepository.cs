using Azure;
using Kader_System.Domain;
using Kader_System.Domain.Constants.Enums;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Response;
using Kader_System.Domain.DTOs.Response.Employee_Requests;
using Kader_System.Domain.Interfaces.EmployeeRequest;
using Kader_System.Domain.Models.EmployeeRequests;
using Microsoft.Extensions.Localization;

namespace Kader_System.DataAccess.Repositories.EmployeeRequests
{
    public class EmployeeRequestsRepository(KaderDbContext context) : BaseRepository<HrEmployeeRequests>(context),IEmployeeRequestsRepository
    {
        public async Task<Domain.Dtos.Response.Response<EmployeeRequestsLookUpsData>> GetEmployeeRequestsLookUpsData(string lang)
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


                return new Domain.Dtos.Response.Response<EmployeeRequestsLookUpsData>()
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
                return new Domain.Dtos.Response.Response<EmployeeRequestsLookUpsData>()
                {
                    Error = exception.InnerException != null ? exception.InnerException.Message : exception.Message,
                    Msg = "Can not able to Get Data",
                    Check = false,
                    Data = null,
                    IsActive = false
                };
            }
        }
        //public async Task<Domain.Dtos.Response.Response<GetAlVacationRequstsResponse>> GetAlVacationRequstsAsync(string lang, Domain.DTOs.Request.EmployeesRequests.GetAllFilltrationForEmployeeRequests model, string host, RequestStatusTypes types)
        //{

        //    var query = (from q in context.HrEmployeeRequests.AsNoTracking()
        //                 join v in context.HrVacationRequests.AsNoTracking()
        //                 on q.RequestId equals v.Id
        //                 where q.IsDeleted == model.IsDeleted &&
        //                 (string.IsNullOrEmpty(model.Word) || q.RequestDate.ToString() == model.Word) &&
        //                 q.Status == (int)types
        //                 select new
        //                 {
        //                     q.Id,
        //                     q.RequestDate,
        //                     EmployeeName = Localization.Arabic == lang ? v.Employee.FirstNameAr : v.Employee.FirstNameEn,
        //                     v.DayCounts,
        //                     VacationType = Localization.Arabic == lang ? v.VacationType.Name : v.VacationType.NameInEnglish,
        //                     q.Status,
        //                     Attachment = v.AttachmentFileName,

        //                 }).AsQueryable().OrderByDescending(x => x.Id);
        //    #region Pagination
        //    var totalRecords = await query.CountAsync();
        //    int page = 1;
        //    int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
        //    if (model.PageNumber < 1)
        //        page = 1;
        //    else
        //        page = model.PageNumber;
        //    var pageLinks = Enumerable.Range(1, totalPages)
        //        .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
        //        .ToList();
        //    #endregion

        //    var result = new GetAlVacationRequstsResponse
        //    {
        //        TotalRecords = totalRecords,
        //        Items = (await query.Cast<object>().ToListAsync()),
        //        CurrentPage = model.PageNumber,
        //        FirstPageUrl = host + $"?PageSize={model.PageSize}&PageNumber=1&IsDeleted={model.IsDeleted}",
        //        From = (page - 1) * model.PageSize + 1,
        //        To = Math.Min(page * model.PageSize, totalRecords),
        //        LastPage = totalPages,
        //        LastPageUrl = host + $"?PageSize={model.PageSize}&PageNumber={totalPages}&IsDeleted={model.IsDeleted}",
        //        PreviousPage = page > 1 ? host + $"?PageSize={model.PageSize}&PageNumber={page - 1}&IsDeleted={model.IsDeleted}" : null,
        //        NextPageUrl = page < totalPages ? host + $"?PageSize={model.PageSize}&PageNumber={page + 1}&IsDeleted={model.IsDeleted}" : null,
        //        Path = host,
        //        PerPage = model.PageSize,
        //        Links = pageLinks,
        //    };

        //    if (result.TotalRecords is 0)
        //    {
        //        string resultMsg = _sharLocalizer[Localization.NotFoundData];

        //        return new()
        //        {
        //            Data = new()
        //            {
        //                Items = []
        //            },
        //            Error = resultMsg,
        //            Msg = resultMsg
        //        };
        //    }

        //    return new()
        //    {
        //        Data = result,
        //        Check = true
        //    };
        //}
        //public async Task<Domain.Dtos.Response.Response<GetAlVacationRequstsResponse>> GetAllSalaryIncreaseRequstsAsync(string lang, Domain.DTOs.Request.EmployeesRequests.GetAllFilltrationForEmployeeRequests model, string host, RequestStatusTypes types)
        //{

        //    var query = (from q in context.HrEmployeeRequests.AsNoTracking()
        //                 join v in context.SalaryIncreaseRequests.AsNoTracking()
        //                 on q.RequestId equals v.Id
        //                 where q.IsDeleted == model.IsDeleted &&
        //                 (string.IsNullOrEmpty(model.Word) || q.RequestDate.ToString() == model.Word) &&
        //                 q.Status == (int)types
        //                 select new
        //                 {
        //                     q.Id,
        //                     q.RequestDate,
        //                     EmployeeName = Localization.Arabic == lang ? v.employee.FirstNameAr : v.employee.FirstNameEn,
        //                     v.Amount,
        //                     q.Status,
        //                     Attachment = v.AtachmentPath,

        //                 }).AsQueryable().OrderByDescending(x => x.Id);
        //    #region Pagination
        //    var totalRecords = await query.CountAsync();
        //    int page = 1;
        //    int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
        //    if (model.PageNumber < 1)
        //        page = 1;
        //    else
        //        page = model.PageNumber;
        //    var pageLinks = Enumerable.Range(1, totalPages)
        //        .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
        //        .ToList();
        //    #endregion

        //    var result = new GetAlVacationRequstsResponse
        //    {
        //        TotalRecords = totalRecords,
        //        Items = (await query.Cast<object>().ToListAsync()),
        //        CurrentPage = model.PageNumber,
        //        FirstPageUrl = host + $"?PageSize={model.PageSize}&PageNumber=1&IsDeleted={model.IsDeleted}",
        //        From = (page - 1) * model.PageSize + 1,
        //        To = Math.Min(page * model.PageSize, totalRecords),
        //        LastPage = totalPages,
        //        LastPageUrl = host + $"?PageSize={model.PageSize}&PageNumber={totalPages}&IsDeleted={model.IsDeleted}",
        //        PreviousPage = page > 1 ? host + $"?PageSize={model.PageSize}&PageNumber={page - 1}&IsDeleted={model.IsDeleted}" : null,
        //        NextPageUrl = page < totalPages ? host + $"?PageSize={model.PageSize}&PageNumber={page + 1}&IsDeleted={model.IsDeleted}" : null,
        //        Path = host,
        //        PerPage = model.PageSize,
        //        Links = pageLinks,
        //    };

        //    if (result.TotalRecords is 0)
        //    {
        //        string resultMsg = _sharLocalizer[Localization.NotFoundData];

        //        return new()
        //        {
        //            Data = new()
        //            {
        //                Items = []
        //            },
        //            Error = resultMsg,
        //            Msg = resultMsg
        //        };
        //    }

        //    return new()
        //    {
        //        Data = result,
        //        Check = true
        //    };
        //}


    }
}
