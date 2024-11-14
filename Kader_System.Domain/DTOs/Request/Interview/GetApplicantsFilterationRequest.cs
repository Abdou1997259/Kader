namespace Kader_System.Domain.DTOs.Request.Interview
{
    public class GetApplicantsFilterationRequest : PaginationRequest
    {
        public int? job_id { get; set; }
        public int? gender { get; set; }
        public int? age { get; set; }
        public decimal? current_salary { get; set; }
        public decimal? expected_salary { get; set; }
        public float? rate { get; set; }
        [AllowedValues([1, 2, 3, 4, null], ErrorMessage = Annotations.AllowedValues)]
        public int? state { get; set; }
        public int? faculty_jd { get; set; }
        public int? university_id { get; set; }
        public int? year_of_experiences { get; set; }
        [AllowedValues([1, 2, 3, 4, null], ErrorMessage = Annotations.AllowedValues)]
        public int? sortby { get; set; }


    }
}
