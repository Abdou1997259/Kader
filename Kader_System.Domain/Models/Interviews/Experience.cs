namespace Kader_System.Domain.Models.Interviews
{
    [Table("experiences")]
    public class Experience : BaseEntity
    {
        public int id { get; set; }
        [Required]
        public string company_name { get; set; } = null!;
        [Required]
        public string job_title { get; set; } = null!;
        [Required]
        public DateOnly from { get; set; }

        [Required]
        public DateOnly to { get; set; }
        public int applicant_id { get; set; }
        [ForeignKey(nameof(applicant_id))]
        public Applicant applicant { get; set; } = default!;

    }
}
