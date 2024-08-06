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
        public Task<Response<GetAllResignations>> GetAllResignationRequest(GetFillterationResignationRequest model, string host);
        public  Task<Response<ResignationRequest>> AddNewResignationRequest(DTOResignationRequest model, string appPath, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.ResignationRequest);
        public  Task<Response<ResignationRequest>> UpdateResignationRequest(int id, DTOResignationRequest model, string appPath, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.ResignationRequest);   
        public Task<Response<ResignationRequest>> DeleteResignationRequest(int id);
        public Task<Response<DtoListOfResignationResposne>> GetById(int id);
        public Task<Response<IEnumerable<DtoListOfResignationResposne>>> ListOfResignationRequest();
    }
}
