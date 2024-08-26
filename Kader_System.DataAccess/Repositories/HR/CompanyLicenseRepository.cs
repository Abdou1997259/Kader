using Microsoft.EntityFrameworkCore;

namespace Kader_System.DataAccess.Repositories.HR
{
    public class CompanyLicenseRepository(KaderDbContext context) : BaseRepository<CompanyLicense>(context), ICompanyLicenseRepository
    {
        public async Task<List<string>> GetCompanyLicensesFileNames(int companyId)
        {
            var result = await context.CompanyLicenses.
                                  AsNoTracking()
                                 .Where(x => x.CompanyId == companyId).
                                 Select(f => f.LicenseName).
                                 ToListAsync();
            return result;
        }

        public async Task<int> UpdateCompanyLicenseFileNames(List<GetFileNameAndExtension> fileNames)
        {
            var result = 0;
            if (fileNames is null)
                return result;

            foreach (var file in fileNames)
            {
                result = (await context.CompanyLicenses.
                                   AsNoTracking().Where(x => x.Id == file.fileId).
                                   ExecuteUpdateAsync(x => x.
                                   SetProperty(p => p.LicenseName, file.FileName).
                                   SetProperty(p => p.LicenseExtension, file.FileExtension))) + result;
            }
            return result;
        }
    }
}
