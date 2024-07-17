using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.EmployeeRequests.PermessionRequests
{
    public interface IDelayPermissionService
    {
        public Task<List<DTODelayPermissionRequest>> GetAllDelayPermissionRequests();
        public Task<int> AddNewDelayPermissionRequest(DTODelayPermissionRequest model);
        public Task<int> UpdateDelayPermissionRequest(DTODelayPermissionRequest model);
        public Task<int> DeleteDelayPermissionRequest(int id);


    }
}
