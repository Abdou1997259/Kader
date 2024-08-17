using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Services.Services.EmployeeRequests.Requests;
namespace Kader_System.Services.IServices.EmployeeRequests.PermessionRequests
{
    public interface IDelayPermissionService
    {

        #region Read
        public Task<Response<GetAllDelayPermissionRequestRequestResponse>> GetAllDelayPermissionRequsts(GetAlFilterationDelayPermissionReuquest model, string host);
        #endregion
        public Task<Response<DTODelayPermissionRequest>> AddNewDelayPermissionRequest(DTODelayPermissionRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.DelayPermission);
        #region Delete
        public Task<Response<string>> DeleteDelayPermissionRequest(int id, string fullPath);
        #endregion

        #region Update
        public Task<Response<DtoListOfDelayRequestReponse>> UpdateDelayPermissionRequest(int id, DTODelayPermissionRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.DelayPermission);
        #endregion

        #region GetById
        public Task<Response<DtoListOfDelayRequestReponse>> GetById(int id);

        #endregion

        #region ListOfDelayPermissionRequest
        public Task<Response<IEnumerable<DTODelayPermissionRequest>>> ListOfDelayPermissionRequest();
        #endregion

        #region Status
        public Task<Response<string>> ApproveRequest(int requestId);
        public Task<Response<string>> RejectRequest(int requestId, string resoan);
        #endregion

    }
}
