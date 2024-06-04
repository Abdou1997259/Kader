namespace Kader_System.DataAccess.Repositories.Auth;

public class RoleRepository : BaseRepository<ApplicationRole>, IRoleRepository
{
    public RoleRepository(KaderDbContext context) : base(context)
    {
    }
}
