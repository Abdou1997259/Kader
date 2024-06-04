namespace Kader_System.Domain.DTOs.Response.HR
{
    public class HrListOfManagementsResponse
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn{ get; set; }
        public int? ManagerId { get; set; }
        public int? CompanyId { get; set; }
    }
}
