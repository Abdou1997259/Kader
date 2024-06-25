namespace Kader_System.DataAccess.Repositories.Trans
{
    public class TransLoanRepository(KaderDbContext db) : BaseRepository<TransLoan>(db), ITransLoanRepository
    {
    }
}
