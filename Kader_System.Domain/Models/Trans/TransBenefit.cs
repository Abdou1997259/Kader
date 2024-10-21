namespace Kader_System.Domain.Models.Trans;

[Table("trans_benefits")]
public class TransBenefit : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateOnly action_month { get; set; }

    public int amount_type_id { get; set; }
    [ForeignKey(nameof(amount_type_id))]
    public TransAmountType amount_type { get; set; } = default!;


    public double amount { get; set; }
    public int salary_effect_id { get; set; }
    [ForeignKey(nameof(salary_effect_id))]
    public TransSalaryEffect salary_effect { get; set; } = default!;

    public int employee_id { get; set; }
    [ForeignKey(nameof(employee_id))]
    public HrEmployee employee { get; set; } = default!;

    public int benefit_id { get; set; }
    [ForeignKey(nameof(benefit_id))]
    public HrBenefit benefit { get; set; } = default!;
    public string? notes { get; set; }
    public string? attachment { get; set; }

    public int? calculate_salary_id { get; set; }
    public int? calculate_salary_details_id { get; set; }
    public int company_id { get; set; }
}
