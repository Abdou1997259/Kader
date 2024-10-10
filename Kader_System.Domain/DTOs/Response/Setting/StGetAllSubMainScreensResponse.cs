namespace Kader_System.Domain.DTOs.Response.Setting;

public class StGetAllSubScreensResponse : PaginationData<SubScreenData>
{
}
public class SubScreenData
{

    public int Ids { get; set; }
    public int? Screen_cat_id { get; set; }
    public required string Screen_sub_title { get; set; }
    public string ScreenMain { get; set; } = string.Empty;
    public string ScreenCat { get; set; } = string.Empty;
    public string? Url { get; set; }
    public string ScreenCode { get; set; } = string.Empty;
    public string? ScreenMainImage { get; set; }





}

