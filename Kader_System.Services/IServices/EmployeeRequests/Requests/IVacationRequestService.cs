using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.Services.IServices.EmployeeRequests.Requests
{
    public interface IVacationRequestService
    {
        public Task<Response<VacationRequests>> AddNewVacationRequest(DTOVacationRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None);
        public Task<Response<GetAllVacationRequestReponse>> GetAllVacationRequest(GetFilterationVacationRequestRequest model, string host);
       
        public Task<Response<VacationRequests>> UpdateVacationRequest(int id, DTOVacationRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None);
        public Task<Response<VacationRequests>> DeleteVacationRequest(int id);
        public Task<Response<DtoListOfVacationRequestResponse>> GetById(int id);
        public Task<Response<IEnumerable<DtoListOfVacationRequestResponse>>> ListOfVacationRequest();
    }
}
