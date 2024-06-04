namespace Kader_System.Domain.DTOs.Response.HR;

public class HrGetAllQualificationsResponse : PaginationData<QualificationData>
{
}
public class QualificationData : SelectListResponse
{
    public int? EmployeesCount { get; set; }
    public string? AddedByUser { get; set;}
}

