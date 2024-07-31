namespace Kader_System.Domain.DTOs.Response.Setting;

public class StSelectListForSubMainScreenResponse : PaginationRequest
{
    public int Id { get; set; }
    public string Sub_title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int Screen_main_cat_id { get; set; }

    public List<StMainScreenCat> List_Screen_main_cat { get; set; }
}
