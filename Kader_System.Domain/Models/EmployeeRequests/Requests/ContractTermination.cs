namespace Kader_System.Domain.Models.EmployeeRequests.Requests
{
    [Table("hr_contract_termination")]
    public class HrContractTermination:BaseEntity
    {
        [Key]
        public int Id { get; set; } 
        public string? Notes { get; set; }      
        public string? Attachment { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public HrEmployee Employee { get; set; }
        public int Status { get; set; }
        public string? StatusMessage { get; set; }
        public int? ApporvalStatus { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int ApprovedBy { get; set; }
    }
}
