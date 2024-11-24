using Kader_System.Services.Design_Patterns;

namespace Kader_System.Services.DesignPatterns
{
    public abstract class CalculateSalaryDesginPattern
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IUserContextService _userContext;
        protected CalculateSalaryDesginPattern(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> stringLocalizer,
            IUserContextService userContext

            )
        {
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
            _userContext = userContext;
        }
        public abstract Task<List<Salaries>> GetSalaries(List<int> empIds);

        public async Task<bool> CheckOnEmpolyees(UpdateCalculateSalaryModelRequest model)
        {
            var currentCompanyId = await _userContext.GetLoggedCurrentCompany();
            var empolyees = await _unitOfWork.Employees.GetSpecificSelectAsync(x =>
            model.EmployeeIds.Any(e => e == x.Id)
            , x => x);

            if (empolyees is null)
            {

                return false;
            }

            foreach (var e in empolyees)
            {
                if (!await _unitOfWork.Contracts.ExistAsync(x => x.employee_id == e.Id &&
                x.company_id == currentCompanyId))
                {
                    var msg = $"{_stringLocalizer[Localization.Contract]} " +
                        $" {_stringLocalizer[Localization.NotFound]}";
                    return false;

                }

            }
            return true;

        }
        public async Task<bool> CalculateBefore(int month)
        {
            if (month < 31)
            {
                return await _unitOfWork.TransSalaryCalculator.ExistAsync(x => x.CalculationDate.Month == month);

            }
            return false;
        }


        public abstract Task<List<ResultOfCollect>> CollectTransaction(UpdateCalculateSalaryModelRequest model);

        public abstract Task UpdateOnTransactions(DateOnly startDate, DateOnly endDate, List<int> empsId);
        public abstract Task AddCalculations(List<ResultOfCollect> model, UpdateCalculateSalaryModelRequest request);
        public async virtual Task<result> Calculate(UpdateCalculateSalaryModelRequest model)
        {


            var transaction = _unitOfWork.BeginTransaction();
            try
            {
                if (await CheckOnEmpolyees(model))
                {



                    await AddCalculations(await CollectTransaction(model), model);

                    #region update Transation from start date to end date on specific emps

                    (DateOnly startDate, DateOnly endDate) = DateManipulation.GetLastDateOfMonth(
                       model.StartCalculationDate,
                       model.StartActionDay);



                    await UpdateOnTransactions(startDate
                   , endDate,
                      model.EmployeeIds);



                    #endregion

                    transaction.Commit();
                    return new result
                    {
                        IsCalculated = true,
                    };
                }


                return new result { EmpNotExited = true };

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new result
                {
                    IsCalculated = false,
                };
            }



        }



    }
    public class ResultOfCollect
    {
        public int EmployeeId { get; set; }
        public double TotalDeductions { get; set; }
        public double TotalBenefits { get; set; }
        public double TotalLoan { get; set; }
        public double TotalAllownces { get; set; }



    }
    public class result
    {
        public bool IsCalculated { get; set; }
        public bool IsCalculatedBefore { get; set; }
        public bool EmpNotExited { get; set; }
    }
    public class Salaries
    {
        public int EmployeeId { get; set; }
        public double Salary { get; set; }
    }
}
