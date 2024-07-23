namespace Kader_System.Domain.Models.HR;

[Table("hr_vacation_distributions")]
public class HrVacationDistribution : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string NameEn { get; set; }
    public required string NameAr { get; set; }
    public int DaysCount { get; set; }

    public int SalaryCalculatorId { get; set; }
    [ForeignKey(nameof(SalaryCalculatorId))]
    public HrSalaryCalculator SalaryCalculator { get; set; } = default!;

    public int VacationId { get; set; }
    [ForeignKey(nameof(VacationId))]
    public HrVacation Vacation { get; set; } = default!;
}
