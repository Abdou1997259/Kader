namespace Kader_System.Domain.Models.Setting
{
    [Table("st_screen_action")]
    public class StScreenAction:BaseEntity
    {
        [Key]
        public int Id { get; set; }


        public int ScreenId { get; set; }
        [ForeignKey(nameof(ScreenId))]
        public StScreenSub Screen { get; set; } = default!;


        public int ActionId { get; set; }
        [ForeignKey(nameof(ActionId))]
        public StAction Action { get; set; } = default!;
    }
}
