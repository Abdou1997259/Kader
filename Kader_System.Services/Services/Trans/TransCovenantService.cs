using Kader_System.Domain.DTOs;
using Kader_System.Services.IServices.AppServices;

namespace Kader_System.Services.Services.Trans
{
    public class TransCovenantService(IUnitOfWork unitOfWork, IUserContextService userContextService, IFileServer fileServer, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : ITransCovenantService
    {
        private TransCovenant _insatance;
        private IFileServer _fileServer = fileServer;
        private IUserContextService _userContextService = userContextService;
        public async Task<Response<IEnumerable<SelectListOfCovenantResponse>>> ListOfTransCovenantsAsync(string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var result =
                await unitOfWork.TransCovenants.GetSpecificSelectAsync(x => x.company_id == currentCompany,
                    includeProperties: $"{nameof(_insatance.employee)}",
                    select: x => new SelectListOfCovenantResponse
                    {
                        Id = x.id,
                        Date = x.date,
                        NameAr = x.name_ar,
                        NameEn = x.name_en,
                        Amount = x.amount,
                        EmployeeId = x.employee_id,
                        EmployeeName = lang == Localization.Arabic ? x.employee!.FullNameAr
                        : x.employee!.FullNameEn,
                        Notes = x.notes

                    }, orderBy: x =>
               x.OrderByDescending(x => x.id));

            if (!result.Any())
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = [],
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

        public async Task<Response<GetAllTransCovenantResponse>> GetAllTransCovenantsAsync(string lang,
            GetAllFilterationForTransCovenant model, string host)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            Expression<Func<TransCovenant, bool>> filter = x =>
            x.IsDeleted == model.IsDeleted && x.company_id == currentCompany &&
            (string.IsNullOrEmpty(model.Word) ||
                x.name_ar.Contains(model.Word) ||
                x.name_en.Contains(model.Word) ||
                (x.employee != null && (x.employee.FullNameAr.Contains(model.Word) ||
                x.employee.FullNameEn.Contains(model.Word)))) &&
            (!model.EmployeeId.HasValue || x.employee_id == model.EmployeeId);



            var totalRecords = await unitOfWork.TransCovenants.CountAsync(filter: filter,
                includeProperties: $"{nameof(_insatance.employee)}");


            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();

            var items = await unitOfWork.TransCovenants.GetSpecificSelectAsync(filter,
                includeProperties: "employee,employee.User,employee.Job", select: x => new TransCovenantData
                {
                    AddedBy = x.employee.User.FullName,
                    AddedOn = x.Add_date,
                    Amount = x.amount,
                    EmployeeId = x.employee_id,
                    EmployeeName = lang == Localization.Arabic ? x.employee.FullNameAr
                    : x.employee.FullNameEn,
                    Id = x.id,
                    Notes = x.notes,
                    NameAr = x.name_ar,
                    NameEn = x.name_en,
                    Date = x.date,
                    JobName = lang == Localization.Arabic ? x.employee.Job.NameAr : x.employee.Job.NameEn,

                }, skip: model.PageSize * (model.PageNumber - 1), take: model.PageSize);
            var result = new GetAllTransCovenantResponse
            {
                TotalRecords = totalRecords,
                Items = items.ToList(),
                // Items = unitOfWork.TransCovenants.GetTransCovenantDataInfo(filter: filter, filterSearch: filterSearch,
                //     skip: (model.PageNumber - 1) * model.PageSize,
                //     take: model.PageSize, lang: lang).Where(x => !model.EmployeeId.HasValue || x.EmployeeId == model.EmployeeId).ToList()
                //,
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
                string resultMsg = sharLocalizer[Localization.NotFoundData];

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

        public async Task<Response<CreateTransCovenantRequest>> CreateTransCovenantAsync(CreateTransCovenantRequest model, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var emp = await unitOfWork.Employees.GetFirstOrDefaultAsync(x => x.Id
            == model.employee_id && x.CompanyId == currentCompany);
            if (emp is null)
            {


                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Employee]]
                };
            }
            var contract = (await unitOfWork.Contracts.GetSpecificSelectAsync(x => x.employee_id ==
            model.employee_id && x.company_id == currentCompany, x => x)).FirstOrDefault();
            if (contract is null)
            {
                string resultMsg = $" {sharLocalizer[Localization.Employee]} {sharLocalizer[Localization.ContractNotFound]}";

                return new()
                {
                    Check = false,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            if (await unitOfWork.TransCovenants.ExistAsync(x => x.company_id
            == currentCompany && (x.name_ar.Trim() == model.name_ar.Trim() ||
            x.name_en.Trim() == model.name_en.Trim())))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.AlreadyExitedWithSameName, lang == Localization.Arabic ?
                    model.name_ar : model.name_en]

                };
            }

            var newTrans = mapper.Map<TransCovenant>(model);

            if (model.attachment_file is not null)
            {
                var dirType = HrDirectoryTypes.Covenant;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                newTrans.attachment = await _fileServer.UploadFileAsync(dir, model.attachment_file);

            }

            newTrans.company_id = currentCompany;
            await unitOfWork.TransCovenants.AddAsync(newTrans);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }

