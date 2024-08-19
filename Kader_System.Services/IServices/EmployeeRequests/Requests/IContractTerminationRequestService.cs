using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.Services.IServices.EmployeeRequests.Requests
{
    public interface IContractTerminationRequestService
    {

        public Task<Response<GetAllContractTermiantionRequestResponse>> GetAllContractTerminationRequest(GetFilterationContractTerminationRequest model, string host);
        public Task<Response<ContractTerminationRequest>> AddNewContractTerminationRequest(DTOContractTerminationRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.TerminateContract);
        public Task<Response<ContractTerminationRequest>> UpdateContractTerminationRequest(int id, DTOContractTerminationRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.TerminateContract);
        public Task<Response<string>> DeleteContracTermniationRequest(int id, string path);
        public Task<Response<ListOfContractTerminationRequestResponse>> GetById(int id);
        public Task<Response<IEnumerable<ListOfContractTerminationRequestResponse>>> ListOfContractTerminationRequest();
        public  Task<Response<string>> ApproveRequest(int requestId);

        public  Task<Response<string>> RejectRequest(int requestId, string resoan);

    }
}
