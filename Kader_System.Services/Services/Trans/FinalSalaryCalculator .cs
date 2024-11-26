using Kader_System.DataAccesss.Context;
using Kader_System.Services.Design_Patterns;
using Kader_System.Services.DesignPatterns;
using Microsoft.EntityFrameworkCore;

namespace Kader_System.Services.Services.Trans
{
    public class FinalSalaryCalculator : CalculateSalaryDesginPattern
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly KaderDbContext _context;
        private readonly IUserContextService _userContext;
        private List<Salaries> _salaries;
        public FinalSalaryCalculator(IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> stringLocalizer,
            IUserContextService userContext, KaderDbContext db
        ) : base(unitOfWork, stringLocalizer, userContext)


        {
            _context = db;
            _unitOfWork = unitOfWork;
            _userContext = userContext;


        }

        public async override Task AddCalculations(List<ResultOfCollect> model, UpdateCalculateSalaryModelRequest request)
        {

            int? masterId = null;

            // Retrieve the logged-in company
            var loggedCompany = await _userContext.GetLoggedCurrentCompany();
            var companyId = request.CompanyId == 0 || request.CompanyId == null ? loggedCompany : request.CompanyId;

            // Check if calculation for the given month already exists
            if (!await CalculateBefore(request.StartCalculationDate.Month))
            {
                // Create a new salary calculation master record
                var masterCalc = new TransSalaryCalculator
                {
                    CalculationDate = request.StartCalculationDate,
                    CompanyId = companyId,
                    Status = Status.Waiting,
                };

                _context.SalaryCalculator.Add(masterCalc);
                await _context.SaveChangesAsync();
                masterId = masterCalc.Id;
            }
            else
            {
                // Retrieve the existing master record for the given month
                var existingMaster = await _unitOfWork.TransSalaryCalculator
                    .GetFirstOrDefaultAsync(x => x.CalculationDate.Month == request.StartCalculationDate.Month);

                if (existingMaster != null)
                    masterId = existingMaster.Id;
            }

            // Ensure masterId is set
            if (!masterId.HasValue)
                throw new InvalidOperationException("Unable to determine master salary calculator ID.");

            // Fetch salaries and prepare details
            _salaries = await GetSalaries(request.EmployeeIds);
            var detailsToInsert = new List<TransSalaryCalculatorDetail>();

            foreach (var result in model)
            {
                // Skip employees already calculated for this master
                if (await EmpolyeeCalculatedBefore(result.EmployeeId, masterId.Value))
                    continue;

                detailsToInsert.Add(new TransSalaryCalculatorDetail
                {
                    CompanyId = companyId,
                    NetSalary = GetSalary(result.EmployeeId) +
                                result.TotalDeductions + result.TotalBenefits +
                                result.TotalAllownces + result.TotalLoan,
                    BasicSalary = GetSalary(result.EmployeeId),
                    TotalAllownces = result.TotalAllownces,
                    TotalDeductions = result.TotalDeductions,
                    TotalBenefits = result.TotalBenefits,
                    TotalLoans = result.TotalLoan,
                    TransSalaryCalculatorsId = masterId,
                    EmployeeId = result.EmployeeId,
                });
            }

            // Perform a bulk insert
            if (detailsToInsert.Any())
            {
                await _context.TransSalaryCalculatorsDetails.AddRangeAsync(detailsToInsert);
                await _context.SaveChangesAsync();  // Use EF Core Extensions for bulk insert
            }



        }
        public async Task<bool> EmpolyeeCalculatedBefore(int empid, int masterId)
        {
            return _context.TransSalaryCalculatorsDetails.Any(x => x.EmployeeId == empid && x.TransSalaryCalculatorsId == masterId);
        }
        public double GetSalary(int EmployeeId)
        {
            return _salaries.Where(x => x.EmployeeId == EmployeeId).Select(x => x.Salary).FirstOrDefault();
        }

        public async override Task<List<ResultOfCollect>> CollectTransaction(
            UpdateCalculateSalaryModelRequest model)
        {
            (DateOnly startDate, DateOnly endDate) = DateManipulation.GetLastDateOfMonth(model.StartCalculationDate, model.StartActionDay);
            var details = (await _unitOfWork.StoredProcuduresRepo.CalculateSalaryDetails(startDate,
              endDate,
                model.EmployeeIds.JoinIntergers()

                 ));
            var result = details
             .GroupBy(e => e.EmployeeId)
             .Select(g => new ResultOfCollect
             {
                 EmployeeId = g.Key.Value,
                 TotalDeductions = g.Where(x => x.JournalType.Value == JournalType.Deduction).Sum(s => s.CalculatedSalary.Value),
                 TotalBenefits = g.Where(x => x.JournalType.Value == JournalType.Benefit).Sum(s => s.CalculatedSalary.Value),
                 TotalLoan = g.Where(x => x.JournalType.Value == JournalType.Loan).Sum(s => s.CalculatedSalary.Value),
                 TotalAllownces = g.Where(x => x.JournalType.Value == JournalType.Allowance).Sum(s => s.CalculatedSalary.Value)

             })
             .ToList();

            return result;



        }

        public async override Task<List<Salaries>> GetSalaries(List<int> empIds)
        {
            return await _context.Contracts.Where(x => empIds.Contains(x.employee_id))
                .Select(x => new Salaries
                {
                    EmployeeId = x.employee_id,
                    Salary = KaderDbContext.GetSalaryWithIncrease(x.employee_id),



                }).ToListAsync();

        }



        public override async Task UpdateOnTransactions(DateOnly startDate, DateOnly endDate,
            List<int> empsId)
        {

            await _context.ExecuteUpdateTransactionAsync(startDate, endDate, empsId.JoinIntergers());




        }
    }
}
