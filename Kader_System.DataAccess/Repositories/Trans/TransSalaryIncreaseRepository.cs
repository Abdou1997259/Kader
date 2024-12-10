using Kader_System.Domain.DTOs.Response.Trans;

namespace Kader_System.DataAccess.Repositories.Trans
{
    public class TransSalaryIncreaseRepository(KaderDbContext context) : BaseRepository<TransSalaryIncrease>(context), ITransSalaryIncreaseRepository
    {
        private readonly KaderDbContext _context = context;
        public async Task<IEnumerable<EmployeeWithSalary>> GetEmployeeWithSalary(string lang, int companyId, DateOnly date)
        {
            // Ensure you're using the correct 

            var result = _context.Employees.Where(x => x.CompanyId == companyId).Select(x => new EmployeeWithSalary
            {
                Id = x.Id,
                Salary = KaderDbContext.GetSalaryWithIncrease(x.Id, date),
                Name = lang == Localization.Arabic ? x.FullNameAr : x.FullNameEn


            });









            return await result.ToListAsync();

        }
    }
}
