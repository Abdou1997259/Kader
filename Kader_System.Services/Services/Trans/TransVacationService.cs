using Kader_System.Domain.DTOs;
using Kader_System.Services.IServices.AppServices;
using TransVacation = Kader_System.Domain.Models.Trans.TransVacation;



namespace Kader_System.Services.Services.Trans
{
    public class TransVacationService(IUnitOfWork unitOfWork, IUserContextService userContextService, IStringLocalizer<SharedResource> sharLocalizer, IFileServer _fileServer, IMapper mapper) : ITransVacationService
    {
        private TransVacation _insatance;
        private readonly IUserContextService _userContextService = userContextService;
        public async Task<Response<IEnumerable<SelectListOfTransVacationResponse>>> ListOfTransVacationsAsync(string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var result =
                await unitOfWork.TransVacations.GetSpecificSelectAsync(x => x.CompanyId == currentCompany,
                    includeProperties: $"{nameof(_insatance.Vacation)},{nameof(_insatance.Employee)}"
                                       ,
                    select: x => new SelectListOfTransVacationResponse
                    {
                        Id = x.Id,
                        StartDate = x.StartDate,
                        DaysCount = x.DaysCount,
                        VacationId = x.VacationId,
                        VacationName = lang == Localization.Arabic ? x.Vacation!.NameAr : x.Vacation!.NameEn,
                        EmployeeId = x.EmployeeId,
                        EmployeeName = lang == Localization.Arabic ? x.Employee!.FullNameAr : x.Employee!.FullNameEn,
                        Notes = x.Notes
                    }, orderBy: x =>
                        x.OrderByDescending(x => x.Id));

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

        public async Task<Response<GetAllTransVacationResponse>> GetAllTransVacationsAsync(string lang,
            GetAllFilterationForTransVacationRequest model, string host)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            Expression<Func<TransVacation, bool>> filter = x => x.IsDeleted == model.IsDeleted && x.CompanyId == currentCompany
                && (string.IsNullOrEmpty(model.Word)
                || x.Employee!.FullNameEn.Contains(model.Word)
                || x.Employee!.FullNameAr.Contains(model.Word)
                || x.Vacation!.NameAr.Contains(model.Word)
                || x.Vacation!.NameEn.Contains(model.Word))
                  && (!model.EmployeeId.HasValue || x.Employee.Id == model.EmployeeId);

            Expression<Func<TransVacationData, bool>> filterSearch = x =>
                (string.IsNullOrEmpty(model.Word)
                 || x.EmployeeName.Contains(model.Word)
                 || x.VacationName.Contains(model.Word)
                 || x.EmployeeName.Contains(model.Word)
                 || x.AddedBy!.Contains(model.Word)
                  && (!model.EmployeeId.HasValue || x.EmployeeId == model.EmployeeId));

            var totalRecords = await unitOfWork.TransVacations.CountAsync(filter: filter,
                includeProperties: $"{nameof(_insatance.Vacation)},{nameof(_insatance.Employee)}");

            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllTransVacationResponse
            {
                TotalRecords = totalRecords,

                Items = unitOfWork.TransVacations.GetTransVacationInfo(filter: filter, filterSearch: filterSearch,
                    skip: (model.PageNumber - 1) * model.PageSize,
                    take: model.PageSize, lang: lang).OrderByDescending(x => x.Id).ToList(),
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
        public async Task<Response<TransVacationLookUpsData>> GetTransVacationLookUpsData(string lang)
        {
            try
            {
                var currentCompany = await _userContextService.GetLoggedCurrentCompany();
                return await unitOfWork.TransVacations.GetTransVacationLookUpsData(lang, currentCompany);

                //var employees = await unitOfWork.Employees.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
                //    select: x => new
                //    {
                //        Id = x.Id,
                //        Name = lang == Localization.Arabic ? x.FullNameAr : x.FullNameEn,

                //    });

                //var vacations = await unitOfWork.Vacations.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
                //    select: x => new
                //    {
                //        Id = x.Id,
                //        Name = lang == Localization.Arabic ? x.NameAr : x.NameEn
                //    });

                //return new Response<TransVacationLookUpsData>()
                //{
                //    Check = true,
                //    IsActive = true,
                //    Error = "",
                //    Msg = "",
                //    Data = new TransVacationLookUpsData()
                //    {
                //        vacations = vacations.ToArray(),
                //        employees = employees.ToArray(),

                //    }
                //};
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


        public async Task<Response<CreateTransVacationRequest>> CreateTransVacationAsync(CreateTransVacationRequest model, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var emp = await unitOfWork.Employees.GetFirstOrDefaultAsync(x => x.CompanyId == currentCompany && x.Id == model.EmployeeId);
            if (emp is null)
            {


                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.IsNotExisted, sharLocalizer[Localization.Employee]]
                };
            }
            var contract = (await unitOfWork.Contracts.GetSpecificSelectAsync(x => x.EmployeeId == model.EmployeeId && x.CompanyId == currentCompany, x => x)).FirstOrDefault();
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
            if (!await unitOfWork.Vacations.ExistAsync(model.VacationId))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Vacation]]
                };
            }


