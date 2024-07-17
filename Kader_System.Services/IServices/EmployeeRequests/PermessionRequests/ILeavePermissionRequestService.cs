using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.EmployeeRequests.PermessionRequests
{
    public interface ILeavePermissionRequestService
    {

        public Task<Response<DTOLeavePermissionRequest>> AddNewLeavePermissionRequest(DTOLeavePermissionRequest model);
    }
}
