using Kader_System.Domain.DTOs;
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
    public interface ILoanRequestService
    {

        public Task<Response<GetAllLoanRequestResponse>> GetAllLoanReques(GetFilterationLoanRequest model,string host);
        public Task<Response<LoanRequest>> AddNewLoanReques(DTOLoanRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None);
        public Task<Response<LoanRequest>> UpdateLoanRequest(int id, DTOLoanRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None);
        public Task<Response<LoanRequest>> DeleteLoanRequest(int id);
        public Task<Response<DTOListOfLoanRequestResponse>> GetById(int id);    
        public Task<Response<IEnumerable<DTOListOfLoanRequestResponse>>> ListOfLoanRequest();    
    }
}
