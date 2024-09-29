namespace Kader_System.Domain.Models.Interviews
{
    [Table("cv_files")]
    public class CvFile : BaseEntity
    {
        public int Id { get; set; }
        public string FileExtension { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public int ApplicantId { get; set; }
        [ForeignKey(nameof(ApplicantId))]
        public Applicant Applicant { get; set; } = default!;

    }
}
