using Kader_System.Domain.Models.EmployeeRequests.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Models.EmployeeRequests.PermessionRequests
{
    [Table("hr_delay_permission")]
    public class DelayPermission : BaseEntity
    {
         [Key]
        public int Id { get; set; }
        public string? AtachmentPath { get; set; }
        public string? Notes { get; set; }
        public TimeOnly DelayHours { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public virtual HrEmployee Employee { get; set; }

        public StatuesOfRequest StatuesOfRequest { get; set; }


    }
}
