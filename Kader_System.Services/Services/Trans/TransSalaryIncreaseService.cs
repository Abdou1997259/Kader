using Kader_System.Domain.DTOs;
using Kader_System.Services.IServices;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Kader_System.Services.Services.Trans
{
    public class TransSalaryIncreaseService(IUnitOfWork unitOfWork, IUserContextService userContextService, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : ITransSalaryIncreaseService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharLocalizer = sharLocalizer;
        private readonly IMapper _mapper = mapper;
        private readonly IUserContextService _userContextService = userContextService;


        #region Old_Code
        //public async Task<int> AddNewSalaryIncrease(CreateTransSalaryIncreaseRequest createTransSalary)
        //{
        //    var empSalary = (await _unitOfWork.Contracts.GetSpecificSelectAsync(x => x.EmployeeId == createTransSalary.employeeId, x => x)).Select(X => X.FixedSalary).FirstOrDefault();
        //    #region SalaryTypesCases
        //    double salaryAfterIncrease = (SalaryIncreaseTypes)createTransSalary.salrayIncreaseTypeId switch
        //    {
        //        SalaryIncreaseTypes.Amount => createTransSalary.increaseValue + empSalary,
        //        SalaryIncreaseTypes.percentage => ((createTransSalary.increaseValue / 100) * empSalary) + empSalary,
        //        _ => empSalary,
        //    };
        //    #endregion

        //    var salaryIncrease = new TransSalaryIncrease()
        //    {
        //        Notes = createTransSalary.details,
        //        Amount = createTransSalary.increaseValue,
        //        Employee_id = createTransSalary.employeeId,
        //        Increase_type = createTransSalary.salrayIncreaseTypeId,
        //        transactionDate = DateTime.Now,
        //        dueDate = DateTime.Now.AddMonths(1),
        //        salaryAfterIncrease = salaryAfterIncrease,
        //    };
        //    await _unitOfWork.TransSalaryIncrease.AddAsync(salaryIncrease);
        //   return await _unitOfWork.CompleteAsync();
        //}

        //public async Task<Response<GetAllSalaryIncreaseResponse>> GetAllSalaryIncreaseAsync(string lang, GetAlFilterationForSalaryIncreaseRequest model, string host)
        //{
        //    Expression<Func<TransSalaryIncrease, bool>> filter = x => x.IsDeleted == model.IsDeleted &&
        //                                                  (string.IsNullOrEmpty(model.Word) || x.transactionDate.ToString() == model.Word);
        //    var totalRecords = await _unitOfWork.TransSalaryIncrease.CountAsync(filter: filter);
        //    int page = model.PageNumber < 1 ? 1 : model.PageNumber;
        //    int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
        //    var pageLinks = Enumerable.Range(1, totalPages)
        //        .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
        //        .ToList();
        //    var result = new GetAllSalaryIncreaseResponse
        //    {
        //        TotalRecords = totalRecords,
        //        Items = (await _unitOfWork.TransSalaryIncrease.GetSpecificSelectAsync(filter: filter,
        //             take: model.PageSize,
        //             skip: (page - 1) * model.PageSize,
        //             select: x => new TransSalaryIncreaseResponse
        //             {
        //                 employeeId = x.Employee_id,
        //                 details = x.Notes,
        //                 dueDate = x.dueDate,
        //                 transationDate = x.transactionDate,
        //                 increaseValue = x.Amount,
        //                 salrayIncreaseTypeId = x.Increase_type,
        //                 salrayIncreaseTypeName = lang == "ar" ? x.ValueType.Name : x.ValueType.NameInEnglish,
        //             }, orderBy: x => x.OrderByDescending(x => x.Id), includeProperties: "")).ToList(),
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

        //    if (result.TotalRecords == 0)
        //    {
        //        string resultMsg = _sharLocalizer[Localization.NotFoundData];

        //        return new()
        //        {
        //            Data = new()
        //            {
        //                Items = new List<TransSalaryIncreaseResponse>()
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

        //public async Task<TransSalaryIncreaseResponse> GetSalaryIncreaseById(int id)
        //{
        //    var result = (await _unitOfWork.TransSalaryIncrease.GetAllAsync(x => x.Id == id)).
        //        Select(q => new TransSalaryIncreaseResponse()
        //        {
        //            employeeId = q.Employee_id,
        //            details = q.Notes,
        //            dueDate = q.dueDate,
        //            transationDate = q.transactionDate,
        //            increaseValue = q.Amount,
        //            salrayIncreaseTypeId = q.Increase_type,
        //        }).FirstOrDefault();

        //    return result;
        //}

        #endregion

        private TransSalaryIncrease _insatance;
        public async Task<Response<IEnumerable<SelectListOfTransSalaryIncrementResponse>>> ListOfTransSalaryIncreaseAsync(string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var result =
                await _unitOfWork.TransSalaryIncrease.GetSpecificSelectAsync(x => x.CompanyId == currentCompany,
                    includeProperties: $"{nameof(_insatance.Employee)}",
                    select: x => new SelectListOfTransSalaryIncrementResponse
                    {
                        Id = x.Id,
                        transationDate = x.transactionDate,

                        employeeName = lang == Localization.Arabic ? x.Employee!.FullNameAr : x.Employee!.FullNameEn,
                        increaseValue = x.Amount,


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

        public async Task<Response<GetAllSalaryIncreaseResponse>> GetAllTransSalaryIncreaseAsync(string lang,
            GetAlFilterationForSalaryIncreaseRequest model, string host)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            Expression<Func<TransSalaryIncrease, bool>> filter = x =>
           x.IsDeleted == model.IsDeleted && x.CompanyId == currentCompany &&
           (string.IsNullOrEmpty(model.Word) ||
           x.transactionDate.ToString().Contains(model.Word)
        || x.Employee.FullNameAr.Contains(model.Word) || x.Employee.FullNameEn.Contains(model.Word) || x.Employee.User.FullName.Contains(model.Word));



            var totalRecords = await _unitOfWork.TransSalaryIncrease.CountAsync(filter: filter);
            int page = 1; var lookups = await _unitOfWork.Employees.GetEmployeesNameIdSalaryAsLookUp(lang);

            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            var users = await _unitOfWork.Users.GetAllAsync();
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link() { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();

            var transSalaryIncreases = await _unitOfWork.TransSalaryIncrease.GetSpecificSelectAsync(
                    filter: filter,
                    take: model.PageSize,
                    skip: (model.PageNumber - 1) * model.PageSize,
                    select: x => new
                    {
                        x.transactionDate,
                        x.salaryAfterIncrease,
                        x.PreviousSalary,
                        x.Added_by,
                        x.Employee_id,
                        x.Id,
                        EmployeeName = lang == Localization.Arabic ? x.Employee!.FullNameAr : x.Employee!.FullNameEn,
                        x.Amount,
                        IncreaseType = x.Increase_type,
                    },
                    includeProperties: "Employee.User,Employee",
                    orderBy: x => x.OrderByDescending(x => x.Id)
                );


            var result = new GetAllSalaryIncreaseResponse
            {
                TotalRecords = await _unitOfWork.TransSalaryIncrease.CountAsync(filter: filter),
                //? (x.grouIcemp.Increase_type == 2
                //                        ? (x.grouIcemp.Amount / 100) * g.Key.FixedSalary
                //                        : x.grouIcemp.Amount)
                Items = transSalaryIncreases
                    .AsEnumerable() // Switching to client-side evaluation for the next part
                    .Select(x => new TransSalaryIncreaseResponse
                    {
                        Id = x.Id,
                        EmployeeId = x.Employee_id,
                        TransactionDate = x.transactionDate,
                        AfterIncreaseSalary = x.salaryAfterIncrease,
                        PreviousSalary = x.PreviousSalary,
                        AddedBy = users.FirstOrDefault(u => u.Id == x.Added_by)?.UserName,
                        EmployeeName = x.EmployeeName,
                        IncreaseValue = x.IncreaseType == 2 ? (x.Amount / 100) * x.PreviousSalary : x.PreviousSalary,
                        SalaryIncreaseType = x.IncreaseType == 1 && lang == Localization.Arabic ? "قيمة" :
                                             x.IncreaseType == 2 && lang == Localization.Arabic ? "نسبة" :
                                             x.IncreaseType == 1 && lang == Localization.English ? "Value" :
                                             x.IncreaseType == 2 && lang == Localization.English ? "Percentage" :
                                             string.Empty
                    }).Where(x => !model.EmployeeId.HasValue || x.EmployeeId == model.EmployeeId).ToList(),
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
                Links = pageLinks,
            };

            if (result.TotalRecords is 0)
            {
                string resultMsg = _sharLocalizer[Localization.NotFoundData];

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

        public async Task<Response<CreateTransSalaryIncreaseRequest>>
            CreateTransSalaryIncreaseAsync(CreateTransSalaryIncreaseRequest model, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();

            var emp = await unitOfWork.Employees.GetFirstOrDefaultAsync(x => x.Id == model.Employee_id && x.CompanyId == currentCompany);
            if (emp is null)
            {


                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Employee]]
                };
            }
            var contract = (await unitOfWork.Contracts.GetSpecificSelectAsync(x
                => x.employee_id == model.Employee_id && !x.IsDeleted
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
            var newTrans = _mapper.Map<TransSalaryIncrease>(model);
            newTrans.CompanyId = currentCompany;
            newTrans.transactionDate = model.TransactionDate;
            var empSalary = (await _unitOfWork.Contracts.GetFirstOrDefaultAsync(x
                => x.employee_id == model.Employee_id && x.company_id == currentCompany)).fixed_salary;
            #region SalaryTypesCases
            double salaryAfterIncrease = (SalaryIncreaseTypes)model.Increase_type switch
            {
                SalaryIncreaseTypes.Amount => model.Amount + empSalary,
                SalaryIncreaseTypes.percentage => ((model.Amount / 100) * empSalary) + empSalary,

                _ => empSalary,
            };


            #endregion

            newTrans.salaryAfterIncrease = salaryAfterIncrease;
            newTrans.PreviousSalary = empSalary;

            await _unitOfWork.TransSalaryIncrease.AddAsync(newTrans);
            await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };
        }

        public async Task<Response<GetSalaryIncreaseByIdResponse>> GetTransSalaryIncreaseByIdAsync(int id, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await _unitOfWork.TransSalaryIncrease.GetFirstOrDefaultAsync(
       c => c.Id == id && c.CompanyId == currentCompany,
       includeProperties: $"{nameof(_insatance.ValueType)},{nameof(_insatance.Employee)}");

            if (obj is null)
            {
                string resultMsg = sharLocalizer[Localization.NotFoundData];
                return new()
                {
                    Data = new GetSalaryIncreaseByIdResponse(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            var salaryIncreaseTypes = await _unitOfWork.SalaryIncreaseTypeRepository.GetAllAsync();
            var salaryIncreaseTypeLookup = salaryIncreaseTypes
                .Select(x => new { x.Id, Name = lang == Localization.Arabic ? x.Name : x.NameInEnglish })
                .ToList();

            var employee = await _unitOfWork.Employees.GetFirstOrDefaultAsync(x => x.Id == obj.Employee_id && x.CompanyId == currentCompany);
            var employeeName = lang == Localization.Arabic ? employee?.FullNameAr : employee?.FullNameEn;

            var previousSalary = (await _unitOfWork.TransSalaryIncrease.GetEmployeeWithSalary(lang, currentCompany))
                .FirstOrDefault(x => x.Id == obj.Employee_id)?.Salary ?? 0;

            var lookups = await _unitOfWork.TransSalaryIncrease.GetEmployeeWithSalary(lang, currentCompany);
            var typeLookup = await _unitOfWork.SalaryIncreaseTypeRepository.GetSalaryIncreaseType(lang);

            return new()
            {
                Data = new GetSalaryIncreaseByIdResponse
                {
                    Amount = obj.Amount,
                    Employee_id = obj.Employee_id,
                    Increase_type = obj.Increase_type,
                    Notes = obj.Notes,
                    TransactionDate = obj.transactionDate,
                    EmployeeName = employeeName,
                    PerviousSalary = previousSalary,
                },
                Check = true,
                LookUps = new
                {
                    employees = lookups,
                    salaryIncreaseType = typeLookup,
                }
            };


        }
        public async Task<Response<IEnumerable<EmployeeWithSalary>>> GetEmployeesLookups(string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            return new()
            {
                Check = true,
                Data = await _unitOfWork.TransSalaryIncrease
                .GetEmployeeWithSalary(lang, currentCompany)
            };

        }

        public async Task<Response<CreateTransSalaryIncreaseRequest>> UpdateTransSalaryIncreaseAsync(int id, CreateTransSalaryIncreaseRequest model)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await _unitOfWork.TransSalaryIncrease.GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompany);
            var emp = await unitOfWork.Employees.GetFirstOrDefaultAsync(x => x.Id == model.Employee_id && x.CompanyId == currentCompany);
            if (emp is null)
            {


                return new()
                {
                    Check = false,
                    Msg = sharLocalizer[Localization.CannotBeFound, sharLocalizer[Localization.Employee]]
                };
            }
            var contract = (await unitOfWork.Contracts.GetSpecificSelectAsync(x =>
            x.employee_id == model.Employee_id && x.company_id == currentCompany, x => x)).FirstOrDefault();
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
            var empSalary = (await _unitOfWork.Contracts.GetFirstOrDefaultAsync(x =>
            x.employee_id ==
            model.Employee_id && x.company_id == currentCompany)).fixed_salary;
            if (obj is null)


            {
                string empMsg = sharLocalizer[Localization.Contract];
                string resultMsg = sharLocalizer[Localization.IsNotExisted, empMsg];

                return new()
                {
                    Data = new(),
                    Error = resultMsg,
                    Msg = resultMsg
                };
            }

            var IsCaculated = await _unitOfWork.TransSalaryCalculatorDetailsRepo.GetFirstOrDefaultAsync(x => x.EmployeeId == model.Employee_id);
            if (IsCaculated != null)
            {
                var msg = _sharLocalizer[Localization.Calculated];
                return new()
                {
                    Data = new(),
                    Error = msg,
                    Msg = msg
                };

            }
            #region SalaryTypesCases
            obj.transactionDate = model.TransactionDate;
            double salaryAfterIncrease = (SalaryIncreaseTypes)model.Increase_type switch
            {
                SalaryIncreaseTypes.Amount => model.Amount + empSalary,
                SalaryIncreaseTypes.percentage => ((model.Amount / 100) * empSalary) + empSalary,
                _ => empSalary,
            };
            #endregion

            obj.Notes = model.Notes;
            obj.Amount = model.Amount;
            obj.Increase_type = model.Increase_type;
            obj.transactionDate = model.TransactionDate;
            obj.CompanyId = currentCompany;
            obj.salaryAfterIncrease = salaryAfterIncrease;
            _unitOfWork.TransSalaryIncrease.Update(obj);
            await _unitOfWork.CompleteAsync();
            return new()
            {
                Msg = sharLocalizer[Localization.Done],
                Check = true,
            };
        }
        public async Task<Response<object>> RestoreTransSalaryIncreaseAsync(int id)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await _unitOfWork.TransSalaryIncrease.GetFirstOrDefaultAsync(x => x.CompanyId == currentCompany && x.Id == id);

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

            _unitOfWork.TransSalaryIncrease.Update(obj);
            await _unitOfWork.CompleteAsync();
            return new()
            {
                Error = string.Empty,
                Check = true,
                Data = obj,
                LookUps = null,
                Msg = sharLocalizer[Localization.Restored]
            };
        }
        public Task<Response<string>> UpdateActiveOrNotTransSalaryIncreaseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<string>> DeleteTransSalaryIncreaseAsync(int id)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var obj = await _unitOfWork.TransSalaryIncrease.GetFirstOrDefaultAsync(x => x.Id == id && x.CompanyId == currentCompany);
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

            obj.IsDeleted = true;
            _unitOfWork.TransSalaryIncrease.Update(obj);
            await _unitOfWork.CompleteAsync();
            return new()
            {
                Check = true,
                Data = string.Empty,
                Msg = sharLocalizer[Localization.Deleted]
            };
        }
    }
}
