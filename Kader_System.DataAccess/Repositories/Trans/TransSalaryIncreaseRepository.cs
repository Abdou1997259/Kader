using Kader_System.Domain.DTOs.Response.Trans;

namespace Kader_System.DataAccess.Repositories.Trans;

public class TransSalaryIncreaseRepository(KaderDbContext context) : BaseRepository<TransSalaryIncrease>(context), ITransSalaryIncreaseRepository
{
    public Task<int> AddNewSalaryIncrease(TransSalaryIncreaseResponse salaryIncreaseResponse)
    {
       var empSalary = context.Employees.AsNoTracking().
            Where(x =>x.Id == salaryIncreaseResponse.employeeId).
            Select(x =>x.TotalSalary).
            FirstOrDefault();

        var salaryIncrease = new TransSalaryIncrease()
        {
            Notes = salaryIncreaseResponse.details,
            Amount = salaryIncreaseResponse.increaseValue,
            Employee_id = salaryIncreaseResponse.employeeId,
            Increase_type = salaryIncreaseResponse.salrayIncreaseTypeId,
            transactionDate = DateTime.Now,
            dueDate = DateTime.Now.AddMonths(1),
            salaryAfterIncrease = salaryIncreaseResponse.increaseValue + empSalary
        };
        context.TransSalaryIncreases.Add(salaryIncrease);
        return context.SaveChangesAsync();  

    }
}
