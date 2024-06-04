namespace Kader_System.DataAccess.Repositories.Trans;

public class TransAmountTypeRepository(KaderDbContext context) : BaseRepository<TransAmountType>(context), ITransAmountTypeRepository
{
}
