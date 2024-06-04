namespace Kader_System.Domain.DTOs.Response.Setting;

public class StSelectListForSubMainScreenResponse
{
    public int Id { get; set; }
    public string Sub_title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int Screen_main_id { get; set; }
}
