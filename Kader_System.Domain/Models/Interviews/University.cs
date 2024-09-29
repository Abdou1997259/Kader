namespace Kader_System.Domain.Models.Interviews
{
    [Table("universities")]
    public class University
    {
        public int Id { get; set; }

        public string NameAr { get; set; } = null!;
        public string NameEn { get; set; } = null!;

        public ICollection<Faculty> Faculties { get; set; } = new HashSet<Faculty>();
    }
}
