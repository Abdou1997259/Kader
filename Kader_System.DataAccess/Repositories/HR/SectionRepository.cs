namespace Kader_System.DataAccess.Repositories.HR;

public class SectionRepository(KaderDbContext context) : BaseRepository<HrSection>(context), ISectionRepository
{
}
