namespace Kader_System.Domain.DTOs.Request.Setting
{
    public class CreateTitleRequest
    {
        public  string TitleNameAr { get; set; }
        public  string TitleNameEn { get; set; }
        public List<CreateTitlePermissionRequest> Permissions { get; set; }
    }

    public class CreateTitlePermissionRequest
    {
        public int SubScreenId { get; set; }
        public string Permissions { get; set; }
    }

}
