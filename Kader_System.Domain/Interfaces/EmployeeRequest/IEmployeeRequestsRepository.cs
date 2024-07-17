using Kader_System.Domain.DTOs.Response;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;

namespace Kader_System.Domain.Interfaces.EmployeeRequest
{
    public interface IEmployeeRequestsRepository : IBaseRepository<LeavePermissionRequest>
    {
        Task<Response<EmployeeRequestsLookUpsData>> GetEmployeeRequestsLookUpsData(string lang);

    }
}
