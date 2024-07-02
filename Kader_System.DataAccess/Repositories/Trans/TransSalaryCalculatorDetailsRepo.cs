namespace Kader_System.DataAccess.Repositories.Trans
{
    public class TransSalaryCalculatorDetailsRepo(KaderDbContext db) :
        BaseRepository<TransSalaryCalculatorDetail>(db),
        ITransSalaryCalculatorDetailsRepo
    {
    }
}
