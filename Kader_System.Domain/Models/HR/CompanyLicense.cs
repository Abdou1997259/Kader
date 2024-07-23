namespace Kader_System.Domain.Models.HR
{
    [Table("hr_company_licenses")]
    public class CompanyLicense:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string LicenseName { get; set; }
        public string LicenseExtension { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public HrCompany Company { get; set; } = default!;
    }
}
