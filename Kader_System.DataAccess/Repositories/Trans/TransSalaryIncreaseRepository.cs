using Kader_System.Domain.Constants.Enums;
using Kader_System.Domain.DTOs.Request.Trans;
using Kader_System.Domain.DTOs.Response.Trans;
using Kader_System.Domain.Models.Trans;

namespace Kader_System.DataAccess.Repositories.Trans;

public class TransSalaryIncreaseRepository(KaderDbContext context) : BaseRepository<TransSalaryIncrease>(context), ITransSalaryIncreaseRepository
{
    public Task<int> AddNewSalaryIncrease(CreateTransSalaryIncreaseRequest createTransSalary)
    {
        var empSalary = context.Employees.AsNoTracking().
             Where(x => x.Id == createTransSalary.employeeId).
             Select(x => x.TotalSalary).
             FirstOrDefault();

        #region SalaryTypesCases
        double salaryAfterIncrease = (SalaryIncreaseTypes)createTransSalary.salrayIncreaseTypeId switch
        {
            SalaryIncreaseTypes.Amount => createTransSalary.increaseValue + empSalary,
            SalaryIncreaseTypes.percentage => ((createTransSalary.increaseValue / 100) * empSalary) + empSalary,
            _ => empSalary,
        };
        #endregion

        var salaryIncrease = new TransSalaryIncrease()
        {
            Notes = createTransSalary.details,
            Amount = createTransSalary.increaseValue,
            Employee_id = createTransSalary.employeeId,
            Increase_type = createTransSalary.salrayIncreaseTypeId,
            transactionDate = DateTime.Now,
            dueDate = DateTime.Now.AddMonths(1),
            salaryAfterIncrease = salaryAfterIncrease,
        };
        context.TransSalaryIncreases.Add(salaryIncrease);
        return context.SaveChangesAsync();

    }

    public Task<List<TransSalaryIncreaseResponse>> GetAllSalaryIncrease()
    {
        var result = (from q in context.TransSalaryIncreases.AsNoTracking()
                      select new TransSalaryIncreaseResponse
                      {
                          employeeId = q.Employee_id,
                          details = q.Notes,
                          dueDate = q.dueDate,
                          transationDate = q.transactionDate,
                          increaseValue = q.Amount,
                          salrayIncreaseTypeId = q.Increase_type,
                      }).ToListAsync();

        return result;
    }

    public Task<TransSalaryIncreaseResponse> GetSalaryIncreaseById(int id)
    {
        var result = (from q in context.TransSalaryIncreases.AsNoTracking()
                      where q.Id == id
                      select new TransSalaryIncreaseResponse
                      {
                          employeeId = q.Employee_id,
                          details = q.Notes,
                          dueDate = q.dueDate,
                          transationDate = q.transactionDate,
                          increaseValue = q.Amount,
                          salrayIncreaseTypeId = q.Increase_type,
                      }).FirstOrDefaultAsync();

        return result;
    }
}

