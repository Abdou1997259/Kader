namespace Kader_System.Domain.DTOs.Response.Setting;

public class StGetAllMainScreensResponse : PaginationData<MainScreenData>
{
}
public class MainScreenData
{
    public int Id { get; set; }
    public required string Screen_main_title { get; set; }
     public string? Screen_main_image { get; set; } = string.Empty;
}

