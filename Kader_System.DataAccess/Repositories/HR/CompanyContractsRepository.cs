namespace Kader_System.DataAccess.Repositories.HR
{
    public class CompanyContractsRepository(KaderDbContext context) : BaseRepository<HrCompanyContract>(context), ICompanyContractsRepository
    {
    }
}
