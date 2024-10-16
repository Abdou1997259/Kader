namespace Kader_System.Domain.Models.Trans;

[Table("trans_covenants")]
public class TransCovenant : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string NameEn { get; set; }
    public required string NameAr { get; set; }
    public DateOnly Date { get; set; }
    public string? Notes { get; set; }
    public double Amount { get; set; }

    public int EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public HrEmployee Employee { get; set; } = default!;
    public int CompanyId { get; set; }
    public string? Attachment { get; set; }

}
