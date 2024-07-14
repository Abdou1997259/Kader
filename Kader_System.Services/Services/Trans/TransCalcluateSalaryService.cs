using Kader_System.Domain.DTOs;

namespace Kader_System.Services.Services.Trans
{
    public class TransCalcluateSalaryService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IStringLocalizer<SharedResource> localizer) : ITransCalcluateSalaryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer = localizer;

        private readonly UserManager<ApplicationUser> _userManager = userManager;
        public async Task<Response<string>> CalculateSalaryDetailedTrans(CalcluateSalaryModelRequest model)
        {

            var empolyees = await _unitOfWork.Employees.GetSpecificSelectAsync(x =>
            model.EmployeeIds.Any(e => e == x.Id)
            , x => x);










            if (empolyees is null)
            {
                var msg = _localizer[Localization.NotFoundData];
                return new()
                {
                    Data = null,
                    Msg = msg,
                    Check = false
                };
            }




            var empolyeeWithCaculatedSalary = await _unitOfWork.StoredProcuduresRepo.SpCalculateSalary(model.StartCalculationDate, model.StartActionDay, string.Join('-', empolyees.Select(x => x.Id).ToList()));

            //var spcaculatedSalarydetils = await _unitOfWork.StoredProcuduresRepo.SpCalculateSalaryDetails(model.StartCalculationDate, model.StartActionDay, string.Join('-', empolyees.Select(x => x.Id).ToList()));
            var spcaculatedSalarytransDetils = await _unitOfWork.StoredProcuduresRepo.SpCalculatedSalaryDetailedTrans(model.StartCalculationDate, model.StartActionDay, string.Join('-', empolyees.Select(x => x.Id).ToList()));
            var transcation = _unitOfWork.BeginTransaction();
            try
            {

                var TransCalculatorMaster = (await _unitOfWork.TransSalaryCalculator.GetSpecificSelectAsync(x => x.DocumentDate == model.DocumentDate, x => x)).FirstOrDefault();

                if (TransCalculatorMaster == null)
                {
                    TransCalculatorMaster = new TransSalaryCalculator
                    {
                        DocumentDate = model.DocumentDate,
                        CompanyId = model.CompanyId,
                        ManagementId = model.ManagementId,
                        Status = Status.Waiting,

                        IsMigrated = true
                    };


                    TransCalculatorMaster = await _unitOfWork.TransSalaryCalculator.AddAsync(TransCalculatorMaster);
                    await _unitOfWork.CompleteAsync();

                }


                var listoftransDetails = new List<TransSalaryCalculatorDetail>();
                foreach (var empolyee in empolyeeWithCaculatedSalary)
                {
                    var calculatedBefore = await _unitOfWork.TransSalaryCalculatorDetailsRepo.ExistAsync(
                        x => x.EmployeeId == empolyee.EmployeeId
                        && x.TransSalaryCalculatorsId == TransCalculatorMaster.Id);

                    if (!calculatedBefore)
                    {
                        var transDetails = new TransSalaryCalculatorDetail
                        {
                            EmployeeId = empolyee.EmployeeId,
                            Salary = empolyee.CalculatedSalary + empolyee.TotalSalary,
                            Amount = empolyee.CalculatedSalary,
                            TransSalaryCalculatorsId = TransCalculatorMaster.Id,


                        };
                        listoftransDetails.Add(transDetails);


                    }

                }
                await _unitOfWork.TransSalaryCalculatorDetailsRepo.AddRangeAsync(listoftransDetails);

                await _unitOfWork.CompleteAsync();
                var allownces = new List<TransAllowance>();
                var deductions = new List<TransDeduction>();
                var benefits = new List<TransBenefit>();
                var loans = new List<TransLoan>();
                var vacations = new List<TransVacation>();
                foreach (var trans in spcaculatedSalarytransDetils)
                {

                    if (trans.CalculateSalaryDetailsId is null)
                    {
                        var cacluateSalaryId = (await _unitOfWork.TransSalaryCalculatorDetailsRepo.GetSpecificSelectAsync(x => x.TransSalaryCalculatorsId == TransCalculatorMaster.Id && x.EmployeeId == trans.EmployeeId, x => x)).FirstOrDefault();
                        if (trans.JournalType == JournalType.Allowance)
                        {
                            var allownce = await _unitOfWork.TransAllowances.GetByIdAsync(trans.TransId);

                            allownce.CalculateSalaryDetailsId = cacluateSalaryId?.Id;
                            allownce.CalculateSalaryId = TransCalculatorMaster.Id;

                            allownces.Add(allownce);

                        }
                        else if (trans.JournalType == JournalType.Deduction)
                        {
                            var deduction = await _unitOfWork.TransDeductions.GetByIdAsync(trans.TransId);



                            deduction.CalculateSalaryDetailsId = cacluateSalaryId?.Id;
                            deduction.CalculateSalaryId = TransCalculatorMaster.Id;
                            deductions.Add(deduction);

                        }
                        else if (trans.JournalType == JournalType.Benefit)
                        {
                            var benefit = await _unitOfWork.TransBenefits.GetByIdAsync(trans.TransId);



                            benefit.CalculateSalaryDetailsId = cacluateSalaryId?.Id;
                            benefit.CalculateSalaryId = TransCalculatorMaster.Id;
                            benefits.Add(benefit);

                        }
                        else if (trans.JournalType == JournalType.Loan)
                        {
                            var loan = await _unitOfWork.LoanRepository.GetByIdAsync(trans.TransId);



                            loan.CalculateSalaryDetailsId = cacluateSalaryId?.Id;
                            loan.CalculateSalaryId = TransCalculatorMaster.Id;
                            loans.Add(loan);


                        }
                        else if (trans.JournalType == JournalType.Vacation)
                        {


                            var vacation = await _unitOfWork.TransVacations.GetByIdAsync(trans.TransId);



                            vacation.CalculateSalaryDetailsId = cacluateSalaryId?.Id;
                            vacation.CalculateSalaryId = TransCalculatorMaster.Id;
                            vacations.Add(vacation);




                        }






                    }


                }
                _unitOfWork.LoanRepository.UpdateRange(loans);
                _unitOfWork.TransDeductions.UpdateRange(deductions);
                _unitOfWork.TransBenefits.UpdateRange(benefits);
                _unitOfWork.TransAllowances.UpdateRange(allownces);
                await _unitOfWork.CompleteAsync();











                transcation.Commit();

            }
            catch (Exception ex)
            {

                transcation.Rollback();


            }
            return new()
            {
                Data = "Calculated successfully"
               ,
                Msg = null,
                Error = null
            };

        }

        public async Task<Response<string>> DeleteCalculator(int Id)
        {
            var transaction = await _unitOfWork.TransSalaryCalculator.GetByIdAsync(Id);
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
            var transactions = await _unitOfWork.TransSalaryCalculatorDetailsRepo.GetSpecificSelectAsync(x => x.TransSalaryCalculatorsId == transaction.Id, x => x);

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
            Expression<Func<TransSalaryCalculator, bool>> filter = x => x.IsDeleted == model.IsDeleted;

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
            var transations = await _unitOfWork.TransSalaryCalculator.GetSpecificSelectAsync(x => x.IsDeleted == false, x => x, includeProperties: "TransSalaryCalculatorsDetails");
            var msg = _localizer[Localization.NotFoundData];
            if (transations is null)
            {

                return new()
                {
                    Data = null,
                    Msg = msg,
                    Check = false
                };
            }
            var result = new GetSalaryCalculatorResponse
            {
                TotalRecords = await _unitOfWork.TransSalaryCalculator.CountAsync(filter: filter),

                Items = (await _unitOfWork.TransSalaryCalculator.GetSpecificSelectAsync(filter: filter,
                    take: model.PageSize,
                    skip: (model.PageNumber - 1) * model.PageSize,
                    select: x => new GetSalaryCalculatorList
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Status = x.Status,
                        Amount = x.TransSalaryCalculatorsDetails.Sum(s => s.Amount),
                        CalculationDate = x.DocumentDate,
                        AddedDate = x.Add_date,
                        AddedBy = x.DeleteBy

                    }, orderBy: x =>
                x.OrderByDescending(x => x.Id), includeProperties: "TransSalaryCalculatorsDetails")).AsEnumerable().Select(
                    x => new GetSalaryCalculatorList
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Status = x.Status,
                        Amount = x.Amount,
                        CalculationDate = x.CalculationDate,
                        AddedDate = x.AddedDate,
                        AddedBy = empolyeeWithJobs.Where(e => e.UserId == x.AddedBy)
                                       .Select(e => lang == Localization.Arabic ? e.FirstNameAr + " " + e.FamilyNameAr : e.FirstNameEn + " " + e.FamilyNameEn)
                                       .FirstOrDefault(),

                        JobName = empolyeeWithJobs.Where(e => e.UserId == x.AddedBy)
                                      .Select(e => lang == Localization.Arabic ? e.Job.NameAr : e.Job.NameEn)
                                      .FirstOrDefault()
                    }


                    ).ToList()



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

        public async Task<Response<IEnumerable<GetSalariesEmployeeResponse>>> GetDetailsOfCalculation(CalcluateEmpolyeeFilters model, string lang)
        {
            var empolyees = await _unitOfWork.Employees.GetSpecificSelectAsync(x =>
          (!model.EmployeeId.HasValue || x.Id == model.EmployeeId)
          && (!model.CompanyId.HasValue || x.CompanyId == model.CompanyId)
          && (!model.ManagerId.HasValue || x.ManagementId == model.ManagerId)
          && (!model.DepartmentId.HasValue || x.DepartmentId == model.DepartmentId)



          , x => x);



            var empolyeeWithCaculatedSalary = await _unitOfWork.StoredProcuduresRepo.SpCalculateSalary(model.StartCalculationDate, model.StartActionDay, string.Join('-', empolyees.Select(x => x.Id).ToList()));
            var spcaculatedSalarytransDetils = (await _unitOfWork.StoredProcuduresRepo.SpCalculatedSalaryDetailedTrans(model.StartCalculationDate, model.StartActionDay, string.Join('-', empolyees.Select(x => x.Id).ToList()))).Where(x => x.CalculateSalaryId != null);

            var vacations = await _unitOfWork.TransVacations.GetAllAsync();



            var contracts = await _unitOfWork.Contracts.GetAllAsync();

            if (empolyees is null)
            {
                var msg = _localizer[Localization.NotFoundData];
                return new()
                {
                    Data = null,
                    Msg = msg,
                    Check = false
                };
            }

            return new()
            {

                Data = empolyeeWithCaculatedSalary.Select(x => new GetSalariesEmployeeResponse
                {
                    EmployeeId = x.EmployeeId,
                    EmployeeName = Localization.Arabic == lang ? x.FullNameAr : x.FullNameEn,
                    AccommodationAllowance = x.AccommodationAllowance,
                    BasicSalary = x.TotalSalary,
                    HousingAllownces = contracts.Where(c => c.EmployeeId == x.EmployeeId).Select(s => s.HousingAllowance).FirstOrDefault(),
                    WrokingDay = 30,
                    DisbursementType = DisbursementType.BankingType,
                    Headers = new Header
                    {
                        WorkedDays = Localization.Arabic == lang ? "ايام العمل" : "Wroking Days",
                        TotalAll = lang == Localization.Arabic ? "الاجمالي" : "Total",
                        TotalMinues = lang == Localization.Arabic ? "مجموع الحسميان" : "Total Minues",
                        TotalAdditionalValues = lang == Localization.Arabic ? "مجموع الاضافات" : "Total Additional",

                        Absent = lang == Localization.Arabic ? ["الايام", "مبلغ"] : ["Days", "Amount"],
                        EmpolyeeId = Localization.Arabic == lang ? "الرقم الوظيفي" : "Employee Id",
                        EmpolyeeName = Localization.Arabic == lang ? "الاسم الوظيفي" : "Employee Name",
                        Fixed = Localization.Arabic == lang ? "الاساسي" : "Fixed",
                        AddtionalValues = spcaculatedSalarytransDetils.Where(e => e.EmployeeId == x.EmployeeId && e.JournalType == JournalType.Benefit || JournalType.Allowance == e.JournalType).AsEnumerable().Select(s => s.JournalType.ToString()).ToList(),

                        MinuesValues = spcaculatedSalarytransDetils.Where(e => e.EmployeeId == x.EmployeeId && e.JournalType == JournalType.Deduction || JournalType.Loan == e.JournalType).AsEnumerable().Select(s => s.JournalType.ToString()).ToList(),



                    },
                    MinuesValues = new MinuesValues
                    {
                        Deductions = spcaculatedSalarytransDetils.Where(e => e.EmployeeId == x.EmployeeId && e.JournalType == JournalType.Deduction).Select(t => new Deduction
                        {
                            Id = t.TransId,
                            Value = t.CalculatedSalary
                        }),
                        Loans = spcaculatedSalarytransDetils.Where(e => e.EmployeeId == x.EmployeeId && e.JournalType == JournalType.Loan).Select(t => new Loan
                        {
                            Id = t.TransId,
                            Value = t.CalculatedSalary
                        }),


                    },
                    Absents = spcaculatedSalarytransDetils.Where(e => e.EmployeeId == x.EmployeeId && e.JournalType == JournalType.Vacation).Select(v => new Absent
                    {
                        Days = vacations.Where(vv => vv.EmployeeId == x.EmployeeId && vv.StartDate == v.JournalDate && vv.Id == v.TransId)?.FirstOrDefault()?.DaysCount,
                        Sum = vacations.Where(vv => vv.EmployeeId == x.EmployeeId && vv.StartDate == v.JournalDate && vv.Id == v.TransId)?.FirstOrDefault()?.DaysCount == null ? 0 : v.CalculatedSalary,

                    }),

                    AddtionalValues = spcaculatedSalarytransDetils.Where(e => e.EmployeeId == x.EmployeeId && (e.JournalType == JournalType.Allowance || e.JournalType == JournalType.Benefit)).Select(t => new AddtionalValues
                    {
                        Id = t.TransId,
                        Name = t.JournalType.ToString(),

                        Value = t.CalculatedSalary
                    })


                })

            };




        }

        public async Task<Response<GetLookupsCalculatedSalaries>> GetLookups(string lang)
        {
            var emps = await _unitOfWork.Employees.GetSpecificSelectAsync(x => true, x => x, includeProperties: "Management,Department");

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
            var companies = await _unitOfWork.Companies.GetAllAsync();
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
            var mangements = await _unitOfWork.Managements.GetSpecificSelectAsync(x => true, x => x, includeProperties: "Company,Manager");

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
            var departments = await _unitOfWork.Departments.GetSpecificSelectAsync(x => true, x => x, includeProperties: "Management,Manager");
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
                    Name = Localization.Arabic == lang ? x.FirstNameAr + " " + x.FatherNameAr + " " + x.FatherNameAr : x.FirstNameEn + " " + x.FatherNameEn + " " + x.FatherNameEn,
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
    }
}
