using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.Services.IServices.EmployeeRequests.Requests
{
    public interface IAllowanceRequestService
    {
        public Task<Response<GetAllowanceRequestRequestResponse>> AddNewAllowanceRequest(DTOAllowanceRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.AllowanceRequest);
        public Task<Response<GetAllowanceRequestRequestResponse>> GetAllowanceRequest(GetAllFilterationAllowanceRequest model, string host);
        public Task<Response<ListOfAllowanceRequestResponse>> GetById(int id);
        public Task<Response<AllowanceRequest>> UpdateAllowanceRequest(int id, DTOAllowanceRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.AllowanceRequest);
        public Task<Response<AllowanceRequest>> DeleteAllowanceRequest(int id,string fileName);

        public Task<Response<IEnumerable<DTOAllowanceRequest>>> ListOfAllowanceRequest();

        Task<Response<string>> ApproveRequest(int requestId);
        Task<Response<string>> RejectRequest(int requestId, string resoan);



    }
}
