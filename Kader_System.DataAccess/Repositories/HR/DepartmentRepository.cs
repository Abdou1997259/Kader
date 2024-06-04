namespace Kader_System.DataAccess.Repositories.HR;

public class DepartmentRepository(KaderDbContext context) : BaseRepository<HrDepartment>(context), IDepartmentRepository
{
    public IQueryable<HrDepartment> GetDepartmentsByCompanyId(int companyId)
    {
        return context.Departments.Where(d => context.Managements
            .Where(m => m.CompanyId == companyId)
            .Select(m => m.Id)
            .Contains(d.ManagementId));
    }
}
