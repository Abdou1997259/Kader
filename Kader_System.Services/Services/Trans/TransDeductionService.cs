using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Response;
using Kader_System.Services.IServices.AppServices;

namespace Kader_System.Services.Services.Trans
{
    public class TransDeductionService(IUnitOfWork unitOfWork, IUserContextService userContextService, IFileServer fileServer, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : ITransDeductionService
    {
        private TransDeduction _insatance;
        private IFileServer _fileServer = fileServer;
        private IUserContextService _userContextService = userContextService;
        public async Task<Response<IEnumerable<SelectListOfTransDeductionResponse>>> ListOfTransDeductionsAsync(string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();

            var result =
                await unitOfWork.TransDeductions.GetSpecificSelectAsync(x => x.CompanyId == currentCompany,
                    includeProperties: $"{nameof(_insatance.Deduction)},{nameof(_insatance.Employee)},{nameof(_insatance.SalaryEffect)}" +
                                       $",{nameof(_insatance.AmountType)}",
                    select: x => new SelectListOfTransDeductionResponse
                    {
                        Id = x.Id,
                        ActionMonth = x.ActionMonth,
                        SalaryEffect = lang == Localization.Arabic ? x.SalaryEffect!.Name : x.SalaryEffect!.NameInEnglish,
                        AddedOn = x.Add_date,
                        DeductionId = x.DeductionId,
                        DeductionName = lang == Localization.Arabic ? x.Deduction!.Name_ar : x.Deduction!.Name_en,
                        Amount = x.Amount,
                        EmployeeId = x.EmployeeId,
                        EmployeeName = lang == Localization.Arabic ? x.Employee!.FullNameAr : x.Employee!.FullNameEn,
                        Notes = x.Notes,
                        SalaryEffectId = x.SalaryEffectId,
                        AmountTypeId = x.AmountTypeId,
                        ValueTypeName = lang == Localization.Arabic ? x.AmountType!.Name : x.AmountType!.NameInEnglish
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

        public async Task<Response<GetAllTransDeductionResponse>> GetAllTransDeductionsAsync(string lang,
            GetAllFilterationForTransDeductionRequest model, string host)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            Expression<Func<TransDeduction, bool>> filter = x =>
                x.IsDeleted == model.IsDeleted && x.CompanyId == currentCompany &&
                (
                    (string.IsNullOrEmpty(model.Word) || x.ActionMonth.ToString().Contains(model.Word)
                     || x.AmountType!.Name.Contains(model.Word)
                     || x.AmountType!.NameInEnglish.Contains(model.Word)
                     || x.Deduction!.Name_en.Contains(model.Word)
                     || x.Deduction!.Name_ar.Contains(model.Word)
                     || x.Employee!.FullNameEn.Contains(model.Word)
                     || x.Employee!.FullNameAr.Contains(model.Word))
                    && (!model.EmployeeId.HasValue || x.EmployeeId == model.EmployeeId)
                );

            Expression<Func<TransDeductionData, bool>> filterSearch = x =>
                (string.IsNullOrEmpty(model.Word)
                 || x.DeductionName.Contains(model.Word)
                 || x.EmployeeName.Contains(model.Word)
                 || x.DiscountType.Contains(model.Word));

            var totalRecords = await unitOfWork.TransDeductions.CountAsync(filter: filter,
                includeProperties: $"{nameof(_insatance.Deduction)},{nameof(_insatance.Employee)}," +
                                   $"{nameof(_insatance.SalaryEffect)}" +
                                   $",{nameof(_insatance.AmountType)}");
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var result = new GetAllTransDeductionResponse
            {
                TotalRecords = totalRecords,

                Items = unitOfWork.TransDeductions.GetTransDeductionInfo(filter: filter, filterSearch: filterSearch, skip: (model.PageNumber - 1) * model.PageSize
                    , take: model.PageSize, lang: lang).Where(x => !model.EmployeeId.HasValue || x.EmployeeId == model.EmployeeId).OrderByDescending(x => x.Id).ToList()
                ,
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


        public async Task<Response<DeductionLookUps>> GetDeductionsLookUpsData(string lang)
        {
            try
            {
                var currentCompany = await _userContextService.GetLoggedCurrentCompany();
                var employees = await unitOfWork.Employees.GetSpecificSelectAsync(filter => filter.IsDeleted == false && filter.CompanyId == currentCompany,
                    select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.FullNameAr : x.FullNameEn
                    });

                var deductions = await unitOfWork.Deductions.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
                    select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.Name_ar : x.Name_en
                    });

                var salaryEffect = await unitOfWork.TransSalaryEffects.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
                    select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.Name : x.NameInEnglish,

                    });
                var amountType = await unitOfWork.TransAmountTypes.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
                    select: x => new
                    {
                        Id = x.Id,
                        Name = lang == Localization.Arabic ? x.Name : x.NameInEnglish,

                    });

                return new Response<DeductionLookUps>()
                {
                    Check = true,
                    IsActive = true,
                    Error = "",
                    Msg = "",
                    Data = new DeductionLookUps()
                    {
                        deductions = deductions.ToArray(),
                        employees = employees.ToArray(),
                        salary_effects = salaryEffect.ToArray(),
                        trans_amount_types = amountType.ToArray()
                    }
                };
            }
            catch (Exception exception)
            {
                return new Response<DeductionLookUps>()
                {
                    Error = exception.InnerException != null ? exception.InnerException.Message : exception.Message,
                    Msg = "Can not able to Get Data",
                    Check = false,
                    Data = null,
                    IsActive = false
                };
            }

        }

