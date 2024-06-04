namespace Kader_System.DataAccess.Repositories.HR;

public class DeductionRepository(KaderDbContext context) : BaseRepository<HrDeduction>(context), IDeductionRepository
{
}
