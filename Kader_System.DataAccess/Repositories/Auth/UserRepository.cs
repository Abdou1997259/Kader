
namespace Kader_System.DataAccess.Repositories.Auth;

public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
{
    public UserRepository(KaderDbContext context) : base(context)
    {
    }
}
