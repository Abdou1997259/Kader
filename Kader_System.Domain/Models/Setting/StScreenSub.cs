namespace Kader_System.Domain.Models.Setting;

[Table("St_ScreensSubs")]
public class StScreenSub : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public required string Screen_sub_title_en { get; set; } 
    public required string Screen_sub_title_ar { get; set; } 
    public required string Url { get; set; } 
    public required string Name { get; set; }
    
    public int ScreenCatId { get; set; }
    [ForeignKey(nameof(ScreenCatId))]
    public StMainScreenCat ScreenCat { get; set; } = default!;

    public ICollection<StSubMainScreenAction> ListOfActions { get; set; } = [];
}
