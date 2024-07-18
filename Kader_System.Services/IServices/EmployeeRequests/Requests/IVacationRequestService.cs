using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;

namespace Kader_System.Services.IServices.EmployeeRequests.PermessionRequests
{
    public interface IVacationRequestService
    {
        public Task<Response<DTOVacationRequest>> AddNewVacationRequest(DTOVacationRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None);
    }
}
