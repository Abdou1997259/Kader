namespace Kader_System.Domain.DTOs.Response.HR
{
    public class GetAllManagementsResponse : PaginationData<ManagementData>
    {
    }
    public class ManagementData
    {
        public int Id { get; set; }
        public string NameEn { get; set; }

        public string NameAr { get; set; }
        public int? ManagerId { get; set; }
        public string ManagerName { get; set; } = string.Empty;
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
    }
}
