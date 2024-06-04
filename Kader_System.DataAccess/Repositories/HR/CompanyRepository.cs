namespace Kader_System.DataAccess.Repositories.HR;

public class CompanyRepository(KaderDbContext context) : BaseRepository<HrCompany>(context), ICompanyRepository
{
}
