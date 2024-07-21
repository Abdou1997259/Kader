using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;

namespace Kader_System.Services.IServices.EmployeeRequests.PermessionRequests
{
    public interface ILeavePermissionRequestService
    {
        public Task<Response<DTOLeavePermissionRequest>> AddNewLeavePermissionRequest(DTOCreateLeavePermissionRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest);
    }
}
