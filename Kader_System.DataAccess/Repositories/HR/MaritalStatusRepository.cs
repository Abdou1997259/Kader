namespace Kader_System.DataAccess.Repositories.HR
{
    public class MaritalStatusRepository(KaderDbContext context) : BaseRepository<HrMaritalStatus>(context), IMaritalStatusRepository
    {
    }
}
