namespace Kader_System.Domain.Models
{
    public class TitlePermission
    {
        [Key]
        public int Id { get; set; }
        public int TitleId { get; set; }
        [ForeignKey(nameof(TitleId))] 
        public Title Title { get; set; } = default!;
        public int SubScreenId { get; set; }
        [ForeignKey(nameof(SubScreenId))]
        public StScreenSub ScreenSub { get; set; } = default!;
        [MaxLength(50)]
        public string Permissions { get; set; } = default!;
    }
}
