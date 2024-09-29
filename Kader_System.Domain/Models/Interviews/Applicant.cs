namespace Kader_System.Domain.Models.Interviews
{
    [Table("applicants")]
    public class Applicant : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;

        public int? YearOfExperiences { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public decimal? CurrentSalary { get; set; }
        [Required]
        public decimal ExpectedSalary { get; set; }
        [Required]
        public int Gender { get; set; }

        public string? ImagePath { get; set; }

        public ICollection<Education> Educations { get; set; } = new HashSet<Education>();

        public ICollection<Experience> Experiences { get; set; } = new HashSet<Experience>();
        public ICollection<CvFile> CvFiles { get; set; } = new HashSet<CvFile>();




    }
}
