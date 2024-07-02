namespace Kader_System.Domain.Interfaces
{
    public interface IStoredProcuduresRepo
    {
        Task<IEnumerable<SpCacluateSalary>> SpCacluateSalaries(DateOnly actionDate, int EmpId);
    }
}
