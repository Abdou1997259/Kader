namespace Kader_System.Domain.Interfaces.Trans;

public interface ITransSalaryIncreaseRepository : IBaseRepository<TransSalaryIncrease>
{
    Task<IEnumerable<EmployeeWithSalary>> GetEmployeeWithSalary(string lang, int companyId, DateOnly date);




}
