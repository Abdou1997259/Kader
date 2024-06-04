namespace Kader_System.Domain.Models.HR;

[Table("Hr_Jobs")]
public class HrJob : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string NameEn { get; set; }
    public required string NameAr { get; set; }
    public bool HasNeedLicense { get; set; }
    public bool HasAdditionalTime { get; set; }
}
