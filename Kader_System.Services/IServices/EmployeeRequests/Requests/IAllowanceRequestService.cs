using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.Services.IServices.EmployeeRequests.Requests
{
    public interface IAllowanceRequestService
    {
        public Task<Response<GetAllowanceRequestResponse>> AddNewAllowanceRequest(DTOAllowanceRequest model, string appPath, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.AllowanceRequest);
        public Task<Response<GetAllowanceRequestResponse>> GetAllowanceRequest(GetAllFilterationAllowanceRequest model, string host);
        public Task<Response<DTOAllowanceRequestResponse>> GetById(int id);
        public Task<Response<AllowanceRequest>> UpdateAllowanceRequest(int id, DTOAllowanceRequest model, string appPath, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.AllowanceRequest);
        public Task<Response<AllowanceRequest>> DeleteAllowanceRequest(int id);

        public Task<Response<IEnumerable<DTOAllowanceRequest>>> ListOfAllowanceRequest();


    }
}
