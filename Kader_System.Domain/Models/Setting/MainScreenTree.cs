
namespace Kader_System.Domain.Models.Setting
{

    public class MainScreenTree : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public required string ScreenTitleEn { get; set; }
        public required string ScreenTitleAr { get; set; }
        public required string Url { get; set; }
        public required string Name { get; set; }
        public ICollection<StSubMainScreenAction> ListOfActions { get; set; } = [];
        public int? ParentId { get; set; }
        public MainScreenTree Parent { get; set; }
        public ICollection<MainScreenTree> Children { get; set; } = new List<MainScreenTree>();



    }
}
