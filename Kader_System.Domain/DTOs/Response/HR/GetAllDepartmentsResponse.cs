namespace Kader_System.Domain.DTOs.Response.HR
{
    public class GetAllDepartmentsResponse : PaginationData<DepartmentData>
    {

    }
    public class DepartmentData
    {
        public int Id { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public int? ManagerId { get; set; } = null!;
        public string ManagerName { get; set; } = string.Empty;
        public int ManagementId { get; set; }
        public string ManagementName { get; set; } = string.Empty;

    }
}
