namespace Kader_System.Domain.Models
{
    public class Title:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public required string TitleNameAr { get; set; }
        public required string TitleNameEn { get; set; }
        public  List<TitlePermission> TitlePermissions { get; set; }

    }
}
