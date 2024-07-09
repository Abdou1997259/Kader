namespace Kader_System.Domain.Interfaces.Trans;

public interface ITransSalaryIncreaseRepository : IBaseRepository<TransSalaryIncrease>
{
    public Task<int> AddNewSalaryIncrease(TransSalaryIncreaseResponse salaryIncreaseResponse);
}
