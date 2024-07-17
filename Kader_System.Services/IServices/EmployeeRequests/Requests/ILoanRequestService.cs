using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.EmployeeRequests.Requests
{
    public interface ILoanRequestService
    {
        public Task<List<DTOLoanRequest>> GetAllLoanReques();
        public Task<int> AddNewLoanReques(DTOLoanRequest model);
        public Task<int> UpdateLoanReques(DTOLoanRequest model);
        public Task<int> DeleteLoanRequest(int id);
    }
}
