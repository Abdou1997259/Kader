namespace Kader_System.Domain.Models.Setting;

[Table("St_MainScreenCats")]
public class StMainScreenCat : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string Screen_cat_title_en { get; set; }
    public required string Screen_cat_title_ar { get; set; }

    public int MainScreenId { get; set; }
    [ForeignKey(nameof(MainScreenId))]
    public StMainScreen MainScreen { get; set; } = default!;
}
