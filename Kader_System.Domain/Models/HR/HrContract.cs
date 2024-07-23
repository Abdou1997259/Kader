namespace Kader_System.Domain.Models.HR;

[Table("hr_contracts")]
public class HrContract : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public double TotalSalary { get; set; }
    public double FixedSalary { get; set; }
    public double HousingAllowance { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public  string FileName { get; set; }
    public  string FileExtension { get; set; }

    public int EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public HrEmployee Employee { get; set; } = default!;

    public ICollection<HrContractAllowancesDetail> ListOfAllowancesDetails { get; set; } = [];
}
