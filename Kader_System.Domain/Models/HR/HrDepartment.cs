namespace Kader_System.Domain.Models.HR;

[Table("Hr_Departments")]
public class HrDepartment : BaseEntity
{
    public int Id { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public int? ManagerId { get; set; }
    [ForeignKey(nameof(ManagerId))]
    public HrEmployee Manager { get; set; }
    public int ManagementId { get; set; }
    [ForeignKey(nameof(ManagementId))]
    public HrManagement Management { get; set; }
}
