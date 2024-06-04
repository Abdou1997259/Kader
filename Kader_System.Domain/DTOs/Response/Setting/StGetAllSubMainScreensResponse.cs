namespace Kader_System.Domain.DTOs.Response.Setting;

public class StGetAllSubMainScreensResponse : PaginationData<SubMainScreenData>
{
}
public class SubMainScreenData
{
    public int Sub_id { get; set; }
    public string Sub_title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int Screen_cat_id { get; set; }
    public string Cat_title { get; set; } = string.Empty;
    public int Main_id { get; set; }
    public string Main_title { get; set; } = string.Empty;
    public string Main_image { get; set; } = string.Empty;

}

