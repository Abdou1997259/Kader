namespace Kader_System.Domain.Interfaces.Trans;

public interface ITransAllowanceRepository : IBaseRepository<TransAllowance>
{
    List<TransAllowanceData> GetTransAllowanceInfo(
        Expression<Func<TransAllowance, bool>> filter,
        Expression<Func<TransAllowanceData, bool>> filterSearch,
        int? skip = null,
        int? take = null, string lang = "ar"
    );
}
