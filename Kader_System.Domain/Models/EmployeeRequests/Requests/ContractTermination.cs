namespace Kader_System.Domain.Models.EmployeeRequests.Requests
{
    [Table("hr_contract_termination")]
    public class HrContractTermination:BaseEntity
    {
        [Key]
        public int Id { get; set; } 
        public string? Notes { get; set; }      
        public string? Attachment { get; set; }
        public string? AttachmentFileName { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public HrEmployee Employee { get; set; }

        public StatuesOfRequest StatuesOfRequest { get; set; }

    }
}
