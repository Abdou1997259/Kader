namespace Kader_System.Domain.DTOs.Response.HR;

public class HrGetAllShiftsResponse : PaginationData<ShiftData>
{
}
public class ShiftData 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TimeOnly Start_shift { get; set; }
    public TimeOnly End_shift { get; set; }

    public int?  EmployeesCount { get; set; }
    public string? AddedByUser { get; set; }
}

