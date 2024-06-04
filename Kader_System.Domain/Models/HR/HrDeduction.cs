namespace Kader_System.Domain.Models.HR;

[Table("Hr_Deductions")]
public class HrDeduction : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string Name_ar { get; set; }
    public required string Name_en { get; set; }
    public long? AccountNo { get; set; }
}
