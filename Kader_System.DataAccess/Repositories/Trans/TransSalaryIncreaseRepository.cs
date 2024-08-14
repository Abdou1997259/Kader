using Kader_System.Domain.Constants.Enums;
using Kader_System.Domain.DTOs.Request.HR.Loan;
using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.Trans;
using Kader_System.Domain.DTOs.Response.Loan;
using Kader_System.Domain.DTOs.Response.Trans;
using Kader_System.Domain.Interfaces;
using Kader_System.Domain.Models.Trans;
using Kader_System.Domain.DTOs.Request.HR;
using Kader_System.Domain;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace Kader_System.DataAccess.Repositories.Trans
{
    public class TransSalaryIncreaseRepository(KaderDbContext context) : BaseRepository<TransSalaryIncrease>(context), ITransSalaryIncreaseRepository
    {
        private readonly KaderDbContext _context = context;
        public async Task<IEnumerable<EmployeeWithSalary>> GetEmployeeWithSalary(string lang)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var query = from e in _context.Employees
                        join c in _context.Contracts
                        on e.Id equals c.EmployeeId
                        where e.IsDeleted == false  && c.IsDeleted ==false
                        select new
                        {
                            Name = Localization.Arabic == lang ? e.FullNameAr : e.FullNameEn,
                            e.Id,
                            c.FixedSalary
                            
                        }
                        into ecemps
                        join i in _context.TransSalaryIncreases
                        on ecemps.Id equals i.Employee_id
                        into iemps
                        from grouIcemp in iemps.DefaultIfEmpty()
                        where grouIcemp.transactionDate < today || grouIcemp ==null && grouIcemp.IsDeleted==false
                        select new EmployeeWithSalary
                        {
                            Name = ecemps.Name,
                            Id = ecemps.Id,
                            Salary = ecemps.FixedSalary + (grouIcemp == null ? 0 : grouIcemp.Amount),

                        };


            return await query.ToListAsync();

        }
    }
}
