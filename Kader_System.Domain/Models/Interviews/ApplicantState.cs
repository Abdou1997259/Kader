namespace Kader_System.Domain.Models.Interviews
{
    [Table("intern_applicant_state")]
    public class ApplicantState
    {
        public int id { get; set; }
        public string name_ar { get; set; }
        public string name_en { get; set; }
    }
}
