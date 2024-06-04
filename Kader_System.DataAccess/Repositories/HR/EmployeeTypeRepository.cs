namespace Kader_System.DataAccess.Repositories.HR;

public class EmployeeTypeRepository(KaderDbContext context) : BaseRepository<HrEmployeeType>(context), IEmployeeTypeRepository
{
}
