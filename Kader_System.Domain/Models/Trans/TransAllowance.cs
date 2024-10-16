namespace Kader_System.Domain.Models.Trans;

[Table("trans_allowances")]
public class TransAllowance : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateOnly ActionMonth { get; set; }
    public string? Notes { get; set; }
    public double Amount { get; set; }

    public int SalaryEffectId { get; set; }
    [ForeignKey(nameof(SalaryEffectId))]
    public TransSalaryEffect SalaryEffect { get; set; } = default!;

    public int EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public HrEmployee Employee { get; set; } = default!;

    public int AllowanceId { get; set; }
    [ForeignKey(nameof(AllowanceId))]
    public HrAllowance Allowance { get; set; } = default!;
    public int? CalculateSalaryId { get; set; }
    public int? CalculateSalaryDetailsId { get; set; }
    public int CompanyId { get; set; }
}
