namespace Kader_System.Domain.DTOs.Response.HR
{
    public class ListOfDepartmentsResponse
    {
        public int Id { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public int? ManagerId { get; set; } = null!;
        public string ManagerNameAr { get; set; } = string.Empty;
        public int ManagementId { get; set; }
        public string ManagementName { get; set; } = string.Empty;
    }
}
