using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Domain.DTOs.Response;
using Kader_System.Domain.DTOs.Response.EmployeesRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Services.Services.EmployeeRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Kader_System.Services.IServices.EmployeeRequests.PermessionRequests
{
    public interface IDelayPermissionService
    {

        #region Read
        public Task<Response<GetAllDelayRequestRespond>> GetAllDelayPermissionRequsts(GetAlFilterationDelayPermissionReuquest model, string host);
        #endregion
        public Task<Response<DTODelayPermissionRequest>> AddNewDelayPermissionRequest(DTODelayPermissionRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None);
        #region Delete
        public Task<Response<string>> DeleteDelayPermissionRequest(int id, string fullPath);
        #endregion
       
        #region Update
        public Task<Response<DtoListOfDelayRequestReponse>> UpdateDelayPermissionRequest(int id,DTODelayPermissionRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest);
        #endregion

        #region GetById
        public Task<Response<DtoListOfDelayRequestReponse>> GetById(int id);

        #endregion

        #region ListOfDelayPermissionRequest
        public Task<Response<IEnumerable<DTODelayPermissionRequest>>> ListOfDelayPermissionRequest();
        #endregion

    }
}
