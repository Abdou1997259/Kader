namespace Kader_System.Domain.DTOs.Response.Setting;

public class StGetAllMainScreensCategoriesResponse : PaginationData<MainScreenCategoryData>
{
}
public class MainScreenCategoryData
{
  
    public int Id { get; set; }
    public int screen_main_id { get; set; }
    public string Screen_main_title { get; set; } = string.Empty;
  
    public string Screen_cat_Title { get; set; } 

    public string? Screen_main_image { get; set; }


}

