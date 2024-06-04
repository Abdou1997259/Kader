namespace Kader_System.Domain.Interfaces.Trans;

public interface ITransDeductionRepository : IBaseRepository<TransDeduction>
{
    List<TransDeductionData> GetTransDeductionInfo(
        Expression<Func<TransDeduction, bool>> filter,
        Expression<Func<TransDeductionData, bool>> filterSearch,
        int? skip = null,
        int? take = null, string lang = "ar"
    );
}
