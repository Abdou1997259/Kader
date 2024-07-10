using Kader_System.Domain.DTOs.Request.Trans;

namespace Kader_System.Domain.Interfaces.Trans;

public interface ITransSalaryIncreaseRepository : IBaseRepository<TransSalaryIncrease>
{
    public Task<List<TransSalaryIncreaseResponse>> GetAllSalaryIncrease();
    public Task<TransSalaryIncreaseResponse> GetSalaryIncreaseById(int id);
    public Task<int> AddNewSalaryIncrease(CreateTransSalaryIncreaseRequest salaryIncreaseResponse);
}
