namespace Kader_System.Domain.Models.Trans;

[Table("trans_deductions")]
public class TransDeduction : BaseEntity
{
    [Key]
    public int id { get; set; }

    public DateOnly action_month { get; set; }
    public double amount { get; set; }
    public int amount_type_id { get; set; }
    [ForeignKey(nameof(amount_type_id))]
    public TransAmountType amount_type { get; set; } = default!;

    public int salary_effect_id { get; set; }
    [ForeignKey(nameof(salary_effect_id))]
    public TransSalaryEffect salary_effect { get; set; } = default!;

    public int employee_id { get; set; }
    [ForeignKey(nameof(employee_id))]
    public HrEmployee employee { get; set; } = default!;

    public int deduction_id { get; set; }
    [ForeignKey(nameof(deduction_id))]
    public HrDeduction deduction { get; set; } = default!;
    public string? notes { get; set; }
    public string? attachment { get; set; }
    public int company_id { get; set; }
    public int? calculate_salary_id { get; set; }
    public int? calculate_salary_details_id { get; set; }

}
