using Kader_System.Services.IServices.EmployeeRequests;

namespace Kader_System.Services.Services.EmployeeRequests
{
    public class EmployeeRequestsService(IUnitOfWork _unitOfWork, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper) : IEmployeeRequestsService
    {

    }
}
