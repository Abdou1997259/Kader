using Kader_System.Domain.Interfaces.EmployeeRequest.Request;
using Kader_System.Domain.Models.EmployeeRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.DataAccess.Repositories.EmployeeRequests.Requests
{
    internal class ResignationRequestRepository(KaderDbContext db):BaseRepository<ResignationRequest>(db), IResignationRequesteRepository
    {
    }
}
