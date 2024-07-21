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
    public interface IResignationRequestService
    {
        public Task<Response<GetAllResignations>> GetAllLoanReques(GetFillterationResignationRequest model, string host);
        public Task<Response<ResignationRequest>> AddNewLoanReques(DTOResignationRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None);
        public Task<Response<ResignationRequest>> UpdateLoanRequest(int id, DTOResignationRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None);
        public Task<Response<ResignationRequest>> DeleteLoanRequest(int id);
        public Task<Response<DtoListOfResignationResposne>> GetById(int id);
        public Task<Response<IEnumerable<DtoListOfResignationResposne>>> ListOfLoanRequest();
    }
}
