namespace Kader_System.Domain.DTOs.Response.HR
{
    public class GetAllFingerPrintDevicesResponse : PaginationData<FingerPrintDeviceData>
    {
        

    }
    public class FingerPrintDeviceData
    {
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

    }
}
