namespace Kader_System.Domain.Models.EmployeeRequests.Requests
{
    [Table("hr_loan_request")]

    public class LoanRequest : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int InstallmentsCount { get; set; }
        public DateOnly StartDate { get; set; }
        public decimal Amount { get; set; }
        public string? AttachmentPath { get; set; }
        public string? Notes { get; set; }
        public int CompanyId { get; set; }

        public int? EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public virtual HrEmployee? Employee { get; set; }

        public StatuesOfRequest? StatuesOfRequest { get; set; }

    }
}
