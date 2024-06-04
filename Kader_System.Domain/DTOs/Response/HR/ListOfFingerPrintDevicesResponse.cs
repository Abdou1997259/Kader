namespace Kader_System.Domain.DTOs.Response.HR
{
    public class ListOfFingerPrintDevicesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IPAddress { get; set; }
        public string Port { get; set; }
        public int CompanyId { get; set; }
    }
}
