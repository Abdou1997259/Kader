namespace Kader_System.Domain.DTOs.Response.Trans
{
    public class GetTitleByIdResponse
    {
        public int Id { get; set; }
        public string TitleNameAr { get; set; }
        public string TitleNameEn { get; set; }
        public List<GetTitlePermissionResponse>  all_permissions { get; set; }
    }

    public class GetTitlePermissionResponse
    {
        public int sub_id { get; set; }
        public string sub_title { get; set; }
        public int cat_id { get; set; }
        public string cat_title { get; set;}
        public int main_id { get; set; }
        public string main_title { get; set;}
        public string main_image { get; set; }
        public string actions { get; set;}
        public string url { get; set; }
        public PermissionsValue permissions { get; set; }

    }

    public class PermissionsValue
    {
        public bool view { get; set; }
        public bool add { get; set; }
        public bool edit { get; set; }
        public bool delete { get; set; }
        
    }


}
