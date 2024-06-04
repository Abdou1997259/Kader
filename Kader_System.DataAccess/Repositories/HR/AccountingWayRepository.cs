namespace Kader_System.DataAccess.Repositories.HR;

public class AccountingWayRepository(KaderDbContext context) : BaseRepository<HrSalaryCalculator>(context), IAccountingWayRepository
{
}