        public async Task<Response<GetTransCovenantById>> GetTransCovenantByIdAsync(int id, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();

            var obj = await unitOfWork.TransCovenants.GetFirstOrDefaultAsync(c =>
            c.id == id && c.company_id == currentCompany,
                includeProperties: $"{nameof(_insatance.employee)}");



            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            var lookups = await unitOfWork.Employees.GetEmployeesDataNameAndIdAsLookUp(lang, currentCompany);
            var dirType = HrDirectoryTypes.Covenant;
            var dir = dirType.GetModuleNameWithType(Modules.Trans);
            return new()
            {
                Data = new GetTransCovenantById()
                {
                    AddedOn = obj.Add_date,
                    Amount = obj.amount,
                    Date = obj.date,
                    NameAr = obj.name_ar,
                    NameEn = obj.name_en,
                    Notes = obj.notes,
                    EmployeeId = obj.employee_id,
                    EmployeeName = lang == Localization.Arabic ?
                    obj.employee!.FullNameAr : obj.employee!.FullNameEn,
                    Id = obj.id,

                },
                Check = true,
                LookUps = new
                {
                    employees = lookups
                }
            };
        }

        public async Task<Response<CreateTransCovenantRequest>> UpdateTransCovenantAsync(int id, CreateTransCovenantRequest model, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();

            var obj = await unitOfWork.TransCovenants.GetFirstOrDefaultAsync
                (x => x.id == id && x.company_id == currentCompany);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }
            var emp = await unitOfWork.Employees.GetByIdAsync(model.employee_id);
            if (emp is null)
            {


                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Employee]]
                };
            }
            var contract = (await unitOfWork.Contracts.GetSpecificSelectAsync(x
                => x.employee_id == model.employee_id
                && x.company_id == currentCompany, x => x)).FirstOrDefault();
            if (contract is null)
            {
                string resultMsg = $" {sharLocalizer[Localization.Employee]} {sharLocalizer[Localization.ContractNotFound]}";

                return new()
                {
                    Check = false,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            if (await unitOfWork.TransCovenants.ExistAsync(x => x.id != id && x.company_id
            == currentCompany && (x.name_ar.Trim() == model.name_ar.Trim() ||
           x.name_en.Trim() == model.name_en.Trim())))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.AlreadyExitedWithSameName, lang
                    == Localization.Arabic ? model.name_ar : model.name_en]

                };
            }

            if (model.attachment_file is not null)
            {
                var dirType = HrDirectoryTypes.Covenant;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                if (!string.IsNullOrEmpty(obj?.attachment))
                    _fileServer.RemoveFile(dir, obj.attachment);
                obj.attachment = await _fileServer.UploadFileAsync(dir, model.attachment_file);
            }



            obj.amount = model.amount;
            obj.date = model.date;
            obj.name_ar = model.name_ar;
            obj.name_en = model.name_en;
            obj.notes = model.notes;
            obj.employee_id = model.employee_id;
            obj.company_id = currentCompany;
            unitOfWork.TransCovenants.Update(obj);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }
        public async Task<Response<object>> RestoreTransCovenantAsync(int id)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();

            var obj = await unitOfWork.TransCovenants.GetFirstOrDefaultAsync
                (x => x.id == id && x.company_id == currentCompany);

            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            obj.IsDeleted = false;

            unitOfWork.TransCovenants.Update(obj);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Error = string.Empty,
                Check = true,
                Data = obj,
                LookUps = null,
                Msg = sharLocalizer[Localization.Restored]
            };
        }
        public Task<Response<string>> UpdateActiveOrNotTransCovenantAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<string>> DeleteTransCovenantAsync(int id)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();

            var obj = await unitOfWork.TransCovenants.GetFirstOrDefaultAsync(x => x.id == id && x.company_id
            == currentCompany);
            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = string.Empty,
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            if (!string.IsNullOrEmpty(obj.attachment))
            {
                ManageFilesHelper.RemoveFile(GoRootPath.TransFilesPath + obj.attachment);
            }

            unitOfWork.TransCovenants.Remove(obj);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Check = true,
                Data = string.Empty,
                Msg = sharLocalizer[Localization.Deleted]
            };
        }
    }
}
