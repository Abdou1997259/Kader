namespace Kader_System.Domain.DTOs.Response.HR;

public class HrGetAllAllowancesResponse : PaginationData<AllowanceData>
{
}
public class AllowanceData : SelectListResponse
{
    public string? AddedByUser { get; set; }
}

