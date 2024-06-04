namespace Kader_System.Domain.Models.Setting;

[Table("St_MainScreens")]
public class StMainScreen : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string Screen_main_title_en { get; set; }
    public required string Screen_main_title_ar { get; set; } 
    public string? Screen_main_image { get; set; }
    public string? ImageExtension { get; set; }
}
