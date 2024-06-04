namespace Kader_System.Domain.Models.Trans;

[Table("Trans_Allowances")]
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
}
