using Kader_System.Domain.Models.EmployeeRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Models.EmployeeRequests.PermessionRequests
{
    [Table("Hr_LeavePermissionRequest")]
    public class LeavePermissionRequest:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public TimeOnly LeaveTime { get; set; }
        public TimeOnly? BackTime { get; set; }
        public string? Notes { get; set; }
        public string? AttachmentPath { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public virtual HrEmployee Employee { get; set; } = default!;
        public StatuesOfRequest  StatuesOfRequest { get; set; }
    }
}
