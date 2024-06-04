namespace Kader_System.Domain.DTOs.Response.Setting;

public class StGetMainScreenByIdResponse
{
    public int Id { get; set; }
    public string Screen_cat_title_en { get; set; } = string.Empty;
    public string Screen_cat_title_ar { get; set; } = string.Empty;
    public int Screen_main_id { get; set; }
    public int Screen_cat_id { get; set; }

    public string Main_title_ar { get; set; } = string.Empty;
    public string Main_title_en { get; set; } = string.Empty;
}
