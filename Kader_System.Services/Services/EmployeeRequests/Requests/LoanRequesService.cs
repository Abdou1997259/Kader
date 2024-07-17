using Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests;
using Kader_System.Services.IServices.EmployeeRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services.EmployeeRequests.Requests
{
    public class LoanRequesService : ILoanRequestService
    {
        public Task<int> AddNewLoanReques(DTOLoanRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteLoanRequest(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DTOLoanRequest>> GetAllLoanReques()
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateLoanReques(DTOLoanRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
