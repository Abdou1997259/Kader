
namespace Kader_System.Domain.Models.Setting
{
    public class Screen:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public Screen ParentScreen { get; set; } = default!;
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string? Url { get; set; }
        public string? EndPoint { get; set; }
        public string? Icon { get; set; }
        public string? ActiveIcon { get; set; }
        public int Sort { get; set; }
        public int ScreenType { get; set; }
        public ICollection<StScreenAction> Actions { get; set; } = [];
    }
}
