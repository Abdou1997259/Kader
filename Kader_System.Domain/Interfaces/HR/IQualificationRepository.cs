using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.Domain.Interfaces.HR;

public interface IQualificationRepository : IBaseRepository<HrQualification>
{
    List<QualificationData> GetQualificationInfo(
        Expression<Func<HrQualification, bool>> qualFilter,
        int? skip = null,
        int? take = null, string lang = "ar");
}

