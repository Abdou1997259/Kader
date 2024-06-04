namespace Kader_System.Domain.Models.Trans;

[Table("Trans_Vacations")]
public class TransVacation : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateOnly StartDate { get; set; }
    public double DaysCount { get; set; }
    public int EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public HrEmployee Employee { get; set; } = default!;

    public int VacationId { get; set; }
    [ForeignKey(nameof(VacationId))]
    public HrVacationDistribution Vacation { get; set; } = default!;
    public string? Notes { get; set; }
    public string? Attachment { get; set; }
    public string? AttachmentExtension { get; set; }
}
