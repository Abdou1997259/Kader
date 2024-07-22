using Kader_System.Domain.DTOs.Response;
using Kader_System.Domain.DTOs.Response.Employee_Requests;
using Kader_System.Domain.Models.EmployeeRequests;

namespace Kader_System.Domain.Interfaces.EmployeeRequest
{
    public interface IEmployeeRequestsRepository : IBaseRepository<HrEmployeeRequests>
    {
        //public Task<Domain.Dtos.Response.Response<GetAlVacationRequstsResponse>> GetAlVacationRequstsAsync(string lang, Domain.DTOs.Request.EmployeesRequests.GetAllFilltrationForEmployeeRequests model, string host, RequestStatusTypes types);
        public Task<Domain.Dtos.Response.Response<EmployeeRequestsLookUpsData>> GetEmployeeRequestsLookUpsData(string lang);
    }
}
