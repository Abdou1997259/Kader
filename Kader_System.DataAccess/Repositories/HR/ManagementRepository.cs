namespace Kader_System.DataAccess.Repositories.HR
{
    public class ManagementRepository(KaderDbContext context): BaseRepository<HrManagement>(context), IManagementRepository
    {

    }
}
