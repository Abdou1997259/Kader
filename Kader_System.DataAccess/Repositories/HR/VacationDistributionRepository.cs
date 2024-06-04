namespace Kader_System.DataAccess.Repositories.HR;

public class VacationDistributionRepository(KaderDbContext context) : BaseRepository<HrVacationDistribution>(context), IVacationDistributionRepository
{
}
