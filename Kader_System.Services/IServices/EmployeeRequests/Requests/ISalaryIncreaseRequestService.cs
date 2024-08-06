using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.EmployeeRequests.Requests
{
    public interface ISalaryIncreaseRequestService
    {
        public Task<Response<GetSalaryIncreseRequestResponse>> GetAllSalaryIncreaseRequest(GetAlFilterationForSalaryIncreaseRequest model, string host);
        public  Task<Response<SalaryIncreaseRequest>> AddNewSalaryIncreaseRequest(DTOSalaryIncreaseRequest model, string appPath, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.SalaryIncreaseRequest);
        public Task<Response<SalaryIncreaseRequest>> UpdateSalaryIncreaseRequest(int id, DTOSalaryIncreaseRequest model, string appPath, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.SalaryIncreaseRequest);
        public Task<Response<SalaryIncreaseRequest>> DeleteSalaryIncreaseRequest(int id);
        public Task<Response<DTOListOfSalaryIncreaseRepostory>> GetById(int id);

        public Task<Response<IEnumerable<DTOSalaryIncreaseRequest>>> ListOfSalaryIncreaseRequest();

    }
}
