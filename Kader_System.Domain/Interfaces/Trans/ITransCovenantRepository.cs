namespace Kader_System.Domain.Interfaces.Trans;

public interface ITransCovenantRepository : IBaseRepository<TransCovenant>
{
    List<TransCovenantData> GetTransCovenantDataInfo(
        Expression<Func<TransCovenant, bool>> filter,
        Expression<Func<TransCovenantData, bool>> filterSearch,
        int? skip = null,
        int? take = null, string lang = "ar"
    );
}
