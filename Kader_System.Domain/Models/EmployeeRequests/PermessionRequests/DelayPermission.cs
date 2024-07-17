using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Models.EmployeeRequests.PermessionRequests
{
    [Table("Hr_DelayPermission")]
    public class DelayPermission : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public double  HoursDelay { get; set; }
        public string? AtachmentPath { get; set; }
        public string? Notes { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public HrEmployee employee { get; set; } = default!;
    }
}
