namespace Kader_System.Domain.DTOs.Response.HR;

public class HrGetShiftByIdResponse
{
    public int Id { get; set; }
    public string Name_en { get; set; } = string.Empty;
    public string Name_ar { get; set; } = string.Empty;

    public TimeOnly Start_shift { get; set; }
    public TimeOnly End_shift { get; set; }
}

