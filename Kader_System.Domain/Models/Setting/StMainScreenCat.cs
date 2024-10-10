namespace Kader_System.Domain.Models.Setting;

[Table("st_screen_cats")]
public class StScreenCat : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string Screen_cat_title_en { get; set; }
    public required string Screen_cat_title_ar { get; set; }





    public int MainScreenId { get; set; }
    [ForeignKey(nameof(MainScreenId))]
    public StMainScreen ScreenMain { get; set; } = default!;
    public int Order { get; set; }
    public ICollection<StScreenSub> StScreenSub { get; set; } = new HashSet<StScreenSub>();

}
