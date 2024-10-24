using Kader_System.Domain.DTOs;
using Kader_System.Services.IServices.AppServices;

namespace Kader_System.Services.Services.Trans
{
    public class TransBenefitService(IUnitOfWork unitOfWork, IUserContextService userContextService, IFileServer fileServer, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : ITransBenefitService
    {
        private TransBenefit _insatance;
        private IFileServer _fileServer = fileServer;
        private IUserContextService _userContextService = userContextService;
        public async Task<Response<IEnumerable<SelectListOfTransBenefitResponse>>> ListOfTransBenefitsAsync(string lang)
        {

            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var result = await unitOfWork.TransBenefits.GetSpecificSelectAsync(x => x.company_id == currentCompany,
                includeProperties: $"{nameof(_insatance.benefit)},{nameof(_insatance.employee)}" +
                $",{nameof(_insatance.salary_effect)}" +
                                   $",{nameof(_insatance.amount_type)}",



                select: x => new SelectListOfTransBenefitResponse
                {
                    Id = x.Id,
                    ActionMonth = x.action_month,
                    SalaryEffect = lang == Localization.Arabic ? x.salary_effect!.Name : x.salary_effect!.NameInEnglish,
                    AddedOn = x.Add_date,
                    BenefitId = x.benefit_id,
                    BenefitName = lang == Localization.Arabic ? x.benefit!.Name_ar : x.benefit!.Name_en,
                    Amount = x.amount,
                    EmployeeId = x.employee_id,
                    EmployeeName = lang == Localization.Arabic ? x.employee!.FullNameAr : x.employee!.FullNameEn,
                    Notes = x.notes,
                    SalaryEffectId = x.salary_effect_id,
                    AmountTypeId = x.amount_type_id,
                    ValueTypeName = lang == Localization.Arabic ? x.amount_type!.Name : x.amount_type
                    !.NameInEnglish
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

        public async Task<Response<GetAllTransBenefitResponse>> GetAllTransBenefitsAsync(string lang,
            GetAllFilterationForTransBenefitRequest model, string host)
        {

            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            Expression<Func<TransBenefit, bool>> filter = x => x.IsDeleted == model.IsDeleted &&
            x.company_id == currentCompany
                && (string.IsNullOrEmpty(model.Word) || x.action_month.ToString().Contains(model.Word)
                || x.amount_type!.Name.Contains(model.Word) || x.amount_type!.NameInEnglish.Contains(model.Word)
                || x.benefit!.Name_en.Contains(model.Word)
                || x.benefit!.Name_ar.Contains(model.Word)
                || x.employee!.FullNameEn.Contains(model.Word)
                || x.employee!.FullNameAr.Contains(model.Word)
                  && (!model.EmployeeId.HasValue || x.employee.Id == model.EmployeeId)
                    );
            Expression<Func<TransBenefitData, bool>> filterSearch = x =>
                (string.IsNullOrEmpty(model.Word)
                 || x.BenefitName.Contains(model.Word)
                 || x.EmployeeName.Contains(model.Word)
                 || x.ValueTypeName.Contains(model.Word)
                   && (!model.EmployeeId.HasValue || x.EmployeeId == model.EmployeeId));

            var totalRecords = await unitOfWork.TransBenefits.CountAsync(filter: filter,
                includeProperties: $"{nameof(_insatance.benefit)},{nameof(_insatance.employee)}," +
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
            var result = new GetAllTransBenefitResponse
            {
                TotalRecords = totalRecords,

                Items = (unitOfWork.TransBenefits.GetTransBenefitInfo(filter: filter, filterSearch: filterSearch,
                    skip: (model.PageNumber - 1) * model.PageSize, take: model.PageSize, lang)).Where(x => !model.EmployeeId.HasValue || x.EmployeeId == model.EmployeeId).OrderByDescending(x => x.Id).ToList()
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

        public async Task<Response<BenefitLookUps>> GetBenefitsLookUpsData(string lang)
        {
            try
            {
                var currentCompany = await _userContextService.GetLoggedCurrentCompany();
                var employees = await unitOfWork.Employees.GetEmployeesDataNameAndIdAsCustomTypeLookUp(lang, currentCompany);
                var benefits = await unitOfWork.Benefits.GetSpecificSelectAsync(filter =>

                !filter.IsDeleted,
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

                return new Response<BenefitLookUps>()
                {
                    Check = true,
                    IsActive = true,
                    Error = "",
                    Msg = "",
                    Data = new BenefitLookUps()
                    {
                        benefit = benefits.ToArray(),
                        employees = employees.ToList(),
                        salary_effects = salaryEffect.ToArray(),
                        trans_amount_types = amountType.ToArray()
                    }
                };
            }
            catch (Exception exception)
            {
                return new Response<BenefitLookUps>()
                {
                    Error = exception.InnerException != null ? exception.InnerException.Message : exception.Message,
                    Msg = "Can not able to Get Data",
                    Check = false,
                    Data = null,
                    IsActive = false
                };
            }

        }

        public async Task<Response<CreateTransBenefitRequest>> CreateTransBenefitAsync(
            CreateTransBenefitRequest model, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var emp = await unitOfWork.Employees.GetFirstOrDefaultAsync(x =>
            x.Id == model.employee_id && x.CompanyId == currentCompany);
            if (emp is null)
            {


                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Employee]]
                };
            }
            var contract = (await unitOfWork.Contracts.GetSpecificSelectAsync(x => x.employee_id ==
            model.employee_id && !x.IsDeleted && x.company_id == currentCompany, x => x)).FirstOrDefault();
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


            if (!await unitOfWork.Benefits.ExistAsync(model.benefit_id))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Benefit]]
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


            if (await unitOfWork.TransBenefits.ExistAsync(x =>
           x.employee_id ==
            model.employee_id && x.company_id == currentCompany &&
                x.benefit_id == model.benefit_id &&
                x.action_month == model.action_month))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.TodayTrans,
                    Localization.Arabic == lang ? emp.FullNameAr : emp.FullNameEn]
                };
            }

            var newTrans = mapper.Map<TransBenefit>(model);

            if (model.attachment_file is not null)
            {
                var dirType = HrDirectoryTypes.Benefits;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                newTrans.attachment = await _fileServer.UploadFileAsync(dir, model.attachment_file);

            }
            newTrans.company_id = currentCompany;
            await unitOfWork.TransBenefits.AddAsync(newTrans);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = model
            };
        }

