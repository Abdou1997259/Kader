namespace Kader_System.Domain.Interfaces.HR;

public interface IDepartmentRepository : IBaseRepository<HrDepartment>
{
    IQueryable<HrDepartment> GetDepartmentsByCompanyId(int companyId);
}
