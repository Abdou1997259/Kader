using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Interfaces.EmployeeRequest.Request
{
    public interface IVacationRequestRepository : IBaseRepository<VacationRequests>
    {
    }
}
