using Kader_System.Domain.DTOs.Request.HR;
using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.DataAccess.Repositories.HR;

public class JobRepository(KaderDbContext context) : BaseRepository<HrJob>(context), IJobRepository
{
    public List<JobData> GetJobInfo(
        Expression<Func<HrJob, bool>> jobFilter,
        int? skip = null,
        int? take = null)
    {
        var query = context.Set<HrJob>()
            .Where(jobFilter)
            .GroupJoin(
                context.Set<HrEmployee>(),
                v => v.Id,
                e => e.JobId,
                (j, employees) => new { Job = j, Employees = employees })
            .SelectMany(
                x => x.Employees.DefaultIfEmpty(),
                (vacation, employee) => new { vacation.Job, Employee = employee });


        // Continue with the rest of the query
        var groupedQuery = query
            .GroupBy(x => new { x.Job.Id, x.Job.NameAr, x.Job.NameEn, x.Job.HasAdditionalTime, x.Job.HasNeedLicense })
            .Select(group => new JobData()
            {
                Id = group.Key.Id,
                Name = group.Key.NameAr,
                EmployeesCount = group.Count(x => x.Employee != null),
                HasAdditionalTime = group.Key.HasAdditionalTime,
                HasNeedLicense = group.Key.HasNeedLicense,
            });
      
        if (take.HasValue)
            groupedQuery = groupedQuery.Take(take.Value);
        if (skip.HasValue)
            groupedQuery = groupedQuery.Skip(skip.Value);
        return groupedQuery.ToList();
    }
}
