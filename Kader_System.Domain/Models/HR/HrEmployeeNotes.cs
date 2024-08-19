using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Models.HR
{
    [Table("hr_employee_notes")]
    public class HrEmployeeNotes:BaseEntity
    {
        public int Id { get; set; }
        public string Notes { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public HrEmployee Employee { get; set; } = default!;
    }
}
