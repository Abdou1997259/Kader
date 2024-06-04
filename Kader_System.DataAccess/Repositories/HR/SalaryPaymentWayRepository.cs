namespace Kader_System.DataAccess.Repositories.HR;

public class SalaryPaymentWayRepository(KaderDbContext context) : BaseRepository<HrSalaryPaymentWay>(context), ISalaryPaymentWayRepository
{
}
