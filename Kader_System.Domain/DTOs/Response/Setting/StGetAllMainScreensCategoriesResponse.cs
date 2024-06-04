namespace Kader_System.Domain.DTOs.Response.Setting;

public class StGetAllMainScreensCategoriesResponse : PaginationData<MainScreenCategoryData>
{
}
public class MainScreenCategoryData
{
    public int Id { get; set; }
    public string Screen_main_title { get; set; } = string.Empty;
    public string? Screen_main_image { get; set; }
}

