using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.Domain.Interfaces.HR;

public interface IEmployeeRepository : IBaseRepository<HrEmployee>
{
    Response<GetEmployeeByIdResponse> GetEmployeeByIdAsync(int id, string lang);
    Task<object> GetEmployeesDataAsLookUp(string lang);
    Task<object> GetEmployeesDataNameAndIdAsLookUp(string lang);
    List<EmployeesData> GetEmployeesInfo(
        Expression<Func<HrEmployee, bool>> filter,
        Expression<Func<EmployeesData, bool>> filterSearch,
        int? skip = null,
        int? take = null, string lang = "ar"
    );
}
