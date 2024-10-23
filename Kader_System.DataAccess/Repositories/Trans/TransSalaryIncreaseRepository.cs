using Kader_System.Domain.DTOs.Response.Trans;

namespace Kader_System.DataAccess.Repositories.Trans
{
    public class TransSalaryIncreaseRepository(KaderDbContext context) : BaseRepository<TransSalaryIncrease>(context), ITransSalaryIncreaseRepository
    {
        private readonly KaderDbContext _context = context;
        public async Task<IEnumerable<EmployeeWithSalary>>
            GetEmployeeWithSalary(string lang, int companyId)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var query = from e in _context.Employees
                        join c in _context.Contracts
                        on e.Id equals c.employee_id
                        where e.IsDeleted == false &&
                        c.IsDeleted == false && e.CompanyId == companyId &&
                        c.company_id == companyId
                        select new
                        {
                            Name = Localization.Arabic == lang ? e.FullNameAr : e.FullNameEn,
                            e.Id,
                            c.fixed_salary
                        }
                        into ecemps
                        join i in _context.TransSalaryIncreases
                        on ecemps.Id equals i.Employee_id into iemps
                        from grouIcemp in iemps.DefaultIfEmpty()
                        group new { ecemps, grouIcemp } by new
                        {
                            ecemps.Name,
                            ecemps.Id,
                            ecemps.fixed_salary
                        } into g
                        select new EmployeeWithSalary
                        {
                            Name = g.Key.Name,
                            Id = g.Key.Id,
                            Salary = g.Key.fixed_salary + g.Sum(x =>
                                x.grouIcemp != null && x.grouIcemp.transactionDate <= today
                                    ? (x.grouIcemp.Increase_type == 2
                                        ? (x.grouIcemp.Amount / 100) * g.Key.fixed_salary
                                        : x.grouIcemp.Amount)
                                    : 0)
                        };



            var s = query.ToQueryString();


            return await query.ToListAsync();

        }
    }
}
