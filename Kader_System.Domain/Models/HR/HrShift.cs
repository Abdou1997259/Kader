namespace Kader_System.Domain.Models.HR;

[Table("hr_shifts")]
public class HrShift : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string Name_en { get; set; }
    public required string Name_ar { get; set; }
    public TimeOnly Start_shift { get; set; }
    public TimeOnly End_shift { get; set; }
    public ICollection<HrEmployee> Employees { get; set; } = new HashSet<HrEmployee>();
}
