using Kader_System.Domain.DTOs;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Response;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Kader_System.Services.IServices.EmployeeRequests.PermessionRequests
{
    public interface IDelayPermissionService
    {
        public Task<Response<DTODelayPermissionRequest>> AddNewDelayPermissionRequest(DTODelayPermissionRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest = HrEmployeeRequestTypesEnums.None);
        #region Delete
        public Task<Response<string>> DeleteDelayPermissionRequest(int id, string fullPath);
        #endregion
        #region Read
        public Task<Response<GetAllLeavePermissionRequestResponse>> GetAllDelayPermissionRequsts(string lang, Domain.DTOs.Request.EmployeesRequests.GetAllFilltrationForEmployeeRequests model, string host);
        #endregion
        #region Update
        public Task<Response<DTOLeavePermissionRequest>> UpdateDelayPermissionRequest(DTODelayPermissionRequest model, string root, string clientName, string moduleName, HrEmployeeRequestTypesEnums hrEmployeeRequest);
        #endregion
    }
}
