using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class ResignationRequestService : IResignationRequestService
    {
        public Task<int> AddNewResignationRequest(DTOResignationRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteResignationRequest(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DTOResignationRequest>> GetAllResignationRequest()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateResignationRequest(DTOResignationRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
