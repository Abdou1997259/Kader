namespace Kader_System.Domain.DTOs.Request.HR
{
    public class GetAlFilterationForSalaryIncreaseRequest : PaginationRequest
    {
        public RequestStatusTypes ApporvalStatus { get; set; }

    }
}
