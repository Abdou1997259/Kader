namespace Kader_System.Domain.DTOs.Response.HR;

public class HrGetAllCompaniesResponse : PaginationData<CompanyData>
{
}
public class CompanyData
{
    public int Id { get; set; }
    public string? Added_by { get; set; }
    public string Add_date { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Company_type_name { get; set; } = string.Empty;
    public string Company_owner { get; set; } = string.Empty;
    public int Employees_count { get; set; }
}

