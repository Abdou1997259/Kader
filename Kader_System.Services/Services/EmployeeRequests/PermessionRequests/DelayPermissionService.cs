using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Services.IServices.EmployeeRequests.PermessionRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services.EmployeeRequests.PermessionRequests
{
    public class DelayPermissionService : IDelayPermissionService
    {
        public Task<int> AddNewDelayPermissionRequest(DTODelayPermissionRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteDelayPermissionRequest(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DTODelayPermissionRequest>> GetAllDelayPermissionRequests()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateDelayPermissionRequest(DTODelayPermissionRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
