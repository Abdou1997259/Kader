namespace Kader_System.DataAccess.Repositories.HR;

public class EmployeeAttachmentRepository(KaderDbContext context) : BaseRepository<HrEmployeeAttachment>(context), IEmployeeAttachmentRepository
{
}
