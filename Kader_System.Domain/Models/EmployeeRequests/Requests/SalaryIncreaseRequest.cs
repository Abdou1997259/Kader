namespace Kader_System.Domain.Models.EmployeeRequests.Requests
{
    [Table("Hr_SalaryIncreaseRequest")]

    public class SalaryIncreaseRequest : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public double Amount { get; set; }
        public string? AttachmentPath { get; set; }
        public string? Notes { get; set; }
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public HrEmployee employee { get; set; }

        public StatuesOfRequest? StatuesOfRequest { get; set; }


    }
}
