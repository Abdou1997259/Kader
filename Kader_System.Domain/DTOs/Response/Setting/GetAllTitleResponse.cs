namespace Kader_System.Domain.DTOs.Response.Setting
{
    public class GetAllTitleResponse : PaginationData<TitleData>
    {
      

    }

    public class TitleData
    {
        public int Id { get; set; }
        public string TitleNameAr { get; set; }
        public string TitleNameEn { get; set; }
        public List<GetAllTitlePermissionResponse> Permissions { get; set; }
    }

    public class GetAllTitlePermissionResponse
    {
        public int Id { get; set; }
        public int SubScreenId { get; set; }
        public string sub_title { get; set; }
        public string actions { get; set; }
        public string url { get; set; }
        public List<int> title_permission { get; set; }
    }
}
