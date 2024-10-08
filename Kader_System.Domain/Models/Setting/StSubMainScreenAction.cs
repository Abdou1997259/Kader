﻿namespace Kader_System.Domain.Models.Setting;

[Table("st_sub_main_screen_actions")]
public class StSubMainScreenAction : BaseEntity
{
    [Key]
    public int Id { get; set; }

    public int ScreenSubId { get; set; }
   
    public StScreenSub ScreenSub { get; set; } = default!;

    public int ActionId { get; set; }
    [ForeignKey(nameof(ActionId))]
    public StAction Action { get; set; } = default!;
}
