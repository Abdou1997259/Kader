namespace Kader_System.Domain.DTOs.Response.Setting;


public class StGetAllMainScreenByIdResponse : PaginationData<StGetMainScreenByIdResponse>
{

}

public class StGetMainScreenByIdResponse
{
    public int Id { get; set; }
    public string Screen_cat_title_en { get; set; } = string.Empty;
    public string Screen_cat_title_ar { get; set; } = string.Empty;
 
    public string Main_title_ar { get; set; } = string.Empty;
    public string Main_title_en { get; set; } = string.Empty;
}
