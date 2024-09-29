namespace Kader_System.Domain.Models.Interviews
{
    [Table("experiences")]
    public class Experience : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; } = null!;
        [Required]
        public string JobTitle { get; set; } = null!;
        [Required]
        public DateOnly From { get; set; }

        [Required]
        public DateOnly To { get; set; }
        public int ApplicantId { get; set; }
        [ForeignKey(nameof(ApplicantId))]
        public Applicant Applicant { get; set; } = default!;

    }
}
