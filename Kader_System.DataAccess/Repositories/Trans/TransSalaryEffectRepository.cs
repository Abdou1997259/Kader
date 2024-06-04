namespace Kader_System.DataAccess.Repositories.Trans;

public class TransSalaryEffectRepository(KaderDbContext context) : BaseRepository<TransSalaryEffect>(context), ITransSalaryEffectRepository
{
}
