

namespace Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests
{
    public class GetFilterationVacationRequestRequest:PaginationRequest
    {
        public RequestStatusTypes ApporvalStatus { get; set; }
    }
}
