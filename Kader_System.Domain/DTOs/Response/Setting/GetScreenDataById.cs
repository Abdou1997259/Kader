namespace Kader_System.Domain.DTOs.Response.Setting
{
    public class GetScreenDataById
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string? ParentName { get; set; }
        public int? ParentId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int ScreenType { get; set; }
        public string? Url { get; set; }
        public string? EndPoint { get; set; }
        public string? Icon { get; set; }
        public string? ActiveIcon { get; set; }
        public int Sort { get; set; }
        public List<int> Actions { get; set; } = [];
    }
}
