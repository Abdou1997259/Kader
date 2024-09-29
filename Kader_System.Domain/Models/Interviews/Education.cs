namespace Kader_System.Domain.Models.Interviews
{
    [Table("educations")]
    public class Education : BaseEntity
    {

        public int Id { get; set; }
        public int FaultyId { get; set; }
        [ForeignKey(nameof(FaultyId))]
        public Faculty Faculty { get; set; } = default!;

        public DateOnly From { get; set; }
        public DateOnly To { get; set; }
        public int ApplicantId { get; set; }
        [ForeignKey(nameof(ApplicantId))]
        public Applicant Applicant { get; set; } = default!;




    }
}
