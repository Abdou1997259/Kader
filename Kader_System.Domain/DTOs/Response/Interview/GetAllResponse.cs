namespace Kader_System.Domain.DTOs.Response.Interview
{
    public class GetAllResponse : PaginationData<JobList>
    {
        public int all_applicant_count { get; set; }
        public int job_count { get; set; }
        public int finished_job_count { get; set; }
    }
    public class JobList
    {
        public int id { get; set; }
        public DateOnly to { get; set; }
        public DateOnly from { get; set; }
        public string name { get; set; }
        public int applicant_count { get; set; }
        public string state { get; set; }

    }
}
