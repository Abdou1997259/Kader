namespace Kader_System.Domain.DTOs.Request.HR
{
    public class HrGetJobByIdResponse
    {
        public int Id { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public bool HasNeedLicense { get; set; }
        public bool HasAdditionalTime { get; set; }
        public int EmployeesCount { get; set; }
    }
}
