namespace Kader_System.Domain.DTOs.Request.Trans
{
    public class GetAllFilterationAllowanceRequest : PaginationRequest
    {
        public RequestStatusTypes ApporvalStatus { get; set; }

    }
}
