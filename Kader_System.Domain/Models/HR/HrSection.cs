namespace Kader_System.Domain.Models.HR;

[Table("hr_sections")]
public class HrSection : SelectList
{
    public int CompanyId { get; set; }
    [ForeignKey(nameof(CompanyId))]
    public HrCompany Company { get; set; } = default!;

    public ICollection<HrSectionDepartment> ListOfDepartments { get; set; } = [];
}
