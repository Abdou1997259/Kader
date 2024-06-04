namespace Kader_System.DataAccess.Repositories.HR;

public class BenefitRepository(KaderDbContext context) : BaseRepository<HrBenefit>(context), IBenefitRepository
{
}
