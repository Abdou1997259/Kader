using Kader_System.Domain.Models.EmployeeRequests.Requests;

namespace Kader_System.Domain.Models.EmployeeRequests.PermessionRequests
{
    [Table("hr_leave_permission_request")]
    public class LeavePermissionRequest : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public TimeOnly LeaveTime { get; set; }
        public TimeOnly? BackTime { get; set; }
        public string? Notes { get; set; }
        public string? AttachmentPath { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]

        public int CompanyId { get; set; }
        public virtual HrEmployee Employee { get; set; } = default!;

        public StatuesOfRequest? StatuesOfRequest { get; set; }

    }
}
