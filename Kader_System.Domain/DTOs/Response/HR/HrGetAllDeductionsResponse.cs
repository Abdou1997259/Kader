namespace Kader_System.Domain.DTOs.Response.HR;

public class HrGetAllDeductionsResponse : PaginationData<DeductionData>
{
}
public class DeductionData : SelectListResponse
{
    public string? Added_by { get; set; }

}

