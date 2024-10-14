namespace Kader_System.Domain.Models.Interviews
{
    [Table("inter_universities")]
    public class University
    {
        public int id { get; set; }

        public string name_ar { get; set; } = null!;
        public string name_en { get; set; } = null!;

        public ICollection<Faculty> faculties { get; set; } = new HashSet<Faculty>();
    }
}
