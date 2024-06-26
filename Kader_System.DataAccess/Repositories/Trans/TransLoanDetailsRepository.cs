

namespace Kader_System.DataAccess.Repositories.Trans
{
    public class TransLoanDetailsRepository(KaderDbContext db) : BaseRepository<TransLoanDetails>(db), ITransLoanDetailsRepository
    {
    }
}
