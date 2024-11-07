namespace Kader_System.Domain.Models.Interviews
{
    [Table("inter_job")]
    public class Job : BaseEntity
    {
        public int id { get; set; }
        public DateOnly from { get; set; }
        public DateOnly? to { get; set; }
        public int applicant_count { get; set; }
        public string name_ar { get; set; }
        public string name_en { get; set; }
        public bool is_finished { get; set; }
        public string? description { get; set; }
        public int state_id { get; set; }
        [ForeignKey(nameof(state_id))]
        public JobState state { get; set; } = default!;
        public ICollection<Applicant> applicants { get; set; } = new HashSet<Applicant>();
    }
}
