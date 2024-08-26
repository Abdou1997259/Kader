using Microsoft.EntityFrameworkCore;

namespace Kader_System.DataAccess.Repositories.HR
{
    public class CompanyContractsRepository(KaderDbContext context) : BaseRepository<HrCompanyContract>(context), ICompanyContractsRepository
    {
        public async Task<List<string>> GetCompanyContractFileNames(int companyId)
        {
            var result = await context.CompanyContracts.
                                  AsNoTracking()
                                 .Where(x => x.CompanyId == companyId).
                                 Select(f => f.CompanyContracts).
                                 ToListAsync();
            return result;
        }

        public async Task<int> UpdateCompanyContractFileNames(List<GetFileNameAndExtension> fileNames)
        {
            var result = 0;
            if (fileNames is null)
                return result;

            foreach (var file in fileNames)
            {
                result = (await context.CompanyContracts.
                                   AsNoTracking().Where(x => x.Id == file.fileId).
                                   ExecuteUpdateAsync(x => x.
                                   SetProperty(p => p.CompanyContracts, file.FileName).
                                   SetProperty(p => p.CompanyContractsExtension, file.FileExtension))) + result;
            }
            return result;
        }
    }
}
