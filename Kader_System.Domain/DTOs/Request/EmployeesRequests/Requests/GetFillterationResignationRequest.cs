

namespace Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests
{
    public class GetFillterationResignationRequest:PaginationRequest
    {
        public RequestStatusTypes ApporvalStatus { get; set; }
    }
}
