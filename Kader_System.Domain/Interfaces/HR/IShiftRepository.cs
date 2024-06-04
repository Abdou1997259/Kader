using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.Domain.Interfaces.HR;

public interface IShiftRepository : IBaseRepository<HrShift>
{
    List<ShiftData> GetShiftInfo(
        Expression<Func<HrShift, bool>> shiftFilter,
        int? skip = null,
        int? take = null);
}
