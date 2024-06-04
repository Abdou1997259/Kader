namespace Kader_System.Domain.DTOs.Response.HR;

public class HrGetCompanyByIdResponse
{
    public int Id { get; set; }
    public string Name_en { get; set; } = string.Empty;
    public string Name_ar { get; set; } = string.Empty;
    public string Company_owner { get; set; } = string.Empty;
    public int Company_type { get; set; }
    public string Company_type_name { get; set; }

    public List<CompanyContractResponse> Company_licenses { get; set; }
    public List<CompanyLicenseResponse> Company_contracts { get; set; }

    public int employees_count { get; set; }
    public int managements_count { get; set; }
    public int departments_count { get; set; }
}

public class CompanyContractResponse
{

    public int Id { get; set; }
    public string? Contract { get; set; }
}

public class CompanyLicenseResponse
{
    public int Id { get; set; }
    public string License { get; set; }
}

