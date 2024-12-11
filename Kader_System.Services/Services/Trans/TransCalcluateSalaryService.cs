
using Kader_System.DataAccesss.Context;
using Kader_System.Domain.DTOs;
using Kader_System.Services.Design_Patterns;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Kader_System.Services.Services.Trans
{
    public class TransCalcluateSalaryService(
        IUnitOfWork unitOfWork,
        IUserContextService userContextService,
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<SharedResource>
        localizer,
        KaderDbContext context




        ) : ITransCalcluateSalaryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer = localizer;
        private readonly IUserContextService _userContextService = userContextService;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly KaderDbContext _context = context;

        public async Task<Response<string>> CalculateSalary(UpdateCalculateSalaryModelRequest request)
        {

            FinalSalaryCalculator cal = new FinalSalaryCalculator(_unitOfWork, _localizer, _userContextService, _context);
            var result = await cal.Calculate(request);

            if (result.EmpNotExited)
            {
                var msg = $"{_localizer[Localization.Contract]}  {_localizer[Localization.NotFound]}";
                return new()
                {
                    Check = false,
                    Msg = msg
                };
            }
            return new()
            {
                Check = true,
                Msg = _localizer[Localization.Calculated]

            };
        }


        public async Task<Response<string>> DeleteCalculator(int Id)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var transaction = await _unitOfWork.TransSalaryCalculator.GetFirstOrDefaultAsync(x => x.Id == Id && x.CompanyId == currentCompany);
            if (transaction == null)
            {
                string msgs = _localizer[Localization.NotFoundData];
                return new()
                {
                    Data = null,
                    Msg = msgs,
                    Check = false
                };

            }
            _unitOfWork.TransSalaryCalculator.Remove(transaction);
            var transactions = await _unitOfWork.TransSalaryCalculatorDetailsRepo
                .GetSpecificSelectAsync(x => x.TransSalaryCalculatorsId == transaction.Id && x.CompanyId == currentCompany, x => x);

            _unitOfWork.TransSalaryCalculatorDetailsRepo.RemoveRange(transactions);
            await _unitOfWork.CompleteAsync();

            var msg = _localizer[Localization.Deleted];
            return new()
            {

                Data = msg,

                Msg = msg,
                Check = true
            };


        }

        public async Task<Response<GetSalaryCalculatorResponse>> GetAllCalculators(GetSalaryCalculatorFilterRequest model, string host, string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            Expression<Func<TransSalaryCalculator, bool>> filter =
                x => x.IsDeleted == model.IsDeleted
                && x.CompanyId == currentCompany
                ;

            var empolyeeWithJobs = await _unitOfWork.Employees.GetSpecificSelectAsync(x => true, x => x, includeProperties: "Job");
            var totalRecords = await _unitOfWork.TransSalaryCalculator.CountAsync(filter: filter);
            int page = 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / (model.PageSize == 0 ? 10 : model.PageSize));
            if (model.PageNumber < 1)
                page = 1;
            else
                page = model.PageNumber;
            var pageLinks = Enumerable.Range(1, totalPages)
                .Select(p => new Link { label = p.ToString(), url = host + $"?PageSize={model.PageSize}&PageNumber={p}&IsDeleted={model.IsDeleted}", active = p == model.PageNumber })
                .ToList();
            var transactions = await _unitOfWork.TransSalaryCalculator.GetSpecificSelectAsync(x => x.IsDeleted == false, x => x, includeProperties: "TransSalaryCalculatorsDetails");
            var msg = _localizer[Localization.NotFoundData];
            if (transactions is null)
            {

                return new()
                {
                    Data = null,
                    Msg = msg,
                    Check = false
                };
            }

            var data = (await _unitOfWork.TransSalaryCalculator.
                GetSpecificSelectAsync(filter: filter,
                    take: model.PageSize,
                    skip: (model.PageNumber - 1) * model.PageSize,
                    select: x => new GetSalaryCalculatorList
                    {
                        Id = x.Id,

                        Status = x.Status,
                        Total = x.TransSalaryCalculatorsDetails.Sum(s => s.Total),
                        CalculationDate = x.CalculationDate,
                        AddedDate = DateOnly.FromDateTime(x.Add_date.Value),
                        AddedBy = x.User.FullName,
                        JobName = lang == Localization.Arabic ? x.User.Job.name_ar : x.User.Job.name_en

                    }, orderBy: x =>
                x.OrderByDescending(x => x.Id), includeProperties:
                "TransSalaryCalculatorsDetails,User.Job")).ToList();




            var result = new GetSalaryCalculatorResponse
            {
                TotalRecords = await _unitOfWork.TransSalaryCalculator.CountAsync(filter: filter),

                Items = data


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
                string resultMsg = _localizer[Localization.NotFoundData];

                return new()
                {
                    Data = null,
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

        public async Task<Response<SalaryResponse>> GetById(int id, string lang)
        {

            var obj = await _unitOfWork.TransSalaryCalculator.GetFirstOrDefaultAsync(x => x.Id
            == id, includeProperties: "TransSalaryCalculatorsDetails.Employee.Contract");



            if (obj == null)
            {
                return new Response<SalaryResponse>()
                {
                    Check = false,
                    Msg = _localizer[Localization.NotFound]
                };
            };

            var result = obj.TransSalaryCalculatorsDetails.Select(x => new GetSalariesEmployeeResponse
            {
                EmployeeId = x.EmployeeId,
                EmployeeName = lang == Localization.Arabic ? x.Employee.FullNameAr : x.Employee.FullNameEn,
                DisbursementType = x.Employee.SalaryPaymentWayId,

                HousingAllowances = x.Employee.Contract?.FirstOrDefault(c => !c.IsDeleted).housing_allowance,
                Absents = new Absent
                {
                    Days = 0,
                    Sum = 0
                },
                BasicSalary = x.BasicSalary,

                MinuesValues = new MinuesValues
                {
                    Deductions = new List<Deduction>
                                     {
                           new Deduction
                           {

                               Value = x.TotalDeductions
                           }
                       },
                    Loans = new List<Loan> { new Loan
                       {

                                 Value=x.TotalLoans
                       } }
                },


                TotalAll = x.Total,
                TotalAdditionalValues = x.TotalAllownces + x.TotalBenefits,
                TotalMinues = x.TotalLoans + x.TotalDeductions,
                AdditionalValues = new List<AdditionalValues> {
                   new AdditionalValues
                   {
                       Id=1,
                       Name=lang==Localization.Arabic?"استحقاقات":"Benefits",
                       Value=x.TotalBenefits
                   }
                   ,new AdditionalValues
                   {
                       Id=2,
                       Name=lang==Localization.Arabic?"البدلات":"Allowance",
                       Value=x.TotalAllownces
                   }

                   }


            });

            var headers = new Header
            {
                WorkedDays = lang == Localization.Arabic ? "ايام العمل" : "Working Days",
                TotalAll = lang == Localization.Arabic ? "الاجمالي" : "Total",
                TotalMinues = lang == Localization.Arabic ? "مجموع الحسميان" : "Total Minues",
                TotalAdditionalValues = lang == Localization.Arabic ? "مجموع الاضافات" : "Total Additional",
                Absent = lang == Localization.Arabic ? new[] { "الايام", "مبلغ" } : new[] { "Days", "Amount" },
                EmployeeId = lang == Localization.Arabic ? "الرقم الوظيفي" : "Employee Id",
                EmployeeName = lang == Localization.Arabic ? "الاسم الوظيفي" : "Employee Name",
                Fixed = lang == Localization.Arabic ? "الاساسي" : "Fixed",
                AdditionalValues = lang == Localization.Arabic ? new[] { "استحقاقات", "البدلات" } : new[] { "Benefits", "Allowance" },
                MinuesValues = lang == Localization.Arabic ? new[] { "السلف", "الخصومات" } : new[] { "Loans", "Deductions" },
                PaymentMethod = lang == Localization.Arabic ? "طريقة الدفغ" : "Payment Method"


            };



            return new Response<SalaryResponse>
            {
                Data = new SalaryResponse
                {
                    Details = result.ToList(),
                    Headers = headers
                },
                Msg = string.Empty,
                Check = true
            };

        }

        public async Task<Response<Tuple<Header, List<GetSalariesEmployeeResponse>>>>
            GetDetailsOfCalculation(EmployeeTransactionDetailsFilters model, string lang)
        {

            (DateOnly startDate, DateOnly endDate) = DateManipulation.GetLastDateOfMonth(
               model.StartCalculationDate,
               model.StartActionDay);

            var result = (await _unitOfWork.StoredProcuduresRepo.Get_Details_Calculations(
                startDate,
                endDate, model.CompanyId, model.DepartmentId,
                model.EmployeeId, model.ManagerId, lang == Localization.Arabic ? 1 : 2)).Select(x => new GetSalariesEmployeeResponse
                {
                    EmployeeId = x.Id,
                    EmployeeName = x.FullName,
                    HousingAllowances = x.HousingAllowance,
                    Absents = new Absent
                    {
                        Days = x.AbsDays,
                        Sum = x.AbsSum
                    },
                    BasicSalary = x.FixedSalary,

                    MinuesValues = new MinuesValues
                    {
                        Deductions = new List<Deduction>
                                     {
                           new Deduction
                           {

                               Value = x.Deduction
                           }
                       },
                        Loans = new List<Loan> { new Loan
                       {

                                 Value=x.Loan
                       } }
                    },
                    DisbursementType = x.PaymentWay,
                    WorkingDay = x.WorkingDays,
                    TotalAll = x.Total,
                    TotalAdditionalValues = x.AdditionalValues,
                    TotalMinues = x.MinuesValues,
                    AdditionalValues = new List<AdditionalValues> {
                   new AdditionalValues
                   {
                       Id=1,
                       Name=lang==Localization.Arabic?"استحقاقات":"Benefits",
                       Value=x.Benefit
                   }
                   ,new AdditionalValues
                   {
                       Id=2,
                       Name=lang==Localization.Arabic?"البدلات":"Allowance",
                       Value=x.Allowance
                   }

                   }


                });
            var headers = new Header
            {
                WorkedDays = lang == Localization.Arabic ? "ايام العمل" : "Working Days",
                TotalAll = lang == Localization.Arabic ? "الاجمالي" : "Total",
                TotalMinues = lang == Localization.Arabic ? "مجموع الحسميان" : "Total Minues",
                TotalAdditionalValues = lang == Localization.Arabic ? "مجموع الاضافات" : "Total Additional",
                Absent = lang == Localization.Arabic ? new[] { "الايام", "مبلغ" } : new[] { "Days", "Amount" },
                EmployeeId = lang == Localization.Arabic ? "الرقم الوظيفي" : "Employee Id",
                EmployeeName = lang == Localization.Arabic ? "الاسم الوظيفي" : "Employee Name",
                Fixed = lang == Localization.Arabic ? "الاساسي" : "Fixed",
                AdditionalValues = lang == Localization.Arabic ? new[] { "استحقاقات", "البدلات" } : new[] { "Benefits", "Allowance" },
                MinuesValues = lang == Localization.Arabic ? new[] { "السلف", "الخصومات" } : new[] { "Loans", "Deductions" },
                PaymentMethod = lang == Localization.Arabic ? "طريقة الدفغ" : "Payment Method"
            };



            return new Response<Tuple<Header, List<GetSalariesEmployeeResponse>>>
            {
                Data = Tuple.Create(headers, result.ToList()),
                Msg = string.Empty,
                Check = true
            };
        }


        public async Task<Response<GetLookupsCalculatedSalaries>> GetLookups(string lang)
        {
            var currentCompany = await _userContextService.GetLoggedCurrentCompany();
            var currentCompnies = await _userContextService.GetLoggedCurrentCompanies();
            var emps = await _unitOfWork.Employees.GetSpecificSelectAsync(
                x => x.IsDeleted == false && x.IsActive == true && x.CompanyId == currentCompany, x => x, includeProperties: "Management,Department");

            if (emps is null)
            {
                var msg = _localizer[Localization.NotFoundData];
                return new Response<GetLookupsCalculatedSalaries>()
                {
                    Check = false,
                    Data = null,
                    Msg = msg

                };
            }
            var companies = await _unitOfWork.Companies.GetSpecificSelectAsync(x
                => currentCompnies.Contains(x.Id) && x.IsDeleted == false, x => x);
            if (companies is null)
            {
                var msg = _localizer[Localization.NotFoundData];
                return new Response<GetLookupsCalculatedSalaries>()
                {
                    Check = false,
                    Data = null,
                    Msg = msg

                };
            }
            var mangements = await _unitOfWork.
                Managements.GetSpecificSelectAsync
                (x => currentCompnies.Contains(x.CompanyId) && x.IsDeleted == false
                , x => x, includeProperties: "Company,Manager");

            if (mangements is null)
            {
                var msg = _localizer[Localization.NotFoundData];
                return new Response<GetLookupsCalculatedSalaries>()
                {
                    Check = false,
                    Data = null,
                    Msg = msg

                };
            }
            var mangementsIds = (await _unitOfWork.
              Managements.GetSpecificSelectAsync
              (x => currentCompnies.Contains(x.CompanyId) && x.IsDeleted == false
              , x => x, includeProperties: "Company,Manager")).Select(x => x.Id);
            var departments = await _unitOfWork.Departments.GetSpecificSelectAsync(x => mangementsIds.Contains(x.ManagementId), x => x, includeProperties: "Management,Manager");
            if (departments is null)
            {
                var msg = _localizer[Localization.NotFoundData];
                return new Response<GetLookupsCalculatedSalaries>()
                {
                    Check = false,
                    Data = null,
                    Msg = msg

                };
            }
            var result = new GetLookupsCalculatedSalaries
            {
                CompanyLookups = companies.Select(x => new CompanyLookup
                {
                    Id = x.Id,
                    CompnayName = Localization.Arabic == lang ? x.NameAr : x.NameEn
                }).ToList(),
                DepartmentLookups = departments.Select(x => new DepartmentLookup
                {
                    DepartmentName = Localization.Arabic == lang ? x.NameAr : x.NameEn,
                    ManagementId = x.ManagementId,
                    ManagerId = x.ManagerId,
                    Id = x.Id,

                }).ToList(),
                ManagementLookups = mangements.Select(x => new ManagementLookup
                {
                    ManagementName = Localization.Arabic == lang ? x.NameAr : x.NameEn,
                    ManagerId = x.ManagerId,
                    CompanyId = x.CompanyId,

                    Id = x.Id
                }).ToList(),
                EmployeeLookups = emps.Select(x => new Empolyeelookups
                {
                    Name = Localization.Arabic == lang ? x.FullNameAr : x.FullNameEn,

                    Id = x.Id,
                    MangmentId = x.ManagementId,
                    DepartmentId = x.DepartmentId

                }).ToList()

            };

            return new Response<GetLookupsCalculatedSalaries>
            {
                Check = true,
                Data = result,
                LookUps = null
            };


        }

        public async Task<Response<string>> PaySalary(int id)
        {
            var transaction = _unitOfWork.BeginTransaction();
            try
            {

                var transSalary = await _unitOfWork.TransSalaryCalculator.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "TransSalaryCalculatorsDetails");
                if (transSalary is null)
                {
                    return new()
                    {
                        Check = false,
                        Msg = _localizer[Localization.IsNotExisted, Localization.SalaryCalculator]
                    };
                }
                transSalary.Status = Status.PaidOff;
                var paymnetTrans = new PaymentSalary
                {
                    company_id = transSalary.CompanyId,
                    employee_number = transSalary.TransSalaryCalculatorsDetails.Count(),
                    transSalary_calculator_id = transSalary.Id,
                    total_amount = transSalary.TransSalaryCalculatorsDetails.Sum(x => x.Total).Value,


                };
                await _context.PaymentSalaries.AddAsync(paymnetTrans);
                await _context.SaveChangesAsync();
                transaction.Commit();
                return new()
                {
                    Check = true,
                    Msg = _localizer[Localization.Paid]
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new()
                {
                    Check = false,
                    Msg = ex?.InnerException?.Message
                };
            }
        }
    }
}
