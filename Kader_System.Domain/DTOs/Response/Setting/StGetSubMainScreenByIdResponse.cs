namespace Kader_System.Domain.DTOs.Response.Setting;

public class StGetSubMainScreenByIdResponse
{
    public int Id { get; set; }
    public required string Screen_sub_title_en { get; set; }
    public required string Screen_sub_title_ar { get; set; }
    public int Screen_cat_id { get; set; }
    public required string Url { get; set; }
    public required string Name { get; set; }
    public List<ActionsData> Actions { get; set; } = [];
}
public class ActionsData : SpecificSelectListResponse
{
}