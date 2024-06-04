namespace Kader_System.DataAccess.Repositories.HR;

public class CompanyTypeRepository(KaderDbContext context) : BaseRepository<HrCompanyType>(context), ICompanyTypeRepository
{
}
