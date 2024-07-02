

namespace Kader_System.DataAccess.Repositories.Trans
{
    public class TransSalaryCalculatorRepo(KaderDbContext db) : BaseRepository<TransSalaryCalculator>(db), ITransSalaryCalculatorRepo
    {
    }
}
