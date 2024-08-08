namespace Kader_System.Domain.DTOs.Response.Setting;

public class StGetMainScreenCategoryByIdResponse
{
    public int Id { get; set; }
    public string Screen_cat_title_en { get; set; } = string.Empty;
    public string Screen_cat_title_ar { get; set; } = string.Empty;
    public int ScreenMainId { get; set; }   
    public string Screen_main_cat_image { get; set; } = string.Empty;
}