        public async Task<Response<CreateTransDeductionRequest>> CreateTransDeductionAsync
            (CreateTransDeductionRequest model, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
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

            if (!await unitOfWork.Deductions.ExistAsync(model.DeductionId))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Deduction]]
                };
            }
            if (!await unitOfWork.TransSalaryEffects.ExistAsync(model.SalaryEffectId))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.SalaryEffect]]
                };
            }
            if (!await unitOfWork.TransAmountTypes.ExistAsync(x => x.Id == model.AmountTypeId))

            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.AmountTypes]]
                };

            }

            if (await unitOfWork.TransDeductions.ExistAsync(x => x.EmployeeId == model.EmployeeId && x.CompanyId == currentCompany &&
            x.DeductionId == model.DeductionId &&
            DateOnly.FromDateTime(x.Add_date.Value) == DateOnly.FromDateTime(DateTime.Now)))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.TodayTrans,
                    Localization.Arabic == lang ? emp.FullNameAr : emp.FullNameEn]
                };
            }
            var newTrans = mapper.Map<TransDeduction>(model);




            if (model.Attachment_File is not null)
            {
                var dirType = HrDirectoryTypes.Deductions;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                newTrans.Attachment = await _fileServer.UploadFileAsync(dir, model.Attachment_File);

            }



            newTrans.CompanyId = currentCompany;
            await unitOfWork.TransDeductions.AddAsync(newTrans);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }

        public async Task<Response<GetTransDeductionById>> GetTransDeductionByIdAsync(int id, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await unitOfWork.TransDeductions.GetFirstOrDefaultAsync(d => d.Id == id && d.CompanyId == currentCompany,
                includeProperties: $"{nameof(_insatance.Deduction)},{nameof(_insatance.Employee)}," +
                                   $"{nameof(_insatance.SalaryEffect)}" +
                                   $",{nameof(_insatance.AmountType)}");

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
            var dirType = HrDirectoryTypes.Deductions;
            var dir = dirType.GetModuleNameWithType(Modules.Trans);
            return new()
            {
                Data = new GetTransDeductionById()
                {
                    ActionMonth = obj.ActionMonth,
                    AddedOn = obj.Add_date,
                    DeductionId = obj.DeductionId,
                    DeductionName = lang == Localization.Arabic ? obj.Deduction!.Name_ar : obj.Deduction!.Name_en,
                    EmployeeId = obj.EmployeeId,
                    EmployeeName = lang == Localization.Arabic ? obj.Employee!.FullNameAr : obj.Employee.FullNameEn,
                    Id = obj.Id,
                    SalaryEffect = lang == Localization.Arabic ? obj.SalaryEffect!.Name : obj.SalaryEffect!.NameInEnglish,
                    SalaryEffectId = obj.SalaryEffectId,
                    Notes = obj.Notes,
                    AmountTypeId = obj.AmountTypeId,
                    Amount = obj.Amount,
                    discount_type = lang == Localization.Arabic ? obj.AmountType.Name : obj.AmountType.NameInEnglish,
                    AttachmentFile = _fileServer.CombinePath(dir, obj.Attachment)
                },
                Check = true
            };
        }

        public async Task<Response<GetTransDeductionById>> UpdateTransDeductionAsync(int id, CreateTransDeductionRequest model, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await unitOfWork.TransDeductions.GetByIdAsync(id);
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





            if (!await unitOfWork.TransDeductions.ExistAsync(model.DeductionId))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Deduction]]
                };
            }

            if (!await unitOfWork.TransSalaryEffects.ExistAsync(model.SalaryEffectId))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.SalaryEffect]]
                };
            }
            if (!await unitOfWork.TransAmountTypes.ExistAsync(x => x.Id == model.AmountTypeId))

                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.AmountTypes]]
                };





            obj.Amount = model.Amount;
            obj.AmountTypeId = model.AmountTypeId;
            obj.SalaryEffectId = model.SalaryEffectId;
            obj.DeductionId = model.DeductionId;
            obj.ActionMonth = model.ActionMonth;
            obj.Notes = model.Notes;
            obj.EmployeeId = model.EmployeeId;
            obj.CompanyId = currentCompany;
            if (model.Attachment_File is not null)
            {
                var dirType = HrDirectoryTypes.Deductions;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                if (!string.IsNullOrEmpty(obj?.Attachment))
                    _fileServer.RemoveFile(dir, obj.Attachment);
                obj.Attachment = await _fileServer.UploadFileAsync(dir, model.Attachment_File);
            }
            unitOfWork.TransDeductions.Update(obj);
            await unitOfWork.CompleteAsync();

            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = mapper.Map<GetTransDeductionById>(obj)
            };
        }

        public async Task<Response<object>> RestoreTransDeductionAsync(int id)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await unitOfWork.TransDeductions.GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompany);

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

            unitOfWork.TransDeductions.Update(obj);
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


        public Task<Response<string>> UpdateActiveOrNotTransDeductionAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<string>> DeleteTransDeductionAsync(int id)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await unitOfWork.TransDeductions.GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompany);
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

            if (!string.IsNullOrEmpty(obj.Attachment))
            {
                var dirType = HrDirectoryTypes.Deductions;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                if (!string.IsNullOrEmpty(obj?.Attachment))
                    _fileServer.RemoveFile(dir, obj.Attachment);

            }
            unitOfWork.TransDeductions.Remove(obj);
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
