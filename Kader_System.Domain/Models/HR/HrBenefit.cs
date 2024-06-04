namespace Kader_System.Domain.Models.HR;

[Table("Hr_Benefits")]
public class HrBenefit : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string Name_ar { get; set; }
    public required string Name_en { get; set; }
    public long? AccountNo { get; set; }
}
