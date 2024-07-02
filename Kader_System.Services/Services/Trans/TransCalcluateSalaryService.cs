namespace Kader_System.Services.Services.Trans
{
    public class TransCalcluateSalaryService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> localizer) : ITransCalcluateSalaryService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer = localizer;
        public async Task<Response<object>> CalculateSalary(CalcluateSalaryModelRequest model)
        {
            var dateOfCalculation = DateTime.Now.ToGetOnlyDate();
            var empolyee = await _unitOfWork.Employees.GetByIdAsync(model.EmployeeId);

            var contract = (await _unitOfWork.Contracts.GetSpecificSelectAsync(x => x.EmployeeId == model.EmployeeId, x => x)).FirstOrDefault();
            var company = await _unitOfWork.Companies.GetByIdAsync(model.CompanyId);
            var isInCompany = empolyee?.CompanyId == company?.Id;

            if (company is null)
            {
                string resultMsg = $"{_localizer[Localization.Company]} {_localizer[Localization.IsNotExisted]} ";
                return new()
                {
                    Msg = resultMsg,
                    Error = resultMsg,
                    Data = null
                };
            }
            if (empolyee == null)
            {

                string resultMsg = $"{_localizer[Localization.Employee]} {_localizer[Localization.IsNotExisted]} ";
                return new()
                {
                    Msg = resultMsg,
                    Error = resultMsg,
                    Data = null
                };
            }
            if (!isInCompany)
            {
                string resultMsg = $"{_localizer[Localization.Employee]} {_localizer[Localization.IsNotExistedIn]} {_localizer[Localization.Company]} ";
                return new()
                {
                    Msg = resultMsg,
                    Error = resultMsg,
                    Data = null
                };

            }
            if (contract == null)
            {

                string resultMsg = $" {_localizer[Localization.Employee]} {_localizer[Localization.ContractNotFound]}";
                return new()
                {
                    Error = resultMsg,
                    Msg = resultMsg
                };

            }


            var spCacluateSalaries = await _unitOfWork.StoredProcuduresRepo.SpCacluateSalaries(model.StartActionDate, model.EmployeeId);
            var caculatedBasedOnJournalType = spCacluateSalaries
                                           .GroupBy(x => x.JournalType)
                                           .Select(x => new
                                           {
                                               JournalType = x.Key,
                                               TotalCalc = x.Sum(x => x.CalculatedSalary),
                                               ActionDate = x.Select(x => x.JournalDate).FirstOrDefault(),
                                               Id = x.Select(x => x.Id).FirstOrDefault(),
                                               CacluateSalaryId = x.Select(x => x.CacluateSalaryId).FirstOrDefault()
                                           }).ToList();

            var allValueCalculated = spCacluateSalaries.Sum(x => x.CalculatedSalary);

            var transcation = _unitOfWork.BeginTransaction();
            try
            {



                var transSalaryCalculators = (await _unitOfWork.TransSalaryCalculator.GetSpecificSelectAsync(x => x.DocumentDate.Month == DateTime.Today.Month, x => x)).FirstOrDefault();

                if (transSalaryCalculators is null)
                {


                    var salaryCalculator = new TransSalaryCalculator
                    {
                        DocumentDate = model.DocumentDate,
                        CompanyId = model.CompanyId,
                        IsMigrated = model.IsMigrated,


                    };


                    transSalaryCalculators = await _unitOfWork.TransSalaryCalculator.AddAsync(salaryCalculator);
                    await _unitOfWork.CompleteAsync();






                }
                var transSalaryCalculatorDetails = (await _unitOfWork.TransSalaryCalculatorDetailsRepo.
                    GetSpecificSelectAsync(x => x.TransSalaryCalculatorsId == transSalaryCalculators.Id && x.EmployeeId == model.EmployeeId, x => x)).FirstOrDefault();
                if (transSalaryCalculatorDetails is not null)
                {
                    string resultMsg = $" {_localizer[Localization.CalculatedAready]}";
                    return new()
                    {
                        Error = resultMsg,
                        Msg = resultMsg
                    };

                }
                double totalSalary = 0;
                if (spCacluateSalaries is null)
                {
                    totalSalary = contract.TotalSalary;
                    goto DontUpdate;
                }
                else
                    totalSalary = contract.TotalSalary + allValueCalculated;

                var transSalaryCalculatorDetailsModel = new TransSalaryCalculatorDetail
                {
                    EmployeeId = model.EmployeeId,
                    Salary = totalSalary,
                    TransSalaryCalculatorsId = transSalaryCalculators.Id
                };
                await _unitOfWork.TransSalaryCalculatorDetailsRepo.AddAsync(transSalaryCalculatorDetailsModel);
                await _unitOfWork.CompleteAsync();


                foreach (var transcationOnSalary in spCacluateSalaries)
                {


                    switch (transcationOnSalary.JournalType)
                    {
                        case JournalType.Deduction:
                            if (transcationOnSalary.CacluateSalaryId is null)
                            {
                                var deductions = await _unitOfWork.TransDeductions.GetByIdAsync(transcationOnSalary.Id);

                                deductions.CacluateSalaryId = transSalaryCalculatorDetailsModel.Id;
                                _unitOfWork.TransDeductions.Update(deductions);

                            }

                            break;
                        case JournalType.Allowance:
                            if (transcationOnSalary.CacluateSalaryId is null)
                            {
                                var allowances = await _unitOfWork.TransAllowances.GetByIdAsync(transcationOnSalary.Id);

                                allowances.CacluateSalaryId = transSalaryCalculatorDetailsModel.Id;
                                _unitOfWork.TransAllowances.Update(allowances);
                            }
                            break;
                        case JournalType.Benefit:
                            if (transcationOnSalary.CacluateSalaryId is null)
                            {
                                var benefits = await _unitOfWork.TransBenefits.GetByIdAsync(transcationOnSalary.Id);

                                benefits.CacluateSalaryId = transSalaryCalculatorDetailsModel.Id;
                                _unitOfWork.TransBenefits.Update(benefits);


                            }

                            break;
                        case JournalType.Loan:
                            if (transcationOnSalary.CacluateSalaryId is null)
                            {
                                var loans = await _unitOfWork.LoanRepository.GetByIdAsync(transcationOnSalary.Id);

                                loans.CacluateSalaryId = transSalaryCalculatorDetailsModel.Id;
                                _unitOfWork.LoanRepository.Update(loans);

                            }

                            break;



                    }



                }
                await _unitOfWork.CompleteAsync();
            DontUpdate:

                transcation.Commit();

            }
            catch (Exception ex)
            {

                transcation.Rollback();


            }
            return new()
            {
                Data = new
                {
                    ListOfsp = spCacluateSalaries,
                    caluculatedEvery = caculatedBasedOnJournalType,
                    sumvalue = allValueCalculated

                },
                Msg = null,
                Error = null
            };

        }
    }
}
