namespace Kader_System.Domain.DTOs.Request.Interview
{
    public class ReplayJobRequest
    {
        public DateOnly to { get; set; }
        public DateOnly from { get; set; }
        public int? applicant_count { get; set; }
    }
}
