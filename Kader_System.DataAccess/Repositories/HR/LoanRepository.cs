namespace Kader_System.DataAccess.Repositories.HR
{
    public class LoanRepository(KaderDbContext db) : BaseRepository<HrLoan>(db), ILoanRepository
    {
    }
}
