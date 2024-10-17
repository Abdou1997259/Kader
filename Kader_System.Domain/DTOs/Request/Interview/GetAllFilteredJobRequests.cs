namespace Kader_System.Domain.DTOs.Request.Interview
{
    public class GetAllFilteredJobRequests : PaginationRequest
    {
        [DefaultValue(false)]
        public bool? IsFinished { get; set; }
        public DateOnly? From { get; set; }
        public DateOnly? To { get; set; }
    }
}
