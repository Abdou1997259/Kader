using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;

namespace Kader_System.Services.IServices.EmployeeRequests.Requests
{
    public interface IAllowanceRequestService
    {
        public Task<Response<DTOVacationRequest>> AddNewAllowanceRequest(DTOAllowanceRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None);
    }
}
