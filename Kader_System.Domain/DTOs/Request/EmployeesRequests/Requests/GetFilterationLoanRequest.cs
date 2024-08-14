

namespace Kader_System.Domain.DTOs.Request.EmployeesRequests.Requests
{
    public class GetFilterationLoanRequest:PaginationRequest
    {
        public RequestStatusTypes ApporvalStatus { get; set; }

    }
}
