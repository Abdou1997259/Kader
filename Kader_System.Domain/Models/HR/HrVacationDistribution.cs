namespace Kader_System.Domain.Models.HR;

[Table("Hr_VacationDistributions")]
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
