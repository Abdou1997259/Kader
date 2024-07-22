using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Response;

namespace Kader_System.Services.IServices.EmployeeRequests.PermessionRequests
{
    public interface ILeavePermissionRequestService
    {
        public  Task<Response<GetAllLeavePermissionRequestResponse>> GetAllLeavePermissionRequestRequstsAsync(string lang, Domain.DTOs.Request.EmployeesRequests.GetAllFilltrationForEmployeeRequests model, string host);
        public Task<Response<DTOLeavePermissionRequest>> AddNewLeavePermissionRequest(DTOCreateLeavePermissionRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest);
    }
}
