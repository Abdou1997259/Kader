using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Response;

namespace Kader_System.Services.IServices.EmployeeRequests.PermessionRequests
{
    public interface ILeavePermissionRequestService
    {
        public Task<Response<GetAllLeavePermissionRequestResponse>> GetAllLeavePermissionRequsts(string lang, Domain.DTOs.Request.EmployeesRequests.GetAllFilltrationForEmployeeRequests model, string host);
        public Task<Response<DTOLeavePermissionRequest>> AddNewLeavePermissionRequest(DTOCreateLeavePermissionRequest model,string appPath, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest);
        public Task<Response<DTOLeavePermissionRequest>> UpdateLeavePermissionRequest(int id ,DTOCreateLeavePermissionRequest model,string appPath, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest);
        public Task<Response<string>> DeleteLeavePermissionRequest(int id, string fullPath);
        public Task<Response<string>> ApproveRequest(int requestId);
        public  Task<Response<string>> RejectRequest(int requestId, string resoan);

    }
}
