using Kader_System.Domain.DTOs.Response.HR;

namespace Kader_System.Domain.Interfaces.HR;

public interface IEmployeeRepository : IBaseRepository<HrEmployee>
{
    Response<GetEmployeeByIdResponse> GetEmployeeByIdAsync(int id, string lang);
    Task<object> GetEmployeesDataAsLookUp(string lang);
    Task<object> GetEmployeesDataNameAndIdAsLookUp(string lang);
    Task<object> GetEmployeesNameIdSalaryAsLookUp(string lang);
    public Task<List<EmployeesData>> GetAllEmployeeDetails(bool isDeleted, int currentCompany, int skip = 0, int take = 10, string lang = "en");

    Task<object> GetEmployeesNameIdSalaryWithoutContractAsLookUp(string lang);
    //List<EmployeesData> GetEmployeesInfo(
    //    Expression<Func<HrEmployee, bool>> filter,
    //    Expression<Func<EmployeesData, bool>> filterSearch,
    //    int? skip = null,
    //    int? take = null, string lang = "ar"
    //);
}
