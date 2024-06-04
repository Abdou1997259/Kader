namespace Kader_System.Domain.DTOs.Response.HR;

public class HrListOfCompaniesResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Company_licenses { get; set; }
}