        public async Task<Response<GetTransBenefitById>> GetTransBenefitByIdAsync(int id, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await unitOfWork.TransBenefits.GetFirstOrDefaultAsync(b => b.Id == id &&
            b.IsDeleted == false && b.company_id == currentCompany,
                includeProperties: $"{nameof(_insatance.benefit)},{nameof(_insatance.employee)}," +
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
            var dirType = HrDirectoryTypes.Benefits;
            var dir = dirType.GetModuleNameWithType(Modules.Trans);

            return new()
            {
                Data = new GetTransBenefitById()
                {
                    ActionMonth = obj.action_month,
                    AddedOn = obj.Add_date,
                    BenefitId = obj.benefit_id,
                    BenefitName = lang == Localization.Arabic ?
                    obj.benefit!.Name_ar : obj.benefit!.Name_en,
                    EmployeeId = obj.employee_id,
                    EmployeeName = lang == Localization.Arabic ? obj.employee!.FullNameAr : obj.employee.FullNameEn,
                    Id = obj.Id,
                    benefits_type = lang == Localization.Arabic ? obj.salary_effect!.Name : obj.salary_effect!.NameInEnglish,
                    SalaryEffectId = obj.salary_effect_id,
                    Notes = obj.notes,
                    increase_type_id = obj.amount_type_id,
                    increase_type = lang == Localization.Arabic ? obj.amount_type!.Name
                    : obj.amount_type!.NameInEnglish,
                    Amount = obj.amount,
                    AttachmentFile = _fileServer.CombinePath(dir, obj.attachment)
                },
                Check = true
            };
        }

        public async Task<Response<GetTransBenefitById>> UpdateTransBenefitAsync(int id, CreateTransBenefitRequest model, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await unitOfWork.TransBenefits.GetFirstOrDefaultAsync(x => x.Id == id
            && x.company_id == currentCompany);
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
            var contract = (await unitOfWork.Contracts.GetSpecificSelectAsync(x =>
            x.employee_id == model.employee_id && x.company_id == currentCompany,
            x => x)).FirstOrDefault();
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

            if (!await unitOfWork.Benefits.ExistAsync(model.benefit_id))
            {
                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound,
                    sharLocalizer[Localization.Benefit]]
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



            if (model.attachment_file is not null)
            {
                var dirType = HrDirectoryTypes.Benefits;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                if (!string.IsNullOrEmpty(obj?.attachment))
                    _fileServer.RemoveFile(dir, obj.attachment);
                obj.attachment = await _fileServer.UploadFileAsync(dir, model.attachment_file);
            }



            obj.amount = model.amount;
            obj.amount_type_id = model.increase_type_id;
            obj.salary_effect_id = model.salary_effect_id;
            obj.benefit_id = model.benefit_id;
            obj.action_month = model.action_month;
            obj.notes = model.notes;
            obj.employee_id = model.employee_id;
            obj.company_id = currentCompany;
            unitOfWork.TransBenefits.Update(obj);
            await unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
                Data = mapper.Map<GetTransBenefitById>(obj)
            };
        }

        public Task<Response<string>> UpdateActiveOrNotTransBenefitAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<Response<object>> RestoreTransBenefitAsync(int id)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await unitOfWork.TransBenefits.GetFirstOrDefaultAsync(x => x.Id == id &&
            x.company_id == currentCompany);

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

            unitOfWork.TransBenefits.Update(obj);
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

        public async Task<Response<string>> DeleteTransBenefitAsync(int id)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await unitOfWork.TransBenefits.GetFirstOrDefaultAsync(x => x.Id
            == id && x.company_id == currentCompany);
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
                var dirType = HrDirectoryTypes.Benefits;
                var dir = dirType.GetModuleNameWithType(Modules.Trans);
                if (!string.IsNullOrEmpty(obj?.attachment))
                    _fileServer.RemoveFile(dir, obj.attachment);

            }
            unitOfWork.TransBenefits.Remove(obj);
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
