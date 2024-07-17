using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;

﻿using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.EmployeeRequests.PermessionRequests
{
    public interface ILeavePermissionRequestService
    {
        public Task<List<DTOLeavePermissionRequest>> GetAllLeavePermissionRequests();
        public Task<int> AddNewLeavePermissionRequest(DTOLeavePermissionRequest model);
        public Task<int>UpdateLeavePermissionRequest(DTOLeavePermissionRequest model);
        public Task<int> DeleteLeavePermissionRequest(int id);
    }
}
