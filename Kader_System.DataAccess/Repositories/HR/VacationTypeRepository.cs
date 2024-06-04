namespace Kader_System.DataAccess.Repositories.HR;

public class VacationTypeRepository(KaderDbContext context) : BaseRepository<HrVacationType>(context), IVacationTypeRepository
{
}
