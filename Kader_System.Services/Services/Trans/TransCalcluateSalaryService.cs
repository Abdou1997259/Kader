namespace Kader_System.Services.Services.Trans
{
    public class TransCalcluateSalaryService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> localizer) : ITransCalcluateSalaryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer = localizer;


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
                foreach (var trans in spcaculatedSalarytransDetils)
                {

                    if (trans.CacluateSalaryId is null)
                    {
                        var cacluateSalaryId = (await _unitOfWork.TransSalaryCalculatorDetailsRepo.GetSpecificSelectAsync(x => x.TransSalaryCalculatorsId == TransCalculatorMaster.Id && x.EmployeeId == trans.EmployeeId, x => x)).FirstOrDefault();
                        if (trans.JournalType == JournalType.Allowance)
                        {
                            var allownce = await _unitOfWork.TransAllowances.GetByIdAsync(trans.TransId);

                            allownce.CacluateSalaryId = cacluateSalaryId?.Id;
                            allownces.Add(allownce);

                        }
                        if (trans.JournalType == JournalType.Deduction)
                        {
                            var deduction = await _unitOfWork.TransDeductions.GetByIdAsync(trans.TransId);



                            deduction.CacluateSalaryId = cacluateSalaryId?.Id;
                            deductions.Add(deduction);

                        }
                        if (trans.JournalType == JournalType.Benefit)
                        {
                            var benefit = await _unitOfWork.TransBenefits.GetByIdAsync(trans.TransId);



                            benefit.CacluateSalaryId = cacluateSalaryId?.Id;
                            benefits.Add(benefit);

                        }
                        if (trans.JournalType == JournalType.Loan)
                        {
                            var loan = await _unitOfWork.LoanRepository.GetByIdAsync(trans.TransId);



                            loan.CacluateSalaryId = cacluateSalaryId?.Id;
                            loans.Add(loan);


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



        public async Task<Response<IEnumerable<GetSalariesEmployeeResponse>>> GetDetailsOfCalculation(CalcluateEmpolyeeFilters model, string lang)
        {
            var empolyees = await _unitOfWork.Employees.GetSpecificSelectAsync(x =>
          (!model.EmployeeId.HasValue || x.Id == model.EmployeeId) &&
          (!model.CompanyId.HasValue || x.CompanyId == model.CompanyId) &&
          (!model.ManagerId.HasValue || x.ManagementId == model.ManagerId) &&
          (!model.ManagerId.HasValue || x.DepartmentId == model.DepartmentId)
          , x => x);
            var empolyeeWithCaculatedSalary = await _unitOfWork.StoredProcuduresRepo.SpCalculateSalary(model.StartCalculationDate, model.StartActionDay, string.Join('-', empolyees.Select(x => x.Id).ToList()));
            var spcaculatedSalarytransDetils = await _unitOfWork.StoredProcuduresRepo.SpCalculatedSalaryDetailedTrans(model.StartCalculationDate, model.StartActionDay, string.Join('-', empolyees.Select(x => x.Id).ToList()));






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
                    WrokingDay = 30,
                    DisbursementType = DisbursementType.BankingType,
                    Absents = spcaculatedSalarytransDetils.Where(e => e.EmployeeId == x.EmployeeId).Select(t => new Absent
                    {
                        Days = t.VacationDayCount,
                        Sum = t.VacationSum
                    })
                  ,
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
                    AddtionalValues = spcaculatedSalarytransDetils.Where(e => e.EmployeeId == x.EmployeeId && (e.JournalType == JournalType.Allowance || e.JournalType == JournalType.Benefit)).Select(t => new AddtionalValues
                    {
                        Id = t.TransId,
                        Name = t.JournalType.ToString(),
                        Value = t.CalculatedSalary
                    })


                })

            };




        }


    }
}
