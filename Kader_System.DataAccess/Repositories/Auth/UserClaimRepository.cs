namespace Kader_System.DataAccess.Repositories.Auth
{
    public class UserClaimRepository(KaderDbContext context) : BaseRepository<ApplicationUserClaim>(context), IUserClaimRepository
    {
    }
}
