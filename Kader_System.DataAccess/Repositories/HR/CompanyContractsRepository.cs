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

        public async Task<int> UpdateCompanyContractFileNames(int id,string newFileName)
        {
            var result = await context.CompanyContracts
                                  .Where(x => x.Id == id).
                                  ExecuteUpdateAsync(x => x.
                                  SetProperty(p => p.CompanyContracts, newFileName));
            return result;
        }
    }
}
