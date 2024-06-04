namespace Kader_System.Domain.Interfaces.Trans;

public interface ITransBenefitRepository : IBaseRepository<TransBenefit>
{
    List<TransBenefitData> GetTransBenefitInfo(
        Expression<Func<TransBenefit, bool>> filter,
        Expression<Func<TransBenefitData, bool>> filterSearch,
        int? skip = null,
        int? take = null, string lang = "ar"
    );
}
