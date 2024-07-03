namespace Kader_System.Domain.Interfaces
{
    public interface IStoredProcuduresRepo
    {
        Task<IEnumerable<SpCacluateSalary>> SpCalculateSalary(DateOnly actionDate, int days, string EmpId);
        Task<IEnumerable<SpCaclauateSalaryDetails>> SpCalculateSalaryDetails(DateOnly startCalculationDate, int days, string Emps);
        Task<IEnumerable<SpCaclauateSalaryDetailedTrans>> SpCalculatedSalaryDetailedTrans(DateOnly startCalculationDate, int days, string Emps);
    }
}
