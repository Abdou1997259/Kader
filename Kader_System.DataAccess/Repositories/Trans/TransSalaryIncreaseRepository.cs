using Kader_System.Domain.DTOs.Response.Trans;

namespace Kader_System.DataAccess.Repositories.Trans
{
    public class TransSalaryIncreaseRepository(KaderDbContext context) : BaseRepository<TransSalaryIncrease>(context), ITransSalaryIncreaseRepository
    {
        private readonly KaderDbContext _context = context;
        public async Task<IEnumerable<EmployeeWithSalary>> GetEmployeeWithSalary(string lang, int companyId)
        {
            // Ensure you're using the correct 
            var result = _context.EmployeeLookupQueries.FromSql(@$"




                    select query.Id,Name,SUM(Salary) + SUM( c.fixed_salary) as Salary from (select e.Id as Id,case when {lang}='ar'
                    then e.FullNameAr 
                    else e.FullNameEn
                    end as Name,

                    case when t.Increase_type=2

                    then t.Amount/100 *c.fixed_salary
                    else t.Amount 
                    end as Salary

                    from hr_contracts c 
                    join hr_employees e 
                    on e.Id =c.employee_id
                    left join trans_salary_increases t
                    on e.Id=t.Employee_id and t.transactionDate<=GETDATE()
                    where e.IsActive=1and e.IsDeleted=0and c.IsDeleted=0    and e.CompanyId={companyId}

                    ) as query
                    join hr_contracts  c on
                    c.employee_id=query.Id
                    where c.IsDeleted=0 
                    group by query.Id,Name 


   ").Select(x => new EmployeeWithSalary
            {
                Id = x.Id,
                Salary = x.Salary,
                Name = x.Name,
            });






            return await result.ToListAsync();

        }
    }
}
