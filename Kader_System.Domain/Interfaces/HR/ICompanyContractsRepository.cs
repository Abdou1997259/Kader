namespace Kader_System.Domain.Interfaces.HR
{
    public interface ICompanyContractsRepository:IBaseRepository<HrCompanyContract>
    {
        public  Task<List<string>> GetCompanyContractFileNames(int companyId);
        public Task<int> UpdateCompanyContractFileNames(int id, string newFileName);
    }
}
