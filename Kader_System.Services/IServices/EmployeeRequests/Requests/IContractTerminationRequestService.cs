using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.Services.IServices.EmployeeRequests.Requests
{
    public interface IContractTerminationRequestService
    {

        public Task<Response<GetAllContractTermiantionResponse>> GetAllContractTerminationRequest(GetFilterationContractTerminationRequest model, string host);
        public Task<Response<HrContractTermination>> AddNewContractTerminationRequest(DTOContractTerminationRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None);
        public Task<Response<HrContractTermination>> UpdateContractTerminationRequest(int id, DTOContractTerminationRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None);
        public Task<Response<HrContractTermination>> DeleteContracTermniationRequest(int id, string path);
        public Task<Response<DTOListOfContractTerminationResponse>> GetById(int id);
        public Task<Response<IEnumerable<DTOListOfContractTerminationResponse>>> ListOfContractTerminationRequest();
    }
}
