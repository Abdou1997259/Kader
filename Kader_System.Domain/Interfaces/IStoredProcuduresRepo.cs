using Kader_System.Domain.DTOs.Request.Setting;

namespace Kader_System.Domain.Interfaces
{
    public interface IStoredProcuduresRepo
    {
        Task<IEnumerable<SpCaclauateSalaryDetails>>
       CalculateSalaryDetails(DateOnly startCalculationDate,
       DateOnly EndCalculationDate, string listEmployeesString);
        Task<GetMyProfilePermissionAndScreen> SpGetScreen(string userId, int titleId, string lang);
        Task<IEnumerable<SpCacluateSalary>> SpCalculateSalary(DateOnly actionDate,
            int days, string EmpId, int? companyId, int? departmentId, int? empId);
        Task<IEnumerable<SpCaclauateSalaryDetails>> SpCalculateSalaryDetails(DateOnly startCalculationDate,
         DateOnly EndCalculationDate, string listEmployeesString, string EmpId, int? companyId, int? departmentId, int? empId);
        Task<IEnumerable<SpCaclauateSalaryDetails>> SpCalculateSalaryDetails(DateOnly startCalculationDate, int days, string Emps);
        Task<IEnumerable<SpCaclauateSalaryDetailedTrans>> SpCalculatedSalaryDetailedInfo(DateOnly startCalculationDate, int days, string Emps);

        Task<IEnumerable<SpCacluateSalary>> Sp_GET_EMP_SALARY(DateOnly startCalculationDate,
             int days, int? companyId, int? departmentId, int? empId);

        Task<IEnumerable<Get_Details_Calculations>> Get_Details_Calculations(DateOnly startCalculationDate,
             DateOnly EndCalculationDate, int?
            companyId, int? departmentId, int? empId, int lang);
    }
}
