namespace Kader_System.Domain.Models.EmployeeRequests
{
    [Table("hr_employee_request")]
    public class HrEmployeeRequests:BaseEntity
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int RequestType { get; set; }
        public DateTime RequestDate { get; set; }
        public int Status { get; set; }
        public int? StatusBy { get; set; }
    }
}
