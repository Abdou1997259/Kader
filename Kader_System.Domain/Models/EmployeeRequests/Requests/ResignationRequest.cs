namespace Kader_System.Domain.Models.EmployeeRequests.Requests
{
    [Table("hr_resignation_request")]
    public class ResignationRequest : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string? AttachmentPath { get; set; }
        public string? Notes { get; set; }
        public int? EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public virtual HrEmployee? Employee { get; set; }

        public StatuesOfRequest StatuesOfRequest { get; set; } = default!;



    }
}
