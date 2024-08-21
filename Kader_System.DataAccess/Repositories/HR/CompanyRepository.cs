using Kader_System.Domain.DTOs.Response.HR;
using System.Linq;

namespace Kader_System.DataAccess.Repositories.HR;

public class CompanyRepository(KaderDbContext context) : BaseRepository<HrCompany>(context), ICompanyRepository
{
    private readonly KaderDbContext _context=context;   
    public async Task<IEnumerable<EmployeeOfCompanyResponse>> GetEmployeeOfCompany(int companyId,string lang ,Expression<Func<EmployeeOfCompanyResponse, bool>>? filter,int? take,int? skip)
    {

        var query = from c in _context.Companys
                    join m in _context.Managements
                    on c.Id equals m.CompanyId
                    join d in _context.Departments
                    on m.Id equals d.ManagementId
                    join e in _context.Employees
                    on d.Id equals e.DepartmentId
                    join j in _context.HrJobs
                     on e.JobId equals j.Id
                    join n in _context.Nationalities
                    on e.NationalityId equals n.Id
                 
                    where c.Id == companyId  && e.IsDeleted == false
                    select new EmployeeOfCompanyResponse
                    {
                        id = companyId,
                        employee_name = Localization.Arabic == lang ? e.FullNameAr : e.FullNameEn,
                        job_name = Localization.Arabic == lang ? j.NameAr : j.NameEn,
                        management_name = Localization.Arabic == lang ? m.NameAr : j.NameEn,
                        nationality_name = Localization.Arabic == lang ? n.Name : n.NameInEnglish,

                    };

        if (filter is not null)
            query = query.Where(filter);

        if(take.HasValue )
           query= query.Take(take.Value);
        if(skip.HasValue)
                query = query.Skip(skip.Value);
        return await query.ToListAsync();



    }
}
