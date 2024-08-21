using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.Domain.Interfaces.HR;

public interface ICompanyRepository : IBaseRepository<HrCompany>
{
    Task<IEnumerable<EmployeeOfCompanyResponse>> GetEmployeeOfCompany(int companyId, string lang, Expression<Func<EmployeeOfCompanyResponse, bool>>? filter, int? take, int? skip);


}
