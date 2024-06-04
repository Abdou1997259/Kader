using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.Domain.Interfaces.HR;

public interface IVacationRepository : IBaseRepository<HrVacation>
{
    List<VacationData> GetVacationInfo(
        Expression<Func<HrVacation, bool>> vacationFilter,
        int? skip = null,
        int? take = null
        ,string lang="ar");
}
