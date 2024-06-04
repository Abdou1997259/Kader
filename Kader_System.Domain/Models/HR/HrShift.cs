namespace Kader_System.Domain.Models.HR;

[Table("Hr_Shifts")]
public class HrShift : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string Name_en { get; set; }
    public required string Name_ar { get; set; }
    public TimeOnly Start_shift { get; set; }
    public TimeOnly End_shift { get; set; }
}
