using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.DataAccess.Repositories.HR;

public class AllowanceRepository(KaderDbContext context) : BaseRepository<HrAllowance>(context), IAllowanceRepository
{
    public List<AllowanceData> GetAllowanceInfo(
        Expression<Func<HrAllowance, bool>> filter,
        int? skip = null,
        int? take = null
        ,string lang ="ar")
    {
        var query = context.Set<HrAllowance>()

            .Where(filter)
            .GroupJoin(
                context.Set<ApplicationUser>(),
                x => x.Added_by,  // Property in HrShift to join on
                user => user.Id,      // Property in ApplicationUser to join on
                (shiftEmployee, users) => new { Allowance = shiftEmployee, Users = users })
            .SelectMany(
                x => x.Users.DefaultIfEmpty(),
                (allowance, user) => new { allowance.Allowance, User = user });


        if (skip.HasValue)
            query = query.Skip(skip.Value);
        if (take.HasValue)
            query = query.Take(take.Value);

        return query
            .GroupBy(x => new { x.Allowance.Id, x.Allowance.Name_ar, x.Allowance.Name_en })
            .Select(group => new AllowanceData()
            {
                Id = group.Key.Id,
                Name = lang==Localization.Arabic? group.Key.Name_ar:group.Key.Name_en,
                AddedByUser = group.FirstOrDefault()!.User!.UserName
            })
            .ToList();
    }


    public async Task<object> GetAllowancesDataAsLookUp(string lang)
    {
        
        return await context.Allowances.
            Where(e => !e.IsDeleted)
            .Select(a => new
            {
                id = a.Id,
                name=lang==Localization.Arabic?a.Name_ar:a.Name_en

            }).ToListAsync();
    }
}