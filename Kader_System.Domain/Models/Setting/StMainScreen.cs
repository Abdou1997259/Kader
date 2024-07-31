namespace Kader_System.Domain.Models.Setting;

[Table("st_main_screens")]
public class StMainScreen : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string Screen_main_title_en { get; set; }
    public required string Screen_main_title_ar { get; set; } 
    public IFormFile? Screen_main_image { get; set; }
    public string? ImageExtension { get; set; }
    public string? Url { get; set; }
    public string? Name { get; set; }

    public ICollection<StMainScreenCat> CategoryScreen { get; set; }
    //public StScreenSub subScreen { get; set; }
}
