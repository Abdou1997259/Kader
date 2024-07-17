using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.EmployeeRequests.Requests
{
    public interface IResignationRequestService
    {
        public Task<List<DTOResignationRequest>> GetAllResignationRequest();
        public Task<int> AddNewResignationRequest(DTOResignationRequest model);
        public Task<int> UpdateResignationRequest(DTOResignationRequest model);
        public Task<int> DeleteResignationRequest(int id);
    }
}
