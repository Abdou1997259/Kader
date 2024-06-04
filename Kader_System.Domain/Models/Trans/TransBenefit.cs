namespace Kader_System.Domain.Models.Trans;

[Table("Trans_Benefits")]
public class TransBenefit : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateOnly ActionMonth { get; set; }
  
    public int AmountTypeId { get; set; }
    [ForeignKey(nameof(AmountTypeId))]
    public TransAmountType AmountType { get; set; } = default!;
    public double Amount { get; set; }
    public int SalaryEffectId { get; set; }
    [ForeignKey(nameof(SalaryEffectId))]
    public TransSalaryEffect SalaryEffect { get; set; } = default!;

    public int EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public HrEmployee Employee { get; set; } = default!;

    public int BenefitId { get; set; }
    [ForeignKey(nameof(BenefitId))]
    public HrBenefit Benefit { get; set; } = default!;
    public string? Notes { get; set; }
    public string? Attachment { get; set; }
    public string? AttachmentExtension { get; set; }
}
