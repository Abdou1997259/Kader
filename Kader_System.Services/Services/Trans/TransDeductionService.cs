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
                await unitOfWork.TransDeductions.GetSpecificSelectAsync(x => x.company_id
                == currentCompany,
                    includeProperties: $"{nameof(_insatance.deduction)}," +
                    $"{nameof(_insatance.employee)},{nameof(_insatance.salary_effect)}" +
                                       $",{nameof(_insatance.amount_type)}",
                    select: x => new SelectListOfTransDeductionResponse
                    {
                        Id = x.id,
                        ActionMonth = x.action_month,
                        SalaryEffect = lang == Localization.Arabic ? x.salary_effect!.Name
                        : x.salary_effect!.NameInEnglish,
                        AddedOn = x.Add_date,
                        DeductionId = x.deduction_id,
                        DeductionName = lang == Localization.Arabic ? x.deduction!.Name_ar
                        : x.deduction!.Name_en,
                        Amount = x.amount,
                        EmployeeId = x.employee_id,
                        EmployeeName = lang == Localization.Arabic ? x.employee!.FullNameAr
                        : x.employee!.FullNameEn,
                        Notes = x.notes,
                        SalaryEffectId = x.salary_effect_id,
                        AmountTypeId = x.amount_type_id,
                        ValueTypeName = lang == Localization.Arabic ? x.amount_type!.Name
                        : x.amount_type!.NameInEnglish
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

        public async Task<Response<GetAllTransDeductionResponse>> GetAllTransDeductionsAsync(string lang,
            GetAllFilterationForTransDeductionRequest model, string host)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            Expression<Func<TransDeduction, bool>> filter = x =>
                x.IsDeleted == model.IsDeleted && x.company_id == currentCompany &&
                (
                    (string.IsNullOrEmpty(model.Word) || x.action_month.ToString().Contains(model.Word)
                     || x.amount_type!.Name.Contains(model.Word)
                     || x.amount_type!.NameInEnglish.Contains(model.Word)
                     || x.deduction!.Name_en.Contains(model.Word)
                     || x.deduction!.Name_ar.Contains(model.Word)
                     || x.employee!.FullNameEn.Contains(model.Word)
                     || x.employee!.FullNameAr.Contains(model.Word))
                    && (!model.EmployeeId.HasValue || x.employee_id == model.EmployeeId)
                );

            Expression<Func<TransDeductionData, bool>> filterSearch = x =>
                (string.IsNullOrEmpty(model.Word)
                 || x.DeductionName.Contains(model.Word)
                 || x.EmployeeName.Contains(model.Word)
                 || x.DiscountType.Contains(model.Word));

            var totalRecords = await unitOfWork.TransDeductions.CountAsync(filter: filter,
                includeProperties: $"{nameof(_insatance.deduction)},{nameof(_insatance.employee)}," +
                                   $"{nameof(_insatance.salary_effect)}" +
                                   $",{nameof(_insatance.amount_type)}");
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
                var employees = await unitOfWork.Employees.GetEmployeesDataNameAndIdAsLookUp(lang, currentCompany);

                var deductions = await unitOfWork.Deductions
                    .GetSpecificSelectAsync(filter => filter.IsDeleted == false,
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
                        employees = employees,
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
            var emp = await unitOfWork.Employees.GetFirstOrDefaultAsync(x => x.Id ==
            model.employee_id && x.CompanyId == currentCompany);
            if (emp is null)
            {


                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Employee]]
                };
            }
            var contract = (await unitOfWork.Contracts
                .GetSpecificSelectAsync(x => x.employee_id == model.employee_id && !x.IsDeleted &&
                x.company_id == currentCompany, x => x)).FirstOrDefault();
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

            if (!await unitOfWork.Deductions.ExistAsync(model.deduction_id))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Deduction]]
                };
            }
            if (!await unitOfWork.TransSalaryEffects.ExistAsync(model.salary_effect_id))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.SalaryEffect]]
                };
            }
            if (!await unitOfWork.TransAmountTypes.ExistAsync(x => x.Id == model.amount_type_id))

            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.AmountTypes]]
                };

            }

            if (await unitOfWork.TransDeductions.ExistAsync(x => x.employee_id ==
            model.employee_id && x.company_id == currentCompany &&
            x.deduction_id == model.deduction_id &&
            x.action_month == model.action_month))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.TodayTrans,
                    Localization.Arabic == lang ? emp.FullNameAr : emp.FullNameEn]
                };
            }
            var newTrans = mapper.Map<TransDeduction>(model);




            if (model.attachment_file is not null)
            {
                var dirType = HrDirectoryTypes.Deductions;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                newTrans.attachment = await _fileServer.UploadFileAsync(dir,
                    model.attachment_file);

            }



            newTrans.company_id = currentCompany;
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
            var obj = await unitOfWork.TransDeductions.GetFirstOrDefaultAsync(d => d.id
            == id && d.company_id == currentCompany,
                includeProperties: $"{nameof(_insatance.deduction)},{nameof(_insatance
                .employee)}," +
                                   $"{nameof(_insatance.salary_effect)}" +
                                   $",{nameof(_insatance.amount_type)}");

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
                    ActionMonth = obj.action_month,
                    AddedOn = obj.Add_date,
                    DeductionId = obj.deduction_id,
                    DeductionName = lang == Localization.Arabic ? obj.deduction!.Name_ar
                    : obj.deduction!.Name_en,
                    EmployeeId = obj.employee_id,
                    EmployeeName = lang == Localization.Arabic ? obj.employee!.
                    FullNameAr : obj.employee.FullNameEn,
                    Id = obj.id,
                    SalaryEffect = lang == Localization.Arabic ? obj.salary_effect!.Name
                    : obj.salary_effect!.NameInEnglish,
                    SalaryEffectId = obj.salary_effect_id,
                    Notes = obj.notes,
                    AmountTypeId = obj.amount_type_id,
                    Amount = obj.amount,
                    discount_type = lang == Localization.Arabic ?
                    obj.amount_type.Name : obj.amount_type.NameInEnglish,
                    AttachmentFile = _fileServer.CombinePath(dir, obj.attachment)
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
            var contract = (await unitOfWork.Contracts.GetSpecificSelectAsync(x
                => x.employee_id == model.employee_id &&
                x.company_id == currentCompany, x => x)).FirstOrDefault();
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





            if (!await unitOfWork.Deductions.ExistAsync(model.deduction_id))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Deduction]]
                };
            }

            if (!await unitOfWork.TransSalaryEffects.ExistAsync(model.salary_effect_id))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.SalaryEffect]]
                };
            }
            if (!await unitOfWork.TransAmountTypes.ExistAsync(x => x.Id ==
            model.amount_type_id))

                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.AmountTypes]]
                };





            obj.amount = model.amount;
            obj.amount_type_id = model.amount_type_id;
            obj.salary_effect_id = model.salary_effect_id;
            obj.deduction_id = model.deduction_id;
            obj.action_month = model.action_month;
            obj.notes = model.notes;
            obj.employee_id = model.employee_id;
            obj.company_id = currentCompany;
            if (model.attachment_file is not null)
            {
                var dirType = HrDirectoryTypes.Deductions;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                if (!string.IsNullOrEmpty(obj?.attachment))
                    _fileServer.RemoveFile(dir, obj.attachment);
                obj.attachment = await _fileServer.UploadFileAsync(dir, model.attachment_file);
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
            var obj = await unitOfWork.TransDeductions.GetFirstOrDefaultAsync(x => x.id
            == id && x.company_id == currentCompany);

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
            var obj = await unitOfWork.TransDeductions.GetFirstOrDefaultAsync(x
                => x.id == id && x.company_id == currentCompany);
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
                var dirType = HrDirectoryTypes.Deductions;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                if (!string.IsNullOrEmpty(obj?.attachment))
                    _fileServer.RemoveFile(dir, obj.attachment);

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
