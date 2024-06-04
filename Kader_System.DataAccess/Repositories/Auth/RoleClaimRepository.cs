namespace Kader_System.DataAccess.Repositories.Auth;

public class RoleClaimRepository(KaderDbContext context) : BaseRepository<ApplicationRoleClaim>(context), IRoleClaimRepository
{
}
