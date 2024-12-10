

using Kader_System.Domain.DTOs.Response;

namespace Kader_System.DataAccess.Repositories.Auth;

public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
{
    private readonly KaderDbContext _context;
    public UserRepository(KaderDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<CompanyYearResponse>> GetCompanyYearsAsync()
    {
        return (await _context.CompanyYears.ToListAsync()).Select(x => new CompanyYearResponse
        {
            Id = x.Id,
            Year = x.FinancialYear,
        }).ToList();
    }
}
