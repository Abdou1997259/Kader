namespace Kader_System.DataAccess.Repositories.HR;

public class SectionDepartmentRepository(KaderDbContext context) : BaseRepository<HrSectionDepartment>(context), ISectionDepartmentRepository
{
}
