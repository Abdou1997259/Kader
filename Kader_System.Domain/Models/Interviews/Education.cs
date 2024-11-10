namespace Kader_System.Domain.Models.Interviews
{
    [Table("inter_educations")]
    public class Education : BaseEntity
    {

        public int id { get; set; }
        public int faculty_id { get; set; }
        [ForeignKey(nameof(faculty_id))]
        public Faculty faculty { get; set; } = default!;

        public DateOnly from { get; set; }
        public DateOnly to { get; set; }
        public int applicant_id { get; set; }
        [ForeignKey(nameof(applicant_id))]
        public Applicant applicant { get; set; } = default!;



    }
}
