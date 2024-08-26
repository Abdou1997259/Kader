namespace Kader_System.Domain.Interfaces.HR
{
    public interface ICompanyLicenseRepository:IBaseRepository<CompanyLicense>
    {
        public  Task<List<string>> GetCompanyLicensesFileNames(int companyId);
        public Task<int> UpdateCompanyLicenseFileNames(List<GetFileNameAndExtension> fileNames);

    }
}