            if (await unitOfWork.TransVacations.ExistAsync(x =>
            x.EmployeeId == model.EmployeeId && x.CompanyId == currentCompany &&
            x.VacationId == model.VacationId &&
            DateOnly.FromDateTime(x.Add_date.Value) == DateOnly.FromDateTime(DateTime.Now)))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.TodayTrans,
                    Localization.Arabic == lang ? emp.FullNameAr : emp.FullNameEn]
                };
            }
            var newTrans = mapper.Map<TransVacation>(model);
            newTrans.CompanyId = currentCompany;
            var usedDays =
                await unitOfWork.TransVacations.GetVacationDaysUsedByEmployee(model.EmployeeId, model.VacationId, currentCompany);
            var totalBalance =
                await unitOfWork.TransVacations.GetVacationTotalBalance(model.VacationId, currentCompany);
            var reminderDays = totalBalance - usedDays;
            if (model.DaysCount > reminderDays)
            {
                return new()
                {
                    Error = string.Empty,
                    Check = false,
                    Data = model,
                    LookUps = null,
                    Msg = sharLocalizer[Localization.BalanceNotEnough],

                };
            }

            if (model.AttachmentFile is not null)
            {
                var dirType = HrDirectoryTypes.Vacation;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                newTrans.Attachment = await _fileServer.UploadFileAsync(dir, model.AttachmentFile);

            }

            await unitOfWork.TransVacations.AddAsync(newTrans);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }

        public async Task<Response<GetTransVacationById>> GetTransVacationByIdAsync(int id, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await unitOfWork.TransVacations.GetTransVacationByIdAsync(id, lang, currentCompany);


            if (obj == null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            return new()
            {
                Data = obj,
                Check = true
            };
        }

        public async Task<Response<GetTransVacationById>> UpdateTransVacationAsync(int id, CreateTransVacationRequest model, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await unitOfWork.TransVacations.GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompany);
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
            var emp = await unitOfWork.Employees.GetFirstOrDefaultAsync(x => x.Id == model.EmployeeId && x.CompanyId == currentCompany);
            if (emp is null)
            {


                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Employee]]
                };
            }
            var contract = (await unitOfWork.Contracts.GetSpecificSelectAsync(x => x.EmployeeId == model.EmployeeId && x.CompanyId == currentCompany, x => x)).FirstOrDefault();
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


            if (!await unitOfWork.Vacations.ExistAsync(model.VacationId))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Vacation]]
                };
            }


            var usedDays =
                await unitOfWork.TransVacations.GetVacationDaysUsedByEmployee(model.EmployeeId,

                model.VacationId, currentCompany);
            var totalBalance =
                await unitOfWork.TransVacations.GetVacationTotalBalance(model.VacationId, currentCompany);
            var reminderDays = totalBalance + obj.DaysCount - usedDays;
            if (model.DaysCount > reminderDays)
            {
                return new()
                {
                    Error = string.Empty,
                    Check = false,
                    Data = mapper.Map<GetTransVacationById>(obj),
                    LookUps = null,
                    Msg = sharLocalizer[Localization.BalanceNotEnough],

                };
            }

            HrEmployeeRequestTypesEnums hrEmployeeRequests = HrEmployeeRequestTypesEnums.VacationRequest;
            var moduleNameWithType = hrEmployeeRequests.GetModuleNameWithType(Modules.Trans);
            #region UpdateFile
            if (model.AttachmentFile is not null)
            {
                var dirType = HrDirectoryTypes.Vacation;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                if (!string.IsNullOrEmpty(obj?.Attachment))
                    _fileServer.RemoveFile(dir, obj.Attachment);
                obj.Attachment = await _fileServer.UploadFileAsync(dir, model.AttachmentFile);
            }

            #endregion



            obj.DaysCount = model.DaysCount;
            obj.StartDate = model.StartDate;
            obj.VacationId = model.VacationId;
            obj.Notes = model.Notes;
            obj.CompanyId = currentCompany;
            obj.EmployeeId = model.EmployeeId;
            unitOfWork.TransVacations.Update(obj);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = mapper.Map<GetTransVacationById>(obj)
            };
        }

        public Task<Response<string>> UpdateActiveOrNotTransVacationAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<Response<object>> RestoreTransVacationAsync(int id)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await unitOfWork.TransVacations.GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompany);

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

            unitOfWork.TransVacations.Update(obj);
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

        public async Task<Response<string>> DeleteTransVacationAsync(int id)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await unitOfWork.TransVacations.GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompany);
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


            unitOfWork.TransVacations.Remove(obj);
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
