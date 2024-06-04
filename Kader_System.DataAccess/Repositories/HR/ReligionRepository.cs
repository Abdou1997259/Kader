namespace Kader_System.DataAccess.Repositories.HR
{
    public class ReligionRepository(KaderDbContext context) :   BaseRepository<HrRelegion>(context), IReligionRepository
    {
    }
}
