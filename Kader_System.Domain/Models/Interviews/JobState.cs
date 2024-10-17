namespace Kader_System.Domain.Models.Interviews
{
    [Table("inter_job_state")]
    public class JobState
    {

        public int id { get; set; }
        public string name_ar { get; set; }
        public string name_en { get; set; }
    }
}
