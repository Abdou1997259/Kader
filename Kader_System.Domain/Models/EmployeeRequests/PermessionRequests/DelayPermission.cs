using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.Domain.Models.EmployeeRequests.PermessionRequests
{
    [Table("hr_delay_permission")]
    public class DelayPermission : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string? AttachmentPath { get; set; }
        public string? Notes { get; set; }
        public int? DelayHours { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public virtual HrEmployee Employee { get; set; }
        public int CompanyId { get; set; }
        public StatuesOfRequest StatuesOfRequest { get; set; }


    }
}
