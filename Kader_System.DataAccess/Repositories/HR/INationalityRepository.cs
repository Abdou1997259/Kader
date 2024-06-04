namespace Kader_System.DataAccess.Repositories.HR
{
    public class NationalityRepository(KaderDbContext context) : BaseRepository<HrNationality>(context), INationalityRepository
    {
    }
}
