using Kader_System.Domain.DTOs.Response;

namespace Kader_System.Domain.Interfaces.Auth;

public interface IUserRepository : IBaseRepository<ApplicationUser>
{
    Task<IReadOnlyList<CompanyYearResponse>> GetCompanyYearsAsync();
}
