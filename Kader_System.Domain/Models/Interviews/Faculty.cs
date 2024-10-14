namespace Kader_System.Domain.Models.Interviews
{
    [Table("inter_faculties")]
    public class Faculty
    {
        public int id { get; set; }

        public string name_ar { get; set; } = null!;
        public string name_en { get; set; } = null!;
        public int university_id { get; set; }
        [ForeignKey(nameof(university_id))]
        public University university { get; set; } = null!;
        public ICollection<Education> educations { get; set; } = new HashSet<Education>();


    }
}
