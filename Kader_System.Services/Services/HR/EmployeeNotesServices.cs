using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Microsoft.AspNetCore.Http;

namespace Kader_System.Services.Services.HR
{
    public class EmployeeNotesServices(
        IUnitOfWork unitOfWork,
        IStringLocalizer<SharedResource> shareLocalizer,
         IHttpContextAccessor _httpContextAccessor,
    IMapper mapper) : IEmployeeNotesServices
    {
        public async Task<Response<CreateEmployeeNotes>> CreateEmployeeNotesAsync(CreateEmployeeNotes model)
        {
            HrEmployeeNotes hrEmployeeNotes = new()
            {
                EmployeeId = model.EmployeeId,
                Notes = model.notes,
            };
            await unitOfWork.EmployeeNotes.AddAsync(hrEmployeeNotes);
            await unitOfWork.CompleteAsync();

            return new()
            {
                Msg = shareLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }
        public async Task<Response<GetAllEmployeeNotesResponse>> GetAllEmployeeNotesAsync(string lang ,GetAllEmployeeNotesRequest model, string host)
        {
            Expression<Func<HrEmployeeNotes, bool>> filter = x => x.IsDeleted == model.IsDeleted &&
                                                           (string.IsNullOrEmpty(model.Word)
                                                            || x.Notes.Contains(model.Word));
            var totalRecords = await unitOfWork.EmployeeNotes.CountAsync(filter: filter);
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllEmployeeNotesResponse
            {
                TotalRecords = totalRecords,

                Items = (await unitOfWork.EmployeeNotes.GetSpecificSelectAsync(filter: filter, includeProperties: "Employee",
                    take: model.PageSize,
                    skip: (model.PageNumber - 1) * model.PageSize,
                    select: x => new EmployeeNotesData
                    {
                        Id = x.Id,
                        employee_id = x.EmployeeId,
                        employee_name = lang == Localization.English ? x.Employee.FirstNameEn + " " +
                        x.Employee.FatherNameEn + " " + x.Employee.GrandFatherNameEn :
                        x.Employee.FirstNameAr + " " + x.Employee.FatherNameAr + " " + x.Employee.GrandFatherNameAr,
                        notes = x.Notes,    
                    }, orderBy: x =>
                        x.OrderByDescending(x => x.Id))).ToList(),
                CurrentPage = model.PageNumber,
                FirstPageUrl = host + $"?PageSize={model.PageSize}&PageNumber=1&IsDeleted={model.IsDeleted}",
                From = (page - 1) * model.PageSize + 1,
                To = Math.Min(page * model.PageSize, totalRecords),
                LastPage = totalPages,
                LastPageUrl = host + $"?PageSize={model.PageSize}&PageNumber={totalPages}&IsDeleted={model.IsDeleted}",
                PreviousPage = page > 1 ? host + $"?PageSize={model.PageSize}&PageNumber={page - 1}&IsDeleted={model.IsDeleted}" : null,
                NextPageUrl = page < totalPages ? host + $"?PageSize={model.PageSize}&PageNumber={page + 1}&IsDeleted={model.IsDeleted}" : null,
                Path = host,
                PerPage = model.PageSize,
                Links = pageLinks
            };

            if (result.TotalRecords is 0)
            {
                string resultMsg = shareLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new()
                    {
                        Items = []
                    },
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            return new()
            {
                Data = result,
                Check = true
            };

        }

        public async Task<Response<EmployeeNotesData>> GetEmployeeNotesByIdAsync(int id)
        {

            var result = await unitOfWork.EmployeeNotes.GetByIdAsync(id);
            if (result == null)
            {
                var msg = shareLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = null,
                    Msg = msg,
                    Check = false
                };

            }
            var mappingResult = mapper.Map<EmployeeNotesData>(result);
            return new()
            {
                Data = mappingResult,
                Check = true,

            };
        }
        public async Task<Response<CreateEmployeeNotes>> UpdateEmployeeNotesAsync(int id, CreateEmployeeNotes model)
        {
            var employeeNotes = await unitOfWork.EmployeeNotes.GetByIdAsync(id);
            employeeNotes.Notes = model.notes;
            unitOfWork.EmployeeNotes.Update(employeeNotes);
            await unitOfWork.CompleteAsync();

            return new()
            {
                Msg = shareLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }

        public async Task<Response<string>> DeleteEmployeeNotesAsync(int id)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var msg = $"{shareLocalizer[Localization.EmployeeNotes]} {shareLocalizer[Localization.NotFound]}";
            var _employeeNotes = await unitOfWork.EmployeeNotes.GetByIdAsync(id);
            if (_employeeNotes != null)
            {
                var result = await unitOfWork.EmployeeNotes.SoftDeleteAsync(_employeeNotes, DeletedBy: userId);
                if (result > 0)
                {         
                    msg = shareLocalizer[Localization.Deleted];
                    return new()
                    {
                        Msg = msg,
                        Check = true,
                    };
                }
            }
            return new()
            {
                Check = false,
                Data = null,
                Msg = msg
            };
        }
    }
}
