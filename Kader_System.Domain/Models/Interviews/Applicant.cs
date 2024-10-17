namespace Kader_System.Domain.Models.Interviews
{
    [Table("inter_applicants")]
    public class Applicant : BaseEntity
    {
        public int id { get; set; }
        [Required]
        public string full_name { get; set; } = null!;

        [Required]
        public string email { get; set; } = null!;
        [Required]
        public string phone { get; set; } = null!;

        public int? year_of_experiences { get; set; }

        public DateOnly? date_of_birth { get; set; }

        public decimal? current_salary { get; set; }
        [Required]
        public decimal expected_salary { get; set; }
        [Required]
        public int gender { get; set; }
        public float? rate { get; set; }

        public int age { get; set; }
        public string? image_path { get; set; }
        public string? cv_file_path { get; set; } = default!;
        public ICollection<Education> educations { get; set; } = new HashSet<Education>();

        public ICollection<Experience> experiences { get; set; } = new HashSet<Experience>();
        [ForeignKey(nameof(job_id))]
        public Job job { get; set; } = default!;
        public int job_id { get; set; }
        public int state_id { get; set; }
        [ForeignKey(nameof(state_id))]
        public ApplicantState state { get; set; } = default!;




    }
}
