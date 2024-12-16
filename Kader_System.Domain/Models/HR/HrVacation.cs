namespace Kader_System.Domain.Models.HR;

[Table("hr_vacations")]
public class HrVacation : BaseEntity
{
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// تطبق بعد الشهر ...
    /// </summary>
    public int ApplyAfterMonth { get; set; }
    public int TotalBalance { get; set; }
    public bool CanTransfer { get; set; }
    public required string NameEn { get; set; }
    public required string NameAr { get; set; }
    [ForeignKey(nameof(Added_by))]
    public ApplicationUser User { get; set; } = default!;
    public int? VacationTypeId { get; set; }
    [ForeignKey(nameof(VacationTypeId))]
    public HrVacationType VacationType { get; set; } = default!;

    public ICollection<HrVacationDistribution> VacationDistributions { get; set; } = [];


    public ICollection<HrEmployee> Employees { get; set; }
}
