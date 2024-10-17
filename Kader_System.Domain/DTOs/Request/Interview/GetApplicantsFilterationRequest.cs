namespace Kader_System.Domain.DTOs.Request.Interview
{
    public class GetApplicantsFilterationRequest : PaginationRequest
    {
        public int? job_id { get; set; }
    }
}
