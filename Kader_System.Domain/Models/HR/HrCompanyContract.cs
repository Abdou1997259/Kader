﻿namespace Kader_System.Domain.Models.HR;

[Table("hr_company_contracts")]
public class HrCompanyContract : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public string? CompanyContracts { get; set; }
    public string? CompanyContractsExtension { get; set; }

    public int CompanyId { get; set; }
    [ForeignKey(nameof(CompanyId))]
    public HrCompany Company { get; set; } = default!;
}
