using Kader_System.Domain.DTOs.Request.Setting;

namespace Kader_System.Domain.Interfaces
{
    public interface IStoredProcuduresRepo
    {
        Task<GetMyProfilePermissionAndScreen> SpGetScreen(string userId, int titleId, string lang);
        Task<IEnumerable<SpCacluateSalary>> SpCalculateSalary(DateOnly actionDate, int days, string EmpId);
        Task<IEnumerable<SpCaclauateSalaryDetails>> SpCalculateSalaryDetails(DateOnly startCalculationDate, int days, string Emps);
        Task<IEnumerable<SpCaclauateSalaryDetailedTrans>> SpCalculatedSalaryDetailedInfo(DateOnly startCalculationDate, int days, string Emps);
    }
}
