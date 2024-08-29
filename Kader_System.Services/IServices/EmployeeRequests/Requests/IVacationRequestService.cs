using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.Services.IServices.EmployeeRequests.Requests
{
    public interface IVacationRequestService
    {
        public  Task<Response<IEnumerable<DTOVacationRequest>>> ListOfVacationRequest();

        #region PaginatedLoanRequest
        public Task<Response<GetAllVacationRequestReponse>> GetAllVacationRequest(GetFilterationVacationRequestRequest model, string host);
        #endregion

        #region GetLoanRequetById
        public  Task<Response<ListOfVacationRequestResponse>> GetById(int id);
        #endregion

        #region AddLoanRequest
        public Task<Response<VacationRequests>> AddNewVacationRequest(DTOVacationRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.VacationRequest);
        #endregion

        #region DeleteLoanRequets
        public  Task<Response<VacationRequests>> DeleteVacationRequest(int id, string moduleName);
        #endregion

        #region UpdateLoanRequest
        public  Task<Response<VacationRequests>> UpdateVacationRequest(int id, DTOVacationRequest model, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.VacationRequest);



        #endregion

        #region Status
        public  Task<Response<string>> ApproveRequest(int requestId,string lang);
        public  Task<Response<string>> RejectRequest(int requestId, string resoan);
        #endregion
    }
}
