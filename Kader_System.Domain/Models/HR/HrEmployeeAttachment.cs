namespace Kader_System.Domain.Models.HR;

[Table("Hr_EmployeeAttachments")]
public class HrEmployeeAttachment : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string FileName { get; set; }
    public required string FileExtension { get; set; }

    public int EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public HrEmployee Employee { get; set; } = default!;
}
