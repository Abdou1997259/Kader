using Kader_System.Domain.DTOs.Response.HR;
using Kader_System.Domain.DTOs.Response.Loan;

namespace Kader_System.Domain.Interfaces.HR;

public interface IEmployeeRepository : IBaseRepository<HrEmployee>
{
    Response<GetEmployeeByIdResponse> GetEmployeeByIdAsync(int id, string lang);
    Task<object> GetEmployeesDataAsLookUp(string lang);
    Task<object[]> GetEmployeesDataNameAndIdAsLookUp(string lang, int companyId);

    Task<IEnumerable<EmployeeLookup>> GetEmployeesDataNameAndIdAsCustomTypeLookUp(string lang, int companyId);
    Task<object> GetEmployeesNameIdSalaryAsLookUp(string lang);
    public Task<List<EmployeesData>> GetAllEmployeeDetails(
    string word, bool isDeleted, int currentCompany, int skip = 0, int take = 10, string lang = "en");

    Task<object> GetEmployeesNameIdSalaryWithoutContractAsLookUp(string lang, int companyId);
    //List<EmployeesData> GetEmployeesInfo(
    //    Expression<Func<HrEmployee, bool>> filter,
    //    Expression<Func<EmployeesData, bool>> filterSearch,
    //    int? skip = null,
    //    int? take = null, string lang = "ar"
    //);
}
