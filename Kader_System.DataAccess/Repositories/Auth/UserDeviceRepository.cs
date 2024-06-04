namespace Kader_System.DataAccess.Repositories.Auth;

public class UserDeviceRepository : BaseRepository<ApplicationUserDevice>, IUserDeviceRepository
{
    public UserDeviceRepository(KaderDbContext context) : base(context)
    {
    }
}
