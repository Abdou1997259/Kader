namespace Kader_System.Domain.Models.Trans;

[Table("Trans_SalaryIncreases")]
public class TransSalaryIncrease : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public double Amount { get; set; }
    public double salaryAfterIncrease { get; set; }
    public string? Notes { get; set; }
    public DateTime transactionDate { get; set; }
    public DateTime dueDate { get; set; }


    public int Increase_type { get; set; }
    [ForeignKey(nameof(Increase_type))]
    public HrValueType ValueType { get; set; } = default!;

    public int Employee_id { get; set; }
    [ForeignKey(nameof(Employee_id))]
    public HrEmployee Employee { get; set; } = default!;
}
