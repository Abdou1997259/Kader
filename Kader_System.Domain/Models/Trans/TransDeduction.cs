namespace Kader_System.Domain.Models.Trans;

[Table("Trans_Deductions")]
public class TransDeduction : BaseEntity
{
    [Key]
    public int Id { get; set; }

    public DateOnly ActionMonth { get; set; }
    public double Amount { get; set; }
    public int AmountTypeId { get; set; }
    [ForeignKey(nameof(AmountTypeId))]
    public TransAmountType AmountType { get; set; } = default!;

    public int SalaryEffectId { get; set; }
    [ForeignKey(nameof(SalaryEffectId))]
    public TransSalaryEffect SalaryEffect { get; set; } = default!;

    public int EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public HrEmployee Employee { get; set; } = default!;

    public int DeductionId { get; set; }
    [ForeignKey(nameof(DeductionId))]
    public HrDeduction Deduction { get; set; } = default!;
    public string? Notes { get; set; }
    public string? Attachment { get; set; }
    public string? AttachmentExtension { get; set; }
}
