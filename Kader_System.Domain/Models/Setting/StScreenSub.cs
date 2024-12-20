﻿namespace Kader_System.Domain.Models.Setting;

[Table("st_screens_subs")]
public class StScreenSub : BaseEntity
{

    [Key]
    public int Id { get; set; }
    public required string Screen_sub_title_en { get; set; }
    public required string Screen_sub_title_ar { get; set; }


    public string ScreenCode { get; set; }
    public string? Url { get; set; }


    public int ScreenCatId { get; set; }
    [ForeignKey(nameof(ScreenCatId))]

    public StScreenCat ScreenCat { get; set; } = default!;
    public int Order { get; set; }
    public int incrementalScreenCode { get; set; }
    public ICollection<StSubMainScreenAction> ListOfActions { get; set; } = [];
}
