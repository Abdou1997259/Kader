﻿using Kader_System.Domain.DTOs.Response.Trans;

namespace Kader_System.DataAccess.Repositories.Trans;

public class TransCovenantRepository(KaderDbContext context) : BaseRepository<TransCovenant>(context), ITransCovenantRepository
{
    public List<TransCovenantData> GetTransCovenantDataInfo(
       Expression<Func<TransCovenant, bool>> filter,
       Expression<Func<TransCovenantData, bool>> filterSearch,
       int? skip = null,
       int? take = null, string lang = "ar"
      )
    {

        var query = from trans in context.TransCovenants.Where(filter)
                    .OrderByDescending(c => c.id)
                    join employee in context.Employees on trans.employee_id
                    equals employee.Id into empGroup
                    from employee in empGroup.DefaultIfEmpty()
                    join job in context.HrJobs on employee.JobId equals job.Id into jobGroup
                    from job in jobGroup.DefaultIfEmpty()
                    join u in context.Users on trans.Added_by equals u.Id into userGroup
                    from u in userGroup.DefaultIfEmpty()
                    select new TransCovenantData()
                    {

                        AddedBy = u.UserName,
                        AddedOn = trans.Add_date,
                        Amount = trans.amount,
                        EmployeeId = trans.employee_id,
                        EmployeeName = lang == Localization.Arabic ? employee.FullNameAr : employee.FullNameEn,
                        Id = trans.id,
                        Notes = trans.notes,
                        NameAr = trans.name_ar,
                        NameEn = trans.name_en,
                        Date = trans.date,
                        JobName = lang == Localization.Arabic ? job.NameAr : job.NameEn,
                    };

        if (filterSearch != null)
            query = query.Where(filterSearch);

        if (skip.HasValue)
            query = query.Skip(skip.Value);
        if (take.HasValue)
            query = query.Take(take.Value);


        return query.ToList();

    }
}
