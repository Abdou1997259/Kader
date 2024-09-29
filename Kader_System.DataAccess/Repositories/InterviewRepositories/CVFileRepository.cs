using Kader_System.Domain.Interfaces.EducationRepositories;
using Kader_System.Domain.Models.Interviews;

namespace Kader_System.DataAccess.Repositories.InterviewRepositories
{
    public class CVFileRepository(KaderDbContext context) : BaseRepository<CvFile>(context), ICVFileRepository
    {
    }
}
