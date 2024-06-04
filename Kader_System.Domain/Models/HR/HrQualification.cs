namespace Kader_System.Domain.Models.HR;

[Table("Hr_Qualifications")]
public class HrQualification : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string NameEn { get; set; }
    public required string NameAr { get; set; }
    
}
