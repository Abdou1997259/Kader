using Kader_System.Domain.DTOs.Request.HR;

namespace Kader_System.Domain.Interfaces.HR;

public interface IJobRepository : IBaseRepository<HrJob>
{
     List<JobData> GetJobInfo(
        Expression<Func<HrJob, bool>> jobFilter,
        int? skip = null,
        int? take = null);
}
