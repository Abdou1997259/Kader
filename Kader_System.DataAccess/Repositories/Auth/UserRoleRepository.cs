namespace Kader_System.DataAccess.Repositories.Auth;

public class UserRoleRepository : BaseRepository<ApplicationUserRole>, IUserRoleRepository
{
    public UserRoleRepository(KaderDbContext context) : base(context)
    {
    }
}
