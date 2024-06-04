namespace Kader_System.Domain.Models.HR;

[Table("Hr_SectionDepartments")]
public class HrSectionDepartment : BaseEntity
{
    [Key]
    public int Id { get; set; }

    public int Section_id { get; set; }
    [ForeignKey(nameof(Section_id))]
    public HrSection Section { get; set; } = default!;

    public int Department_id { get; set; }
    [ForeignKey(nameof(Department_id))]
    public HrDepartment Department { get; set; } = default!;
}
