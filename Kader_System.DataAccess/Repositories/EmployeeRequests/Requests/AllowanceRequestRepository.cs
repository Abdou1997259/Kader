using Kader_System.Domain.Interfaces.EmployeeRequest.Request;
using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.DataAccess.Repositories.EmployeeRequests.PermessionRequests
{
    public class AllowanceRequestRepository(KaderDbContext context) : BaseRepository<AllowanceRequest>(context),IAllowanceRequestRepository
    {
    }
}
