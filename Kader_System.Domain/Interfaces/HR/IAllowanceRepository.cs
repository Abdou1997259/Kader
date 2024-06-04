using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.Domain.Interfaces.HR;

public interface IAllowanceRepository : IBaseRepository<HrAllowance>
{
    List<AllowanceData> GetAllowanceInfo(
        Expression<Func<HrAllowance, bool>> filter,
        int? skip = null,
        int? take = null
        , string lang = "ar");

    Task<object> GetAllowancesDataAsLookUp(string lang);
}
