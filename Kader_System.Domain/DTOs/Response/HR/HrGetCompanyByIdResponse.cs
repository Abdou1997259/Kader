namespace Kader_System.Domain.DTOs.Response.HR;

public class HrGetCompanyByIdResponse
{
    public int Id { get; set; }
    public string Name_en { get; set; } = string.Empty;
    public string Name_ar { get; set; } = string.Empty;
    public string Company_owner { get; set; } = string.Empty;
    public int Company_type { get; set; }
    public string Company_type_name { get; set; }

    public List< CompanyLicenseResponse> Company_licenses { get; set; }
    public List<CompanyContractResponse> Company_contracts { get; set; }

    public int employees_count { get; set; }
    public int managements_count { get; set; }
    public int departments_count { get; set; }
}

public class CompanyContractResponse
{


    public string? Contract { get; set; }
    public int company_contract_id {  get; set; }   
    public string file_name { get; set; }

    public string file_extension { get;set; }
    public DateTime? add_date { get; set; } 
}

public class CompanyLicenseResponse
{

    public string License { get; set; }
     
    public int company_license_id { get; set; }
    public string file_name { get; set; }
    public string file_extension { get;set; }
    public DateTime? add_date { get; set; } 
}

