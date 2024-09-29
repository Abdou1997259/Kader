namespace Kader_System.Domain.Models.Interviews
{
    [Table("faculties")]
    public class Faculty
    {
        public int Id { get; set; }

        public string NameAr { get; set; } = null!;
        public string NameEn { get; set; } = null!;
        public int UniversityId { get; set; }
        [ForeignKey(nameof(UniversityId))]
        public University University { get; set; } = null!;
        public ICollection<Education> Educations { get; set; } = new HashSet<Education>();


    }
}
