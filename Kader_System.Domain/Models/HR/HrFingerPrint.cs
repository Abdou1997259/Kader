namespace Kader_System.Domain.Models.HR;

[Table("Hr_FingerPrints")]
public class HrFingerPrint : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string IPAddress { get; set; }
    public required string NameAr { get; set; }
    public required string NameEn { get; set; }
    public required string Password { get; set; }
    public required string Port { get; set; }
    public required string Username { get; set; }

    public int CompanyId { get; set; }
    [ForeignKey(nameof(CompanyId))]
    public HrCompany Company { get; set; } = default!;
}
