using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.DataAccess.Repositories.HR;

public class ShiftRepository(KaderDbContext context) : BaseRepository<HrShift>(context), IShiftRepository
{
    public List<ShiftData> GetShiftInfo(
        Expression<Func<HrShift, bool>> shiftFilter,
        int? skip = null,
        int? take = null)
    {
        var query = context.Set<HrShift>()
        
        .Where(shiftFilter)
        .GroupJoin(
        context.Set<HrEmployee>(),
                v => v.Id,
                e => e.ShiftId,
                (j, employees) => new { Shift = j, Employees = employees })
            .SelectMany(
                x => x.Employees.DefaultIfEmpty(),
                (shift, employee) => new { shift.Shift, Employee = employee })

            .GroupJoin(
                context.Set<ApplicationUser>(),
                x => x.Shift.Added_by,  // Property in HrShift to join on
                user => user.Id,      // Property in ApplicationUser to join on
                (shiftEmployee, users) => new { ShiftEmployee = shiftEmployee, Users = users })
            .SelectMany(
                x => x.Users.DefaultIfEmpty(),
                (shiftEmployee, user) => new { shiftEmployee.ShiftEmployee, User = user });




        var groupedQuery = query
            .GroupBy(x => new { x.ShiftEmployee.Shift.Id, x.ShiftEmployee.Shift.Name_ar, x.ShiftEmployee.Shift.Name_en, x.ShiftEmployee.Shift.Start_shift, x.ShiftEmployee.Shift.End_shift })
            .Select(group => new ShiftData()
            {
                Id = group.Key.Id,
                Name = group.Key.Name_ar,
                Start_shift = group.Key.Start_shift,
                End_shift = group.Key.End_shift,
                EmployeesCount = group.Count(x => x.ShiftEmployee.Employee != null),
                AddedByUser = group.FirstOrDefault()!.User!.UserName,
            });

        if (take.HasValue)
            groupedQuery = groupedQuery.Take(take.Value);
        if (skip.HasValue)
            groupedQuery = groupedQuery.Skip(skip.Value);
        return groupedQuery.ToList();
    }
}
