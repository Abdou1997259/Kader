namespace Kader_System.Domain.DTOs.Request.HR
{
    public class HrGetAllJobsResponse : PaginationData<JobData>
    {

    }
    public class JobData : SelectListResponse
    {
        public bool HasNeedLicense { get; set; }
        public bool HasAdditionalTime { get; set; }
        public int EmployeesCount { get; set; }
    }
}
