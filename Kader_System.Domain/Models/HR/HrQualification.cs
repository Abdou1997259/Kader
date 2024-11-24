namespace Kader_System.Domain.Models.HR;

[Table("hr_qualifications")]
public class HrQualification : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string NameEn { get; set; }
    public required string NameAr { get; set; }

    [ForeignKey(nameof(Added_by))]
    public ApplicationUser User { get; set; } = default!;
    public ICollection<HrEmployee> Employees { get; set; } = new HashSet<HrEmployee>();

}
