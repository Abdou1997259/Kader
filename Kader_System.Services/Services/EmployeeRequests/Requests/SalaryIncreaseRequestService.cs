using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class SalaryIncreaseRequestService : ISalaryIncreaseRequestService
    {
        public Task<int> AddNewSalaryIncreaseRequest(DTOSalaryIncreaseRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteSalaryIncreaseRequest(int id)
        {
            throw new NotImplementedException();
        }


        public Task<List<DTOSalaryIncreaseRequest>> GetAllSalaryIncreaseRequest()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateSalaryIncreaseRequest(DTOSalaryIncreaseRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
