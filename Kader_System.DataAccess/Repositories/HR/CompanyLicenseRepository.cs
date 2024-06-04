namespace Kader_System.DataAccess.Repositories.HR
{
    public class CompanyLicenseRepository(KaderDbContext context) :BaseRepository<CompanyLicense>(context),ICompanyLicenseRepository
    {
    }
}
