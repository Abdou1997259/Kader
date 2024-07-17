
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;

namespace Kader_System.Services.Services.EmployeeRequests.PermessionRequests
{
    public class LeavePermissionRequestService : ILeavePermissionRequestService
    {
        public Task<List<DTOLeavePermissionRequest>> GetAllLeavePermissionRequests()
        {

        }
        public Task<int> AddNewLeavePermissionRequest(DTOLeavePermissionRequest model) { }
        public Task<int> UpdateLeavePermissionRequest(DTOLeavePermissionRequest model) { }
        public Task<int> DeleteLeavePermissionRequest(int id) { }
    }
}
