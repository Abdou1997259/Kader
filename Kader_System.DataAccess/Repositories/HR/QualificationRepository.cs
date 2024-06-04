using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.DataAccess.Repositories.HR;

public class QualificationRepository(KaderDbContext context) : BaseRepository<HrQualification>(context), IQualificationRepository
{


    public List<QualificationData> GetQualificationInfo(
        Expression<Func<HrQualification, bool>> qualFilter,
        int? skip = null,
        int? take = null,string lang="ar")
    {
        var query = context.Set<HrQualification>()

            .Where(qualFilter)
            .GroupJoin(
                context.Set<HrEmployee>(),
                v => v.Id,
                e => e.ShiftId,
                (j, employees) => new { Qualification = j, Employees = employees })
            .SelectMany(
                x => x.Employees.DefaultIfEmpty(),
                (shift, employee) => new { shift.Qualification, Employee = employee })

            .GroupJoin(
                context.Set<ApplicationUser>(),
                x => x.Qualification.Added_by,  // Property in HrShift to join on
                user => user.Id,      // Property in ApplicationUser to join on
                (shiftEmployee, users) => new { QualificationEmployee = shiftEmployee, Users = users })
            .SelectMany(
                x => x.Users.DefaultIfEmpty(),
                (shiftEmployee, user) => new { shiftEmployee.QualificationEmployee, User = user });


        var groupedQuery= query
            .GroupBy(x => new { x.QualificationEmployee.Qualification.Id, x.QualificationEmployee.Qualification.NameAr, x.QualificationEmployee.Qualification.NameEn })
            .Select(group => new QualificationData()
            {
                Id = group.Key.Id,
                Name =lang==Localization.Arabic? group.Key.NameAr:group.Key.NameEn,
                EmployeesCount = group.Count(x => x.QualificationEmployee.Employee != null),
                AddedByUser = group.FirstOrDefault()!.User!.UserName,
            });
        if (take.HasValue)
            groupedQuery = groupedQuery.Take(take.Value);
        if (skip.HasValue)
            groupedQuery = groupedQuery.Skip(skip.Value);
        return groupedQuery.ToList();

    }
}
