using Kader_System.Domain.Interfaces.EmployeeRequest.PermessionRequests;
using Kader_System.Domain.Models.EmployeeRequests.PermessionRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.DataAccess.Repositories.EmployeeRequests.PermessionRequests
{
    public class LeavePermissionRequestRepository(KaderDbContext context) : BaseRepository<LeavePermissionRequest>(context), ILeavePermissionRequestRepository
    {
    }
}
