namespace Kader_System.Domain.Models.Interviews
{
    [Table("inter_job_offer")]
    public class JobOffer
    {
        public int id { get; set; }

        public string? file_path { get; set; }
        [Required]
        public string details_message { get; set; }
        [Required]
        public DateTime interview_date { get; set; }
        public int applicant_id { get; set; }
        [ForeignKey(nameof(applicant_id))]
        public Applicant Applicant { get; set; } = default!;
    }
}
