namespace Kader_System.DataAccess.Repositories.HR
{
    public class GenderRepository(KaderDbContext context) :  BaseRepository<HrGender>(context), IGenderRepository
    {
    }
}
