using Kader_System.Domain.DTOs.Response;
using Kader_System.Services.IServices.EmployeeRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services.EmployeeRequests
{
    public class EmployeeRequestsService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : IEmployeeRequestsService
    {
    }
}
