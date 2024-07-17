using Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests;
using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.EmployeeRequests.Requests
{
    public interface ISalaryIncreaseRequestService
    {
        public Task<List<DTOSalaryIncreaseRequest>> GetAllSalaryIncreaseRequest();
        public Task<int> AddNewSalaryIncreaseRequest(DTOSalaryIncreaseRequest model);
        public Task<int> UpdateSalaryIncreaseRequest(DTOSalaryIncreaseRequest model);
        public Task<int> DeleteSalaryIncreaseRequest(int id);
    }
}
