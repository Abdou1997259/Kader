using Kader_System.Domain.DTOs.Request.HR;
using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.DataAccess.Repositories.HR;

public class JobRepository(KaderDbContext context) : BaseRepository<HrJob>(context), IJobRepository
{
    private readonly KaderDbContext _context = context;
    public List<JobData> GetJobInfo(
        Expression<Func<HrJob, bool>> jobFilter,
        int? skip = null,
        int? take = null)
    {
        var query = context.Set<HrJob>()
        .Where(jobFilter)
        .GroupJoin(
            context.Set<HrEmployee>(),
            job => job.Id,
            employee => employee.JobId,
            (job, employees) => new { Job = job, Employees = employees })
        .SelectMany(
            jobWithEmployees => jobWithEmployees.Employees.DefaultIfEmpty(),
            (jobWithEmployees, employee) => new { JobWithEmployees = jobWithEmployees, Employee = employee })
        .GroupJoin(
            context.Users,
            je => je.Employee.Added_by,
            user => user.Id,
            (je, users) => new { JobWithEmployees = je.JobWithEmployees, Employee = je.Employee, Users = users })
        .SelectMany(
            jeWithUsers => jeWithUsers.Users.DefaultIfEmpty(),
            (jeWithUsers, user) => new
            {
                Job = jeWithUsers.JobWithEmployees.Job,
                Employee = jeWithUsers.Employee,
                AddedBy=user.FullName ?? ""
            });


        // Continue with the rest of the query
        var groupedQuery = query
             
            .GroupBy(x => new { x.Job.Id, x.Job.NameAr, x.Job.NameEn, x.Job.HasAdditionalTime, x.Job.HasNeedLicense,x.AddedBy })
           
            .Select(group => new JobData()
            {
                Id = group.Key.Id,
                Name = group.Key.NameAr,
                EmployeesCount = group.Count(x => x.Employee != null),
                HasAdditionalTime = group.Key.HasAdditionalTime,
                HasNeedLicense = group.Key.HasNeedLicense ?? false,
                AddedBy=group.Key.AddedBy,
                
            });
        if (skip.HasValue)
            groupedQuery = groupedQuery.Skip(skip.Value);
        if (take.HasValue)
            groupedQuery = groupedQuery.Take(take.Value);
        var querystring = groupedQuery.ToQueryString();
        return groupedQuery.ToList();
    }
}
