namespace Kader_System.Domain.Models.Setting;

[Table("st_main_screen_cats")]
public class StMainScreenCat : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string Screen_cat_title_en { get; set; }
    public required string Screen_cat_title_ar { get; set; }

    public string? Screen_main_cat_image { get; set; }
    public string? ImageExtension { get; set; }

    public int MainScreenId { get; set; }
    [ForeignKey(nameof(MainScreenId))]
    public StMainScreen screenCat { get; set; } = default!;

    public ICollection<StScreenSub> StScreenSub { get; set; }
    //public StMainScreenCat stMainScreenCat { get; set; }
}
