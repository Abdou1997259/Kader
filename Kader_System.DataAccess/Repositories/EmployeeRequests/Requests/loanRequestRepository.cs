
using Kader_System.Domain.Interfaces.EmployeeRequest.Request;
using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.DataAccess.Repositories.EmployeeRequests.Requests
{
    public class LoanRequestRepository(KaderDbContext db) : BaseRepository<LoanRequest>(db), ILoanRequestRepository { 
    }
}
