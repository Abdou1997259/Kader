using Kader_System.Domain.Interfaces.InterViews;
using Kader_System.Domain.Models.Interviews;

namespace Kader_System.DataAccess.Repositories.InterviewRepositories
{
    public class InterJobRepository(KaderDbContext db) : BaseRepository<Job>(db), IInterJobRepository
    {
    }
}
