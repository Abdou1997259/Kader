namespace Kader_System.Domain.Models.Trans;

[Table("trans_salary_increases")]
public class TransSalaryIncrease : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public double Amount { get; set; }
    public double salaryAfterIncrease { get; set; }
    public string? Notes { get; set; }
    public DateOnly transactionDate { get; set; }
    public DateTime dueDate { get; set; }
    public double PreviousSalary { get; set; }

    public int Increase_type { get; set; }
    [ForeignKey(nameof(Increase_type))]
    public HrValueType ValueType { get; set; } = default!;

    public int Employee_id { get; set; }
    [ForeignKey(nameof(Employee_id))]
    public HrEmployee Employee { get; set; } = default!;
}
