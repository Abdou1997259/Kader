namespace Kader_System.DataAccess.Repositories.HR;

public class FingerPrintRepository(KaderDbContext context) : BaseRepository<HrFingerPrint>(context), IFingerPrintRepository
{
}
