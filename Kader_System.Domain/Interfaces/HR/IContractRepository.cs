using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.Domain.Interfaces.HR;

public interface IContractRepository : IBaseRepository<HrContract>
{

    List<ContractData> GetAllContractsAsync(
        Expression<Func<HrContract, bool>> contractFilter,
        string lang,
        int? skip = null,
        int? take = null);

    GetContractDataByIdResponse GetContractById(int id, string lang);
}
