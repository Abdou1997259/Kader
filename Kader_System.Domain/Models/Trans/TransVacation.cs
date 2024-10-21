namespace Kader_System.Domain.Models.Trans;

[Table("trans_vacations")]
public class TransVacation : BaseEntity
{
    [Key]
    public int id { get; set; }
    public DateOnly start_date { get; set; }
    public double? salary_amount { get; set; }

    public double days_count { get; set; }
    public int employee_id { get; set; }
    [ForeignKey(nameof(employee_id))]
    public HrEmployee employee { get; set; } = default!;

    public int vacation_id { get; set; }
    [ForeignKey(nameof(vacation_id))]
    public HrVacationDistribution vacation { get; set; } = default!;
    public string? notes { get; set; }
    public string? attachment { get; set; }

    public int? calculate_salary_id { get; set; }
    public int? calculate_salary_details_id { get; set; }
    public int company_id { get; set; }
}
