using Kader_System.Domain.Interfaces.EmployeeRequest.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.DataAccess.Repositories.EmployeeRequests.PermessionRequests
{
    public class VacationRequestRepository(KaderDbContext context) : BaseRepository<VacationRequests>(context),IVacationRequestRepository
    {
    }
}
