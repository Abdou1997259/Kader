namespace Kader_System.Domain.Models.HR;

[Table("Hr_Companies")]
public class HrCompany : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string NameEn { get; set; }
    public required string NameAr { get; set; }
    public required string CompanyOwner { get; set; }
    public string? Company_licenses { get; set; }
    public string? Company_licenses_extension { get; set; }
    public int CompanyTypeId { get; set; }
    [ForeignKey(nameof(CompanyTypeId))]
    public HrCompanyType CompanyType { get; set; } = default!;

    public ICollection<HrCompanyContract> ListOfsContract { get; set; } = [];
    public ICollection<CompanyLicense> Licenses { get; set; } = [];
}
