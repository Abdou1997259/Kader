
using Kader_System.Domain.DTOs;


namespace Kader_System.Services.Services.Trans;
public class TransAllowanceService(IUnitOfWork unitOfWork, IUserContextService userContextService, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : ITransAllowanceService
{
    private TransAllowance _insatance;
    private IUserContextService _userContextService = userContextService;
    #region Retreive

    public async Task<Response<IEnumerable<SelectListForTransAllowancesResponse>>> ListOfTransAllowancesAsync(string lang)
    {
        var result =
            await unitOfWork.TransAllowances.GetSpecificSelectAsync(null!,
                includeProperties: $"{nameof(_insatance.Allowance)},{nameof(_insatance.Employee)}" +
                $",{nameof(_insatance.SalaryEffect)}",
                select: x => new SelectListForTransAllowancesResponse
                {
                    Id = x.Id,
                    ActionDate = x.ActionMonth,
                    SalaryEffect = lang == Localization.Arabic ? x.SalaryEffect!.Name : x.SalaryEffect!.NameInEnglish,
                    AddedOn = x.Add_date,
                    AllowanceId = x.AllowanceId,
                    AllowanceName = lang == Localization.Arabic ? x.Allowance!.Name_ar : x.Allowance!.Name_en,
                    Amount = x.Amount,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = lang == Localization.Arabic ? x.Employee!.FullNameAr : x.Employee!.FullNameEn,
                    Notes = x.Notes,
                    SalaryEffectId = x.SalaryEffectId
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

    public async Task<Response<TransAllowanceGetAllResponse>> GetAllTransAllowancesAsync(string lang,
        GetAllFilterationAllowanceRequest model, string host)
    {
        var currentCompany = await _userContextService.GetLoggedCurrentCompany();
        Expression<Func<TransAllowance, bool>> filter = x => x.IsDeleted == model.IsDeleted && x.CompanyId == currentCompany
                                                             && (string.IsNullOrEmpty(model.Word)
                                                               || x.ActionMonth.ToString().Contains(model.Word)
                                                                 || x.Allowance!.Name_en.Contains(model.Word)
                                                                 || x.Allowance!.Name_ar.Contains(model.Word)
                                                                 || x.Employee!.FullNameEn.Contains(model.Word)
                                                                 || x.Employee!.FullNameAr.Contains(model.Word)
                                                                     && (!model.EmployeeId.HasValue || x.EmployeeId == model.EmployeeId));
        ;

        Expression<Func<TransAllowanceData, bool>> filterSearch = x =>
            (string.IsNullOrEmpty(model.Word)
             || x.AllowanceName.Contains(model.Word)
             || x.EmployeeName.Contains(model.Word)
                 && (!model.EmployeeId.HasValue || x.EmployeeId == model.EmployeeId));

        var totalRecords = await unitOfWork.TransAllowances.CountAsync(filter: filter,
            includeProperties: $"{nameof(_insatance.Allowance)},{nameof(_insatance.Employee)},{nameof(_insatance.SalaryEffect)}");


        int page = 1;
        int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
        if (model.PageNumber < 1)
            page = 1;
        else
            page = model.PageNumber;
        var pageLinks = Enumerable.Range(1, totalPages)
            .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
            .ToList();
        var result = new TransAllowanceGetAllResponse
        {
            TotalRecords = totalRecords,

            Items = unitOfWork.TransAllowances.GetTransAllowanceInfo(filter: filter, filterSearch: filterSearch, skip: (model.PageNumber - 1) * model.PageSize
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

    public async Task<Response<TransactionAllowanceGetByIdResponse>> GetTransAllowanceByIdAsync(int id, string lang)
    {
        var currentCompany = await _userContextService.GetLoggedCurrentCompany();
        var obj = await unitOfWork.TransAllowances.GetFirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false && a.CompanyId == currentCompany,
            includeProperties: $"{nameof(_insatance.Allowance)},{nameof(_insatance.Employee)},{nameof(_insatance.SalaryEffect)}");

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

        return new()
        {
            Data = new TransactionAllowanceGetByIdResponse()
            {
                ActionMonth = obj.ActionMonth,
                AddedOn = obj.Add_date,
                AllowanceId = obj.AllowanceId,
                Amount = obj.Amount,
                EmployeeId = obj.EmployeeId,
                Id = obj.Id,
                SalaryEffectId = obj.SalaryEffectId,
                Notes = obj.Notes,
                AllowanceName = lang == Localization.Arabic ? obj.Allowance!.Name_ar : obj.Allowance!.Name_en,
                EmployeeName = lang == Localization.Arabic ? obj.Employee!.FullNameAr : obj.Employee!.FullNameEn,
                SalaryEffectName = lang == Localization.Arabic ? obj.SalaryEffect!.Name : obj.SalaryEffect!.NameInEnglish
            },
            Check = true
        };
    }


    public async Task<Response<TransAllowanceLookUpsData>> GetAllowancesLookUpsData(string lang)
    {
        try
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var employees = await unitOfWork.Employees.
                GetEmployeesDataNameAndIdAsCustomTypeLookUp(lang, currentCompany);

            var allowances = await unitOfWork.Allowances.GetSpecificSelectAsync(filter => filter.IsDeleted == false,
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

            return new Response<TransAllowanceLookUpsData>()
            {
                Check = true,
                IsActive = true,
                Error = "",
                Msg = "",
                Data = new TransAllowanceLookUpsData()
                {
                    allowances = allowances.ToArray(),
                    employees = employees,
                    salary_effects = salaryEffect.ToArray(),
                }
            };
        }
        catch (Exception exception)
        {
            return new Response<TransAllowanceLookUpsData>()
            {
                Error = exception.InnerException != null ? exception.InnerException.Message : exception.Message,
                Msg = "Can not able to Get Data",
                Check = false,
                Data = null,
                IsActive = false
            };
        }

    }
    #endregion

    #region Create
    public async Task<Response<CreateTransAllowanceRequest>> CreateTransAllowanceAsync
        (CreateTransAllowanceRequest model, string lang)
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
        var contract = (await unitOfWork.Contracts.GetSpecificSelectAsync(x
            => x.employee_id == model.EmployeeId && x.company_id == currentCompany, x => x)).FirstOrDefault();
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
        if (await unitOfWork.TransAllowances.ExistAsync(x => x.EmployeeId ==
        model.EmployeeId && x.CompanyId == currentCompany &&
          x.AllowanceId == model.AllowanceId && !x.IsDeleted &&

                DateOnly.FromDateTime(x.Add_date.Value) == DateOnly.FromDateTime(DateTime.Now)))
        {
            return new()
            {
                Check = false,
                Msg = sharLocalizer[Localization.TodayTrans,
                Localization.Arabic == lang ? emp.FullNameAr : emp.FullNameEn]
            };
        }
        if (await unitOfWork.TransAllowances.ExistAsync(x => x.EmployeeId ==
      model.EmployeeId && x.CompanyId == currentCompany &&
        x.AllowanceId == model.AllowanceId &&

              DateOnly.FromDateTime(x.Add_date.Value) == DateOnly.FromDateTime(DateTime.Now) && x.IsDeleted))
        {
            return new()
            {
                Check = false,
                Msg = sharLocalizer[Localization.TodayTransDeleted,
                Localization.Arabic == lang ? emp.FullNameAr : emp.FullNameEn]
            };
        }
        var newTrans = mapper.Map<TransAllowance>(model);
        newTrans.CompanyId = currentCompany;
        await unitOfWork.TransAllowances.AddAsync(newTrans);

        await unitOfWork.CompleteAsync();
        return new()
        {
            Msg = sharLocalizer[Localization.Done],
            Check = true,
            Data = model
        };
    }

    #endregion

    #region Update
    public async Task<Response<TransactionAllowanceGetByIdResponse>> UpdateTransAllowanceAsync(int id, CreateTransAllowanceRequest model)
    {

        var obj = await unitOfWork.TransAllowances.GetByIdAsync(id);
        var currentCompany = await _userContextService.GetLoggedCurrentCompany();

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
        var contract = (await unitOfWork.Contracts.GetSpecificSelectAsync(
            x => x.employee_id == model.EmployeeId && x.company_id == currentCompany,
            x => x)).FirstOrDefault();
        if (contract is null)
        {
            string resultMsg = $" {sharLocalizer[Localization.Employee]} " +
                $"{sharLocalizer[Localization.ContractNotFound]}";

            return new()
            {
                Check = false,
                Error = resultMsg,
                Msg = resultMsg
            };
        }
        obj.AllowanceId = model.AllowanceId;
        obj.Amount = model.Amount;
        obj.EmployeeId = model.EmployeeId;
        obj.Notes = model.Notes;
        obj.SalaryEffectId = model.SalaryEffectId;
        obj.ActionMonth = model.ActionMonth;
        obj.CompanyId = currentCompany;
        unitOfWork.TransAllowances.Update(obj);
        await unitOfWork.CompleteAsync();
        return new()
        {
            Msg = sharLocalizer[Localization.Done],
            Check = true,
            Data = mapper.Map<TransactionAllowanceGetByIdResponse>(obj)
        };
    }

    public Task<Response<string>> UpdateActiveOrNotTransAllowanceAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<object>> RestoreTransAllowanceAsync(int id)
    {
        var currentCompany = await _userContextService.GetLoggedCurrentCompany();
        var obj = await unitOfWork.TransAllowances.GetFirstOrDefaultAsync(x => x.CompanyId == currentCompany && x.Id == id);

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

        unitOfWork.TransAllowances.Update(obj);
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
    #endregion

    #region Delete
    public async Task<Response<string>> DeleteTransAllowanceAsync(int id)
    {
        var currentCompany = await _userContextService.GetLoggedCurrentCompany();
        var obj = await unitOfWork.TransAllowances.GetFirstOrDefaultAsync(x =>
        x.CompanyId == currentCompany && x.Id == id);
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
        unitOfWork.TransAllowances.Remove(obj);
        await unitOfWork.CompleteAsync();
        return new()
        {
            Check = true,
            Data = string.Empty,
            Msg = sharLocalizer[Localization.Deleted]
        };
    }
    #endregion

}
